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

namespace WindowsFormsApp1
{
    public partial class gameForm : Form
    {
        public gameForm()
        {
            InitializeComponent();
            ListPics();
            ResetGame();
        }
        Random rnd = new Random();
        List<string> picPaths = new List<string>();

        private void ListPics()
        {
            picPaths = Directory.GetFiles(Directory.GetCurrentDirectory().Replace(@"\bin\Debug", @"\pics") ).ToList();
            
        }

        private void ResetGame()
        {
            Panel gamePanel = new Panel();
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(240, 320);
            gamePanel.Location = new Point((Width / 2 - gamePanel.Width / 2), (Height / 2 - gamePanel.Height / 2));
            gamePanel.Anchor = AnchorStyles.None;
            Controls.Add(gamePanel);

            Button btnStartGame = new Button();
            btnStartGame.Name = "btnStartGame";
            btnStartGame.BackColor = Color.Black;
            btnStartGame.Font = new Font("Noto Mono", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnStartGame.ForeColor = Color.White;
            btnStartGame.Size = new Size(169, 35);
            btnStartGame.Location = new Point(120-btnStartGame.Width/2, 160);
            btnStartGame.TabIndex = 1;
            btnStartGame.Text = "Start";
            btnStartGame.Click += BtnStartGame_Click;
            gamePanel.Controls.Add(btnStartGame);
            Update();

            

        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            Panel gamePanel = (Panel)Controls[Controls.IndexOfKey("gamePanel")];
            gamePanel.Controls.Clear();
            gamePanel.BackgroundImage = Image.FromFile(picPaths[rnd.Next(picPaths.Count)]);
            Label lblLike = new Label();
            Label lblDislike = new Label();
            Size gameBtnSize = new Size(55, 55);
            lblLike.Text = "";
            lblDislike.Text = "";
            lblLike.BackColor = Color.Transparent;
            lblDislike.BackColor = Color.Transparent;
            lblLike.Cursor = Cursors.Hand;
            lblDislike.Cursor = Cursors.Hand;
            lblLike.Size = gameBtnSize;
            lblDislike.Size = gameBtnSize;
            lblDislike.Location = new Point(30, 250);
            lblLike.Location = new Point(155, 250);
            lblLike.Click += lblLike_Click;
            lblDislike.Click += lblDislike_Click;
            gamePanel.Controls.Add(lblLike);
            gamePanel.Controls.Add(lblDislike);
            
        }

        private void lblDislike_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void lblLike_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //Paikalliset muuttujat
        private bool mouseDown;
        private Point lastLocation;

        //Minimoi, maksimoi ja sulje nappien toiminnallisuus
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mm = new MainMenu();
            mm.ShowDialog();
            this.Close();
            
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            this.btnMaximize.Click -= btnMaximize_Click;
            this.btnMaximize.Click += btnNormalize_Click;
        }

        private void btnNormalize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            this.btnMaximize.Click -= btnNormalize_Click;
            this.btnMaximize.Click += btnMaximize_Click;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

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

    }
}
