using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using somReporter;

namespace LIneupUsageEstimator.storage
{
    class TeamInformation
    {
        private static String FILE_NAME = "teaminfo.dat";

        public static TeamInfo loadDatabase()
        {
            if (File.Exists(FILE_NAME))
            {
                Console.WriteLine("Reading saved file");
                Stream openFileStream = File.OpenRead(FILE_NAME);
                BinaryFormatter deserializer = new BinaryFormatter();
                try
                {
                    TeamInfo teamLineup = (TeamInfo)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                    return teamLineup;
                }
                catch (Exception)
                {
                    return new TeamInfo();
                }
            }
            else
            {
                return new TeamInfo();
            }
        }

        public static void saveDatabase(TeamInfo lineups)
        {
            Stream SaveFileStream = File.Create(FILE_NAME);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(SaveFileStream, lineups);
            SaveFileStream.Close();
        }

        public static TeamInfo lookupTeamInfo(Dictionary<String, TeamInfo> lineups, Team team)
        {
            if (!lineups.ContainsKey(team.Abrv))
            {
                TeamInfo teamLineup = new TeamInfo();
                lineups.Add(team.Abrv, teamLineup);
                return teamLineup;
            }
            else
            {
                return lineups[team.Abrv];
            }
        }

        public static void clearDatabase()
        {
            File.Delete(FILE_NAME);
        }
    }
}
