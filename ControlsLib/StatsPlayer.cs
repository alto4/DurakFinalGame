/*@project          OOPFinal Project
 *@file             StatsPlayer.cs 
 *@version          1.0 
 *@since            2021-04-14
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@description      This is the StatsPlayer class its been depricated.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            setPlayerName("Player 1");
            setPlayerWins(0);
            setPlayerTies(0);
            setPlayerLosses(0);
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
       // public static List<StatsPlayer> CreatePlayerList()
        {
            string tempString = "";
            
            const int NUM_OF_COLUMNS = 4;//we know the number of columns in this case
            //Creates an array of strings that equal an individual line from LogsAndStats.dat
            string[] playersRaw = Properties.Resources.DurakStats.Split('\n');
            // before transferring them to the dictionary
            
            int lineCounter = 0;
            
            Dictionary<string, StatsPlayer> allPlayers = new Dictionary<string, StatsPlayer>(); //Initialize the dictionary
            //List<StatsPlayer> temp = new List<StatsPlayer>();
            int numberOfLines = playersRaw.Length;
            //while ((line = file.ReadLine()) != null) //assigns the current line to our variable, and does so until it is null 
            while (lineCounter < numberOfLines)
            {
                int columnCounter = 0;
                string[] columns = playersRaw[lineCounter].Split(','); //splits the line variable into an array using 
                                                                       // a comma as a delimeter
               // Dictionary<string, StatsPlayer> tempPlayers = new Dictionary<string, StatsPlayer>(); //Initialize the dictionary

               StatsPlayer tempPlayer = new StatsPlayer();
                while (columnCounter < NUM_OF_COLUMNS) //this while loop is used for assigning data 
                {                                     //to a StatsPlayer that will be added to a
                                                      //dictionary of StatsPlayer's
                    if (columnCounter == 0) //first piece of data (name)
                    {
                        tempPlayer.setPlayerName(columns[columnCounter]);
                        columnCounter++;
                    }

                    if (columnCounter == 1) //second piece of data (wins)
                    {
                        tempPlayer.setPlayerWins(int.Parse(columns[columnCounter]));
                        columnCounter++;
                    }

                    if (columnCounter == 2) //third piece of data (ties)
                    {
                        tempPlayer.setPlayerTies(int.Parse(columns[columnCounter]));
                        columnCounter++;
                    }

                    if (columnCounter == 3) //fourth piece of data (losses)
                    {
                        tempPlayer.setPlayerLosses(int.Parse(columns[columnCounter]));
                        columnCounter++;
                    }
                    //temp.Add(tempPlayer);
                    //tempString += tempPlayer.ToString();
                }
                allPlayers.Add(tempPlayer.getPlayerName(), tempPlayer);
                // allPlayers.Add(temp[lineCounter].getPlayerName(), temp[lineCounter]);

                //tempPlayer = null;
                // Console.WriteLine(allPlayers.ElementAt(lineCounter).ToString());

                //temp.Add(tempPlayer);

                lineCounter++;
            }
            // return temp;
            MessageBox.Show(tempString);
            return allPlayers; //return the dictionary
        }

        public static StatsPlayer SearchForExistingUser(string username)
        {
            const int NUM_OF_COLUMNS = 4; //we know the length of each line
            string[] playersRaw = Properties.Resources.DurakStats.Split('\n');
            int lineCounter = 0;
            StatsPlayer tempPlayer = new StatsPlayer();
            while (lineCounter < playersRaw.Length)
            {
                int columnCounter = 0;
                string[] columns = playersRaw[lineCounter].Split(',');

                while (columnCounter < NUM_OF_COLUMNS)
                {
                    if (username == columns[columnCounter].ToString()) //if the username is found
                    {
                        tempPlayer.setPlayerName(columnCounter.ToString());
                        tempPlayer.setPlayerWins(int.Parse(columns[columnCounter + 1]));
                        tempPlayer.setPlayerTies(int.Parse(columns[columnCounter + 2]));
                        tempPlayer.setPlayerLosses(int.Parse(columns[columnCounter + 3]));
                        break;
                    }

                }
            }
            if (tempPlayer == new StatsPlayer())
            {
                tempPlayer = new StatsPlayer(username);
            }

            return tempPlayer;
        }

        public static string PrintLogs()
        {
            const int NUM_OF_COLUMNS = 4; //we know the length of each line
            string[] playersRaw = Properties.Resources.DurakStats.Split('\n');
            int lineCounter = 0;
            string tempString = "";
            
            while (lineCounter < playersRaw.Length)
            {
                int columnCounter = 0;
                string[] columns = playersRaw[lineCounter].Split(',');

                while (columnCounter < NUM_OF_COLUMNS)
                {
                    tempString += Environment.NewLine + columns[columnCounter] + ": Wins: " + columns[columnCounter + 1].ToString();
                    tempString += "  Ties: " + columns[columnCounter + 2].ToString() + "  Losses: " + columns[columnCounter + 3].ToString();
                }
            }
            return tempString;
        }

        public static StatsPlayer[] CreatePlayerArray()
        {
            const int NUM_OF_COLUMNS = 4;//we know the number of columns in this case
            //Creates an array of strings that equal an individual line from LogsAndStats.dat
            string[] playersRaw = Properties.Resources.DurakStats.Split('\n');
            // before transferring them to the dictionary
            int arraySize = playersRaw.Length;
            int lineCounter = 0;

           // StatsPlayer[] allPlayers = new StatsPlayer[arraySize];
            StatsPlayer[] tempPlayer = new StatsPlayer[arraySize];
            while (lineCounter < arraySize)
            {
                int columnCounter = 0;
                string[] columns = playersRaw[lineCounter].Split(','); //splits the line variable into an array using 
                                                                       // a comma as a delimeter
                                                                       // Dictionary<string, StatsPlayer> tempPlayers = new Dictionary<string, StatsPlayer>(); //Initialize the dictionary
                StatsPlayer player = new StatsPlayer();

                while (columnCounter < NUM_OF_COLUMNS) //this while loop is used for assigning data 
                {                                     //to a StatsPlayer that will be added to a
                                                      //dictionary of StatsPlayer's
                    tempPlayer[lineCounter] = player;
                    //allPlayers[lineCounter] = tempPlayer;
                    if (columnCounter == 0) //first piece of data (name)
                    {
                        tempPlayer[lineCounter].setPlayerName(columns[columnCounter]);
                        columnCounter++;
                    }

                    if (columnCounter == 1) //second piece of data (wins)
                    {
                        tempPlayer[lineCounter].setPlayerWins(int.Parse(columns[columnCounter]));
                        columnCounter++;
                    }

                    if (columnCounter == 2) //third piece of data (ties)
                    {
                        tempPlayer[lineCounter].setPlayerTies(int.Parse(columns[columnCounter]));
                        columnCounter++;
                    }

                    if (columnCounter == 3) //fourth piece of data (losses)
                    {
                        tempPlayer[lineCounter].setPlayerLosses(int.Parse(columns[columnCounter]));
                        columnCounter++;
                    }
                    
                }
                //allPlayers[lineCounter] = tempPlayer[lineCounter];
               // allPlayers.
                lineCounter++;
            }
            return tempPlayer;
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
