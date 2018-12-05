using System;
using System.Collections.Generic;
using somReporter;
using somReporter.team;
using static somReporter.SOMTeamReportFile;

namespace LIneupUsageEstimator
{
    class AllPitchers_ScheduleWeighted_Calculator : IUsageCalculator
    {
        private String[] types = { "9L", "8L", "7L", "6L", "5L", "4L", "3L", "2L", "1L", "E", "1R", "2R", "3R", "4R", "5R", "6R", "7R", "8R", "9R" };

        private SOMTeamReportFile teamReportFile;
        private Team targetTeam;
        private int inDivision = 0;
        private int outDivision = 0;

        public AllPitchers_ScheduleWeighted_Calculator(SOMTeamReportFile teamReportFile, Team targetTeam)
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
            if (key.Equals(CalculatorOptions.OPTION_IN_DIVISION_GAMES))
                inDivision = (int)value;
            else if (key.Equals(CalculatorOptions.OPTION_OUT_DIVISION_GAMES))
                outDivision = (int)value;
            else if (key.Equals(CalculatorOptions.OPTION_TARGET_AT_BAT))
                CalculateColumnUtil.targetAtBats = (int)value;
        }

        public List<Dictionary<int, int>> calculate(Func<int, String, int, int, int, int, int> createRowFunc)
        {
            if (inDivision == 0 || outDivision == 0)
            {
                throw new Exception("Error, in and Out Division needs to be defined");
            }

            // Initial local variables
            double teamsInDivision = 0, teamsOutDivision = 0, gamesInDivision = 0, gamesOutDivision = 0, totalGames = 0;

            // Count the number of Games in and out of division
            foreach (Team opponentTeam in teamReportFile.getTeams())
            {
                if (!targetTeam.Abrv.Equals(opponentTeam.Abrv))
                {
                    if (targetTeam.Division.Equals(opponentTeam.Division))
                    {
                        gamesInDivision += inDivision;
                        totalGames += inDivision;
                        teamsInDivision++;
                    }
                    else
                    {
                        gamesOutDivision += outDivision;
                        totalGames += outDivision;
                        teamsOutDivision++;
                    }
                }
            }

            // Percentage of Games In and Out of division.
            double pctGamesInDivisionPerTeam = inDivision / totalGames;
            double pctGamesOutDivisionPerTeam = outDivision / totalGames;
            double overallPctInDivision = pctGamesInDivisionPerTeam * teamsInDivision;
            double overallPctOutDivision = pctGamesOutDivisionPerTeam * teamsOutDivision;

            Dictionary<String, int> total_LeftybalanceData_in = new Dictionary<String, int>();
            Dictionary<String, int> total_RightybalanceData_in = new Dictionary<String, int>();
            Dictionary<String, int> total_LeftybalanceData_out = new Dictionary<String, int>();
            Dictionary<String, int> total_RightybalanceData_out = new Dictionary<String, int>();

            double totalLeftIP_in = 0;
            double totalRightIP_in = 0;
            double totalLeftIP_out = 0;
            double totalRightIP_out = 0;

            foreach (Team opponentTeam in teamReportFile.getTeams())
            {
                if (!opponentTeam.Abrv.Equals(targetTeam.Abrv))
                {
                    bool opponentInDivision = opponentTeam.Division.Equals(targetTeam.Division);

                    List<Player> opponentPitchers = teamReportFile.getTeamPitchers(opponentTeam.Abrv);
                    opponentPitchers.Sort();

                    TeamPitchingProfile profile = TeamPitchingProfile.generatedLikelyUsage(opponentPitchers);
                    profile.buildTeamBalanceCount(opponentPitchers);
                    if (opponentInDivision)
                    {
                       totalLeftIP_in += addTeamsBalanceDataToTotal(total_LeftybalanceData_in,
                            profile.getLeftyPitcherBalanceData());

                        totalRightIP_in += addTeamsBalanceDataToTotal(total_RightybalanceData_in,
                            profile.getRightyyPitcherBalanceData());
                    }
                    else
                    {
                        totalLeftIP_out += addTeamsBalanceDataToTotal(total_LeftybalanceData_out,
                            profile.getLeftyPitcherBalanceData());

                        totalRightIP_out += addTeamsBalanceDataToTotal(total_RightybalanceData_out,
                            profile.getRightyyPitcherBalanceData());
                    }
                }
            }


            Dictionary<String, int> est_LeftybalanceData_in = new Dictionary<String, int>();
            Dictionary<String, int> est_RightybalanceData_in = new Dictionary<String, int>();
            Dictionary<String, int> est_LeftybalanceData_out = new Dictionary<String, int>();
            Dictionary<String, int> est_RightybalanceData_out = new Dictionary<String, int>();

    //        int adjustedTotalLeftStarterIP = Convert.ToInt32(totalLeftIP_in);
     //       int adjustedTotalRightStarterIP = Convert.ToInt32(totalRightIP_in);

            int totalStarterIP = (int)((totalLeftIP_in * overallPctInDivision ) + 
                                       (totalRightIP_in * overallPctInDivision) + 
                                       (totalLeftIP_out * overallPctOutDivision) + 
                                       (totalRightIP_out * overallPctOutDivision));
            foreach (String type in types)
            {
                est_LeftybalanceData_in[type] = CalculateColumnUtil.calculateColumn(total_LeftybalanceData_in[type], overallPctInDivision, totalStarterIP);
                est_RightybalanceData_in[type] = CalculateColumnUtil.calculateColumn(total_RightybalanceData_in[type], overallPctInDivision, totalStarterIP);
                est_LeftybalanceData_out[type] = CalculateColumnUtil.calculateColumn(total_LeftybalanceData_out[type], overallPctOutDivision, totalStarterIP);
                est_RightybalanceData_out[type] = CalculateColumnUtil.calculateColumn(total_RightybalanceData_out[type], overallPctOutDivision, totalStarterIP);
            }

            Dictionary<int, int> balanceLefties = new Dictionary<int, int>();
            Dictionary<int, int> balanceRighties = new Dictionary<int, int>();

            int rowCount = 1;
            foreach (String type in types)
            {
                int ip_for_lefties = est_LeftybalanceData_in[type] + est_LeftybalanceData_out[type];
                int ip_for_righties = est_RightybalanceData_in[type] + est_RightybalanceData_out[type];
                if (createRowFunc != null)
                {
                    createRowFunc(rowCount, type,
                        (int)(total_LeftybalanceData_in[type] + total_LeftybalanceData_out[type]), ip_for_lefties,
                        (int)(total_RightybalanceData_in[type] + total_RightybalanceData_out[type]), ip_for_righties);
                }

                balanceLefties.Add(rowCount - 1, ip_for_lefties);
                balanceRighties.Add(rowCount - 1, ip_for_righties);
                rowCount++;
            }

            List<Dictionary<int, int>> returnValue = new List<Dictionary<int, int>>();
            returnValue.Add(balanceLefties);
            returnValue.Add(balanceRighties);

            return returnValue;
        }



        private int addTeamsBalanceDataToTotal(Dictionary<String, int> totalBalanceData, Dictionary<String, int> teamBalanceData)
        {
            int totalIP = 0;
            foreach (String key in teamBalanceData.Keys)
            {
                if (!totalBalanceData.ContainsKey(key))
                {
                    totalBalanceData[key] = 0;
                }
                int ip = teamBalanceData[key];
                totalBalanceData[key] += ip;
                totalIP += ip;
            }
            return totalIP;
        }

    }
}
