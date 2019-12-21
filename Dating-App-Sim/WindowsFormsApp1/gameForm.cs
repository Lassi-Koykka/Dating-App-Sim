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

        //GUI komponentit
        Font fontti = new Font("Noto Mono", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        //Pelin muuttujat
        Random rnd = new Random();
        List<string> picPaths = new List<string>();
        const int TimeLimit = 60000;
        Timer t = new Timer();
        Pelaaja p = new Pelaaja();

        //Ikkunan liikuttamiseen tarvittavat muuttujat
        private bool mouseDown;
        private Point lastLocation;


        private void ListPics()
        {
            picPaths = Directory.GetFiles(Directory.GetCurrentDirectory().Replace(@"\bin\Debug", @"\pics")).ToList();

        }

        private void changePic()
        {
            Panel gamePanel = (Panel)Controls[Controls.IndexOfKey("gamePanel")];
            gamePanel.BackgroundImage = Image.FromFile(picPaths[rnd.Next(picPaths.Count)]);
        }

        //Resetoidaan peli
        private void ResetGame()
        {
            //Luodaan formin keskelle pelialue paneelin muodossa ja siihen lisätään alkuun nimilaatikko ja aloitusnappi
            Panel gamePanel = new Panel();
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(240, 320);
            gamePanel.Location = new Point((Width / 2 - gamePanel.Width / 2), (Height / 2 - gamePanel.Height / 2));
            gamePanel.Anchor = AnchorStyles.None;
            Controls.Add(gamePanel);
            
            TextBox txtName = new TextBox();
            txtName.Font = fontti;
            txtName.Size = new Size(200, 35);
            txtName.Location = new Point(120 - txtName.Width / 2, 130);
            txtName.TabIndex = 1;
            txtName.Text = "Enter your name";
            txtName.ForeColor = Color.DarkGray;
            txtName.KeyPress += TxtName_KeyPress;
            txtName.Enter += TxtName_Enter;
            txtName.TextChanged += TxtName_TextChanged;
            gamePanel.Controls.Add(txtName);

            Button btnStartGame = new Button();
            btnStartGame.Enabled = false;
            btnStartGame.Name = "btnStartGame";
            btnStartGame.BackColor = Color.Gray;
            btnStartGame.Font = fontti;
            btnStartGame.ForeColor = Color.White;
            btnStartGame.Size = new Size(169, 35);
            btnStartGame.Location = new Point(120 - btnStartGame.Width / 2, 160);
            btnStartGame.TabIndex = 2;
            btnStartGame.Text = "Start";
            btnStartGame.Click += BtnStartGame_Click;
            gamePanel.Controls.Add(btnStartGame);
            Update();



        }

        //KeyPress tapahtuma nimi tekstikentässä joka varmistaa että käyttäjä ei lisää erikoismerkkejä tai välilyöntejä
        private void TxtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        //Poistaa placeholder tekstin tekstilaatikosta
        private void TxtName_Enter(object sender, EventArgs e)
        {
            TextBox txtName = (TextBox)sender;

            txtName.Text = "";
            txtName.ForeColor = default;

        }

        //Tarkistaa että tekstikenttä ei ole tyhjä ja tekee start napista tällöin käytettävän
        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            Panel gamePanel = (Panel)Controls[Controls.IndexOfKey("gamePanel")];
            TextBox txtName = (TextBox)sender;
            Button btnStartGame = (Button)gamePanel.Controls[gamePanel.Controls.IndexOfKey("btnStartGame")];
            try
            {
                if (String.IsNullOrWhiteSpace(txtName.Text))
                {
                    throw new Exception("The name Textbox is empty");
                }
                else
                {
                    btnStartGame.BackColor = Color.Black;
                    btnStartGame.Enabled = true;
                }
            }
            catch
            {
                btnStartGame.BackColor = Color.Gray;
                btnStartGame.Enabled = false;
            }
        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            Panel gamePanel = (Panel)Controls[Controls.IndexOfKey("gamePanel")];
            gamePanel.Controls.Clear();
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

            //Ajastin peliä varten
            t.Interval = 1000;


        }

        //Pelin kontrollit ja niihin liittyvä tapahtumankäsittely jossa lisätään pisteitä tai laitetaan timer cooldownille
        private void lblLike_Click(object sender, EventArgs e)
        {
            Panel gamePanel = (Panel)Controls[Controls.IndexOfKey("gamePanel")];
            if (true)
            {

            }
        }

        private void lblDislike_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

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
