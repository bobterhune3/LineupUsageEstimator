using System;
using System.Collections.Generic;
using somReporter;
using somReporter.team;

namespace LIneupUsageEstimator
{
    class SPOnly_Straight_Calculator : IUsageCalculator
    {
        private SOMTeamReportFile teamReportFile;
        private Team targetTeam;
        private int targetAtBats = 615;

        public SPOnly_Straight_Calculator(SOMTeamReportFile teamReportFile, Team targetTeam)
        {
            this.teamReportFile = teamReportFile;
            this.targetTeam = targetTeam;
        }

        public List<Dictionary<int, int>> calculate()
        {
            return calculate(null);
        }

        public void setOptions(String key, Object value)
        {
            if (key.Equals(CalculatorOptions.OPTION_TARGET_AT_BAT))
                targetAtBats = (int)value;
        }

        public List<Dictionary<int, int>> calculate(Func<int, String, int, int, int, int, int> createRowFunc)
        {
            Dictionary<int, int> balanceLefties = new Dictionary<int, int>();
            Dictionary<int, int> balanceRighties = new Dictionary<int, int>();
            String[] types = { "9L", "8L", "7L", "6L", "5L", "4L", "3L", "2L", "1L", "E", "1R", "2R", "3R", "4R", "5R", "6R", "7R", "8R", "9R" };
            int rowCount = 1;
            int totalStarterIP = teamReportFile.getTotalStarterIP(targetTeam);
      //      int totalPitcherIP = teamReportFile.getTotalPitcherIP();

            foreach (String type in types)
            {
                int ip_for_lefties = 0;
                int ip_for_righties = 0;
                List<Player> players = teamReportFile.getBalanceData()[type];
                foreach (Player player in players)
                {
                    if (player.GS > 1 && !player.Team.Abrv.Equals(targetTeam.Abrv))
                    {
                        if (player.Throws.Equals("L"))
                        {
                            ip_for_lefties += player.IP;
                        }
                        else if (player.Throws.Equals("R"))
                        {
                            ip_for_righties += player.IP;
                        }

                    }
                }

                // THIS BUILDS THE ESTIMATED BATTER AT BATS PER TYPE TABLE
                int valueLAB = CalculateColumnUtil.calculateColumn(ip_for_lefties, totalStarterIP);
                int valueRAB = CalculateColumnUtil.calculateColumn(ip_for_righties, totalStarterIP);

                if(createRowFunc != null )
                {
                    createRowFunc(rowCount, type, ip_for_lefties, valueLAB, ip_for_righties, valueRAB);
                }

                balanceLefties.Add(rowCount - 1, valueLAB);
                balanceRighties.Add(rowCount - 1, valueRAB);
                rowCount++;
            }

            List<Dictionary<int, int>> returnValue = new List<Dictionary<int, int>>();
            returnValue.Add(balanceLefties);
            returnValue.Add(balanceRighties);
            return returnValue;
        }
    }
}
