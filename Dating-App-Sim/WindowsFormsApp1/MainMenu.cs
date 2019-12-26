using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace DatingAppSim
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            if (backgroundColor.IsEmpty)
            {
                BackColor = Color.FromArgb(255, 255, 195);
            }
            else
            {
                BackColor = backgroundColor;
            }
            InitializeComponent();
        }

        //Muuttujat how to play ohjeita ja formin liikuttamista varten.
        private string helpText =
            "Increase your score by swiping right on the profiles as fast as possible before the timer runs out, " +
            "but be careful of ads because they activate a cooldown on swiping!\n\n" +
            "You can control the game by clicking the like (green heart) and dislike (red cross) buttons on screen, " +
            "or by using the left and right arrow keys on your keyboard";
        private bool mouseDown;
        private Point lastLocation;
        private Color backgroundColor;

        #region Napit
        /*  Menun napit  */

        //Sulkee MainMenu-formin ja avaa game-formin
        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.Hide();
            GameForm gf = new GameForm();
            if (!backgroundColor.IsEmpty)
            {
                gf.BackColor = backgroundColor;
            }
            gf.ShowDialog();
            this.Close();
        }
        //Avaa messageboxin joka selittää kuinka peliä pelataan
        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(helpText, "How to play", MessageBoxButtons.OK);
        }
        //Näyttää Settings menuBarin
        private void btnShowSettings_Click(object sender, EventArgs e)
        {
            Button btnShowSettings = (Button)sender;
            ToolStrip ts = (ToolStrip)Controls["menuSettings"];
            if (btnShowSettings.Text == "Show Settings")
            {
                btnShowSettings.Text = "Hide Settings";
                ts.Visible = true;
            }
            else
            {
                btnShowSettings.Text = "Show Settings";
                ts.Visible = false;
            }
        }

        /*Setting napin itemit*/

        //Avaa Color dialogin jolla vaihtaa taustavärejä
        private void BackgroundChangeItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult dr = colorDialog.ShowDialog();
            backgroundColor = colorDialog.Color;
            if (dr == DialogResult.OK)
            {
                BackColor = backgroundColor;
            }
        }
        //Poistaa High score tiedoston mikäli se on olemassa
        private void deleteHighScoresItem_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\dating-app-sim_Files\Top_Scores.txt";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    MessageBox.Show("High scores deleted successfully!", "Done!");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Top_Scores.txt could not be found", "Error!");
            }
        }
        #endregion

        #region formin liikuttaminen
        //Eventit jolla tarkistetaan hiiren sijainti, painikkeen tila ja sallitaan mainMenu formin liikuttaminen päivittämällä formia.
        public void dragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        public void dragPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        public void dragPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion

        #region Sulje ja minimoi nappien toiminnallisuus
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
        #endregion



    }
}
