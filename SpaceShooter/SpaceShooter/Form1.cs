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
        Enemy[] ListEnemy = new Enemy[6];
        Player MyPlayer;
        Rock MyRock = new Rock();
        bool IsStart = false;
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

            for (int i = 0; i < ListEnemy.Length; i++)
            {
                ListEnemy[i] = new Enemy();
                ListEnemy[i].Location.X = rd.Next(0, 500);
                ListEnemy[i].Location.Y = rd.Next(-200, -100);
                ListEnemy[i].EnemyType = rd.Next(1, 3);
            }
            MyRock.Location.X = rd.Next(0, 500);
            MyRock.Location.Y = rd.Next(-200, -100);

            MyPlayer = new Player();
            MyPlayer.Location.X = 230;
            MyPlayer.Location.Y = 530;
            MyPlayer.MyBullet = new Bullet();
            MyPlayer.MyBullet.Location = MyPlayer.Location;
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
        private void Rock_Timer_Tick(object sender, EventArgs e)
        {
            rd = new Random();
            MyRock.Location.Y += 7;
            if (MyRock.Location.Y > Main_PictureBox.Height)
            {
                MyRock.Location.X = rd.Next(20, 440);
                MyRock.Location.Y = -500;
            }
        }
        private void Enemy_Timer_Tick(object sender, EventArgs e)
        {
            rd = new Random();
            for (int i = 0; i < ListEnemy.Length; i++)
            {
                if (ListEnemy[i].EnemyType == 1 || ListEnemy[i].EnemyType == 2)
                {
                    ListEnemy[i].Location.Y += 3;
                }
                else
                {
                    ListEnemy[i].Location.Y += 5;
                }
            }
            for (int i = 0; i < ListEnemy.Length; i++)
            {
                if (ListEnemy[i].Location.Y >= Main_PictureBox.Height)
                {
                    ListEnemy[i].Location.X = rd.Next(0, 500);
                    ListEnemy[i].Location.Y = -100;
                    ListEnemy[i].EnemyType = rd.Next(0, 2);
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
                    gp.DrawImage(Properties.Resources.Star2, ListStar[i].Location.X, ListStar[i].Location.Y);
                }
                else
                {
                    if (ListStar[i].StarType == 0 || ListStar[i].StarType == 2)
                        gp.DrawImage(Properties.Resources.Star1, ListStar[i].Location.X, ListStar[i].Location.Y);
                }
            }
            //if (IsStart == false)
            //{
            //    gp.DrawImage(Properties.Resources.Brand1, new PointF(150, 90));
            //}
            for (int i = 0; i < ListEnemy.Length; i++)
            {
                if (ListEnemy[i].EnemyType == 1)
                {
                    gp.DrawImage(Properties.Resources.EnemyBlack, ListEnemy[i].Location.X, ListEnemy[i].Location.Y);
                }
                else
                {
                    if (ListEnemy[i].EnemyType == 2)
                    {
                        gp.DrawImage(Properties.Resources.EnemyGreen, ListEnemy[i].Location.X, ListEnemy[i].Location.Y);
                    }
                    else
                    {
                        gp.DrawImage(Properties.Resources.EnemyRed, ListEnemy[i].Location.X, ListEnemy[i].Location.Y);
                    }
                }
            }
            gp.DrawImage(Properties.Resources.Rock, MyRock.Location);
            if (IsStart==true)
            {
                gp.DrawImage(Properties.Resources.Player, MyPlayer.Location);
                gp.DrawImage(Properties.Resources.laserBlue01, MyPlayer.MyBullet.Location);
            }

           
            Main_PictureBox.Image = bitmap;
            
            //this.Invalidate();
            //Application.DoEvents();
            //System.Threading.Thread.Sleep(1);
        }
        public void KiemTra()
        {
            for (int i = 0; i < ListEnemy.Length; i++)
            {
                if (KiemTraBanTrung(MyPlayer.MyBullet.Location,ListEnemy[i].Location)==true)
                {
                    ListEnemy[i].Location.X = rd.Next(0, 500);
                    ListEnemy[i].Location.Y = -100;
                    ListEnemy[i].EnemyType = rd.Next(0, 2);
                    MyPlayer.MyBullet.Location = MyPlayer.Location;
                    return;
                }s
            }
        }
        public bool KiemTraBanTrung(PointF a,PointF b)
        {
            if (a.X <= b.X + 30 && a.X+7 >= b.X)
            {
                return true;
            }
            return false;
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
            Star_Timer.Start();
            IsStart = true;
            Cursor.Hide();
            Enemy_Timer.Start();
            Rock_Timer.Start();
            Bullet_Timer.Start();

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
        public class Enemy
        {
            public PointF Location;
            public int EnemyType;
            public Bullet EnemyBullet;
        }
        public class Player
        {
            public PointF Location;
            public Bullet MyBullet;
            public int Mark;
            public int Level;
        }
        public class Bullet
        {
            public PointF Location;
            
        }
        public class Rock
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
                Star_Timer.Stop();
            }
        }

        private void Bullet_Timer_Tick(object sender, EventArgs e)
        {
            MyPlayer.MyBullet.Location.Y -= 15;
            if (MyPlayer.MyBullet.Location.Y < 0)
            {
                MyPlayer.MyBullet.Location = MyPlayer.Location;
            }
            KiemTra();
        }
    }
}
