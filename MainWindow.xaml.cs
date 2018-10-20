using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using somReporter;
using somReporter.util.somUsageAnalysis;
using somReporter.team;
using System.Data;


namespace LIneupUsageEstimator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SOMTeamReportFile teamReportFile;
        public enum POSITIONS { CATCHER = 1, FIRSTBASE, SECONDBASE, THIRDBASE, SHORTSTOP, LEFTFIELD, CENTERFIELD, RIGHTFIELD, DH };
        private Boolean dialogInitialized = false;
        private TeamBatterInfo batterInfo;
        private BalanceUsageStats balanceUsage;
        // 0=Righties, 1=Lefties, Map is balance ("9L) and Projected At Bats
        private List<Dictionary<int, int>> balanceAtBats = new List<Dictionary<int, int>>();

        Dictionary<POSITIONS, ComboBox> lhControl = new Dictionary<POSITIONS, ComboBox>();

        private Label lineupLH_label;
        private Label lineupRH_label;

        public static DependencyProperty dp;
        public static DependencyProperty dpPos;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            dp = DependencyProperty.Register("LineupInfo", typeof(object), typeof(Lineup), new UIPropertyMetadata());
            dpPos = DependencyProperty.Register("Positions", typeof(object), typeof(PositionObj), new UIPropertyMetadata());


            teamReportFile = new SOMTeamReportFile(Config.getConfigurationFile("rosterReport.PRT"));
            teamReportFile.parse();

            List<Team> teams = teamReportFile.getTeams();
            CB_LIST_OF_TEAMS.Items.Add("SELECT ONE");
            foreach (Team team in teams)
                CB_LIST_OF_TEAMS.Items.Add(team);
            CB_LIST_OF_TEAMS.SelectedIndex = 0;

            GRID.Background = new SolidColorBrush(Colors.LightSteelBlue);
            GRID.ShowGridLines = true;

            createColumn("POS", 0, 50);
            lineupLH_label = createColumn("LH", 1, 200);
            lineupRH_label = createColumn("RH", 2, 200);


            GRID.Children.Add(BuildPositionTextBox("C", POSITIONS.CATCHER));
            GRID.Children.Add(BuildPositionTextBox("1B", POSITIONS.FIRSTBASE));
            GRID.Children.Add(BuildPositionTextBox("2B", POSITIONS.SECONDBASE));
            GRID.Children.Add(BuildPositionTextBox("3B", POSITIONS.THIRDBASE));
            GRID.Children.Add(BuildPositionTextBox("SS", POSITIONS.SHORTSTOP));
            GRID.Children.Add(BuildPositionTextBox("LF", POSITIONS.LEFTFIELD));
            GRID.Children.Add(BuildPositionTextBox("CF", POSITIONS.CENTERFIELD));
            GRID.Children.Add(BuildPositionTextBox("RF", POSITIONS.RIGHTFIELD));
            GRID.Children.Add(BuildPositionTextBox("DH", POSITIONS.DH));


            Lineup lineupLH = new Lineup();
            lineupLH.PitcherArm = "L";
            lineupLH.BalanceItemFrom = new LineupBalanceItem(0, 9, "L");
            lineupLH.BalanceItemTo = new LineupBalanceItem(18, 9, "R");





            GRID.Children.Add(BuildPlayerPostitionBox( 1, POSITIONS.CATCHER, lineupLH));
            GRID.Children.Add(BuildPlayerPostitionBox( 1, POSITIONS.FIRSTBASE, lineupLH));
            GRID.Children.Add(BuildPlayerPostitionBox( 1, POSITIONS.SECONDBASE, lineupLH));
            GRID.Children.Add(BuildPlayerPostitionBox( 1, POSITIONS.THIRDBASE, lineupLH));
            GRID.Children.Add(BuildPlayerPostitionBox( 1, POSITIONS.SHORTSTOP, lineupLH));
            GRID.Children.Add(BuildPlayerPostitionBox( 1, POSITIONS.LEFTFIELD, lineupLH));
            GRID.Children.Add(BuildPlayerPostitionBox( 1, POSITIONS.CENTERFIELD, lineupLH));
            GRID.Children.Add(BuildPlayerPostitionBox( 1, POSITIONS.RIGHTFIELD, lineupLH));
            GRID.Children.Add(BuildPlayerPostitionBox( 1, POSITIONS.DH, lineupLH));

            Lineup lineupRH = new Lineup();
            lineupRH.PitcherArm = "R";
            lineupRH.BalanceItemFrom = new LineupBalanceItem(0, 9, "L");
            lineupRH.BalanceItemTo = new LineupBalanceItem(18, 9, "R");
            GRID.Children.Add(BuildPlayerPostitionBox( 2, POSITIONS.CATCHER, lineupRH));
            GRID.Children.Add(BuildPlayerPostitionBox( 2, POSITIONS.FIRSTBASE, lineupRH));
            GRID.Children.Add(BuildPlayerPostitionBox( 2, POSITIONS.SECONDBASE, lineupRH));
            GRID.Children.Add(BuildPlayerPostitionBox( 2, POSITIONS.THIRDBASE, lineupRH));
            GRID.Children.Add(BuildPlayerPostitionBox( 2, POSITIONS.SHORTSTOP, lineupRH));
            GRID.Children.Add(BuildPlayerPostitionBox( 2, POSITIONS.LEFTFIELD, lineupRH));
            GRID.Children.Add(BuildPlayerPostitionBox( 2, POSITIONS.CENTERFIELD, lineupRH));
            GRID.Children.Add(BuildPlayerPostitionBox( 2, POSITIONS.RIGHTFIELD, lineupRH));
            GRID.Children.Add(BuildPlayerPostitionBox(2, POSITIONS.DH, lineupRH));

            dialogInitialized = true;

            if (balanceAtBats.Count > 0)
            {
                int lhAB = calculateAtBatsByLineup(balanceAtBats[0], lineupLH);
                lineupLH.EstimatedAtBats = lhAB;
                int rhAB = calculateAtBatsByLineup(balanceAtBats[1], lineupRH);
                lineupRH.EstimatedAtBats = rhAB;
            }

            batterInfo = new TeamBatterInfo(GRID_INFO, GRID);
            balanceUsage = new BalanceUsageStats(GRID_USAGE_STATS);

    //TODO        BTN_MANAGE_LINEUPS.IsEnabled = false;
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
                Lineup lineup = (Lineup)cb.GetValue(MainWindow.dp);
                PositionObj position = (PositionObj)cb.GetValue(MainWindow.dpPos);
                DefenseComboBoxItem selectedPlayer = (DefenseComboBoxItem) cb.SelectedItem;

                Team team = (Team)this.CB_LIST_OF_TEAMS.SelectedItem;
                List<Player> players = teamReportFile.getTeamBatters(team);
                batterInfo.setPlayers(players);
                return;
            }
                
        }

        private void CB_LIST_OF_TEAMS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dialogInitialized) { 
                Object item = CB_LIST_OF_TEAMS.SelectedItem;
                if (item is Team)
                {
                    Team team = (Team)item;
                    List<Player> players = teamReportFile.getTeamBatters(team);
                    batterInfo.setPlayers(players);
                    balanceAtBats = balanceUsage.buildTable(teamReportFile, team);

                    foreach( POSITIONS pos in lhControl.Keys)
                    {
                        lhControl[pos].Items.Clear();
                    }


                    LineupBalanceItem itemStart = new LineupBalanceItem(0, 9, "L");
                    LineupBalanceItem itemEnd = new LineupBalanceItem(18, 9, "R");
                    Lineup lineupLH = new Lineup();
                    lineupLH.BalanceItemFrom = itemStart;
                    lineupLH.BalanceItemTo = itemEnd;
                    lineupLH.PitcherArm = "L";

                    Lineup lineupRH = new Lineup();
                    lineupRH.BalanceItemFrom = itemStart;
                    lineupRH.BalanceItemTo = itemEnd;
                    lineupRH.PitcherArm = "R";

                    int lhAB = calculateAtBatsByLineup(balanceAtBats[0], lineupLH);
                    //            lineupLH.EstimatedAtBats = lhAB;
                    int rhAB = calculateAtBatsByLineup(balanceAtBats[1], lineupRH);

                    if (players != null)
                    {
                        addPlayersByPosition(players, POSITIONS.CATCHER, lhAB);
                        addPlayersByPosition(players, POSITIONS.FIRSTBASE, lhAB);
                        addPlayersByPosition(players, POSITIONS.SECONDBASE, lhAB);
                        addPlayersByPosition(players, POSITIONS.THIRDBASE, lhAB);
                        addPlayersByPosition(players, POSITIONS.SHORTSTOP, lhAB);
                        addPlayersByPosition(players, POSITIONS.LEFTFIELD, lhAB);
                        addPlayersByPosition(players, POSITIONS.CENTERFIELD, lhAB);
                        addPlayersByPosition(players, POSITIONS.RIGHTFIELD, lhAB);
                        addPlayersByPosition(players, POSITIONS.DH, lhAB);
                    }




             //       int lhAB = calculateAtBatsByLineup(balanceAtBats[0], lineupLH);
        //            lineupLH.EstimatedAtBats = lhAB;
           //         int rhAB = calculateAtBatsByLineup(balanceAtBats[1], lineupRH);
          //          lineupRH.EstimatedAtBats = rhAB;

                    lineupLH_label.Content = "LH (" + lhAB + ")";
                    lineupRH_label.Content = "RH (" + rhAB + ")";
                    //MessageBox.Show("Players found for " + team.Abrv + ", count=" + players.Count);
                }
                BTN_MANAGE_LINEUPS.IsEnabled = true;
            }
        }

        private int calculateAtBatsByLineup(Dictionary<int, int> stats, Lineup lineup)
        {
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

        private ComboBox BuildPlayerPostitionBox( int col, POSITIONS row, Lineup lineup)
        {
            ComboBox playerBox = new ComboBox();
            playerBox.Items.Add("NOT SET");
     /*       if (players != null)
            {
                foreach (Player player in players)
                {

                }
            }
            */
            playerBox.FontSize = 12;
            playerBox.FontWeight = FontWeights.Normal;
            playerBox.Foreground = new SolidColorBrush(Colors.Green);
            playerBox.VerticalAlignment = VerticalAlignment.Top;
            playerBox.SelectedIndex = 0;
            playerBox.SelectionChanged += lineup_player_SelectionChanged;
            playerBox.SetValue(dp, lineup);
            playerBox.SetValue(dpPos, new PositionObj(row));
            Grid.SetRow(playerBox, (int)row);
            Grid.SetColumn(playerBox, col);
            if( col == 1)
                lhControl[row] = playerBox;
            return playerBox;
        }

        private Label createColumn(String text, int column, double width)
        {
            ColumnDefinition colLabels = new ColumnDefinition();
            colLabels.Width = new GridLength(width);
            GRID.ColumnDefinitions.Add(colLabels);
            Label lineup = BuildColumnTextBox(text, column);
            GRID.Children.Add(lineup);
            return lineup;
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


        private void addPlayersByPosition(List<Player> players, POSITIONS position, int estimatedAB)
        {
            foreach( Player player in players)
            {
                String defRating = playsPosition(player, position);
                if (defRating.Length > 0 )
                {
                    ComboBox box = lhControl[position];
                    ((Lineup)box.GetValue(dp)).EstimatedAtBats = estimatedAB;
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

                LineupListDlg dlg = new LineupListDlg(team);
                dlg.ShowDialog();
            }
        }
    }


    class DefenseComboBoxItem
    {
        public string Text { get; set; }
        public Player Value { get; set; }

        public override string ToString()
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

    class PositionObj : DependencyObject
    {
        public MainWindow.POSITIONS Position { get; }

        public PositionObj(MainWindow.POSITIONS p)
        {
            Position = p;
        }
        public override string ToString()
        {
            return Position.ToString();
        }

    }
}
