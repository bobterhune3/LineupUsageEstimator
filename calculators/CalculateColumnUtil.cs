using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIneupUsageEstimator
{
    public class CalculateColumnUtil
    {
        public static int targetAtBats = 615;

        public static int calculateColumn(int ip_for_balance, double percentAdj, int totalIP)
        {
            if (ip_for_balance == 0)
                return 0;

            float ip = (float)ip_for_balance;
            float total = (float)totalIP;

            double adj = Math.Ceiling(ip * percentAdj);
            double multiplier = adj / total;
            int value = Convert.ToInt32(Math.Ceiling(multiplier * targetAtBats));
            return value;
        }


        public static int calculateColumn(int ip_for_balance, int totalIP)
        {
            float ip = (float)ip_for_balance;
            float total = (float)totalIP;

            // Always Round up

            int value = Convert.ToInt32(Math.Ceiling((ip / total) * targetAtBats));
            return value;
        }
    }
}
