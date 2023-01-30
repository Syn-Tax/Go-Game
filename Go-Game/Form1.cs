using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Go_Game
{
    public partial class goGame : Form
    {
        public goGame()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) // Click event handler for the strip menu.
        {
            // Creating a message box to display about information.
            DialogResult result;
            result = MessageBox.Show("Go. Alexander Glen, Lewis Simmonds, Oscar Morris. (c) 2023", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
