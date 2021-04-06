using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlsLib;
using Durak;

namespace ControlsLib
{
    public partial class StatsPrompt : Form
    {
        public StatsPrompt()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            // hiding frmStatsPrompt
            Hide();

            // show the new form to take user input for their name
            UsernameInput window = new UsernameInput(); //create an instance
            window.ShowDialog(); //show the form

            // close frmStatsPrompt
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            // hiding frmStatsPrompt
            Hide();

            // do not take their name, play the game
            frmGame game = new frmGame(); //create an instance
            game.ShowDialog(); //show the form

            // close frmMainMenu
            Close();
        }
    }
}
