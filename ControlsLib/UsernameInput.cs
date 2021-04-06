using Durak;
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
    public partial class UsernameInput : Form
    {
        const int MIN_NAME_LENGTH = 0;
        const int MAX_NAME_LENGTH = 4;

        public UsernameInput()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            // hiding frmStatsPrompt
            Hide();

            // show the new form to take user input for their name
            StatsPrompt prompt = new StatsPrompt();
            prompt.ShowDialog();

            // close frmStatsPrompt
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;

            if (username.Length < MIN_NAME_LENGTH || username.Length > MAX_NAME_LENGTH)
            {
                lblPrompt.Text = "Your username must be 1-4 characters in length";
                txtUsername.Text = "";
            }
            else
            {
                //Make username all caps
                username.ToUpper();

                //When the StreamReader and StreamWriters are incorporated, we will pull a Vector
                //of players from a csv, or other text file. 
                //This is where we will pull all the previous stats and put them into a Vector, 
                //Then we are going to search the vector for the username to see if it exists,
                //if it does, then our current player equals that player, otherwise, it will be a
                //new player to add to the stats.


                //But for now just create a player
                StatsPlayer currentPlayer = new StatsPlayer(username);
                //We want this currentPlayer to be Global and persist across windows
                //Do we need to make a constructor for the frmGame to accept a StatsPlayer
                //as an option?

                // hiding frmStatsPrompt
                Hide();

                //Finally open the game
                frmGame game = new frmGame(username); //create an instance
                game.ShowDialog(); //show the form

                // close username input form
                Close();

            }
        }
    }
}
