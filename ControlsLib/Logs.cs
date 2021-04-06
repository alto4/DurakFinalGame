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

            Dictionary<string, StatsPlayer> allPlayers = new Dictionary<string, StatsPlayer>(StatsPlayer.CreatePlayerDictionary());

            lblTestLabel.Text = "Player Name | Wins | Ties | Losses";
            foreach (KeyValuePair<string, StatsPlayer> player in allPlayers)
            {

                lblTestLabel.Text += Environment.NewLine + player.ToString();
            }
        }

        public void frmLogs_Load(object sender, EventArgs e)
        {
            
        }
    }
}
