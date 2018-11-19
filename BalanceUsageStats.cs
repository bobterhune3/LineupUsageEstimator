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
    public class BalanceUsageStats : StaticColumnControlBase
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

        public BalanceUsageStats()
        {

        }

        public List<Dictionary<int, int>> buildTable(IUsageCalculator calculator)
        {
            if (InfoGrid != null)
            {
                clearInfoTable(InfoGrid);
            }
            
            return calculator.calculate(createRow);
        }

        public int createRow(int index, String type, int ipLHP, int valueLAB, int ipRHP, int valueRAB)
        {
            if (InfoGrid != null)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                InfoGrid.RowDefinitions.Add(row);
                InfoGrid.Children.Add(buildRow(type, COLUMNS.BALANCE, index, Colors.Black, FontWeights.Bold));
                InfoGrid.Children.Add(buildRow(ipLHP, COLUMNS.LHIP, index, Colors.Black, FontWeights.Normal));
                InfoGrid.Children.Add(buildRow(valueLAB, COLUMNS.LHAB, index, Colors.Black, FontWeights.Bold));
                InfoGrid.Children.Add(buildRow(ipRHP, COLUMNS.RHIP, index, Colors.Black, FontWeights.Normal));
                InfoGrid.Children.Add(buildRow(valueRAB, COLUMNS.RHAB, index, Colors.Black, FontWeights.Bold));
            }
            return 0;
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


    }
}
