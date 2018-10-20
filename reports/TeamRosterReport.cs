using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace somReporter
{
    class TeamRosterReport : Report
    {
        private const String REPORT_TYPE = "TEAM ROSTER REPORT";

        private bool header_contains_League = true;

        public enum REPORT_SCOPE { ALL, LEAGUE, DIVISION }
        public TeamRosterReport(String title, bool containsLeague) : base(title)
        {
            header_contains_League = containsLeague;
        }

        public override String getReportType() { return REPORT_TYPE; }

        public override void processReport(int n)
        {
            foreach (String line in m_lines)
            {
                string fixedLine = line.Replace("[0]", "");
                collectData(fixedLine);
            }
        }

        public void collectData(string line)
        {
            /*
            String headerRegEx = header_contains_League ? REGEX_HEADER : REGEX_HEADER_NO_LEAGUE;
            Regex regex = new Regex(headerRegEx);
            Match headerMatch = regex.Match(line);
            if (headerMatch.Success && isThisADivisionName(line))
            {
                m_CurrentDivision = headerMatch.Groups[1].Value.Trim();
            }
            else
            {
                regex = new Regex(REGEX_TEAM_RECORD);
                Match teamMatch = regex.Match(line);
                if (teamMatch.Success)
                {
                    String name = teamMatch.Groups[1].Value.Trim();
                    name = name.Substring(0, name.Length - 1).Trim();
                    if (name.Length > 7)
                    {
                        String abv = name.Substring(name.Length - 3).Trim();
                        if (abv.Length == 3)
                        {
                            name = name.Substring(0, name.Length - 3);

                            Team team = new Team(m_CurrentDivision, Program.LEAGUES[0].Length);
                            team.Name = name.Trim();
                            team.Abrv = abv;
                            team.Wins = Convert.ToInt32(teamMatch.Groups[2].Value.Trim());
                            team.Loses = Convert.ToInt32(teamMatch.Groups[3].Value.Trim());
                            //        team.Wpct = Convert.ToDouble(teamMatch.Groups[4].Value.Trim());
                            String gamesBehind = teamMatch.Groups[5].Value.Trim();
                            if (gamesBehind.StartsWith("-"))
                                team.Gb = 0;
                            else
                                team.Gb = Convert.ToDouble(gamesBehind);
                            DATABASE.addTeam(team);
                        }
                    }
                }
            }
            */

        }
        /*

        private List<Team> getTeamsByScope(ReportScope scope)
        {
            List<Team> matches = new List<Team>();
            foreach (Team team in DATABASE.Teams())
            {
                if (scope.AllTeams)
                    matches.Add(team);
                if (scope.Division.Length > 0 &&
                    (Program.LEAGUES[0].Length > 0 && scope.League.Length > 0))

                {
                    if (team.Division.Equals(scope.Division) &&
                        team.League.Equals(scope.League))
                    {
                        matches.Add(team);
                    }
                }
                else if (scope.Division.Length > 0 &&
                   team.Division.Equals(scope.Division))
                {
                    matches.Add(team);
                }
                else if (scope.League.Length > 0 &&
                    team.League.Equals(scope.League))
                {
                    matches.Add(team);
                }
                else if (scope.League.Equals("X") &&
                    scope.Division.Length == 0 &&
                    Program.LEAGUES[0].Length == 0)
                {
                    matches.Add(team);
                }
            }
            return matches;
        }
        */
        /*
        public List<Team> getTeamsByWinPercentage(ReportScope scope)
        {
            List<Team> matches = getTeamsByScope(scope);

            //TODO:
            // Tie Breakers: 1.Head to head W - L / 2.H to H run differential / 3.League wide Pythagorean
            matches.Sort(delegate (Team x, Team y)
            {
                if (scope.OrderAscending)
                {
                    int result = x.Wpct.CompareTo(y.Wpct);
                    if (result == 0)
                        return x.PythagoreanTheorem.CompareTo(y.PythagoreanTheorem);
                    else return result;
                }
                else
                {
                    int result = y.Wpct.CompareTo(x.Wpct);
                    if (result == 0)
                        return y.PythagoreanTheorem.CompareTo(x.PythagoreanTheorem);
                    else return result;
                }

            });

            return matches;
        }
        */
        /*
                matches.Sort(delegate (Team x, Team y)
                    {

                    });
        */
        public class ReportScope
        {
            public String Division = "";
            public String League = "";
            public Boolean AllTeams = false;
            public Boolean OrderAscending = false;

        }
        /*
        public List<Team> getTeamsByName(ReportScope scope)
        {
            List<Team> matches = getTeamsByScope(scope);

            matches.Sort(delegate (Team x, Team y)
            {
                if (scope.OrderAscending)
                {
                    if (x.Name == null && y.Name == null) return 0;
                    else if (x.Name == null) return -1;
                    else if (y.Name == null) return 1;
                    else return x.Name.ToUpper().CompareTo(y.Name.ToUpper());
                }
                else
                {
                    if (x.Name == null && y.Name == null) return 0;
                    else if (x.Name == null) return -1;
                    else if (y.Name == null) return 1;
                    else return y.Name.ToUpper().CompareTo(x.Name.ToUpper());
                }

            });

            return matches;
        }
        */
        /*
        public void calculateHighLowTeamStats(String league, String division, Team.CATEGORY cat)
        {
            ReportScope scope = new ReportScope();
            if (league.Length > 0)
                scope.League = league;
            if (division.Length > 0)
            {
                scope.Division = division;
                scope.League = "X";
            }
            List<Team> teams = getTeamsByScope(scope);
            Team leader = null;
            Team trailing = null;
            foreach (Team t in teams)
            {
                if (leader == null || trailing == null)
                {
                    leader = t;
                    trailing = t;
                }
                processCategory(ref leader, ref trailing, t, cat);
            }

            leader.setLeader(cat);
            trailing.setTrailing(cat);
        }
        */
        private void processCategory(ref Team leader, ref Team trailing, Team t, Team.CATEGORY cat)
        {
            if (cat == Team.CATEGORY.BATTING_AVERAGE)
            {
                if (t.BattingAverage > leader.BattingAverage)
                    leader = t;
                else if (t.BattingAverage < trailing.BattingAverage)
                    trailing = t;
            }
            else if (cat == Team.CATEGORY.EARNED_RUNS_AVG)
            {
                if (t.EarnedRunAvg < leader.EarnedRunAvg)
                    leader = t;
                else if (t.EarnedRunAvg > trailing.EarnedRunAvg)
                    trailing = t;
            }
            else if (cat == Team.CATEGORY.HOME_RUNS)
            {
                if (t.HomeRuns > leader.HomeRuns)
                    leader = t;
                else if (t.HomeRuns < trailing.HomeRuns)
                    trailing = t;
            }
        }
    }
}
