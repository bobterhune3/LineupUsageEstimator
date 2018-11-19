using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIneupUsageEstimator
{
    public class LineupTools
    {

        public static List<LineupBalanceItem> buildDefaultLineupTypes()
        {
            List<LineupBalanceItem> balanceItems = new List<LineupBalanceItem>();
            balanceItems.Add(new LineupBalanceItem(0, 9, "L"));
            balanceItems.Add(new LineupBalanceItem(1, 8, "L"));
            balanceItems.Add(new LineupBalanceItem(2, 7, "L"));
            balanceItems.Add(new LineupBalanceItem(3, 6, "L"));
            balanceItems.Add(new LineupBalanceItem(4, 5, "L"));
            balanceItems.Add(new LineupBalanceItem(5, 4, "L"));
            balanceItems.Add(new LineupBalanceItem(6, 3, "L"));
            balanceItems.Add(new LineupBalanceItem(7, 2, "L"));
            balanceItems.Add(new LineupBalanceItem(8, 1, "L"));
            balanceItems.Add(new LineupBalanceItem(9, 0, ""));
            balanceItems.Add(new LineupBalanceItem(10, 1, "R"));
            balanceItems.Add(new LineupBalanceItem(11, 2, "R"));
            balanceItems.Add(new LineupBalanceItem(12, 3, "R"));
            balanceItems.Add(new LineupBalanceItem(13, 4, "R"));
            balanceItems.Add(new LineupBalanceItem(14, 5, "R"));
            balanceItems.Add(new LineupBalanceItem(15, 6, "R"));
            balanceItems.Add(new LineupBalanceItem(16, 7, "R"));
            balanceItems.Add(new LineupBalanceItem(17, 8, "R"));
            balanceItems.Add(new LineupBalanceItem(18, 9, "R"));

            return balanceItems;
        }
    }

}
