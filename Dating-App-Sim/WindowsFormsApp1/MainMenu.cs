using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private string rules = 
            "Swipe right on the profiles as fast as possible before the timer runs out, " +
            "but be careful of ads because they activate a cooldown on swiping!";

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.Hide();
            gameForm gf = new gameForm();
            gf.ShowDialog();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(rules, "How to play", MessageBoxButtons.OK);
        }
    }
}
