using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShooter
{
    public partial class Form1 : Form
    {
        Graphics gp;
        Bitmap bitmap;
        Random rd;
        Star[] ListStar = new Star[13];
        Emeny[] ListEmeny = new Emeny[10];
        Player MyPlayer;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(Main_PictureBox.Width, Main_PictureBox.Height);
            gp = Graphics.FromImage(bitmap);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            rd = new Random();
            for (int i = 0; i < ListStar.Length; i++)
            {
                ListStar[i] = new Star();
                ListStar[i].Location.X = rd.Next(0, 500);
                ListStar[i].Location.Y = rd.Next(100, 600);
                ListStar[i].StarType = rd.Next(0, 2);
            }

            for (int i = 0; i < ListEmeny.Length; i++)
            {
                ListEmeny[i] = new Emeny();
                ListEmeny[i].Location.X = rd.Next(0, 500);
                ListEmeny[i].Location.Y = rd.Next(-200, -100);
                ListEmeny[i].EmenyType = rd.Next(1, 3);
            }
        }

        private void StarTimer_Tick(object sender, EventArgs e)
        {
            rd = new Random();
            for (int i = 0; i < ListStar.Length; i++)
            {
                if (ListStar[i].StarType == 1)
                {
                    ListStar[i].Location.Y += 2;
                }
                else
                {
                    ListStar[i].Location.Y += 3;
                }
            }
            for (int i = 0; i < ListStar.Length; i++)
            {
                if (ListStar[i].Location.Y >= Main_PictureBox.Height)
                {
                    ListStar[i].Location.X = rd.Next(0, 500);
                    ListStar[i].Location.Y = 0;
                    ListStar[i].StarType = rd.Next(0, 2);
                }
            }
            Draw();
        }
        private void Emeny_Timer_Tick(object sender, EventArgs e)
        {
            rd = new Random();
            for (int i = 0; i < ListEmeny.Length; i++)
            {
                if (ListEmeny[i].EmenyType == 1 || ListEmeny[i].EmenyType == 2)
                {
                    ListEmeny[i].Location.Y += 3;
                }
                else
                {
                    ListEmeny[i].Location.Y += 5;
                }
            }
            for (int i = 0; i < ListEmeny.Length; i++)
            {
                if (ListEmeny[i].Location.Y >= Main_PictureBox.Height)
                {
                    ListEmeny[i].Location.X = rd.Next(0, 500);
                    ListEmeny[i].Location.Y = -100;
                    ListEmeny[i].EmenyType = rd.Next(0, 2);
                }
            }
        }
        public void Draw()
        {
            gp.Clear(Color.White);

            gp.FillRectangle(new SolidBrush(Color.FromArgb(255, (byte)0, (byte)0, (byte)56)), 0, 0, Main_PictureBox.Width, Main_PictureBox.Height);
            for (int i = 0; i < ListStar.Length; i++)
            {
                if (ListStar[i].StarType == 1)
                {
                    gp.DrawImage(Properties.Resources.Star1, ListStar[i].Location.X, ListStar[i].Location.Y);
                }
                else
                {
                    if (ListStar[i].StarType == 0 || ListStar[i].StarType == 2)
                        gp.DrawImage(Properties.Resources.Star2, ListStar[i].Location.X, ListStar[i].Location.Y);
                }
            }
            for (int i = 0; i < ListEmeny.Length; i++)
            {
                if (ListEmeny[i].EmenyType == 1)
                {
                    gp.DrawImage(Properties.Resources.enemyBlack, ListEmeny[i].Location.X, ListEmeny[i].Location.Y);
                }
                else
                {
                    if (ListEmeny[i].EmenyType == 2)
                    {
                        gp.DrawImage(Properties.Resources.enemyGreen, ListEmeny[i].Location.X, ListEmeny[i].Location.Y);
                    }
                    else
                    {
                        gp.DrawImage(Properties.Resources.enemyRed, ListEmeny[i].Location.X, ListEmeny[i].Location.Y);
                    }
                }
            }
            if (MyPlayer != null)
            {
                gp.DrawImage(Properties.Resources.Player, MyPlayer.Location);
            }
            Main_PictureBox.Image = bitmap;
        }
        private void Main_PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (MyPlayer != null)
            {
                MyPlayer.Location = e.Location;
            }
        }
        private void Start_Button_Click(object sender, EventArgs e)
        {
            Start_Button.Hide();
            Exit_Button.Hide();
            StarTimer.Start();
            Cursor.Hide();
            Emeny_Timer.Start();
            MyPlayer = new Player();
            MyPlayer.Location.X = 230;
            MyPlayer.Location.Y = 530;
        }
        private void Exit_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public class Star
        {
            public PointF Location;
            public int StarType;
        }
        public class Emeny
        {
            public PointF Location;
            public int EmenyType;
            public Bullet EmenyBullet;
        }
        public class Player
        {
            public PointF Location;
            public Bullet MyBullet;
        }
        public class Bullet
        {
            public PointF Location;
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                StarTimer.Stop();
            }
        }  
    }
}
