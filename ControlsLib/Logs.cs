using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            Dictionary<string, StatsPlayer> allPlayers = new Dictionary<string, StatsPlayer>(); //StatsPlayer.CreatePlayerDictionary();
            
            
            StatsPlayer[] tempArray = StatsPlayer.CreatePlayerArray();


            // Dictionary<string, StatsPlayer> allPlayers = new Dictionary<string, StatsPlayer>();
            // List<StatsPlayer> tempAllPlayers = StatsPlayer.CreatePlayerList();

            //  StatsPlayer[] allPlayers = StatsPlayer.CreatePlayerArray();

            for (int i = 0; i < tempArray.Length; i++)
            {
                MessageBox.Show(tempArray[i].ToString());
                //allPlayers.Add(tempArray[i].getPlayerName(), tempArray[i]);
            }



            for (int i = 0; i < allPlayers.Count; i++)
            {
                
                lblTestLabel.Text += Environment.NewLine + allPlayers.ElementAt(i).ToString();   //ElementAt(i).ToString(); //Iterates through the dictionary
            }
        }

        public void frmLogs_Load(object sender, EventArgs e)
        {
            //lblTestLabel.Text = "Player Name | Wins | Ties | Losses"; //prints the header
            //lblTestLabel.Text += StatsPlayer.PrintLogs();
        }
    }
}
