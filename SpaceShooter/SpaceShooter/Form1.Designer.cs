namespace SpaceShooter
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.Star_Timer = new System.Windows.Forms.Timer(this.components);
            this.Start_Button = new System.Windows.Forms.Button();
            this.Exit_Button = new System.Windows.Forms.Button();
            this.Enemy_Timer = new System.Windows.Forms.Timer(this.components);
            this.Rock_Timer = new System.Windows.Forms.Timer(this.components);
            this.Main_PictureBox = new System.Windows.Forms.PictureBox();
            this.Bullet_Timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Main_PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Star_Timer
            // 
            this.Star_Timer.Enabled = true;
            this.Star_Timer.Interval = 1;
            this.Star_Timer.Tick += new System.EventHandler(this.StarTimer_Tick);
            // 
            // Start_Button
            // 
            this.Start_Button.BackColor = System.Drawing.Color.RoyalBlue;
            this.Start_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Start_Button.Location = new System.Drawing.Point(200, 310);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(93, 44);
            this.Start_Button.TabIndex = 1;
            this.Start_Button.Text = "Start";
            this.Start_Button.UseVisualStyleBackColor = false;
            this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
            // 
            // Exit_Button
            // 
            this.Exit_Button.BackColor = System.Drawing.Color.Red;
            this.Exit_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Exit_Button.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit_Button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Exit_Button.Location = new System.Drawing.Point(200, 360);
            this.Exit_Button.Name = "Exit_Button";
            this.Exit_Button.Size = new System.Drawing.Size(93, 40);
            this.Exit_Button.TabIndex = 2;
            this.Exit_Button.Text = "Exit";
            this.Exit_Button.UseVisualStyleBackColor = false;
            this.Exit_Button.Click += new System.EventHandler(this.Exit_Button_Click);
            // 
            // Enemy_Timer
            // 
            this.Enemy_Timer.Interval = 20;
            this.Enemy_Timer.Tick += new System.EventHandler(this.Enemy_Timer_Tick);
            // 
            // Rock_Timer
            // 
            this.Rock_Timer.Interval = 30;
            this.Rock_Timer.Tick += new System.EventHandler(this.Rock_Timer_Tick);
            // 
            // Main_PictureBox
            // 
            this.Main_PictureBox.BackColor = System.Drawing.Color.White;
            this.Main_PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Main_PictureBox.Location = new System.Drawing.Point(0, 0);
            this.Main_PictureBox.MaximumSize = new System.Drawing.Size(500, 600);
            this.Main_PictureBox.Name = "Main_PictureBox";
            this.Main_PictureBox.Size = new System.Drawing.Size(500, 600);
            this.Main_PictureBox.TabIndex = 3;
            this.Main_PictureBox.TabStop = false;
            this.Main_PictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_PictureBox_MouseMove);
            // 
            // Bullet_Timer
            // 
            this.Bullet_Timer.Interval = 2;
            this.Bullet_Timer.Tick += new System.EventHandler(this.Bullet_Timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(503, 599);
            this.Controls.Add(this.Exit_Button);
            this.Controls.Add(this.Start_Button);
            this.Controls.Add(this.Main_PictureBox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(519, 638);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Main_PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Star_Timer;
        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.Button Exit_Button;
        private System.Windows.Forms.PictureBox Main_PictureBox;
        private System.Windows.Forms.Timer Enemy_Timer;
        private System.Windows.Forms.Timer Rock_Timer;
        private System.Windows.Forms.Timer Bullet_Timer;
    }
}

