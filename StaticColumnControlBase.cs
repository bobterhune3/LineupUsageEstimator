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
    abstract class StaticColumnControlBase
    {

        protected void createColumn(Grid grid, String text, int column, double width)
        {
            ColumnDefinition colLabels = new ColumnDefinition();
            colLabels.Width = new GridLength(width);
            grid.ColumnDefinitions.Add(colLabels);
            grid.Children.Add(BuildColumnLabel(text, column));
        }


        protected Label BuildColumnLabel(String title, int lineup)
        {
            Label label = new Label();
            label.Content = title;
            label.FontSize = 10;
            label.FontWeight = FontWeights.Bold;
            label.Foreground = new SolidColorBrush(Colors.Red);
            label.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(label, 0);
            Grid.SetColumn(label, lineup);
            return label;
        }

        protected void clearInfoTable(Grid grid)
        {
            for (int i = grid.Children.Count - 1; i >= grid.ColumnDefinitions.Count; i--)
            {
                grid.Children.Remove(grid.Children[i]);
            }
        }
    }
}
