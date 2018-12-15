using System;
using System.Collections.Generic;
using somReporter.team;

namespace LIneupUsageEstimator.storage
{
    // <Snippet2>
    [Serializable()]
    // </Snippet2>
    // <Snippet1>
    public class TeamLineup// : INotifyPropertyChanged
    {
        public List<LineupData> Lineups { get; set; }
        public List<Player> playerByGRID { get; set; }
#pragma warning disable CS0657 // Not a valid attribute location for this declaration
        [field: NonSerialized()]
#pragma warning restore CS0657 // Not a valid attribute location for this declaration
        public DateTime TimeLastLoaded { get; set; }
        /*
        private string customer;
        public string Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                PropertyChanged?.Invoke(this,
                  new PropertyChangedEventArgs(nameof(Customer)));
            }
        }
        */

    //    [field: NonSerialized()]
    //    public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public TeamLineup()
        {
            this.Lineups = new List<LineupData>();
        }
    }
}
