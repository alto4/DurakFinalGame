/*@project          OOPFinal Project
 *@file             SelectDeckSize.cs 
 *@version          1.0 
 *@since            2021-03-04 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@description      This is the select deck size form.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using CardLib;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsLib
{
    public partial class frmSelectDeckSize : Form
    {
        int choice = 36;

        public frmSelectDeckSize()
        {
            InitializeComponent();
        }

        private void btnSmall_Click(object sender, EventArgs e)
        {
            choice = (int)SizeOfDecks.Small;
            this.Visible = false;
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            choice = (int)SizeOfDecks.Normal;
            this.Visible = false;
        }

        private void btnLarge_Click(object sender, EventArgs e)
        {
            choice = (int)SizeOfDecks.Large;
            this.Visible = false;
        }

        public int GetSizeChoice()
        {
            return choice;
        }
    }
}
