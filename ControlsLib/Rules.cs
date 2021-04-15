/*@project          OOPFinal Project
 *@file             Rules.cs 
 *@version          1.0 
 *@since            2021-03-24 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@description      This is the form to show the rules
 */

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
    public partial class frmRules : Form
    {

        public frmRules()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmRules_Load(object sender, EventArgs e)
        {
            lblTitle.Focus();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            
        }

    }
}
