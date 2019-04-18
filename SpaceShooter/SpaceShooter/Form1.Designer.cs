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
            this.Bullet_Timer = new System.Windows.Forms.Timer(this.components);
            this.Rock_Timer = new System.Windows.Forms.Timer(this.components);
            this.Mark_Label = new System.Windows.Forms.Label();
            this.MarkLogo_Label = new System.Windows.Forms.Label();
            this.Level_Label = new System.Windows.Forms.Label();
            this.LevelLogo_Label = new System.Windows.Forms.Label();
            this.Main_PictureBox = new System.Windows.Forms.PictureBox();
            this.AttackEnemy_Timer = new System.Windows.Forms.Timer(this.components);
            this.EnemyBullet_Timer = new System.Windows.Forms.Timer(this.components);
            this.AttackEnemyBullet_Timer = new System.Windows.Forms.Timer(this.components);
            this.Boss_Timer = new System.Windows.Forms.Timer(this.components);
            this.BossBullet_Timer = new System.Windows.Forms.Timer(this.components);
            this.LostBullet_Timer = new System.Windows.Forms.Timer(this.components);
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
            this.Start_Button.Location = new System.Drawing.Point(200, 260);
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
            this.Exit_Button.Location = new System.Drawing.Point(200, 310);
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
            // Bullet_Timer
            // 
            this.Bullet_Timer.Interval = 1;
            this.Bullet_Timer.Tick += new System.EventHandler(this.Bullet_Timer_Tick);
            // 
            // Rock_Timer
            // 
            this.Rock_Timer.Interval = 20;
            this.Rock_Timer.Tick += new System.EventHandler(this.Rock_Timer_Tick);
            // 
            // Mark_Label
            // 
            this.Mark_Label.AutoSize = true;
            this.Mark_Label.BackColor = System.Drawing.Color.Transparent;
            this.Mark_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.Mark_Label.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.Mark_Label.Location = new System.Drawing.Point(69, 9);
            this.Mark_Label.Name = "Mark_Label";
            this.Mark_Label.Size = new System.Drawing.Size(21, 24);
            this.Mark_Label.TabIndex = 29;
            this.Mark_Label.Text = "0";
            // 
            // MarkLogo_Label
            // 
            this.MarkLogo_Label.AutoSize = true;
            this.MarkLogo_Label.BackColor = System.Drawing.Color.Transparent;
            this.MarkLogo_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MarkLogo_Label.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.MarkLogo_Label.Location = new System.Drawing.Point(12, 9);
            this.MarkLogo_Label.Name = "MarkLogo_Label";
            this.MarkLogo_Label.Size = new System.Drawing.Size(61, 24);
            this.MarkLogo_Label.TabIndex = 28;
            this.MarkLogo_Label.Text = "Mark:";
            // 
            // Level_Label
            // 
            this.Level_Label.AutoSize = true;
            this.Level_Label.BackColor = System.Drawing.Color.Transparent;
            this.Level_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.Level_Label.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.Level_Label.Location = new System.Drawing.Point(464, 9);
            this.Level_Label.Name = "Level_Label";
            this.Level_Label.Size = new System.Drawing.Size(32, 24);
            this.Level_Label.TabIndex = 31;
            this.Level_Label.Text = "01";
            // 
            // LevelLogo_Label
            // 
            this.LevelLogo_Label.AutoSize = true;
            this.LevelLogo_Label.BackColor = System.Drawing.Color.Transparent;
            this.LevelLogo_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelLogo_Label.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.LevelLogo_Label.Location = new System.Drawing.Point(386, 9);
            this.LevelLogo_Label.Name = "LevelLogo_Label";
            this.LevelLogo_Label.Size = new System.Drawing.Size(80, 24);
            this.LevelLogo_Label.TabIndex = 30;
            this.LevelLogo_Label.Text = "LEVEL:";
            // 
            // Main_PictureBox
            // 
            this.Main_PictureBox.BackColor = System.Drawing.Color.White;
            this.Main_PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Main_PictureBox.Location = new System.Drawing.Point(2, 1);
            this.Main_PictureBox.MaximumSize = new System.Drawing.Size(500, 600);
            this.Main_PictureBox.Name = "Main_PictureBox";
            this.Main_PictureBox.Size = new System.Drawing.Size(500, 600);
            this.Main_PictureBox.TabIndex = 3;
            this.Main_PictureBox.TabStop = false;
            this.Main_PictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_PictureBox_MouseMove);
            // 
            // AttackEnemy_Timer
            // 
            this.AttackEnemy_Timer.Interval = 15;
            this.AttackEnemy_Timer.Tick += new System.EventHandler(this.AttackEnemy_Timer_Tick);
            // 
            // EnemyBullet_Timer
            // 
            this.EnemyBullet_Timer.Interval = 25;
            this.EnemyBullet_Timer.Tick += new System.EventHandler(this.EnemyBullet_Timer_Tick);
            // 
            // AttackEnemyBullet_Timer
            // 
            this.AttackEnemyBullet_Timer.Interval = 20;
            this.AttackEnemyBullet_Timer.Tick += new System.EventHandler(this.AttackEnemyBullet_Timer_Tick);
            // 
            // Boss_Timer
            // 
            this.Boss_Timer.Interval = 40;
            this.Boss_Timer.Tick += new System.EventHandler(this.Boss_Timer_Tick);
            // 
            // BossBullet_Timer
            // 
            this.BossBullet_Timer.Interval = 20;
            this.BossBullet_Timer.Tick += new System.EventHandler(this.BossBullet_Timer_Tick);
            // 
            // LostBullet_Timer
            // 
            this.LostBullet_Timer.Interval = 20;
            this.LostBullet_Timer.Tick += new System.EventHandler(this.LostBullet_Timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(503, 599);
            this.Controls.Add(this.Level_Label);
            this.Controls.Add(this.LevelLogo_Label);
            this.Controls.Add(this.Mark_Label);
            this.Controls.Add(this.MarkLogo_Label);
            this.Controls.Add(this.Exit_Button);
            this.Controls.Add(this.Start_Button);
            this.Controls.Add(this.Main_PictureBox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(519, 638);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Main_PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Star_Timer;
        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.Button Exit_Button;
        private System.Windows.Forms.PictureBox Main_PictureBox;
        private System.Windows.Forms.Timer Enemy_Timer;
        private System.Windows.Forms.Timer Bullet_Timer;
        private System.Windows.Forms.Timer Rock_Timer;
        private System.Windows.Forms.Label Mark_Label;
        private System.Windows.Forms.Label MarkLogo_Label;
        private System.Windows.Forms.Label Level_Label;
        private System.Windows.Forms.Label LevelLogo_Label;
        private System.Windows.Forms.Timer AttackEnemy_Timer;
        private System.Windows.Forms.Timer EnemyBullet_Timer;
        private System.Windows.Forms.Timer AttackEnemyBullet_Timer;
        private System.Windows.Forms.Timer Boss_Timer;
        private System.Windows.Forms.Timer BossBullet_Timer;
        private System.Windows.Forms.Timer LostBullet_Timer;
    }
}

