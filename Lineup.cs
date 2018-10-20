using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LIneupUsageEstimator
{
    public class Lineup: DependencyObject
    {
        public Lineup()
        {
            PitcherArm = "X";
        }

        public String PitcherArm { get; set; }
        public LineupBalanceItem BalanceItemTo { get; set; }
        public LineupBalanceItem BalanceItemFrom { get; set; }
        public int EstimatedAtBats { get; set; }

        public override String ToString()
        {
            if (PitcherArm.Equals("X"))
                return "";
            return PitcherArm + " " + BalanceItemTo + "-" + BalanceItemFrom;
        }
    }
}
