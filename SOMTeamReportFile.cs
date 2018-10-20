using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using somReporter.team;

namespace somReporter
{
    public class SOMTeamReportFile
    {
        private String m_fileName = "";
        private Dictionary<String, List<Player>> pitcherDataByBalance = null;
        private Dictionary<Team, List<Player>> pitcherDataByTeam = null;
        private Dictionary<Team, List<Player>> batterDataByTeam = null;
        private List<Team> teams = new List<Team>();

        public SOMTeamReportFile(String reportPath)
        {
            m_fileName = reportPath;
            if (!File.Exists(m_fileName))
                throw new Exception("File " + reportPath + " cannot be found");

        }

        public void parse()
        {
            List<String> lines = readFileLinesOnly(true);
            organizeDataTeam(lines);
            removeNonStartingPitchers(pitcherDataByTeam);
            pitcherDataByBalance = organizePitcherByBalance(pitcherDataByTeam);
            return;
        }

        public int getTotalStarterIP()
        {
            return getTotalStarterIP(null);
        }

        public int getTotalStarterIP(Team excludeTeam)
        {
            int totalIP = 0;
            // First we need the total IP across all players
            foreach ( String key in pitcherDataByBalance.Keys)
            {
                foreach (Player player in pitcherDataByBalance[key])
                {
                    if (excludeTeam == null || !excludeTeam.Abrv.Equals(player.Team.Abrv))
                    {
                        if (player.GS > 1)
                        {
                            totalIP += player.IP;
                        }
                    }
                }
            }
            return totalIP;
        }

        public int getTotalPitcherIP()
        {
            int totalIP = 0;
            // First we need the total IP across all players
            foreach (String key in pitcherDataByBalance.Keys)
            {
                foreach (Player player in pitcherDataByBalance[key])
                {
                    totalIP += player.IP;
                }
            }
            return totalIP;
        }


        public Dictionary<String, List<Player>> getBalanceData()
        {
            return pitcherDataByBalance;
        }

        public List<Player> getTeamPitchers(String teamAbv)
        {
            Team team = lookupTeam(teamAbv);
            return pitcherDataByTeam[team];
        }

        public List<Player> getTeamBatters(String teamAbv)
        {
            Team team = lookupTeam(teamAbv);
            return batterDataByTeam[team];
        }

        public List<Player> getTeamBatters(Team team)
        {
            return batterDataByTeam[team];
        }

        public List<Team> getTeams() { return teams; }

        private void organizeDataTeam(List<string> lines)
        {
            Regex regex = new Regex(@"^([0-9]+) (.{1,18})");
            Regex regexPitcherLine = new Regex(@"^(.{1,22}) [0-9]+ +[0-9]+ +[0-9]+ +([A-Z]+) +([RL]) {1,6}([0-9 ])");
            Regex regexPitcherBalLine = new Regex(@"^(.{1,17})[0-9]+ +([0-9ERL]+) +[0-9]+ +[0-9]+ +[0-9.]+ +([0-9]+) +([0-9]+) +[0-9]+ +[0-9]+ +[0-9]+ +([0-9]+) +[0-9]+");
            Regex regexBaterLine = new Regex(@"^(.{1,17}) +[0-9]+ +[0-9] +[0-9]+ +([A-Z]+) +([0-9ERL]+) +([0-9]+)");
            Regex regexBaterBalanceLine = new Regex(@"^(.{1,16}) +[0-9]+ +([NW])\/([NW]) +([LRS])");

            String currentTeam = "";
            bool inPitcherBalanceSection = false;
            bool inPitcherSection = false;
            bool inBatterSection = false;
            bool inBatterBalanceSection = false;
            bool inDefenseSection = false;
            int pitcherIndex = 0;
            int batterIndex = 0;
            Team team = null;
            List<Player> pitchers = new List<Player>();
            List<Player> batters = new List<Player>();
            pitcherDataByTeam = new Dictionary<Team, List<Player>>();
            batterDataByTeam = new Dictionary<Team, List<Player>>();
            foreach (String line in lines)
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    String thisTeam = isTeamNameLineOfReport(line);
                    if (thisTeam != null)
                    {
                        Console.Out.WriteLine("Finished reading " + currentTeam);
                        currentTeam = thisTeam;
                        inPitcherBalanceSection = false;
                        inPitcherSection = false;
                        if (team != null) { 
                            pitcherDataByTeam.Add(team, pitchers);
                            batterDataByTeam.Add(team, batters);
                        }
                        pitchers = new List<Player>();
                        batters = new List<Player>();
                        pitcherIndex = 0;
                        batterIndex = 0;
                        team = new Team("", 0);
                        teams.Add(team);
                        team.Name = thisTeam;
                    }
                }
                if( !inBatterSection )
                {
                    inBatterSection = line.Contains("CODES YEAR TEAM BAL  AB DO TR HR BAVG  BB K'S RBI  OB%");
                }
                if( !inBatterBalanceSection )
                {
                    inBatterBalanceSection = line.Contains("LEFT% POWER BAT STEAL BUNT H&R RUN");
                    if (inBatterBalanceSection)
                        inBatterSection = false;
                }
                if (!inPitcherBalanceSection)
                {
                    inPitcherBalanceSection = line.Contains("LEFT% BAL WON LOST  ERA");
                    if(inPitcherBalanceSection)
                        inPitcherSection = false;
                }
                if (!inPitcherSection)
                {
                    inPitcherSection = line.Contains("CODES YEAR TEAM  THROWS START RELIEF");
                    if (inPitcherSection)
                    {
                        inDefenseSection = false;
                    }
                }
                if(!inDefenseSection)
                {
                    inDefenseSection = line.Contains("Catcher       1b   2b   3b   ss   lf   cf   rf   ARM");
                    if (inDefenseSection)
                    {
                        batterIndex = 0;
                        inBatterBalanceSection = false;
                    }
                }

                if ( inBatterSection)
                {
                    Match matchB = regexBaterLine.Match(line);
                    if (matchB.Success)
                    {
                        if (matchB.Groups.Count != 5)
                            throw new Exception("Expecting Yeam, Team and Text");
                        Player player = new Player();
                        player.Name = matchB.Groups[1].Value.Trim();
                        if (team.Abrv.Length == 0)
                            team.Abrv = matchB.Groups[2].Value.Trim();
                        player.IsHitter = true;
                        player.Bal = matchB.Groups[3].Value.Trim();
                        player.Actual = Int32.Parse(matchB.Groups[4].Value.Trim());
                        batters.Add(player);
                        Console.Out.WriteLine("   inBatterSection " + currentTeam);
                    }
                }
                else if(inBatterBalanceSection )
                {
                    Match matchBB = regexBaterBalanceLine.Match(line);
                    if (matchBB.Success)
                    {
                        if (matchBB.Groups.Count != 5)
                            throw new Exception("Expecting Yeam, Team and Text");
                        Player player = batters[batterIndex];
                        String name = matchBB.Groups[1].Value.Trim();
                        if (!player.Name.EndsWith(name))
                        {
                            Console.Out.WriteLine("ERROR: Pitcher Name not as expected " + player.Name + " != " + name);
                            continue;
                        }
                        player.PowerL = matchBB.Groups[2].Value.Trim();
                        player.PowerR = matchBB.Groups[3].Value.Trim();
                        player.Throws = matchBB.Groups[4].Value.Trim();

                        batterIndex++;
                        Console.Out.WriteLine("   inBatterBalanceSection " + currentTeam);
                    }
                }
                else if (inPitcherSection)
                {
                    Match matchP = regexPitcherLine.Match(line);
                    if (matchP.Success)
                    {
                        if (matchP.Groups.Count != 5)
                            throw new Exception("Expecting Yeam, Team and Text");
                        Player player = new Player();
                        player.Name = matchP.Groups[1].Value.Trim();
                        if( team.Abrv.Length == 0)
                            team.Abrv = matchP.Groups[2].Value.Trim();
                        player.Team = team;
                        player.IsHitter = false;
                        player.Throws = matchP.Groups[3].Value.Trim();
                        String spRating = matchP.Groups[4].Value.Trim();
                        if(spRating.Length > 0)
                            player.Games = Int32.Parse(spRating);
                        pitchers.Add(player);
                        Console.Out.WriteLine("   inPitcherSection " + currentTeam);
                    }
                }
                else if (inPitcherBalanceSection)
                {
                    Match matchPB = regexPitcherBalLine.Match(line);
                    if (matchPB.Success)
                    {
                        if (matchPB.Groups.Count != 6)
                            throw new Exception("Expecting Yeam, Team and Text");
                        Player player = pitchers[pitcherIndex];
                        String name = matchPB.Groups[1].Value.Trim();
                        if ( !player.Name.EndsWith(name))
                        {
                            Console.Out.WriteLine("ERROR: Pitcher Name not as expected " + player.Name + " != " + name);
                            continue;
                        }
                        player.Bal = matchPB.Groups[2].Value.Trim();
                        player.IP = Int32.Parse(matchPB.Groups[3].Value.Trim());

                        player.Hits = Int32.Parse(matchPB.Groups[4].Value.Trim());
                        player.GS = Int32.Parse(matchPB.Groups[5].Value.Trim());
                        pitcherIndex++;
                    }
                }
                else if (inDefenseSection)
                {
                    if (!line.StartsWith("Catcher"))
                    {
                        String name = line.Substring(0, 16).Trim();
                        String catcher = line.Substring(17, 13).Trim();
                        if(catcher.Length > 0 )
                        {  // strip off passball and throwing error stats
                            catcher = catcher.Substring(0, catcher.IndexOf('t'));
                        }
                        String first = line.Substring(30, 4).Trim();
                        String second = line.Substring(35, 4).Trim();
                        String third = line.Substring(40, 4).Trim();
                        String sstop = line.Substring(45, 4).Trim();
                        String left = line.Substring(50, 4).Trim();
                        String center = line.Substring(55, 4).Trim();
                        String right = line.Substring(60, 4).Trim();
                        String ofArm = line.Substring(65, 4).Trim();
                        Player player = batters[batterIndex];

                        if (!player.Name.EndsWith(name))
                        {
                            // Sometimes the game strips off the last 1-3 characters in the report, so this checks for that.
                            if (!player.Name.EndsWith(name.Substring(0, name.Length - 1)) && 
                                !player.Name.EndsWith(name.Substring(0, name.Length - 2)) &&
                                !player.Name.EndsWith(name.Substring(0, name.Length - 3)))
                            {
                                Console.Out.WriteLine("ERROR: Defense Player Name not as expected " + player.Name + " != " + name);

                                continue;
                            }
                        }
                        Defense def = new Defense(catcher, first, second, third, sstop, left, center, right, ofArm);
                        player.Def = def;

                        batterIndex++;
                    }
                }
            }
            pitcherDataByTeam.Add(team, pitchers);
            batterDataByTeam.Add(team, batters);
        }

        private void removeNonStartingPitchers(Dictionary<Team, List<Player>> data)
        {
            foreach( Team team in data.Keys)
            {
                List<Player> pitchers = data[team];
                List<Player> removes = new List<Player>();
                foreach( Player pitcher in pitchers )
                {
                    int gs = pitcher.GS;
                    if( gs == 0 )
                    {
                        removes.Add(pitcher);
                    }
                }
                foreach( Player remove in removes)
                    pitchers.RemoveAll(item => item == remove);
                return;
            }
        }

        private Dictionary<String, List<Player>> organizePitcherByBalance(Dictionary<Team, List<Player>> data)
        {
            Dictionary<String, List<Player>> balance = new Dictionary<String, List<Player>>();
            foreach (Team team in data.Keys)
            {
                List<Player> pitchers = data[team];
                foreach (Player pitcher in pitchers)
                {
                    String bal = pitcher.Bal;
                    if (!balance.ContainsKey(bal))
                        balance.Add(bal, new List<Player>());
                    List<Player> players = balance[bal];
                    players.Add(pitcher);

                }
            }
            return balance;
        }

        /*
        private Report loadReport()
        {
           return new TeamRosterReport(reportTitle);
        }
        */
        /*
        public List<String> readReportLines()
        {
            List<String> lines = new List<String>();
            System.IO.StreamReader file = null;
            try
            {
                string line;

                // Read the file and display it line by line.
                file = new System.IO.StreamReader(m_fileName);
                while ((line = file.ReadLine()) != null)
                {
                    line = cleanUpLine(line);
                    if (line.Trim().Length == 0)
                        continue;
                    if (lines.Count == 0)
                        parseFirstLineOfReport(line);
                    lines.Add(line);
                }
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
            return lines;
        }
        */
        public List<String> readFileLinesOnly(Boolean cleanup)
        {
            List<String> lines = new List<String>();
            System.IO.StreamReader file = null;
            try
            {
                string line;

                file = new System.IO.StreamReader(m_fileName);
                while ((line = file.ReadLine()) != null)
                {
                    if (cleanup)
                        line = cleanUpLine(line);
                    if (line.Trim().Length == 0)
                        continue;
                    lines.Add(line);
                }
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
            return lines;
        }

        public String isTeamNameLineOfReport(string line)
        {
            Regex regex = new Regex(@"([0-9]+) (.{1,18})");
            Match match = regex.Match(line);
            if (match.Groups.Count != 3)
                throw new Exception("Expecting Yeam, Team and Text");
            return match.Groups[2].Value.Trim();
        }


        public string cleanUpLine(string line)
        {
            //       line = line.Replace("[0]", "");
            line = line.Replace("[1]", "");
            line = line.Replace("[2]", "");
            line = line.Replace("[3]", "");
            line = line.Replace("[4]", "");
            return line.Trim();
        }

        private Team lookupTeam(String teamAbrv)
        {
            foreach( Team team in teams )
            {
                if (team.Abrv.Equals(teamAbrv))
                    return team;
            }
            return null;
        }
    }
}
