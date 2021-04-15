/*@project          OOPFinal Project
 *@file             Durak.cs 
 *@version          1.0 
 *@since            2021-04-14
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@description      This is the StatsPlayer class its been depricated.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsLib
{
    public partial class frmLogs : Form
    {
        public frmLogs()
        {
            InitializeComponent();
            #region "Attempt for stats"
            //Dictionary<string, StatsPlayer> allPlayers = new Dictionary<string, StatsPlayer>(); //StatsPlayer.CreatePlayerDictionary();


            //StatsPlayer[] tempArray = StatsPlayer.CreatePlayerArray();
            //// Dictionary<string, StatsPlayer> allPlayers = new Dictionary<string, StatsPlayer>();
            //// List<StatsPlayer> tempAllPlayers = StatsPlayer.CreatePlayerList();

            ////  StatsPlayer[] allPlayers = StatsPlayer.CreatePlayerArray();

            //for (int i = 0; i < tempArray.Length; i++)
            //{
            //    MessageBox.Show(tempArray[i].ToString());
            //    //allPlayers.Add(tempArray[i].getPlayerName(), tempArray[i]);
            //}
            //for (int i = 0; i < allPlayers.Count; i++)
            //{

            //    lblTestLabel.Text += Environment.NewLine + allPlayers.ElementAt(i).ToString();   //ElementAt(i).ToString(); //Iterates through the dictionary
            //}
            #endregion


            string[] logLines = Properties.Resources.logs.Split('\n');

            int lineCounter = 0;
            int numberOfLines = logLines.Length;

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader("logs.txt"))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        txtLogs.Text += line + "\n";
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            //while (lineCounter < numberOfLines)
            //{
            //    //int columnCounter = 0;
            //    //string[] columns = logLines[lineCounter].Split(','); //splits the line variable into an array using 
            //    // a comma as a delimeter
            //    // Dictionary<string, StatsPlayer> tempPlayers = new Dictionary<string, StatsPlayer>(); //Initialize the dictionary

            //    lblTestLabel.Text += Environment.NewLine + logLines[lineCounter] + "\n";
                
            //    lineCounter++;
            //}
        }

        public void frmLogs_Load(object sender, EventArgs e)
        {
            //lblTestLabel.Text = "Player Name | Wins | Ties | Losses"; //prints the header
            //lblTestLabel.Text += StatsPlayer.PrintLogs();
        }
    }
}
