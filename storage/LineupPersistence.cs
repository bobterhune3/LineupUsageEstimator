using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using somReporter;

namespace LIneupUsageEstimator.storage
{
    public class LineupPersistence
    {
        private static String FILE_NAME = "lineups.dat";

        public static Dictionary<String, TeamLineup> loadDatabase()
        {
            if (File.Exists(FILE_NAME))
            {
                Console.WriteLine("Reading saved file");
                Stream openFileStream = File.OpenRead(FILE_NAME);
                BinaryFormatter deserializer = new BinaryFormatter();
                Dictionary<String,TeamLineup> teamLineup = (Dictionary<String, TeamLineup>)deserializer.Deserialize(openFileStream);
                //teamLineup.TimeLastLoaded = DateTime.Now;
                openFileStream.Close();
                return teamLineup;
            }
            else
            {
                return new Dictionary<String, TeamLineup>();
            }
        }

        public static void saveDatabase(Dictionary<String,TeamLineup> lineups)
        {
            Stream SaveFileStream = File.Create(FILE_NAME);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(SaveFileStream, lineups);
            SaveFileStream.Close();
        }

        public static TeamLineup lookupTeamLineup(Dictionary<String, TeamLineup> lineups, Team team)
        {
            if( !lineups.ContainsKey(team.Abrv))
            {
                TeamLineup teamLineup = new TeamLineup();
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
