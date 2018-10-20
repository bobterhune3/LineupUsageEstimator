using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using somReporter.team;
using System.Windows.Media;

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
            clearInfoTable(InfoGrid);
            List<Player> sorted = players.OrderBy(o => o.Name).ToList();
            int postion = 1;
            foreach (Player player in sorted)
            {
                int totalAB = 0;
                StringBuilder position = new StringBuilder();

                foreach (Object obj in LineupGrid.Children)
                {
                    if (obj is ComboBox)
                    {
                        ComboBox cb = (ComboBox)obj;
                        if (cb.SelectedItem != null && cb.SelectedItem is DefenseComboBoxItem)
                        {
                            Lineup lineup = (Lineup)cb.GetValue(MainWindow.dp);
                            Player selectedPlayer = ((DefenseComboBoxItem)cb.SelectedItem).Value;
                            if (selectedPlayer == player)
                            {
                                MainWindow.POSITIONS pos = ((PositionObj)cb.GetValue(MainWindow.dpPos)).Position;
                                position.Append(shortPositionName(pos));
                                totalAB += lineup.EstimatedAtBats;
                            }
                        }
                    }
                }
                int remaining = player.Actual - totalAB;

                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                InfoGrid.RowDefinitions.Add(row);
                InfoGrid.Children.Add(BuildPlayerInfoRow(player.Name, COLUMNS.NAME, postion, remaining >= 0 ? Colors.Black : Colors.Red));
                InfoGrid.Children.Add(BuildPlayerInfoRow(Convert.ToString(totalAB), COLUMNS.PROJECTED, postion, Colors.Black));
                InfoGrid.Children.Add(BuildPlayerInfoRow(player.Actual.ToString(), COLUMNS.ACTUAL, postion, Colors.Black));
                InfoGrid.Children.Add(BuildPlayerInfoRow(Convert.ToString(remaining), COLUMNS.REMAINING, postion, remaining >= 0 ? Colors.Black : Colors.Red));
                InfoGrid.Children.Add(BuildPlayerInfoRow(player.Bal, COLUMNS.BAL, postion, Colors.Black));
                InfoGrid.Children.Add(BuildPlayerInfoRow(position.ToString(), COLUMNS.POSITIONS, postion, Colors.Black));
                postion++;
            }
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

        private String shortPositionName(MainWindow.POSITIONS position)
        {
            switch (position)
            {
                case MainWindow.POSITIONS.CATCHER:
                    return "C ";
                case MainWindow.POSITIONS.FIRSTBASE:
                    return "1B ";
                case MainWindow.POSITIONS.SECONDBASE:
                    return "2B ";
                case MainWindow.POSITIONS.THIRDBASE:
                    return "3B ";
                case MainWindow.POSITIONS.SHORTSTOP:
                    return "SS ";
                case MainWindow.POSITIONS.LEFTFIELD:
                    return "LF ";
                case MainWindow.POSITIONS.CENTERFIELD:
                    return "CF ";
                case MainWindow.POSITIONS.RIGHTFIELD:
                    return "RF ";
                default:
                    return "DH ";

            }
        }
    }
}
