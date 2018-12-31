using System;
using System.Collections.Generic;
using System.Windows;
using LineupEngine;
using somReporter;
using somReporter.team;

namespace LIneupUsageEstimator
{
    public class LineupDataObj : DependencyObject
    {
        private LineupData serializableLineupData = null;
        //    private Guid guid;

        public long Id { get; set; }

        public LineupDataObj(long Id)
        {
            this.Id = Id;
            this.PitcherArm = "X";
            //       this.guid = Guid.NewGuid();
            this.playersByPos = new List<Player>();
        }

        public LineupDataObj(LineupData storedData)
        {
            this.PitcherArm = storedData.PitcherArm;
            this.Id = storedData.Id;
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
            if (serializableLineupData == null)
            {
                serializableLineupData = new LineupData(RecordIndex.getNextId(RecordIndex.INDEX.LineupDataId),
                                                        PitcherArm, BalanceItemTo, BalanceItemFrom, EstimatedAtBats);
            }
            else
            {
                serializableLineupData.PitcherArm = this.PitcherArm;
                serializableLineupData.Id = this.Id;
                serializableLineupData.BalanceItemTo = this.BalanceItemTo;
                serializableLineupData.BalanceItemFrom = this.BalanceItemFrom;
                serializableLineupData.EstimatedAtBats = this.EstimatedAtBats;
            }

            return serializableLineupData;
        }

        public List<Player> playersByPos { get; set; }
    }

}
