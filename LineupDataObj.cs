using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LIneupUsageEstimator
{

    public class LineupDataObj : DependencyObject
    {
        private LineupData serializableLineupData = null;
        private Guid guid;

        public LineupDataObj()
        {
            this.PitcherArm = "X";
            this.guid = Guid.NewGuid();
        }

        public LineupDataObj(LineupData storedData)
        {
            this.PitcherArm = storedData.PitcherArm;
            this.guid = storedData.lineupGuid;
            this.BalanceItemTo = storedData.BalanceItemTo;
            this.BalanceItemFrom = storedData.BalanceItemFrom;
            this.EstimatedAtBats = storedData.EstimatedAtBats;
        }

        public String PitcherArm { get; set; }
        
        public LineupBalanceItem BalanceItemTo { get; set; }
        public LineupBalanceItem BalanceItemFrom { get; set; }
        public int EstimatedAtBats { get; set; }

        public override String ToString()
        {
            if (PitcherArm.Equals("X"))
                return "";
            return PitcherArm + " " + BalanceItemFrom + "-" + BalanceItemTo;
        }
        
        public LineupData getLineupData()
        {
            if(serializableLineupData == null )
                serializableLineupData = new LineupData(PitcherArm, BalanceItemTo, BalanceItemFrom, EstimatedAtBats, guid);
            else
            {
                serializableLineupData.PitcherArm = this.PitcherArm;
                serializableLineupData.lineupGuid = this.guid;
                serializableLineupData.BalanceItemTo = this.BalanceItemTo;
                serializableLineupData.BalanceItemFrom = this.BalanceItemFrom;
                serializableLineupData.EstimatedAtBats = this.EstimatedAtBats;
            }

            return serializableLineupData;
        }
    }

    [Serializable()]
    public class LineupData
    {
        public LineupData(String arm, LineupBalanceItem to, LineupBalanceItem from, int atBats, Guid guid)
        {
            PitcherArm = arm;
            BalanceItemTo = to;
            BalanceItemFrom = from;
            EstimatedAtBats = atBats;
            lineupGuid = guid;
        }

        public String PitcherArm { get; set; }
        public LineupBalanceItem BalanceItemTo { get; set; }
        public LineupBalanceItem BalanceItemFrom { get; set; }
        public int EstimatedAtBats { get; set; }
        public Guid lineupGuid { get; set; }

        public override String ToString()
        {
            if (PitcherArm.Equals("X"))
                return "";
            return PitcherArm + " " + BalanceItemTo + "-" + BalanceItemFrom;
        }
    }
}
