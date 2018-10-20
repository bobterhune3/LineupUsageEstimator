using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using somReporter.team;
using System.Windows.Media;
using somReporter.util.somUsageAnalysis;
using somReporter;

namespace LIneupUsageEstimator
{
    class BalanceUsageStats : StaticColumnControlBase
    {
        private enum COLUMNS { BALANCE = 0, LHIP, LHAB, RHIP, RHAB };
         private Grid InfoGrid { get; }

        public BalanceUsageStats(Grid infoGrid)
        {
            InfoGrid = infoGrid;
            createColumn(InfoGrid, "BAL", 0, 40);
            createColumn(InfoGrid, "LH IP", 1, 40);
            createColumn(InfoGrid, "Est. LH AB", 2, 60);
            createColumn(InfoGrid, "RH IP", 3, 40);
            createColumn(InfoGrid, "Est. RH AB", 4, 60);
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            InfoGrid.RowDefinitions.Add(row);
        }

        public List<Dictionary<int, int>> buildTable(SOMTeamReportFile teamReportFile, Team targetTeam)
        {
            Dictionary<int, int> balanceLefties = new Dictionary<int, int>();
            Dictionary<int, int> balanceRighties = new Dictionary<int, int>();
            String[] types = { "9L", "8L", "7L", "6L", "5L", "4L", "3L", "2L", "1L", "E", "1R", "2R", "3R", "4R", "5R", "6R", "7R", "8R", "9R" };
            int rowCount = 1;
            int totalStarterIP = teamReportFile.getTotalStarterIP(targetTeam);
            int totalPitcherIP = teamReportFile.getTotalPitcherIP();

            clearInfoTable(InfoGrid);

            foreach ( String type in types )
            {
                int ip_for_lefties = 0;
                int ip_for_righties = 0;
                List<Player> players = teamReportFile.getBalanceData()[type];
                foreach( Player player in players )
                {
                    if( player.GS > 1 && !player.Team.Abrv.Equals(targetTeam.Abrv))
                    {
                        if( player.Throws.Equals("L"))
                        {
                            ip_for_lefties += player.IP;
                        }
                        else if( player.Throws.Equals("R"))
                        {
                            ip_for_righties += player.IP;
                        }

                    }
                }
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                InfoGrid.RowDefinitions.Add(row);
                InfoGrid.Children.Add(buildRow(type, COLUMNS.BALANCE, rowCount, Colors.Black, FontWeights.Bold));
                InfoGrid.Children.Add(buildRow(ip_for_lefties, COLUMNS.LHIP, rowCount, Colors.Black, FontWeights.Normal));
                int valueLAB = calculateColumn(ip_for_lefties, totalStarterIP);
                InfoGrid.Children.Add(buildRow(valueLAB, COLUMNS.LHAB, rowCount, Colors.Black, FontWeights.Bold));
                InfoGrid.Children.Add(buildRow(ip_for_righties, COLUMNS.RHIP, rowCount, Colors.Black, FontWeights.Normal));
                int valueRAB = calculateColumn(ip_for_righties, totalStarterIP);
                InfoGrid.Children.Add(buildRow(valueRAB, COLUMNS.RHAB, rowCount, Colors.Black, FontWeights.Bold));
                balanceLefties.Add(rowCount - 1, valueLAB);
                balanceRighties.Add(rowCount - 1, valueRAB);
                rowCount++;
            }

            List<Dictionary<int, int>> returnValue = new List<Dictionary<int, int>>();
            returnValue.Add(balanceLefties);
            returnValue.Add(balanceRighties);
            return returnValue;
        }

        private Label buildRow(Object data, COLUMNS column, int row, Color color, FontWeight weight)
        {
            Label label = new Label();
            label.Content = data;
            label.FontSize = 8;
            label.FontWeight = weight;
            label.Foreground = new SolidColorBrush(Colors.Black);
            label.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(label, (int)row);
            Grid.SetColumn(label, (int)column);
            return label;
        }

        private int calculateColumn(int ip_for_balance, int totalIP)
        {
            float ip = (float)ip_for_balance;
            float total = (float)totalIP;

            int value = Convert.ToInt32((ip / total ) * 615);
            return value;
        }
    }
}
