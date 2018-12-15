using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using somReporter.team;

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
            this.playersByPos = new List<Player>();
        }

        public LineupDataObj(LineupData storedData)
        {
            this.PitcherArm = storedData.PitcherArm;
            this.guid = storedData.lineupGuid;
            this.BalanceItemTo = storedData.BalanceItemTo;
            this.BalanceItemFrom = storedData.BalanceItemFrom;
            this.EstimatedAtBats = storedData.EstimatedAtBats;
     //       if(storedData.playersByPos != null)
     //           this.playersByPos = storedData.playersByPos;  //For backwards compatibility
     //       else
     //           this.playersByPos = new List<Player>();
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
 //               serializableLineupData.playersByPos = this.playersByPos;
            }

            return serializableLineupData;
        }

        public List<Player> playersByPos { get; set; }
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
    //        playersByPos = new List<Player>();
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
            return PitcherArm + " " + BalanceItemFrom + "-" + BalanceItemTo;
        }

//        public List<Player> playersByPos { get; set; }
    }
}
