using System;
using System.Collections.Generic;
using somReporter;

namespace LIneupUsageEstimator.storage
{
    // <Snippet2>
    [Serializable()]
    // </Snippet2>
    // <Snippet1>
    public class TeamInfo// : INotifyPropertyChanged
    {
        public List<Team> Team { get; set; }
        public int InDivisionGameCount { get; set; }
        public int OutofDivisionGameCount { get; set; }

#pragma warning disable CS0657 // Not a valid attribute location for this declaration
        [field: NonSerialized()]
#pragma warning restore CS0657 // Not a valid attribute location for this declaration
        public DateTime TimeLastLoaded { get; set; }

        public TeamInfo()
        {
            this.Team = new List<Team>();
        }

        public bool hasEmptyData()
        {
            if (Team.Count == 0) return true;
            foreach(Team team in Team)
            {
                if (team.Division.Length == 0)
                    return true;
            }
            return false;
        }
    }
}
