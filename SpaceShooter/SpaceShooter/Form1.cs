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
        Star[] ListStar;
        List<Enemy> ListEnemy;
        List<AttackEnemy> ListAttackEnemy;
        Player MyPlayer;
        Rock MyRock;
        bool IsStart = false;
        bool Pause = false;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(Main_PictureBox.Width, Main_PictureBox.Height);
            gp = Graphics.FromImage(bitmap);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            KhoiTao();
        }
        private void KhoiTao()
        {
            rd = new Random();
            ListStar = new Star[13];
            MyRock = new Rock();
            Mark_Label.Parent = Main_PictureBox;
            MarkLogo_Label.Parent = Main_PictureBox;
            LevelLogo_Label.Parent = Main_PictureBox;
            Level_Label.Parent = Main_PictureBox;
            LevelLogo_Label.Hide();
            Level_Label.Hide();
            Mark_Label.Hide();
            MarkLogo_Label.Hide();

            MyRock.Location.X = rd.Next(0, 450);
            MyRock.Location.Y = rd.Next(-200, -100);
            MyRock.Type = 1;

            MyPlayer = new Player();
            MyPlayer.Location.X = 230;
            MyPlayer.Location.Y = 530;
            MyPlayer.Mark = 0;
            MyPlayer.Level = 1;

            MyPlayer.MyBullet = new List<Bullet>();
            Bullet TempBullet = new Bullet();
            TempBullet.Location = MyPlayer.Location;
            TempBullet.Location.Y -= 80;
            TempBullet.Location.X += 17;
            MyPlayer.MyBullet.Add(TempBullet);

            for (int i = 0; i < ListStar.Length; i++)
            {
                ListStar[i] = new Star();
                ListStar[i].Location.X = rd.Next(0, 500);
                ListStar[i].Location.Y = rd.Next(100, 600);
                ListStar[i].StarType = rd.Next(0, 2);
            }

            ListEnemy = new List<Enemy>();
            KhoiTaoEnemy();

            ListAttackEnemy = new List<AttackEnemy>();
            KhoiTaoAttackEnemy();
        }
       
        private void StarTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ListStar.Length; i++)
            {
                if (ListStar[i].StarType == 1)
                {
                    ListStar[i].Location.Y += 1;
                }
                else
                {
                    ListStar[i].Location.Y += 2;
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
            if (MyRock.Type == 1)
            {
                MyRock.Location.Y += MyPlayer.Level+3;
            }
            else
            {
                MyRock.Location.X += MyRock.DeltaX;
                MyRock.Location.Y = MyRock.a * MyRock.Location.X + MyRock.b;
            }
            if (MyRock.Location.Y > Main_PictureBox.Height+10)
            {
                MyRock.Location.X = rd.Next(20, 440);
                MyRock.Location.Y = -300;
                MyRock.Type = rd.Next(1, 6);
                if (MyRock.Type != 1)
                {
                    int TempType = rd.Next(0, 4);
                    PointF Temp = new PointF();
                    Temp.Y = rd.Next(Main_PictureBox.Height, Main_PictureBox.Height+50);
                    if (TempType == 1)
                    {
                        Temp.X = rd.Next(25,75);
                    }
                    else
                    {
                        Temp.X = rd.Next(410,450);
                    }
                    if (MyRock.Location.X - Temp.X == 0)
                    {
                        Temp.X += (float)1;
                    }
                   MyRock.a = (MyRock.Location.Y - Temp.Y) / (MyRock.Location.X - Temp.X);
                   MyRock.b = MyRock.Location.Y - (MyRock.a * MyRock.Location.X);
                   MyRock.DeltaX = (Temp.X - MyRock.Location.X)/400;
                }
            }
        }
        private void AttackEnemy_Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ListAttackEnemy.Count; i++)
            {
                ListAttackEnemy[i].Location.X += ListAttackEnemy[i].DeltaX;
                ListAttackEnemy[i].Location.Y = (ListAttackEnemy[i].a * ListAttackEnemy[i].Location.X * ListAttackEnemy[i].Location.X) + (ListAttackEnemy[i].b * ListAttackEnemy[i].Location.X) + ListAttackEnemy[i].c;

                if (ListAttackEnemy[i].EnemyType == 1)
                {
                    if (ListAttackEnemy[i].Location.X > Main_PictureBox.Width+100)
                    {
                        ListAttackEnemy.RemoveAt(i);
                    }
                }
                else
                {
                    if (ListAttackEnemy[i].EnemyType == 2)
                    {
                        if (ListAttackEnemy[i].Location.X < -100)
                        {
                            ListAttackEnemy.RemoveAt(i);
                        }
                    }
                }
            }
            if (ListAttackEnemy.Count == 0)
            {
                KhoiTaoAttackEnemy();
            }
            //for (int i = 0; i < ListAttackEnemy.Count; i++)
            //{
            //    if (ListAttackEnemy[i].Location.Y > 0)
            //    {
            //        if (MyPlayer.Location.X <= ListAttackEnemy[i].Location.X + 80 && MyPlayer.Location.X + 80 >= ListAttackEnemy[i].Location.X && MyPlayer.Location.Y <= ListAttackEnemy[i].Location.Y - 2)
            //        {
            //            GameOver();
            //            return;
            //        }
            //    }
            //}
        }
        private void Enemy_Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                if (ListEnemy[i].EnemyType == 1 || ListEnemy[i].EnemyType == 2)
                {
                    ListEnemy[i].Location.Y += MyPlayer.Level;
                }
                else
                {
                    ListEnemy[i].Location.Y += MyPlayer.Level + 1;
                }
                if (ListEnemy[i].Location.Y >= Main_PictureBox.Height)
                {
                    ListEnemy.RemoveAt(i);
                }
            }
            if(ListEnemy.Count == 0)
            {
                KhoiTaoEnemy();
            }
            
        }
        private void EnemyBullet_Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                for (int j = 0; j < ListEnemy[i].EnemyBullet.Count; j++)
                {
                    ListEnemy[i].EnemyBullet[j].Location.Y += 5;
                    if (ListEnemy[i].EnemyBullet[j].Location.Y > Main_PictureBox.Height)
                    {
                        ListEnemy[i].EnemyBullet[j].Location = ListEnemy[i].Location;
                        ListEnemy[i].EnemyBullet[j].Location.X += 15;
                    }
                }
            }
          
        }
        private void Bullet_Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < MyPlayer.MyBullet.Count; i++)
            {
                MyPlayer.MyBullet[i].Location.Y -= 15;
                if (MyPlayer.MyBullet[i].Location.Y < 0)
                {
                    MyPlayer.MyBullet[i].Location = MyPlayer.Location;
                    MyPlayer.MyBullet[i].Location.X += 17;
                }
                KiemTra();
            }
        }

       
        private void KhoiTaoAttackEnemy()
        {
            for (int i = 0; i < MyPlayer.Level; i++)
            {
                AttackEnemy TempEnemy = new AttackEnemy();
                InitalizeAttackEnemy(ref TempEnemy);
                ListAttackEnemy.Add(TempEnemy);
            }
            for (int i = 0; i < ListAttackEnemy.Count; i++)
            {
               // if (rd.Next(0, 3) != 1)
                {
                    Bullet NewBullet = new Bullet();
                    NewBullet.Location = ListAttackEnemy[i].Location;
                    NewBullet.Location.X += 15;

                    NewBullet.a = (MyPlayer.Location.Y - NewBullet.Location.Y) / (MyPlayer.Location.X - NewBullet.Location.X);
                    NewBullet.b = NewBullet.Location.Y - (NewBullet.a * NewBullet.Location.X);
                    NewBullet.DeltaX = (MyPlayer.Location.X - NewBullet.Location.X) / 50;
                    NewBullet.Times = 0;
                    ListAttackEnemy[i].EnemyBullet.Add(NewBullet);
                }
            }
        }
        private void AttackEnemyBullet_Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ListAttackEnemy.Count; i++)
            {
                for (int j = 0; j < ListAttackEnemy[i].EnemyBullet.Count; j++)
                {
                    ListAttackEnemy[i].EnemyBullet[j].Location.X += ListAttackEnemy[i].EnemyBullet[j].DeltaX;
                    ListAttackEnemy[i].EnemyBullet[j].Location.Y = (ListAttackEnemy[i].EnemyBullet[j].Location.X * ListAttackEnemy[i].EnemyBullet[j].a) + ListAttackEnemy[i].EnemyBullet[j].b;
                    ListAttackEnemy[i].EnemyBullet[j].Times++;
                    if (ListAttackEnemy[i].EnemyBullet[j].Times == 10)
                    {
                        ListAttackEnemy[i].EnemyBullet[j].Times = 0;
                        if (MyPlayer.Location.Y - ListAttackEnemy[i].EnemyBullet[j].Location.Y == 0)
                        {
                            ListAttackEnemy[i].EnemyBullet[j].Location.Y += 2;
                        }
                        if (MyPlayer.Location.X - ListAttackEnemy[i].EnemyBullet[j].Location.X == 0)
                        {
                            ListAttackEnemy[i].EnemyBullet[j].Location.X += 2;
                        }
                        ListAttackEnemy[i].EnemyBullet[j].a = (MyPlayer.Location.Y+35- ListAttackEnemy[i].EnemyBullet[j].Location.Y) / (MyPlayer.Location.X + 15 - ListAttackEnemy[i].EnemyBullet[j].Location.X);
                        ListAttackEnemy[i].EnemyBullet[j].b = ListAttackEnemy[i].EnemyBullet[j].Location.Y - (ListAttackEnemy[i].EnemyBullet[j].a * ListAttackEnemy[i].EnemyBullet[j].Location.X);
                        ListAttackEnemy[i].EnemyBullet[j].DeltaX = (MyPlayer.Location.X + 15 - ListAttackEnemy[i].EnemyBullet[j].Location.X) / 30;
                    }
                    //if (/*ListAttackEnemy[i].EnemyBullet[j].Location.X < 0 || ListAttackEnemy[i].EnemyBullet[j].Location.X > Main_PictureBox.Width || ListAttackEnemy[i].EnemyBullet[j].Location.Y < 0 ||*/ ListAttackEnemy[i].EnemyBullet[j].Location.Y > Main_PictureBox.Height)
                    //{
                    //    ListAttackEnemy[i].EnemyBullet[j].Times = 0;
                    //    Bullet NewBullet = new Bullet();
                    //    NewBullet.Location = ListAttackEnemy[i].Location;
                    //    NewBullet.Location.X += 15;
                    //    if (MyPlayer.Location.Y - NewBullet.Location.Y == 0)
                    //    {
                    //        NewBullet.Location.Y += 2;
                    //    }
                    //    if (MyPlayer.Location.X - NewBullet.Location.X == 0)
                    //    {
                    //        NewBullet.Location.X += 2;
                    //    }
                    //    NewBullet.a = (MyPlayer.Location.Y - NewBullet.Location.Y) / (MyPlayer.Location.X - NewBullet.Location.X);
                    //    NewBullet.b = NewBullet.Location.Y - (NewBullet.a * NewBullet.Location.X);
                    //    NewBullet.DeltaX = (MyPlayer.Location.X - NewBullet.Location.X) / 50;
                    //}
                }
            }
        }
        
        private void KhoiTaoEnemy()
        {
            for (int i = 0; i < 2 + MyPlayer.Level; i++)
            {
                Enemy TempEnemy = new Enemy();
                InitalizeEnemy(ref TempEnemy);
                ListEnemy.Add(TempEnemy);
            }
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                if (rd.Next(0, 3)!=1)
                {
                    Bullet NewBullet = new Bullet();
                    NewBullet.Location = ListEnemy[i].Location;
                    NewBullet.Location.X += 15;
                    ListEnemy[i].EnemyBullet.Add(NewBullet);
                }
            }
        }

        public void InitalizeEnemy(ref Enemy TempEnemy)
        {
            TempEnemy.Location.X = rd.Next(0, 450);
            TempEnemy.Location.Y = rd.Next(-200,-100);
            TempEnemy.EnemyType = rd.Next(0, 3);
            TempEnemy.EnemyBullet = new List<Bullet>();
        }

        public void InitalizeAttackEnemy(ref AttackEnemy TempEnemy)
        {
            TempEnemy.EnemyType = rd.Next();
            TempEnemy.EnemyType = (TempEnemy.EnemyType % 2) == 1 ? 1 : 2;
            if (TempEnemy.EnemyType == 1)
            {
                TempEnemy.Location.X = rd.Next(-200, -100);
            }
            else
            {
                TempEnemy.Location.X = rd.Next(700, 800);
            }
            TempEnemy.Location.Y = rd.Next(-300, -200);

            PointF TempPoint = new PointF();
            TempPoint.X = rd.Next(225, 275);
            TempPoint.Y = rd.Next(200, 430);

            if (TempEnemy.Location.X - TempPoint.X == 0)
            {
                TempEnemy.Location.X += 10;
            }
            float X1 = (TempEnemy.Location.X * TempEnemy.Location.X) - (TempPoint.X * TempPoint.X);
            float X2 = TempEnemy.Location.X - TempPoint.X;
            float X3 = 2 * TempPoint.X;
            float Y = TempEnemy.Location.Y - TempPoint.Y;

            TempEnemy.a = (Y / (X1 - (X2 * X3)));
            TempEnemy.b = -(TempEnemy.a * X3);
            TempEnemy.c = TempEnemy.Location.Y - (TempEnemy.a * (TempEnemy.Location.X * TempEnemy.Location.X)) - (TempEnemy.b * TempEnemy.Location.X);
            TempEnemy.DeltaX = (TempPoint.X - TempEnemy.Location.X) / 400;
            TempEnemy.EnemyBullet = new List<Bullet>();
        }

        public void KiemTra()
        {
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                for (int j = 0; j < MyPlayer.MyBullet.Count; j++)
                {
                    if (KiemTraBanTrungEnemy(MyPlayer.MyBullet[j].Location, ListEnemy[i].Location) == true)
                    {
                        ListEnemy.RemoveAt(i);
                        MyPlayer.MyBullet[j].Location = MyPlayer.Location;
                        MyPlayer.MyBullet[j].Location.X += 17;
                        MyPlayer.Mark++;
                        if (MyPlayer.Mark == 30)
                        {
                            MyPlayer.Mark = 0;
                            MyPlayer.Level++;

                            Enemy TempEnemy = new Enemy();
                            InitalizeEnemy(ref TempEnemy);
                            ListEnemy.Add(TempEnemy);

                            AttackEnemy TempAttackEnemy = new AttackEnemy();
                            InitalizeAttackEnemy(ref TempAttackEnemy);
                            ListAttackEnemy.Add(TempAttackEnemy);
                        }
                        Mark_Label.Text = MyPlayer.Mark.ToString();
                        Level_Label.Text = MyPlayer.Level.ToString();
                        i = ListEnemy.Count + 1;
                        break;
                    }
                }
                // if (ListEnemy[i].Location.Y > 0)
                //{
                //    if (MyPlayer.Location.X <= ListEnemy[i].Location.X + 30 && MyPlayer.Location.X + 35 >= ListEnemy[i].Location.X && MyPlayer.Location.Y <= ListEnemy[i].Location.Y - 2)
                //    {
                //        GameOver();
                //        return;
                //    }                    
                //}
                //for (int j = 0; j < ListEnemy[i].EnemyBullet.Count; j++)
                //{
                //    if (MyPlayer.Location.X <= ListEnemy[i].EnemyBullet[j].Location.X + 3 && MyPlayer.Location.X + 35 >= ListEnemy[i].EnemyBullet[j].Location.X && MyPlayer.Location.Y <= ListEnemy[i].EnemyBullet[j].Location.Y - 2)
                //    {
                //        GameOver();
                //        return;
                //    }
                //}
            }
            if (ListEnemy.Count == 0)
            {
                KhoiTaoEnemy();
            }
            for (int i = 0; i < ListAttackEnemy.Count; i++)
            {
                for (int j = 0; j < MyPlayer.MyBullet.Count; j++)
                {
                    if (KiemTraBanTrungAttackEnemy(MyPlayer.MyBullet[j].Location, ListAttackEnemy[i].Location) == true)
                    {
                        ListAttackEnemy.RemoveAt(i);
                        MyPlayer.MyBullet[j].Location = MyPlayer.Location;
                        MyPlayer.MyBullet[j].Location.X += 17;
                        MyPlayer.Mark++;
                        if (MyPlayer.Mark == 30)
                        {
                            MyPlayer.Mark = 0;
                            MyPlayer.Level++;

                            Enemy TempEnemy = new Enemy();
                            InitalizeEnemy(ref TempEnemy);
                            ListEnemy.Add(TempEnemy);

                            AttackEnemy TempAttackEnemy = new AttackEnemy();
                            InitalizeAttackEnemy(ref TempAttackEnemy);
                            ListAttackEnemy.Add(TempAttackEnemy);
                        }
                        Mark_Label.Text = MyPlayer.Mark.ToString();
                        Level_Label.Text = MyPlayer.Level.ToString();
                        i = ListAttackEnemy.Count + 1;
                        break;
                    }
                }
            }
            if (ListAttackEnemy.Count == 0)
            {
                KhoiTaoAttackEnemy();
            }          
        }
        public void GameOver()
        {
            Enemy_Timer.Stop();
            Rock_Timer.Stop();
            Bullet_Timer.Stop();
            AttackEnemy_Timer.Stop();
            EnemyBullet_Timer.Stop();
            AttackEnemyBullet_Timer.Stop();
            Start_Button.Show();
            Exit_Button.Show();
            MarkLogo_Label.Hide();
            Mark_Label.Hide();
            Level_Label.Hide();
            LevelLogo_Label.Hide();
           
            Start_Button.Text=("Play Again");
            IsStart = false;
            Pause = true;
            KhoiTao();
            Cursor.Show();
        }
        public void DeleteAll()
        {
            ListEnemy.Clear();
            ListAttackEnemy.Clear();
            MyPlayer = null;
            MyRock = null;
        }
        public bool KiemTraBanTrungEnemy(PointF a, PointF b)
        {
            if (a.Y >= 0)
            {
                if (a.X <= b.X + 30 && a.X + 7 >= b.X && a.Y <= b.Y - 2)
                {
                    return true;
                }
            }
            return false;
        }
        public bool KiemTraBanTrungAttackEnemy(PointF a, PointF b)
        {
            if (a.Y >= 0)
            {
                if (a.X <= b.X + 75 && a.X + 5 >= b.X && a.Y <= b.Y - 2)
                {
                    return true;
                }
            }
            return false;
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
            if (IsStart == false)
            {
                if (Pause != true)
                {
                    gp.DrawImage(Properties.Resources.Brand1, new PointF(80, 120));
                }
                else
                {
                    gp.DrawImage(Properties.Resources.game_over, new PointF(110, 70));
                }
            }
            else
            {
                for (int i = 0; i < ListEnemy.Count; i++)
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
                    for (int j = 0; j < ListEnemy[i].EnemyBullet.Count; j++)
                    {
                        gp.FillRectangle(new SolidBrush(Color.Yellow), ListEnemy[i].EnemyBullet[j].Location.X, ListEnemy[i].EnemyBullet[j].Location.Y, 3, 20);
                    }
                }
                for (int i = 0; i < ListAttackEnemy.Count; i++)
                {
                    if (ListAttackEnemy[i].EnemyType == 1)
                    {
                        gp.DrawImage(Properties.Resources.ufo, ListAttackEnemy[i].Location.X, ListAttackEnemy[i].Location.Y);
                    }
                    else
                    {
                        gp.DrawImage(Properties.Resources.ufo, ListAttackEnemy[i].Location.X, ListAttackEnemy[i].Location.Y);
                    }
                    for (int j = 0; j < ListAttackEnemy[i].EnemyBullet.Count; j++)
                    {
                        gp.DrawImage(Properties.Resources.Munition, ListAttackEnemy[i].EnemyBullet[j].Location);
                    }
                }
                gp.DrawImage(Properties.Resources.Rock, MyRock.Location);
                {
                    gp.DrawImage(Properties.Resources.Player, MyPlayer.Location);
                    for (int i = 0; i < MyPlayer.MyBullet.Count; i++)
                    {
                        gp.DrawImage(Properties.Resources.laserBlue01, MyPlayer.MyBullet[i].Location);
                    }
                }
            }
            Main_PictureBox.Image = bitmap;
        }
       
        private void Main_PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.Y < 100)
            {
                MyPlayer.Location.Y = 100;
                return;
            }
            MyPlayer.Location = e.Location;
        }
        private void Start_Button_Click(object sender, EventArgs e)
        {
            Start_Button.Hide();
            Exit_Button.Hide();
            MarkLogo_Label.Show();
            Mark_Label.Show();
            Level_Label.Show();
            LevelLogo_Label.Show();
            Star_Timer.Start();
            IsStart = true;
            Pause = false;
            Cursor.Hide();
            Enemy_Timer.Start();
            Rock_Timer.Start();
            Bullet_Timer.Start();
            AttackEnemy_Timer.Start();
            EnemyBullet_Timer.Start();
            AttackEnemyBullet_Timer.Start();
        }
        private void Exit_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }      
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (IsStart == true)
                {
                    Cursor.Show();
                    Start_Button.Show();
                    Exit_Button.Show();
                    Start_Button.Text = "Continue";
                }
                Enemy_Timer.Stop();
                Bullet_Timer.Stop();      
                Rock_Timer.Stop();
                Star_Timer.Stop();
                AttackEnemy_Timer.Stop();
            }
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
            public List<Bullet> EnemyBullet;
        }
        public class AttackEnemy
        {
            public PointF Location;
            public int EnemyType;
            public List<Bullet> EnemyBullet;
            public float a;
            public float b;
            public float c;
            public float DeltaX;
        }
        public class Player
        {
            public PointF Location;
            public List<Bullet> MyBullet;
            public int Mark;
            public int Level;
        }
        public class Bullet
        {
            public PointF Location;
            public int TypeBullet;
            public float a;
            public float b;
            public float Times;
            public float DeltaX;
        }
        public class Rock
        {
            public PointF Location;
            public int Type;
            public float a;
            public float b;
            public float DeltaX;
        }
    }
}
