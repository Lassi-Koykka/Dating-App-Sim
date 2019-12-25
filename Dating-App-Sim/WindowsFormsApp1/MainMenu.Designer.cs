namespace WindowsFormsApp1
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.dragPanel = new System.Windows.Forms.Panel();
            this.btnChangeBackground = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.dragPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Black;
            this.btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMinimize.FlatAppearance.BorderSize = 2;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Brown;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(151, 12);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(33, 31);
            this.btnMinimize.TabIndex = 5;
            this.btnMinimize.Text = "_";
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Maroon;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExit.FlatAppearance.BorderSize = 2;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Brown;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(186, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(33, 31);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = global::WindowsFormsApp1.Properties.Resources.Dating_sim_logo;
            this.picLogo.Location = new System.Drawing.Point(49, 49);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(123, 129);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 6;
            this.picLogo.TabStop = false;
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Black;
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnPlay.Font = new System.Drawing.Font("Noto Mono", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(29, 177);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(169, 35);
            this.btnPlay.TabIndex = 7;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.Black;
            this.btnHelp.Font = new System.Drawing.Font("Noto Mono", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.Color.White;
            this.btnHelp.Location = new System.Drawing.Point(29, 218);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(169, 35);
            this.btnHelp.TabIndex = 9;
            this.btnHelp.Text = "How to play";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // dragPanel
            // 
            this.dragPanel.BackColor = System.Drawing.Color.Transparent;
            this.dragPanel.Controls.Add(this.btnMinimize);
            this.dragPanel.Controls.Add(this.btnExit);
            this.dragPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dragPanel.Location = new System.Drawing.Point(0, 0);
            this.dragPanel.Name = "dragPanel";
            this.dragPanel.Size = new System.Drawing.Size(231, 55);
            this.dragPanel.TabIndex = 11;
            this.dragPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragPanel_MouseDown);
            this.dragPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dragPanel_MouseMove);
            this.dragPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dragPanel_MouseUp);
            // 
            // btnChangeBackground
            // 
            this.btnChangeBackground.BackColor = System.Drawing.Color.Black;
            this.btnChangeBackground.Font = new System.Drawing.Font("Noto Mono", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeBackground.ForeColor = System.Drawing.Color.White;
            this.btnChangeBackground.Location = new System.Drawing.Point(29, 259);
            this.btnChangeBackground.Name = "btnChangeBackground";
            this.btnChangeBackground.Size = new System.Drawing.Size(169, 60);
            this.btnChangeBackground.TabIndex = 10;
            this.btnChangeBackground.Text = "Background Color";
            this.btnChangeBackground.UseVisualStyleBackColor = false;
            this.btnChangeBackground.Click += new System.EventHandler(this.btnBackgroundChange_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(231, 361);
            this.Controls.Add(this.btnChangeBackground);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.dragPanel);
            this.Controls.Add(this.picLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.dragPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Panel dragPanel;
        private System.Windows.Forms.Button btnChangeBackground;
    }
}