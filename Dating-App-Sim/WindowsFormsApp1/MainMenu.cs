﻿using System;
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

        //formin paikalliset muuttujat: peliohjeet, hiirenpainike alhaalla ja formin sijainti.
        private string rules = 
            "Swipe right on the profiles as fast as possible before the timer runs out, " +
            "but be careful of ads because they activate a cooldown on swiping!";
        private bool mouseDown;
        private Point lastLocation;
        
        //sulkee ohjelman
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Minimoi ikkunan
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //Piilottaa MainMenu-formin ja avaa game-formin
        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.Hide();
            gameForm gf = new gameForm();
            gf.ShowDialog();
            this.Close();
        }

        //Avaa messageboxin joka selittää kuinka peliä pelataan
        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(rules, "How to play", MessageBoxButtons.OK);
        }

        //Eventit jolla tarkistetaan hiiren sijainti, painikkeen tila ja sallitaan mainMenu formin liikuttaminen päivittämällä formia.
        public void dragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        public void dragPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        public void dragPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
