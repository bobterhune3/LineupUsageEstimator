using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using somReporter.team;
using System.Windows.Media;
using static LineupEngine.LineupEngine;

namespace LIneupUsageEstimator
{
    class TeamBatterInfo : StaticColumnControlBase
    {
        private enum COLUMNS { NAME = 0, PROJECTED, ACTUAL, REMAINING, BAL, POSITIONS };
        private Grid InfoGrid { get; }
        private Grid LineupGrid { get; }

        public TeamBatterInfo(Grid infoGrid, Grid lineupData)
        {
            InfoGrid = infoGrid;
            LineupGrid = lineupData;
            createColumn(InfoGrid, "Player", 0, 70);
            createColumn(InfoGrid, "Prj AB", 1, 45);
            createColumn(InfoGrid, "Act AB", 2, 45);
            createColumn(InfoGrid, "Remain", 3, 45);
            createColumn(InfoGrid, "Bal", 4, 30);
            createColumn(InfoGrid, "Positions", 5, 100);
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            InfoGrid.RowDefinitions.Add(row);
        }

        public void setPlayers(List<Player> players)
        {
            int ABAdjustment = Int32.Parse(Properties.Settings.Default.ABAddition);
            clearInfoTable(InfoGrid);
            List<Player> sorted = players.OrderBy(o => o.Name).ToList();
            int postion = 1;
            foreach (Player player in sorted)
            {
                int totalAB = 0;
                Dictionary<POSITIONS, int> positions = new Dictionary<POSITIONS, int>();

                foreach (Object obj in LineupGrid.Children)
                {
                    if (obj is ComboBox)
                    {
                        ComboBox cb = (ComboBox)obj;
                        if (cb.SelectedItem != null && cb.SelectedItem is DefenseComboBoxItem)
                        {
                            LineupDataObj lineup = (LineupDataObj)cb.GetValue(MainWindow.dp);
                            Player selectedPlayer = ((DefenseComboBoxItem)cb.SelectedItem).Value;
                            if (selectedPlayer == player)
                            {
                                POSITIONS pos = ((PositionObj)cb.GetValue(MainWindow.dpPos)).Position;
                                adjustPostionCount(positions, pos);
                                totalAB += lineup.EstimatedAtBats;
                            }
                        }
                    }
                }
                int remaining = (player.Actual + ABAdjustment) - totalAB;

                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                InfoGrid.RowDefinitions.Add(row);
                InfoGrid.Children.Add(BuildPlayerInfoRow(player.Name, COLUMNS.NAME, postion, remaining >= 0 ? Colors.Black : Colors.Red));
                InfoGrid.Children.Add(BuildPlayerInfoRow(Convert.ToString(totalAB), COLUMNS.PROJECTED, postion, Colors.Black));
                if(ABAdjustment > 0)
                    InfoGrid.Children.Add(BuildPlayerInfoRow(String.Format("{0}+{1}",player.Actual, ABAdjustment), COLUMNS.ACTUAL, postion, Colors.Black));
                 else
                    InfoGrid.Children.Add(BuildPlayerInfoRow(player.Actual.ToString(), COLUMNS.ACTUAL, postion, Colors.Black));
                InfoGrid.Children.Add(BuildPlayerInfoRow(Convert.ToString(remaining), COLUMNS.REMAINING, postion, remaining >= 0 ? Colors.Black : Colors.Red));
                InfoGrid.Children.Add(BuildPlayerInfoRow(player.Bal, COLUMNS.BAL, postion, Colors.Black));
                InfoGrid.Children.Add(BuildPlayerInfoRow(buildPositionDisplayString(positions), COLUMNS.POSITIONS, postion, Colors.Black));
                postion++;
            }
        }

        private void adjustPostionCount(Dictionary<POSITIONS, int> positions, POSITIONS pos)
        {
            if(positions.ContainsKey(pos))
            {
                positions[pos]++;
            }
            else
            {
                positions.Add(pos, 1);
            }
        }

        private String buildPositionDisplayString(Dictionary<POSITIONS, int> positions)
        {
            StringBuilder sb = new StringBuilder();
            foreach(POSITIONS pos in positions.Keys)
            {
                if(positions.ContainsKey(pos))
                {
                    int count = positions[pos];
                    if (count > 1)
                        sb.Append(shortPositionName(pos) + "(" + count + ")");
                    else
                        sb.Append(shortPositionName(pos));
                }
            }
            return sb.ToString();
        }


        private Label BuildPlayerInfoRow(String data, COLUMNS column, int row, Color color)
        {
            Label label = new Label();
            label.Content = data;
            label.FontSize = 8;
            label.FontWeight = FontWeights.Normal;
            label.Foreground = new SolidColorBrush(color);
            label.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(label, (int)row);
            Grid.SetColumn(label, (int)column);
            return label;
        }

        private String shortPositionName(POSITIONS position)
        {
            switch (position)
            {
                case POSITIONS.CATCHER:
                    return " C";
                case POSITIONS.FIRSTBASE:
                    return " 1B";
                case POSITIONS.SECONDBASE:
                    return " 2B";
                case POSITIONS.THIRDBASE:
                    return " 3B";
                case POSITIONS.SHORTSTOP:
                    return " SS";
                case POSITIONS.LEFTFIELD:
                    return " LF";
                case POSITIONS.CENTERFIELD:
                    return " CF";
                case POSITIONS.RIGHTFIELD:
                    return " RF";
                default:
                    return " DH";

            }
        }
    }
}
