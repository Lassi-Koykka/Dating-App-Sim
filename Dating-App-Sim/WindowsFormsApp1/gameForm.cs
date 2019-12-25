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
        Color backgroundColor;

        //Pelin muuttujat
        Random rnd = new Random();
        List<string> picPaths = new List<string>();
        List<Image> pics = new List<Image>();
        const int TimeLimit = 30000;
        int timePassed;
        int timeLeft;
        Timer t = new Timer();
        Timer cooldownTimer = new Timer();
        int CooldownTicks;
        int score = 0;
        Player p = new Player();

        //Ikkunan liikuttamiseen tarvittavat muuttujat
        private bool mouseDown;
        private Point lastLocation;
        
        #region Pelin resetoiminen ja aloitus       
        //Resetoidaan peli
        private void ResetGame()
        {
            t.Dispose();
            cooldownTimer.Dispose();
            Player p = new Player();
            timePassed = 0;
            timeLeft = 0;
            score = 0;
            CooldownTicks = 0;
            

            updateCurrentScore();

            //Luodaan formin keskelle pelialue paneelin muodossa ja siihen lisätään alkuun nimilaatikko ja aloitusnappi
            Panel gamePanel = new Panel();
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(240, 320);
            gamePanel.Location = new Point((Width / 2 - gamePanel.Width / 2), (Height / 2 - gamePanel.Height / 2));
            gamePanel.Anchor = AnchorStyles.None;
            gamePanel.BackgroundImageLayout = ImageLayout.Stretch;
            gamePanel.BackColor = Color.Transparent;
            Controls.Add(gamePanel);
            
            TextBox txtName = new TextBox();
            txtName.Name = "txtName";
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

            //Ajastimet peliä varten
            t.Interval = 1000;
            t.Tick += T_Tick;
            cooldownTimer.Interval = 1000;
            cooldownTimer.Tick += CooldownTimer_Tick;
            Label lblTime = new Label();
            lblTime.Text = $"Time: {(TimeLimit - timePassed) / 1000}";
            lblTime.ForeColor = Color.Red;
            lblTime.AutoSize = true;
            lblTime.Anchor = AnchorStyles.None;
            lblTime.Font = fontti;
            lblTime.Name = "lblTime";
            lblTime.Location = new Point(gamePanel.Location.X + gamePanel.Width / 4, gamePanel.Location.Y + gamePanel.Height + 20);
            lblTime.Visible = false;
            Controls.Add(lblTime);

            updateHighScores();

            Update();

        }
        //Aloitetaan peli
        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            Panel gamePanel = (Panel)Controls["gamePanel"];
            //Asetetaan pelaajan syöttämä nimi Pelaaja objektille.
            p.Name = gamePanel.Controls["txtName"].Text;
            updateCurrentScore();
            //Lisätään kontrollit
            gamePanel.Controls.Clear();
            changePic();
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
            //eventit controlleihin
            lblLike.Click += lblLike_Click;
            lblDislike.Click += lblDislike_Click;
            gamePanel.KeyDown += GamePanel_KeyDown; ;
            gamePanel.Controls.Add(lblLike);
            gamePanel.Controls.Add(lblDislike);
            //Tehdään ajastimesta näkyvä ja käynnistetään se
            Controls["lblTime"].Visible = true;
            t.Start();

        }
        #endregion

        #region Pisteiden päivitys

        //Päivitetään nykyisen pelin pistetilanne statusbarin labeliin
        private void updateCurrentScore()
        {
            if (!String.IsNullOrWhiteSpace(p.Name))
            {
                statusScore.Items["lblCurrentScore"].Text = $"Current score of {p.Name}: {score}";
            }
            else
            {
                statusScore.Items["lblCurrentScore"].Text = $"Current score: {score}";
            }
        }

        //Yritetään lukea edelliset ennätykset Kahteen Labeliin mikäli kyseinen tiedosto on olemassa
        private void updateHighScores()
        {
            try
            {
                Button btnStartGame = (Button)Controls["gamePanel"].Controls["btnStartGame"];
                List<Player> HighScores = readHighScores();
                statusScore.Items["lblHighScore"].Text ="Current best " + HighScores[0].ToString();
                int shownTopScores;
                Label lblHighScores = new Label();
                lblHighScores.Font = new Font("Noto Mono", 12.25F, FontStyle.Bold);
                lblHighScores.AutoSize = true;
                lblHighScores.Text = "High scores:\n";
                lblHighScores.Anchor = AnchorStyles.None;
                lblHighScores.ForeColor = Color.DarkGoldenrod;
                lblHighScores.Location = new Point(btnStartGame.Location.X, btnStartGame.Location.Y + btnStartGame.Height + 5);
                Controls["gamePanel"].Controls.Add(lblHighScores);
                if (HighScores.Count > 5)
                {
                    shownTopScores = 5;
                }
                else
                {
                    shownTopScores = HighScores.Count;
                }
                for (int i = 0; i < shownTopScores; i++)
                {
                    lblHighScores.Text += $"{i + 1}. {HighScores[i].Name}: {HighScores[i].Score}\n";
                }
            }
            catch
            {
                statusScore.Items["lblHighScore"].Text = "High Score file not found";

            }
        }

        //etsitään nykyinen ennätys statusbarin labeliin lukemalla tiedostosta tulokset ja järjestelemällä ne
        private List<Player> readHighScores()
        {
            string[] scoreTextArray = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\dating-app-sim_Files\Top_Scores.txt");
            List<Player> scores = new List<Player>();
            foreach (var score in scoreTextArray)
            {
                string[] plr = score.Split(':');
                scores.Add(new Player(plr[0].Trim(), int.Parse(plr[1].Trim())));
            }
            return scores.OrderByDescending(p => p.Score).ToList();
        }
        #endregion

        #region kuvat
        //etsii kansiossa olevien kuvien osoitteet ja tekee sitten niistä listan Image objekteja
        private void ListPics()
        {
            picPaths = Directory.GetFiles(Directory.GetCurrentDirectory().Replace(@"\bin\Debug", @"\pics")).ToList();
            foreach(var path in picPaths)
            {
                pics.Add(Image.FromFile(path));
            }

        }
        //tarkista onko kuva sama kuin edellinen ja päivitä se
        private void changePic()
        {
            Panel gamePanel = (Panel)Controls["gamePanel"];
            Image tempImage;
            do
            {
                tempImage = pics[rnd.Next(pics.Count)];
            } while (tempImage == gamePanel.BackgroundImage);
            gamePanel.BackgroundImage = tempImage;
        }
        #endregion

        #region Nimi textboxin eventHandlerit

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
            Panel gamePanel = (Panel)Controls["gamePanel"];
            TextBox txtName = (TextBox)sender;
            Button btnStartGame = (Button)gamePanel.Controls["btnStartGame"];
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

        #endregion 

        #region Pelin ajastimien Tick eventit ja ajan päivitys


        //Pelin ajastimen tick event jossa vähennetään jäljellä olevasta ajasta ja lopetetaan peli kun aika loppuu
        //Tämän jälkeen kysytään haluaako pelaaja pelata uudellee
        private void T_Tick(object sender, EventArgs e)
        {
            if (timePassed < TimeLimit)
            {
                timePassed = timePassed + 1000;
            }
            else
            {
                t.Stop();
                p.Score = score;
                p.saveScore();
                DialogResult dr = MessageBox.Show($"The time ran out\nYour score was {score} points!\n\nWould you like to play again?", "Game over!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    Controls.RemoveByKey("gamePanel");
                    t.Tick -= T_Tick;
                    cooldownTimer.Tick -= CooldownTimer_Tick;
                    Controls["lblTime"].Visible = false;
                    ResetGame();
                }
                else
                {
                    this.Hide();
                    MainMenu mm = new MainMenu();
                    mm.ShowDialog();
                    this.Close();
                }
            }
            updateTimeLeft();
        }
        private void updateTimeLeft()
        {
            Label lblTime = (Label)Controls["lblTime"];
            timeLeft = TimeLimit - timePassed;
            lblTime.Text = "Time: " + timeLeft/1000;
        }
        //CooldownTimerin Tick event
        private void CooldownTimer_Tick(object sender, EventArgs e)
        {
            Panel gamePanel = (Panel)Controls["gamePanel"];
            CooldownTicks++;
            if(CooldownTicks >= 3)
            {
                gamePanel.Visible = true;
                gamePanel.Focus();
                cooldownTimer.Stop();
                CooldownTicks = 0;

            }
        }

        #endregion

        #region pelin kontrollit

        //Pelin kontrollit ja niihin liittyvä tapahtumankäsittely jossa lisätään pisteitä
        private void lblLike_Click(object sender, EventArgs e)
        {
            Panel gamePanel = (Panel)Controls["gamePanel"];
            if (gamePanel.BackgroundImage != pics[0] && gamePanel.BackgroundImage != pics[1] && gamePanel.BackgroundImage != pics[2] && gamePanel.BackgroundImage != pics[3])
            {
                score++;
                updateCurrentScore();
                changePic();
            }
            else
            {
                cooldownTimer.Start();
                gamePanel.Visible = false;
                MessageBox.Show("Oops! You liked an ad and activated a 3 second cooldown");
                score--;
                updateCurrentScore();
                changePic();
            }
        }

        private void lblDislike_Click(object sender, EventArgs e)
        {
            Panel gamePanel = (Panel)Controls["gamePanel"];
            
            if(gamePanel.BackgroundImage != pics[0] && gamePanel.BackgroundImage != pics[1] && gamePanel.BackgroundImage != pics[2] && gamePanel.BackgroundImage != pics[3])
            {
                score--;
                updateCurrentScore();
                changePic();
            }
            else
            {
                changePic();
            }
        }
        private void GamePanel_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                lblLike_Click(sender, e);
            }
            else if(e.KeyCode == Keys.Left)
            {
                lblDislike_Click(sender, e);
            }
        }

        #endregion


        #region Ikkunan raahaamiseen ja maksimoi, minimoi ja sulje nappien toiminnallisuus 

        //Minimoi, maksimoi ja sulje nappien toiminnallisuus
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mm = new MainMenu();
            mm.BackColor = BackColor;
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
        #endregion
    }
}
