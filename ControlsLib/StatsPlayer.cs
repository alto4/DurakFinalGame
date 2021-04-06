using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlsLib
{
    /// <summary>
    /// Class will be used for persistant statistics log
    /// </summary>
    class StatsPlayer
    {
        //Private Variables
        private static string playerName;
        private static int playerWins;
        private static int playerTies;
        private static int playerLosses;

        //Constructors
        //Default Constructor
        public StatsPlayer() 
        {
            //For testing only
            setPlayerName("Nick");
            setPlayerWins(11);
            setPlayerTies(3);
            setPlayerLosses(5);
        }
        //Parameterized Constructors
        public StatsPlayer(string name, int wins, int ties, int losses)
        {
            setPlayerName(name);
            setPlayerWins(wins);
            setPlayerTies(ties);
            setPlayerLosses(losses);
        }

        public StatsPlayer(string name)
        {
            setPlayerName(name);
            setPlayerWins(0);
            setPlayerTies(0);
            setPlayerLosses(0);
        }

        //Getters and Setters
        public string getPlayerName()
        {
            return playerName;
        }

        public void setPlayerName(string name)
        {
            playerName = name;
        }

        public int getPlayerWins()
        {
            return playerWins;
        }

        public void setPlayerWins(int wins)
        {
            playerWins = wins;
        }

        public int getPlayerTies()
        {
            return playerTies;
        }

        public void setPlayerTies(int ties)
        {
            playerTies = ties;
        }

        public int getPlayerLosses()
        {
            return playerLosses;
        }

        public void setPlayerLosses(int losses)
        {
            playerLosses = losses;
        }

        //Public Methods
        /// <summary>
        /// This method will return a dictionary of StatsPlayers that can be used later for output
        /// </summary>
        /// <returns>Dictionary<string, StatsPlayer></returns>
        public static Dictionary<string, StatsPlayer> CreatePlayerDictionary()
        {
            const int NUM_OF_COLUMNS = 4;//we know the number of columns in this case
            //Creates an array of strings that equal an individual line from LogsAndStats.dat
            string[] playersRaw = Properties.Resources.DurakStats.Split('\n');
            //int numberOfLines = playersRaw.Length; //gives us the number of lines
            //StatsPlayer[] players = new StatsPlayer[numberOfLines]; //a place to hold our players
            // before transferring them to the dictionary
            Dictionary<string, StatsPlayer> allPlayers = new Dictionary<string, StatsPlayer>(); //Initialize the dictionary
            int lineCounter = 0;
            //string line;
            //Declare a StreamReader
           // StreamReader file = new StreamReader(Properties.Resources.DurakStats);

            //while ((line = file.ReadLine()) != null) //assigns the current line to our variable, and does so until it is null 
            while (lineCounter < playersRaw.Length)
            {
                int columnCounter = 0;
                string[] columns = playersRaw[lineCounter].Split(','); //splits the line variable into an array using 
                                                    // a comma as a delimeter
                StatsPlayer tempPlayer = new StatsPlayer();

                while(columnCounter < NUM_OF_COLUMNS) //this while loop is used for assigning data 
                {                                     //to a StatsPlayer that will be added to a
                                                      //dictionary of StatsPlayer's
                    if (columnCounter == 0) //first piece of data (name)
                    {
                        tempPlayer.setPlayerName(columns[columnCounter]);
                    }

                    if (columnCounter == 1) //second piece of data (wins)
                    {
                        tempPlayer.setPlayerWins(int.Parse(columns[columnCounter]));
                    }

                    if (columnCounter == 2) //third piece of data (ties)
                    {
                        tempPlayer.setPlayerTies(int.Parse(columns[columnCounter]));
                    }

                    if (columnCounter == 3) //fourth piece of data (losses)
                    {
                        tempPlayer.setPlayerLosses(int.Parse(columns[columnCounter]));
                    }
                    columnCounter++;
                }
                allPlayers.Add(tempPlayer.getPlayerName(), tempPlayer);
                Console.WriteLine(allPlayers.ElementAt(lineCounter).ToString());
                lineCounter++;
            }
            return allPlayers; //return the dictionary
        }

        /// <summary>
        /// Method used to write the stats to a .dat to be read at a later date
        /// </summary>
        /// <returns>string</returns>
        public string FileString()
        {
            return getPlayerName() + "," + getPlayerWins().ToString()
                 + "," + getPlayerTies().ToString() + "," + getPlayerLosses().ToString();
        }

        /// <summary>
        /// Standard ToString override, will be used to tabulate players
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string tempString="";

            tempString += " " + getPlayerName() + ": Wins: " + getPlayerWins().ToString();
            tempString += "  Ties: " + getPlayerTies().ToString() + "  Losses: " + getPlayerLosses().ToString();

            return tempString;
        }
    }
}
