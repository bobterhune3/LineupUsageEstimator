using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIneupUsageEstimator
{
    [Serializable()]
    public class LineupBalanceItem
    {

        public int Value { get; set; }
        public int BalanceLevel { get; set; }
        public String BalanceArm { get; set; }

        public LineupBalanceItem(int index, int level, String arm)
        {
            Value = index;
            BalanceLevel = level;
            BalanceArm = arm;
        }

        public override String ToString()
        {
            return String.Format("{0}{1}", BalanceLevel == 0 ? "E" : Convert.ToString(BalanceLevel), BalanceLevel == 0 ? "" : BalanceArm);
        }

        public static bool operator<(LineupBalanceItem lup1, LineupBalanceItem lup2)
        {

            return Comparison(lup1, lup2) < 0;

        }

        public static bool operator >(LineupBalanceItem lup1, LineupBalanceItem lup2)
        {

            return Comparison(lup1, lup2) > 0;

        }

        public static bool operator ==(LineupBalanceItem lup1, LineupBalanceItem lup2)
        {

            return Comparison(lup1, lup2) == 0;

        }

        public static bool operator !=(LineupBalanceItem lup1, LineupBalanceItem lup2)
        {

            return Comparison(lup1, lup2) != 0;

        }

        public override bool Equals(object obj)
        {

            if (!(obj is LineupBalanceItem)) return false;

            return this == (LineupBalanceItem)obj;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator <=(LineupBalanceItem lup1, LineupBalanceItem lup2)
        {

            return Comparison(lup1, lup2) <= 0;

        }

        public static bool operator >=(LineupBalanceItem lup1, LineupBalanceItem lup2)
        {

            return Comparison(lup1, lup2) >= 0;

        }

        public static int Comparison(LineupBalanceItem lup1, LineupBalanceItem lup2)
        {

            if (lup1.Value < lup2.Value)

                return -1;

            else if (lup1.Value == lup2.Value)

                return 0;

            else if (lup1.Value > lup2.Value)

                return 1;

            return 0;

        }

    }
}
