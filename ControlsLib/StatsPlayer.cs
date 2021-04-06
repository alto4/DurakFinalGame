using System;
using System.Collections.Generic;
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
        public override string ToString()
        {
            string tempString="";

            tempString += " " + getPlayerName() + ": Wins: " + getPlayerWins().ToString();
            tempString += "  Ties: " + getPlayerTies().ToString() + "  Losses: " + getPlayerLosses().ToString();

            return tempString;
        }
    }
}
