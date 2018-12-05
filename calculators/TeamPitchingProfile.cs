using System;
using System.Collections.Generic;
using somReporter.team;

namespace LIneupUsageEstimator
{
    public class TeamPitchingProfile
    {
        private const int HIGH_WATER_MARK_SP = 975;
        private const int HIGH_WATER_MARK_RP = 350;

        private Dictionary<String, int> teamLBalance = new Dictionary<String, int>();
        private Dictionary<String, int> teamRBalance = new Dictionary<String, int>();

        public static TeamPitchingProfile generatedLikelyUsage(List<Player> opponentPitchers)
        {
            return new TeamPitchingProfile(opponentPitchers);
        }

        public Dictionary<String, int> getLeftyPitcherBalanceData() { return teamLBalance; }
        public Dictionary<String, int> getRightyyPitcherBalanceData() { return teamRBalance; }

        public void buildTeamBalanceCount(List<Player> pitchers)
        {
            // First initialize the 
            String[] types = { "9L", "8L", "7L", "6L", "5L", "4L", "3L", "2L", "1L", "E", "1R", "2R", "3R", "4R", "5R", "6R", "7R", "8R", "9R" };
            foreach (String type in types)
            {
                teamLBalance.Add(type, 0);
                teamRBalance.Add(type, 0);
            }

            int index = 1;
            int starterIP = 0;
            foreach (Player pitcher in starters)
            {
                String bal = pitcher.Bal;
                // Use 100% of first fiver pitchers
                if( index < 5)
                {
                    incrementInningsPitched(pitcher, pitcher.IP, bal);
                }
                else
                {
                    // No need to add more pitchers if team has already hit high water mark
                    if (starterIP > HIGH_WATER_MARK_SP)
                        continue;

                    if (PlayerSortingUtil.calculateWHIP(pitcher) < 1.5) {
                        int adj = (int)((double)pitcher.IP / ((double)index - 1));
                        incrementInningsPitched(pitcher, adj, bal);
                    }
                }
                starterIP += pitcher.IP;
                index++;
            }

            // Closer
            if (closer != null)
            {
                String bal = closer.Bal;
                // Use 100% of first fiver pitchers
                incrementInningsPitched(closer, closer.IP, bal);
            }

            index = 0;
            int reliefPitcherIP = 0;
            foreach (Player pitcher in relievers)
            {
                String bal = pitcher.Bal;
                // Use 100% of first fiver pitchers
                if (index < 4)
                {
                    incrementInningsPitched(pitcher, pitcher.IP, bal);
                }
                else
                {
                    if (PlayerSortingUtil.calculateWHIP(pitcher) < 1.5)
                    {
                        // No need to add more pitchers if team has already hit high water mark
                        if (reliefPitcherIP > HIGH_WATER_MARK_SP)
                            continue;

                        int adj = (int)((double)pitcher.IP / ((double)index - 1));
                        incrementInningsPitched(pitcher, adj, bal);
                    }
                }
                reliefPitcherIP += pitcher.IP;
                index++;
            }
        }

        public List<Player> Starters() { return starters; }
        public List<Player> Relievers() { return relievers; }
        public Player Closer() { return closer; }

        private List<Player> starters = new List<Player>();
        private List<Player> relievers = new List<Player>();
        private Player closer = null;

        private TeamPitchingProfile(List<Player> opponentPitchers)
        {
            opponentPitchers.Sort();

            int starterCount = 0;
            int starterIPCount = 0;
            int closerCount = 0;
            foreach( Player pitcher in opponentPitchers)
            {
                if (pitcher.IP < 31) continue;

                if(pitcher.primaryPos.Equals("SP"))
                {
                    if(starterIPCount < HIGH_WATER_MARK_SP && starterCount < 8)
                    {
                        starters.Add(pitcher);
                        starterIPCount += pitcher.IP;
                    }
                    else
                    {
                        pitcher.primaryPos = "RP";
                        relievers.Add(pitcher);
                    }
                    starterCount ++;
                }
                else if(pitcher.primaryPos.Equals("CL"))
                {
                    if (closerCount > 0)
                    {
                        pitcher.primaryPos = "RP";
                        relievers.Add(pitcher);
                    }
                    else
                    {
                        closer = pitcher;
                        closerCount++;
                    }
                }
                else
                {
                    relievers.Add(pitcher);
                }
            }
            relievers.Sort();
        }

        private void incrementInningsPitched( Player pitcher, int ip, String bal)
        {
            if (pitcher.Throws.Equals("L"))
            {
                teamLBalance[bal] += ip;
            }
            else
            {
                teamRBalance[bal] += ip;
            }
        }
    }
}
