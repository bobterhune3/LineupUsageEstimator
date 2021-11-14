using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using somReporter;
using somReporter.team;
using somReportUtils;
using static LineupEngine.LineupEngine;
using LineupEngine;
using LIneupUsageEstimator.ui;

namespace LIneupUsageEstimator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TeamBatterInfo batterInfo;
        private BalanceUsageStats balanceUsage;

        public static DependencyProperty dp;
        public static DependencyProperty dpPos;

        LineupEngine.LineupEngine engine = new LineupEngine.LineupEngine();
        private Boolean dialogInitialized = false;
        private Team currentlySelectedTeam = null;
        private Boolean inResetMode = false;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            TeamUtils.registerTeamAbvMapping( new Config().getTeamAbrvMapping());

            dp = DependencyProperty.Register("LineupInfo", typeof(object), typeof(LineupDataObj), new UIPropertyMetadata());
            dpPos = DependencyProperty.Register("Positions", typeof(object), typeof(PositionObj), new UIPropertyMetadata());

            engine.initialize(Config.getConfigurationFilePath("rosterReport.PRT"));

            CB_LIST_OF_TEAMS.Items.Add("SELECT ONE");
            foreach (Team team in engine.CompleteListOfTeams)
                CB_LIST_OF_TEAMS.Items.Add(team);
            CB_LIST_OF_TEAMS.SelectedIndex = 0;


            if (engine.TeamLineupData.hasEmptyData())
            {
                MessageBox.Show("It appears this is the first time you have run the program. Team division assignments are required in order to estimate the number of games played vs each team.  On the next screen please assign each team a division.",
                    "Team division assignments are required", MessageBoxButton.OK, MessageBoxImage.Information);
                OpponentsDlg dlg = new OpponentsDlg(engine.TeamLineupData, engine.CompleteListOfTeams);
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    this.Close();
                }
                engine.saveDatabase();
            }

            applyDivisionToTeams();

            GRID.Background = new SolidColorBrush(Colors.LightSteelBlue);
            GRID.ShowGridLines = true;

            dialogInitialized = true;

            batterInfo = new TeamBatterInfo(GRID_INFO, GRID);
            balanceUsage = new BalanceUsageStats(GRID_USAGE_STATS);
        }

        private void applyDivisionToTeams()
        {
            foreach( Team teamDB in engine.TeamLineupData.Team)
            {
                foreach( Team teamFile in engine.CompleteListOfTeams)
                {
                    if (teamDB.Abrv.Equals(teamFile.Abrv))
                        teamFile.Division = teamDB.Division;
                }

            }
        }


        private void updateWorkbook(Team team, List<Player> players)
        {
            if (GRID.ColumnDefinitions.Count > 0) { 
              GRID.ColumnDefinitions.RemoveRange(0, GRID.ColumnDefinitions.Count);
              GRID.Children.RemoveRange(0, GRID.Children.Count);
            }

            buildWorksheetLabelColumn();

            TeamLineup lineups = engine.StoredLineups[team.Abrv];
            int index = 1;
            int box = 1;
            foreach (LineupData lineupData in lineups.Lineups)
            {
                LineupDataObj lineup = new LineupDataObj(lineupData);

                int pitcherArmIndex = lineup.PitcherArm.Equals("L") ? 0 : 1;
                lineup.EstimatedAtBats = calculateAtBatsByLineup(engine.BalanceAtBats[pitcherArmIndex], lineup.getLineupData());

                GRID.Children.Add(BuildPlayerPostitionBox(index, POSITIONS.CATCHER, lineup, players, box++));
                GRID.Children.Add(BuildPlayerPostitionBox(index, POSITIONS.FIRSTBASE, lineup, players, box++));
                GRID.Children.Add(BuildPlayerPostitionBox(index, POSITIONS.SECONDBASE, lineup, players, box++));
                GRID.Children.Add(BuildPlayerPostitionBox(index, POSITIONS.THIRDBASE, lineup, players, box++));
                GRID.Children.Add(BuildPlayerPostitionBox(index, POSITIONS.SHORTSTOP, lineup, players, box++));
                GRID.Children.Add(BuildPlayerPostitionBox(index, POSITIONS.LEFTFIELD, lineup, players, box++));
                GRID.Children.Add(BuildPlayerPostitionBox(index, POSITIONS.CENTERFIELD, lineup, players, box++));
                GRID.Children.Add(BuildPlayerPostitionBox(index, POSITIONS.RIGHTFIELD, lineup, players, box++));
                GRID.Children.Add(BuildPlayerPostitionBox(index, POSITIONS.DH, lineup, players, box++));

                createColumn(lineup, index, 160);

                index++;
            }

            GRID.UpdateLayout();
        }

        private void buildWorksheetLabelColumn()
        {
            createColumn("POS", 0, 50);

            GRID.Children.Add(BuildPositionTextBox("C", POSITIONS.CATCHER));
            GRID.Children.Add(BuildPositionTextBox("1B", POSITIONS.FIRSTBASE));
            GRID.Children.Add(BuildPositionTextBox("2B", POSITIONS.SECONDBASE));
            GRID.Children.Add(BuildPositionTextBox("3B", POSITIONS.THIRDBASE));
            GRID.Children.Add(BuildPositionTextBox("SS", POSITIONS.SHORTSTOP));
            GRID.Children.Add(BuildPositionTextBox("LF", POSITIONS.LEFTFIELD));
            GRID.Children.Add(BuildPositionTextBox("CF", POSITIONS.CENTERFIELD));
            GRID.Children.Add(BuildPositionTextBox("RF", POSITIONS.RIGHTFIELD));
            GRID.Children.Add(BuildPositionTextBox("DH", POSITIONS.DH));
        }

        private void BTN_CLOSE_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lineup_player_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if(cb.SelectedItem != null)
            {
                LineupDataObj lineup = (LineupDataObj)cb.GetValue(MainWindow.dp);
                PositionObj position = (PositionObj)cb.GetValue(MainWindow.dpPos);

                Team team = (Team)this.CB_LIST_OF_TEAMS.SelectedItem;
                List<Player> players = engine.TeamReportFile.getTeamBatters(team);
                batterInfo.setPlayers(players);
                return;
            }
                
        }

        private List<Player> lookupAssigned(int index)
        {
            List<Player> playersInLineup = new List<Player>();
            for (int posIndex = index; posIndex <= index + 9; posIndex++) { 

                if (GRID.Children[posIndex] is ComboBox)
                {
                    ComboBox box = (ComboBox)GRID.Children[posIndex];
                    Object value = (Object)box.SelectedValue;
                    if (value is DefenseComboBoxItem)
                    {
                        Player player = ((DefenseComboBoxItem)value).Value;
                        if (player.Id == 0)
                            player.Id = RecordIndex.getNextId(RecordIndex.INDEX.PlayerId);
                        playersInLineup.Add(player);
                    }
                    else
                    {
                        playersInLineup.Add(new Player(true));
                    }
                }
            }
            return playersInLineup;
        }

        private void syncUpTheData(Dictionary<String, TeamLineup> storedLineups)
        {
            
            for(int i=0; i<GRID.Children.Count;i++)	
            {	
                Object box = (Object)GRID.Children[i];	
                if (box is ComboBox)	
                {	
                    Object value = (Object)((ComboBox)box).SelectedValue;	
                    if (value is DefenseComboBoxItem)	
                    {	
                        System.Console.WriteLine("*" + i + ", " + ((Player)((DefenseComboBoxItem)value).Value).Name +
                            "(" + ((Player)((DefenseComboBoxItem)value).Value).Id + ")"
                            );	
                    }	
                    else	
                    {	
                        System.Console.WriteLine("*" + i);	
                    }	
                }	
                else	
                {	
                    System.Console.WriteLine("LINEUP :" + ((Label)box).ToString());	
                }	

            }	

            /* - This is commented out because a file is saved and I want to try to load it */
            if (GRID.Children.Count > 0 && currentlySelectedTeam != null)
            {
                int numberOfLineups = GRID.ColumnDefinitions.Count;
                List<Player> playersByGrid = new List<Player>();
                int firstItemIndex = 10;
                for (int col = 1; col < numberOfLineups; col++)
                {
                    playersByGrid.AddRange(lookupAssigned(firstItemIndex));
                    firstItemIndex += 10;
                }

                Team team = currentlySelectedTeam;
                storedLineups[team.Abrv].playerByGRID = playersByGrid;
            }
            
            return; 
        }

        private void fillBoxesWithSavedDataData()
        {
            if (GRID.Children.Count > 0 && currentlySelectedTeam != null)
            {

                Team team = currentlySelectedTeam;
                List<Player> playersByGrid = engine.StoredLineups[team.Abrv].playerByGRID;
                if (playersByGrid == null)
                    return;

                int numberOfLineups = GRID.ColumnDefinitions.Count;
                int pos = 0;
                int firstItemIndex = 10;// numberOfLineups + 2;
                for (int col =1; col < numberOfLineups; col++)
                {
                    for (int posIndex = firstItemIndex; posIndex < firstItemIndex + 10; posIndex++)
                    {
                        if (pos >= playersByGrid.Count)
                            break;
                        Object box = GRID.Children[posIndex];
                        if (box is ComboBox)
                        {
                            Player player = playersByGrid[pos++];
                            if (player != null)
                            {
                                foreach (Object item in ((ComboBox)box).Items)
                                {
                                    if (item is DefenseComboBoxItem)
                                    {
                                        if (((DefenseComboBoxItem)item).Value.Name.Equals(player.Name))
                                        {
                                            ((ComboBox)box).SelectedItem = item;
                                            System.Console.WriteLine("Assinging Player to "+ team.Abrv+": " + col + ":" +pos+", "+ player.Name +
                                                                                      "(" + player.Id + ")");
                                            break;
                                        }

                                    }
                                }
                            }
                        }
                    }
                    firstItemIndex += 10;
                }
            }
        }

        private void CB_LIST_OF_TEAMS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dialogInitialized)
            {
                if (CB_LIST_OF_TEAMS.SelectedItem is Team)
                {
                    currentlySelectedTeam = (Team)CB_LIST_OF_TEAMS.SelectedItem;

                    TeamLineup selectedTeamLineup = LineupPersistence.lookupTeamLineup(engine.StoredLineups, currentlySelectedTeam);
                    System.Console.WriteLine(currentlySelectedTeam.Abrv + " contains " + selectedTeamLineup.Lineups.Count + " lineups.");

                    List<Player> players = engine.TeamReportFile.getTeamBatters(currentlySelectedTeam);
                    batterInfo.setPlayers(players);
                    IUsageCalculator calculator = CalculatorFactory.getCalculator(USAGE_CALCULATOR, engine.TeamReportFile, currentlySelectedTeam);
                    calculator.setOptions(CalculatorOptions.OPTION_IN_DIVISION_GAMES, engine.TeamLineupData.InDivisionGameCount);
                    calculator.setOptions(CalculatorOptions.OPTION_OUT_DIVISION_GAMES, engine.TeamLineupData.OutofDivisionGameCount);
                    //TODO: Add UI element for Target At Bats
                    calculator.setOptions(CalculatorOptions.OPTION_TARGET_AT_BAT, 615);

                    engine.BalanceAtBats = balanceUsage.buildTable(calculator);

                    updateWorkbook(currentlySelectedTeam, players);

                    if (this.inResetMode == true)
                        this.inResetMode = false;
                    else
                        fillBoxesWithSavedDataData();

                    BTN_MANAGE_LINEUPS.IsEnabled = true;
                }
            }
        }

        private int calculateAtBatsByLineup(Dictionary<int, int> stats, LineupData lineup)
        {
            if (stats.Count == 0) return 0;

            int fromLevel = lineup.BalanceItemFrom.Value;
            int toLevel = lineup.BalanceItemTo.Value;
            int totalAtBats = 0;

            for( int i= fromLevel; i<= toLevel; i++)
            {
                totalAtBats += stats[i];
            }
            
            return totalAtBats;
        }

        private Label BuildColumnTextBox(String title, int lineup)
        {
            Label label = new Label();
            label.Content = title;
            label.FontSize = 14;
            label.FontWeight = FontWeights.Bold;
            label.Foreground = new SolidColorBrush(Colors.Red);
            label.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(label, 0);
            Grid.SetColumn(label, lineup);
            return label;
        }

        private Label BuildPositionTextBox(String position, POSITIONS row)
        {
            Label label = new Label();
            label.Content = position;
            label.FontSize = 14;
            label.FontWeight = FontWeights.Bold;
            label.Foreground = new SolidColorBrush(Colors.Green);
            label.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(label, (int)row);
            Grid.SetColumn(label, 0);
            return label;
        }

        private ComboBox BuildPlayerPostitionBox( int col, POSITIONS postion, LineupDataObj teamLineup, List<Player> players, int index)
        {
            ComboBox playerBox = new ComboBox();
            playerBox.Items.Add("NOT SET");
//            playerBox.Items.Add(index.ToString());
            
            playerBox.FontSize = 12;
            playerBox.FontWeight = FontWeights.Normal;
            playerBox.Foreground = new SolidColorBrush(Colors.Green);
            playerBox.VerticalAlignment = VerticalAlignment.Top;
            playerBox.SelectedIndex = 0;
            playerBox.SelectionChanged += lineup_player_SelectionChanged;
            playerBox.SetValue(dp, teamLineup);
            playerBox.SetValue(dpPos, new PositionObj(postion));

            addPlayersByPosition(playerBox, players, postion, teamLineup.EstimatedAtBats);

            Grid.SetRow(playerBox, (int)postion);
            Grid.SetColumn(playerBox, col);
            return playerBox;
        }

        private void createColumn(LineupDataObj lineup, int columnIndex, double width)
        {
            String text = lineup.ToString() + " (" + lineup.EstimatedAtBats + ")";
            ColumnDefinition col = createColumn(text, columnIndex, width);
            col.SetValue(dp, lineup);

        }

        private ColumnDefinition createColumn(String text, int columnIndex, double width)
        {
            ColumnDefinition colLabels = new ColumnDefinition();
            colLabels.Width = new GridLength(width);
            GRID.ColumnDefinitions.Add(colLabels);
            Label column = BuildColumnTextBox(text, columnIndex);
            GRID.Children.Add(column);
            return colLabels;
        }

        private String buildBatterInfo(Player player)
        {
            String Name = player.Name;
            int AtBats = player.Actual;
            String balance = player.Bal;
            String powerL = player.PowerL;
            String powerR = player.PowerR;
            String hits = player.Throws;
            Defense playerDef = player.Def;

            String text = String.Format("{0} {1} {2} {3} {4}{5}", Name, AtBats, hits, balance, powerL, powerR);
            return text;
        }

        private void addPlayersByPosition(ComboBox box, List<Player> players, POSITIONS position, int estimatedAB)
        {
            foreach( Player player in players)
            {
                String defRating = playsPosition(player, position);
                if (defRating.Length > 0 )
                {
                    DefenseComboBoxItem item = new DefenseComboBoxItem();
                    item.Value = player;
                    item.Text = defRating;
                    box.Items.Add(item);
                }
            }
        }

        private String playsPosition(Player player, POSITIONS position )
        {
            switch( position )
            {
                case POSITIONS.CATCHER:
                    return player.Def.CatcherRating;
                case POSITIONS.FIRSTBASE:
                    return player.Def.FirstBaseRating;
                case POSITIONS.SECONDBASE:
                    return player.Def.SecondBaseRating;
                case POSITIONS.THIRDBASE:
                    return player.Def.ThirdBaseRating;
                case POSITIONS.SHORTSTOP:
                    return player.Def.ShortstopRating;
                case POSITIONS.LEFTFIELD:
                    return player.Def.LeftFieldRating;
                case POSITIONS.CENTERFIELD:
                    return player.Def.CenterFieldRating;
                case POSITIONS.RIGHTFIELD:
                    return player.Def.RightFieldRating;
                default:
                    return "*";
            }
        }

        private void BTN_MANAGE_LINEUPS_Click(object sender, RoutedEventArgs e)
        {
            Object item = CB_LIST_OF_TEAMS.SelectedItem;
            if (item is Team)
            {
                Team team = (Team)item;

                LineupListDlg dlg = new LineupListDlg(team, engine.StoredLineups[team.Abrv]);
                System.Windows.Forms.DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && dlg.ApplyAsTemplate)
                {
                    List<Team> teams = engine.TeamReportFile.getTeams();
                    foreach (Team otherTeam in teams)
                    {
                        if (team == otherTeam)
                            continue;

                        TeamLineup otherTeamLineup = LineupPersistence.lookupTeamLineup(engine.StoredLineups, otherTeam);
                        dlg.applyConfigurationToAnotherTeam(otherTeamLineup);
                        syncUpTheData(engine.StoredLineups);
                        LineupPersistence.saveDatabase(engine.StoredLineups);
                    }
                    //Update the table!
                    CB_LIST_OF_TEAMS_SelectionChanged(this, null);
                }
                else if(result == System.Windows.Forms.DialogResult.OK)
                {
                    //Update the table!
                    CB_LIST_OF_TEAMS_SelectionChanged(this, null);
                }
            }
        }

        private void BTN_OPPONENTS_Click(object sender, RoutedEventArgs e)
        {
            OpponentsDlg dlg = new OpponentsDlg(engine.TeamLineupData, engine.CompleteListOfTeams);
            if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TeamInformation.saveDatabase(engine.TeamLineupData);
            }
        }

        private void LineupUsageCalculator_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            syncUpTheData(engine.StoredLineups);
            LineupPersistence.saveDatabase(engine.StoredLineups);
        }

        private void BTN_SETTINGS_Click(object sender, RoutedEventArgs e)
        {
            SettingsDlg dlg = new SettingsDlg();
            if( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CB_LIST_OF_TEAMS_SelectionChanged(this, null);
            }

        }

        private void BTN_LINEUP_DEPTH_VIEW_OnCLick(object sender, RoutedEventArgs e)
        {
            List<Player> players = engine.TeamReportFile.getTeamBatters(currentlySelectedTeam.Abrv);
            PositionDepthDlg dlg = new PositionDepthDlg(players);
            dlg.Show();

        }

        private void BTN_RESET_LINEUP_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to set all lineup positions to NOT SET?  Restart application to clear all stats", "Reset teams linup?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                inResetMode = true;
                CB_LIST_OF_TEAMS_SelectionChanged(null, null);
            }
        }
    }


    class DefenseComboBoxItem
    {
        public string Text { get; set; }
        public Player Value { get; set; }
        public bool Duplicate { get; set; }

        public override string ToString()
        {
            if (Duplicate)
            {
                return String.Format("DUPE: {0} {1} ({2})\r\n{3} {4}{5} : {6}",
                    Value.Name,
                    Value.Throws,
                    Text,
                    Value.Bal,
                    Value.PowerL, Value.PowerR,
                    Value.Actual);
            }
            else
            {
                return String.Format("{0} {1} ({2})\r\n{3} {4}{5} : {6}",
                    Value.Name,
                    Value.Throws,
                    Text,
                    Value.Bal,
                    Value.PowerL, Value.PowerR,
                    Value.Actual);
            }
        }
    }

    class PositionObj : DependencyObject
    {
        public POSITIONS Position { get; }

        public PositionObj(POSITIONS p)
        {
            Position = p;
        }
        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
