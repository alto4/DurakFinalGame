/** IMAGE ATTRIBUTION
*   ==================
*   <div>Icon made by <a href="https://www.flaticon.com/authors/bqlqn" 
*   title="bqlqn">bqlqn</a> 
*   from <a href="https://www.flaticon.com/" 
*   title="Flaticon">www.flaticon.com</a></div>
*/

using ControlsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Durak
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Close the main menu form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// launch instance of durak form and close the main menu form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            // hidding frmMainMenu 
            this.Hide();

            // new frmGame instance
            frmGame game = new frmGame();

            // show the frmGame form
            game.ShowDialog();

            // close frmMainMenu
            this.Close();
        }

        /// <summary>
        /// Show the logs form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogs_Click(object sender, EventArgs e)
        {
            frmLogs logs = new frmLogs();

            logs.ShowDialog();
        }
    }
}
