using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
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
        List<Bullet> ListTempEnemyBullet;
        List<Bullet> ListTempAttackEnemyBullet;
        Player MyPlayer;
        Rock MyRock;
        Boss MyBoss;
        Item MyItem;
        bool IsStart = false;
        bool BossShow = false;
        int HuongBoss;
        int Status = 0;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(Main_PictureBox.Width, Main_PictureBox.Height);
            gp = Graphics.FromImage(bitmap);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            KhoiTao();
        }

        #region Khoitao
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
                    NewBullet.TotalTime = 0;
                    ListAttackEnemy[i].EnemyBullet.Add(NewBullet);
                }
            }
        }
        private void KhoiTaoEnemy()
        {
            for (int i = 0; i < 3 + MyPlayer.Level; i++)
            {
                Enemy TempEnemy = new Enemy();
                InitalizeEnemy(ref TempEnemy);
                ListEnemy.Add(TempEnemy);
            }
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                if (rd.Next(0, 3) != 1)
                {
                    Bullet NewBullet = new Bullet();
                    NewBullet.Location = ListEnemy[i].Location;
                    NewBullet.Location.X += 15;
                    ListEnemy[i].EnemyBullet.Add(NewBullet);
                }
            }
        }
        public void KhoiTaoBoss()
        {
            BossShow = true;
            MyBoss = new Boss();
            MyBoss.Location = new PointF(50, -400);
            MyBoss.Health = 500;
            MyBoss.TotalTime = 0;
            MyBoss.BossBulletType1 = new List<Bullet>();
            MyBoss.BossBulletType2 = new List<Bullet>();
            Enemy_Timer.Stop();
            EnemyBullet_Timer.Stop();
            AttackEnemy_Timer.Stop();
            AttackEnemyBullet_Timer.Stop();
            HuongBoss = 1;
            InitalizeBossBulletType1();
            InitalizeBossBulletType2();
            Boss_Timer.Start();
            BossBullet_Timer.Start();
        }
        public void InitalizeEnemy(ref Enemy TempEnemy)
        {
            TempEnemy.Location.X = rd.Next(0, 450);
            TempEnemy.Location.Y = rd.Next(-200, -100);
            TempEnemy.EnemyType = rd.Next(0, 3);
            TempEnemy.EnemyBullet = new List<Bullet>();
            TempEnemy.Health = 1;
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
            TempEnemy.Health = 5;
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
            MyPlayer.Health = 10;
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
            ListTempAttackEnemyBullet = new List<Bullet>();
            ListTempEnemyBullet = new List<Bullet>();
            MyItem = new Item();
            MyItem.Times = 0;
        }
        public void InitalizeBossBulletType1()
        {
            Bullet NewBullet;
            PointF A;
            PointF B;

            NewBullet = new Bullet();
            NewBullet.TypeBullet = 1;
            A = new PointF(MyBoss.Location.X + rd.Next(350, 500), MyBoss.Location.Y + 50);
            NewBullet.Location = A;
            B = new PointF(A.X + rd.Next(-250, -150), 700);
            if (A.Y - B.Y == 0)
            {
                A.Y += 3;
            }
            if (A.X - B.X == 0)
            {
                A.X += 3;
            }
            NewBullet.a = (A.Y - B.Y) / (A.X - B.X);
            NewBullet.b = NewBullet.Location.Y - (NewBullet.a * NewBullet.Location.X);
            NewBullet.DeltaX = (B.X - NewBullet.Location.X) / 200;
            MyBoss.BossBulletType1.Add(NewBullet);

            NewBullet = new Bullet();
            NewBullet.TypeBullet = 2;
            A = new PointF(MyBoss.Location.X + rd.Next(-250, -150), MyBoss.Location.Y + 50);
            NewBullet.Location = A;
            B = new PointF(A.X + rd.Next(350, 550), 700);
            if (A.Y - B.Y == 0)
            {
                A.Y += 3;
            }
            if (A.X - B.X == 0)
            {
                A.X += 3;
            }
            NewBullet.a = (A.Y - B.Y) / (A.X - B.X);
            NewBullet.b = NewBullet.Location.Y - (NewBullet.a * NewBullet.Location.X);
            NewBullet.DeltaX = (B.X - NewBullet.Location.X) / 200;
            MyBoss.BossBulletType1.Add(NewBullet);


            NewBullet = new Bullet();
            NewBullet.TypeBullet = 3;
            A = new PointF(MyBoss.Location.X + rd.Next(115, 150), MyBoss.Location.Y + 50);
            NewBullet.Location = A;
            B = new PointF(A.X + rd.Next(105, 140), 700);
            if (A.Y - B.Y == 0)
            {
                A.Y += 3;
            }
            if (A.X - B.X == 0)
            {
                A.X += 3;
            }
            NewBullet.a = (A.Y - B.Y) / (A.X - B.X);
            NewBullet.b = NewBullet.Location.Y - (NewBullet.a * NewBullet.Location.X);
            NewBullet.DeltaX = (B.X - NewBullet.Location.X) / 200;
            MyBoss.BossBulletType1.Add(NewBullet);

        }
        public void InitalizeBossBulletType2()
        {
            Bullet NewBullet;
            PointF A;
            PointF B;

            NewBullet = new Bullet();
            NewBullet.Location = MyBoss.Location;
            NewBullet.Location.X += 100;
            A = NewBullet.Location;
            B = MyPlayer.Location;
            if (A.Y - B.Y == 0)
            {
                A.Y += 3;
            }
            if (A.X - B.X == 0)
            {
                A.X += 3;
            }
            NewBullet.a = (A.Y - B.Y) / (A.X - B.X);
            NewBullet.b = NewBullet.Location.Y - (NewBullet.a * NewBullet.Location.X);
            NewBullet.DeltaX = (MyPlayer.Location.X - NewBullet.Location.X) / 30;
            NewBullet.Times = 0;
            NewBullet.TotalTime = 0;
            MyBoss.BossBulletType2.Add(NewBullet);
        }
        #endregion

        #region process
        public bool KiemTraBanTrungEnemy(PointF a, PointF b)
        {
            if (a.Y >= 0)
            {
                if (a.X <= b.X + 30 && a.X + 4 >= b.X + 3 && a.Y <= b.Y + 23 && a.Y >= b.Y + 3)
                {
                    return true;
                }
            }
            return false;
        }
        public bool KiemTraBanTrungAttackEnemy(PointF a, AttackEnemy b)
        {
            if (a.Y >= 0)
            {
                if (b.EnemyType == 1)
                {
                    if (a.X <= b.Location.X + 80 && a.X + 4 >= b.Location.X + 20 && a.Y <= b.Location.Y + 65 && a.Y >= b.Location.Y + 23)
                    {
                        return true;
                    }
                }
                else
                {
                    if (a.X <= b.Location.X + 61 && a.X + 4 >= b.Location.X && a.Y <= b.Location.Y + 50 && a.Y >= b.Location.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void KiemTra()
        {
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                for (int j = 0; j < MyPlayer.MyBullet.Count; j++)
                {
                    if (KiemTraBanTrungEnemy(MyPlayer.MyBullet[j].Location, ListEnemy[i].Location) == true)
                    {
                        for (int h = 0; h < ListEnemy[i].EnemyBullet.Count; h++)
                        {
                            ListTempEnemyBullet.Add(ListEnemy[i].EnemyBullet[h]);
                        }
                        MyItem.Times++;
                        if (MyItem.Times == 15)
                        {
                            MyItem.TypeItem = rd.Next(0, 2);
                            MyItem.Location = ListEnemy[i].Location;
                            Item_Timer.Start();
                        }
                        ListEnemy.RemoveAt(i);
                        if (j == 0)
                        {
                            MyPlayer.MyBullet[j].Location = MyPlayer.Location;
                            MyPlayer.MyBullet[j].Location.X += -5;
                            MyPlayer.MyBullet[j].Location.Y -= 10;
                        }
                        if (j == 1)
                        {
                            MyPlayer.MyBullet[j].Location = MyPlayer.Location;
                            MyPlayer.MyBullet[j].Location.X += 17;
                            MyPlayer.MyBullet[j].Location.Y -= 20;
                        }
                        if (j == 2)
                        {
                            MyPlayer.MyBullet[j].Location = MyPlayer.Location;
                            MyPlayer.MyBullet[j].Location.X += 40;
                            MyPlayer.MyBullet[j].Location.Y -= 10;
                        }
                        MyItem.Times++;
                        MyPlayer.Mark++;
                        if (MyPlayer.Mark == 20)
                        {
                            MyPlayer.Mark = 0;
                            MyPlayer.Level++;
                            if (MyPlayer.Level < 3)
                            {
                                Enemy TempEnemy = new Enemy();
                                InitalizeEnemy(ref TempEnemy);
                                ListEnemy.Add(TempEnemy);

                                AttackEnemy TempAttackEnemy = new AttackEnemy();
                                InitalizeAttackEnemy(ref TempAttackEnemy);
                                ListAttackEnemy.Add(TempAttackEnemy);
                            }
                        }
                        Mark_Label.Text = MyPlayer.Mark.ToString();
                        Level_Label.Text = MyPlayer.Level.ToString();
                        return;
                    }
                }
            }
            if (MyPlayer.Level < 3)
            {
                if (ListEnemy.Count == 0)
                {
                    KhoiTaoEnemy();
                }
            }
            for (int i = 0; i < ListAttackEnemy.Count; i++)
            {
                for (int j = 0; j < MyPlayer.MyBullet.Count; j++)
                {
                    if (KiemTraBanTrungAttackEnemy(MyPlayer.MyBullet[j].Location, ListAttackEnemy[i]) == true)
                    {
                        ListAttackEnemy[i].Health--;
                        if (ListAttackEnemy[i].Health == 0)
                        {
                            for (int h = 0; h < ListAttackEnemy[i].EnemyBullet.Count; h++)
                            {
                                ListTempAttackEnemyBullet.Add(ListAttackEnemy[i].EnemyBullet[h]);
                            }
                            MyItem.Times++;
                            if (MyItem.Times == 15)
                            {
                                MyItem.TypeItem = rd.Next(0, 2);
                                MyItem.Location = ListAttackEnemy[i].Location;
                                Item_Timer.Start();
                            }
                            ListAttackEnemy.RemoveAt(i);
                            if (j == 0)
                            {
                                MyPlayer.MyBullet[j].Location = MyPlayer.Location;
                                MyPlayer.MyBullet[j].Location.X += -5;
                                MyPlayer.MyBullet[j].Location.Y -= 10;
                            }
                            if (j == 1)
                            {
                                MyPlayer.MyBullet[j].Location = MyPlayer.Location;
                                MyPlayer.MyBullet[j].Location.X += 17;
                                MyPlayer.MyBullet[j].Location.Y -= 20;
                            }
                            if (j == 2)
                            {
                                MyPlayer.MyBullet[j].Location = MyPlayer.Location;
                                MyPlayer.MyBullet[j].Location.X += 40;
                                MyPlayer.MyBullet[j].Location.Y -= 10;
                            }
                            MyPlayer.Mark++;
                            if (MyPlayer.Mark == 20)
                            {
                                MyPlayer.Mark = 0;
                                MyPlayer.Level++;
                                if (MyPlayer.Level < 3)
                                {
                                    Enemy TempEnemy = new Enemy();
                                    InitalizeEnemy(ref TempEnemy);
                                    ListEnemy.Add(TempEnemy);

                                    AttackEnemy TempAttackEnemy = new AttackEnemy();
                                    InitalizeAttackEnemy(ref TempAttackEnemy);
                                    ListAttackEnemy.Add(TempAttackEnemy);
                                }
                            }
                            Mark_Label.Text = MyPlayer.Mark.ToString();
                            Level_Label.Text = MyPlayer.Level.ToString();
                            return;
                        }
                    }
                }
            }
            if (MyPlayer.Level < 3)
            {
                if (ListAttackEnemy.Count == 0)
                {
                    KhoiTaoAttackEnemy();
                }
            }

            if (BossShow == false)
            {
                if (MyPlayer.Level >= 3 && ListEnemy.Count == 0 && ListAttackEnemy.Count == 0)
                {
                    KhoiTaoBoss();
                }
            }
            else
            {
                for (int i = 0; i < MyPlayer.MyBullet.Count; i++)
                {
                    if (MyPlayer.MyBullet[i].Location.X <= MyBoss.Location.X + 350 && MyPlayer.MyBullet[i].Location.X + 4 >= MyBoss.Location.X + 45 && MyPlayer.MyBullet[i].Location.Y <= MyBoss.Location.Y + 187 && MyPlayer.MyBullet[i].Location.Y >= MyBoss.Location.Y + 23)
                    {
                        MyBoss.Health--;
                        if (i == 0)
                        {
                            MyPlayer.MyBullet[i].Location = MyPlayer.Location;
                            MyPlayer.MyBullet[i].Location.X += -5;
                            MyPlayer.MyBullet[i].Location.Y -= 10;
                        }
                        if (i == 1)
                        {
                            MyPlayer.MyBullet[i].Location = MyPlayer.Location;
                            MyPlayer.MyBullet[i].Location.X += 17;
                            MyPlayer.MyBullet[i].Location.Y -= 20;
                        }
                        if (i == 2)
                        {
                            MyPlayer.MyBullet[i].Location = MyPlayer.Location;
                            MyPlayer.MyBullet[i].Location.X += 40;
                            MyPlayer.MyBullet[i].Location.Y -= 10;
                        }
                        if (MyBoss.Health == 0)
                        {
                            GameWin();
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        #region Timer
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
            if (MyPlayer.Location.X <= MyRock.Location.X + 38 && MyPlayer.Location.X + 42 >= MyRock.Location.X+2 && MyPlayer.Location.Y <= MyRock.Location.Y+37 && MyPlayer.Location.Y +38 >= MyRock.Location.Y+6)
            {
                MyPlayer.Health--;
                if (MyPlayer.Health == 0)
                {
                    GameOver();
                    return;
                }
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
        private void LostBullet_Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ListTempEnemyBullet.Count; i++)
            {
                ListTempEnemyBullet[i].Location.Y += MyPlayer.Level + 4;
                if (MyPlayer.Location.X <= ListTempEnemyBullet[i].Location.X + 3 && MyPlayer.Location.X + 42 >= ListTempEnemyBullet[i].Location.X && MyPlayer.Location.Y <= ListTempEnemyBullet[i].Location.Y + 20 && MyPlayer.Location.Y + 38 >= ListTempEnemyBullet[i].Location.Y)
                {
                    MyPlayer.Health--;
                    if (MyPlayer.Health == 0)
                    {
                        GameOver();
                        return;
                    }
                }
                if (ListTempEnemyBullet[i].Location.Y > Main_PictureBox.Height)
                {
                    ListTempEnemyBullet.RemoveAt(i);
                }
            }
            for (int i = 0; i < ListTempAttackEnemyBullet.Count; i++)
            {
                ListTempAttackEnemyBullet[i].Location.X += ListTempAttackEnemyBullet[i].DeltaX;
                ListTempAttackEnemyBullet[i].Location.Y = (ListTempAttackEnemyBullet[i].Location.X * ListTempAttackEnemyBullet[i].a) + ListTempAttackEnemyBullet[i].b;
                ListTempAttackEnemyBullet[i].Times++;
                ListTempAttackEnemyBullet[i].TotalTime++;
                if (ListTempAttackEnemyBullet[i].Times == 10)
                {
                    ListTempAttackEnemyBullet[i].Times = 0;
                    if (MyPlayer.Location.Y - ListTempAttackEnemyBullet[i].Location.Y == 0)
                    {
                        ListTempAttackEnemyBullet[i].Location.Y += 2;
                    }
                    if (MyPlayer.Location.X - ListTempAttackEnemyBullet[i].Location.X == 0)
                    {
                        ListTempAttackEnemyBullet[i].Location.X += 2;
                    }
                    ListTempAttackEnemyBullet[i].a = (MyPlayer.Location.Y + 35 - ListTempAttackEnemyBullet[i].Location.Y) / (MyPlayer.Location.X + 15 - ListTempAttackEnemyBullet[i].Location.X);
                    ListTempAttackEnemyBullet[i].b = ListTempAttackEnemyBullet[i].Location.Y - (ListTempAttackEnemyBullet[i].a * ListTempAttackEnemyBullet[i].Location.X);
                    ListTempAttackEnemyBullet[i].DeltaX = (MyPlayer.Location.X + 15 - ListTempAttackEnemyBullet[i].Location.X) / 30;
                }
                if (MyPlayer.Location.X <= ListTempAttackEnemyBullet[i].Location.X + 12 && MyPlayer.Location.X + 42 >= ListTempAttackEnemyBullet[i].Location.X + 7 && MyPlayer.Location.Y <= ListTempAttackEnemyBullet[i].Location.Y + 12 && MyPlayer.Location.Y + 38 >= ListTempAttackEnemyBullet[i].Location.Y + 7)
                {
                    MyPlayer.Health--;
                    if (MyPlayer.Health == 0)
                    {
                        GameOver();
                        return;
                    }
                }
                if (ListTempAttackEnemyBullet[i].TotalTime == 150)
                {
                    ListTempAttackEnemyBullet.RemoveAt(i);
                }
            }
        }
        private void Item_Timer_Tick(object sender, EventArgs e)
        {
            MyItem.Location.Y += MyPlayer.Level + 4;
            if (MyItem.Location.Y > Main_PictureBox.Height+50)
            {
                MyItem.Location.Y = -100;
                MyItem.Times = 0;
                Item_Timer.Stop();
            }
            if (MyItem.TypeItem == 1)
            {
                if (MyPlayer.Location.X <= MyItem.Location.X + 10 && MyPlayer.Location.X + 42 >= MyItem.Location.X && MyPlayer.Location.Y <= MyItem.Location.Y + 30 && MyPlayer.Location.Y + 38 >= MyItem.Location.Y)
                {
                    if (MyPlayer.MyBullet.Count <= 2)
                    {
                        Bullet TempBullet = new Bullet();
                        TempBullet.Location = MyPlayer.Location;
                        TempBullet.Location.Y -= 80;
                        TempBullet.Location.X += 17;
                        MyPlayer.MyBullet.Add(TempBullet);
                    }
                    MyItem.Location.Y = -100;
                    MyItem.Times = 0;
                    Item_Timer.Stop();
                }
            }
            else
            {
                if (MyItem.TypeItem == 0)
                {
                    if (MyPlayer.Location.X <= MyItem.Location.X + 20 && MyPlayer.Location.X + 42 >= MyItem.Location.X && MyPlayer.Location.Y <= MyItem.Location.Y + 20 && MyPlayer.Location.Y + 38 >= MyItem.Location.Y )
                    {
                        MyPlayer.Health = 10;
                        MyItem.Location.Y = -100;
                        MyItem.Times = 0;
                        Item_Timer.Stop();
                    }
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
                    if (ListAttackEnemy[i].Location.X > Main_PictureBox.Width + 100)
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
                if (MyPlayer.Level < 3)
                {
                    KhoiTaoAttackEnemy();
                }
            }
            else
            {
                for (int i = 0; i < ListAttackEnemy.Count; i++)
                {
                    if (ListAttackEnemy[i].Location.Y > 0)
                    {
                        if (ListAttackEnemy[i].EnemyType == 1)
                        {
                            if (MyPlayer.Location.X <= ListAttackEnemy[i].Location.X + 80 && MyPlayer.Location.X + 42 >= ListAttackEnemy[i].Location.X + 20 && MyPlayer.Location.Y <= ListAttackEnemy[i].Location.Y + 65 && MyPlayer.Location.Y + 38 >= ListAttackEnemy[i].Location.Y + 23)
                            {
                                MyPlayer.Health--;
                                if (MyPlayer.Health == 0)
                                {
                                    GameOver();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (MyPlayer.Location.X <= ListAttackEnemy[i].Location.X + 61 && MyPlayer.Location.X + 42 >= ListAttackEnemy[i].Location.X && MyPlayer.Location.Y <= ListAttackEnemy[i].Location.Y + 50 && MyPlayer.Location.Y + 38 >= ListAttackEnemy[i].Location.Y)
                            {
                                MyPlayer.Health--;
                                if (MyPlayer.Health == 0)
                                {
                                    GameOver();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            if (BossShow == false)
            {
                if (MyPlayer.Level >= 3 && ListEnemy.Count == 0 && ListAttackEnemy.Count == 0)
                {
                    KhoiTaoBoss();
                }
            }
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
            if (ListEnemy.Count != 0)
            {
                for (int i = 0; i < ListEnemy.Count; i++)
                {
                    if (ListEnemy[i].Location.Y > 0)
                    {
                        if (MyPlayer.Location.X <= ListEnemy[i].Location.X + 25 && MyPlayer.Location.X + 42 >= ListEnemy[i].Location.X + 5 && MyPlayer.Location.Y <= ListEnemy[i].Location.Y + 23 && MyPlayer.Location.Y + 38 >= ListEnemy[i].Location.Y + 3)
                        {
                            MyPlayer.Health--;
                            if (MyPlayer.Health == 0)
                            {
                                GameOver();
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                if (MyPlayer.Level < 3)
                {
                    KhoiTaoEnemy();
                }
            }
            if (BossShow == false)
            {
                if (MyPlayer.Level >= 3 && ListEnemy.Count == 0 && ListAttackEnemy.Count == 0)
                {
                    KhoiTaoBoss();
                }
            }
        }
        private void EnemyBullet_Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                for (int j = 0; j < ListEnemy[i].EnemyBullet.Count; j++)
                {
                    ListEnemy[i].EnemyBullet[j].Location.Y += MyPlayer.Level+4;
                    if (ListEnemy[i].EnemyBullet[j].Location.Y > Main_PictureBox.Height)
                    {
                        ListEnemy[i].EnemyBullet[j].Location = ListEnemy[i].Location;
                        ListEnemy[i].EnemyBullet[j].Location.X += 15;
                    }
                    if (MyPlayer.Location.X <= ListEnemy[i].EnemyBullet[j].Location.X + 3 && MyPlayer.Location.X + 42 >= ListEnemy[i].EnemyBullet[j].Location.X && MyPlayer.Location.Y <= ListEnemy[i].EnemyBullet[j].Location.Y + 20 && MyPlayer.Location.Y + 38 >= ListEnemy[i].EnemyBullet[j].Location.Y)
                    {
                        MyPlayer.Health--;
                        if (MyPlayer.Health == 0)
                        {
                            GameOver();
                            return;
                        }
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
                    if (i == 0)
                    {
                        MyPlayer.MyBullet[i].Location = MyPlayer.Location;
                        MyPlayer.MyBullet[i].Location.X += -5;
                        MyPlayer.MyBullet[i].Location.Y -= 10;
                    }
                    if (i == 1)
                    {
                        MyPlayer.MyBullet[i].Location = MyPlayer.Location;
                        MyPlayer.MyBullet[i].Location.X += 17;
                        MyPlayer.MyBullet[i].Location.Y -= 20;
                    }
                    if (i == 2)
                    {
                        MyPlayer.MyBullet[i].Location = MyPlayer.Location;
                        MyPlayer.MyBullet[i].Location.X += 40;
                        MyPlayer.MyBullet[i].Location.Y -= 10;
                    }
                }               
                KiemTra();
            }
        }
        private void Boss_Timer_Tick(object sender, EventArgs e)
        {
            MyBoss.TotalTime++;
            if (MyBoss.Location.Y < 50)
            {
                MyBoss.Location.Y += 2;
            }
            else
            {
                if (HuongBoss == 0)
                {
                    MyBoss.Location.X -= 1;
                }
                else
                {
                    if (HuongBoss == 1)
                    {
                        MyBoss.Location.X += 1;
                    }
                }
                if (MyBoss.Location.X > Main_PictureBox.Width-320)
                {
                    HuongBoss = 0;
                }
                if (MyBoss.Location.X < -50)
                {
                    HuongBoss = 1;
                }               
            }
            if (MyPlayer.Location.X <= MyBoss.Location.X + 500 && MyPlayer.Location.X + 42 >= MyBoss.Location.X + 2 && MyPlayer.Location.Y <= MyBoss.Location.Y + 200 && MyPlayer.Location.Y + 38 >= MyBoss.Location.Y + 2)
            {
                MyPlayer.Health--;
                if (MyPlayer.Health == 0)
                {
                    GameOver();
                    return;
                }
            }
            if (MyBoss.FinalSkill == true)
            {
                if (MyPlayer.Location.X <= MyBoss.Location.X + 285 && MyPlayer.Location.X + 42 >= MyBoss.Location.X + 135 )
                {
                    MyPlayer.Health--;
                    if (MyPlayer.Health == 0)
                    {
                        GameOver();
                        return;
                    }
                }
            }
            if (MyBoss.TotalTime == 120)
            {
                if (MyBoss.FinalSkill == true)
                {
                    MyBoss.FinalSkill = false;
                }
                else
                {
                    MyBoss.FinalSkill = true;
                }
                MyBoss.TotalTime = 0;
            }
        }
        private void BossBullet_Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < MyBoss.BossBulletType1.Count; i++)
            {
                MyBoss.BossBulletType1[i].Location.X += MyBoss.BossBulletType1[i].DeltaX;
                MyBoss.BossBulletType1[i].Location.Y = (MyBoss.BossBulletType1[i].Location.X * MyBoss.BossBulletType1[i].a) + MyBoss.BossBulletType1[i].b;
                if (MyBoss.BossBulletType1[i].TypeBullet == 3)
                {
                    if (MyPlayer.Location.X <= MyBoss.BossBulletType1[i].Location.X + 15 && MyPlayer.Location.X + 42 >= MyBoss.BossBulletType1[i].Location.X + 1 && MyPlayer.Location.Y <= MyBoss.BossBulletType1[i].Location.Y + 49 && MyPlayer.Location.Y + 38 >= MyBoss.BossBulletType1[i].Location.Y + 2)
                    {
                        MyPlayer.Health--;
                        if (MyPlayer.Health == 0)
                        {
                            GameOver();
                            return;
                        }
                    }
                }
                else
                {
                    if (MyBoss.BossBulletType1[i].TypeBullet == 2)
                    {
                        if (MyPlayer.Location.X <= MyBoss.BossBulletType1[i].Location.X + 40 && MyPlayer.Location.X + 42 >= MyBoss.BossBulletType1[i].Location.X + 17 && MyPlayer.Location.Y <= MyBoss.BossBulletType1[i].Location.Y + 48 && MyPlayer.Location.Y + 38 >= MyBoss.BossBulletType1[i].Location.Y + 20)
                        {
                            MyPlayer.Health--;
                            if (MyPlayer.Health == 0)
                            {
                                GameOver();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (MyBoss.BossBulletType1[i].TypeBullet == 1)
                        {
                            if (MyPlayer.Location.X <= MyBoss.BossBulletType1[i].Location.X + 23 && MyPlayer.Location.X + 42 >= MyBoss.BossBulletType1[i].Location.X && MyPlayer.Location.Y <= MyBoss.BossBulletType1[i].Location.Y + 48 && MyPlayer.Location.Y + 38 >= MyBoss.BossBulletType1[i].Location.Y + 20)
                            {
                                MyPlayer.Health--;
                                if (MyPlayer.Health == 0)
                                {
                                    GameOver();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < MyBoss.BossBulletType1.Count; i++)
            {
                if (MyBoss.BossBulletType1[i].Location.Y > 650)
                {
                    MyBoss.BossBulletType1.Remove(MyBoss.BossBulletType1[i]);
                }
            }
            if (MyBoss.BossBulletType1.Count == 0)
            {
                InitalizeBossBulletType1();
            }
            for (int i = 0; i < MyBoss.BossBulletType2.Count; i++)
            {
                MyBoss.BossBulletType2[i].Location.X += MyBoss.BossBulletType2[i].DeltaX;
                MyBoss.BossBulletType2[i].Location.Y = (MyBoss.BossBulletType2[i].Location.X * MyBoss.BossBulletType2[i].a) + MyBoss.BossBulletType2[i].b;
                MyBoss.BossBulletType2[i].Times++;
                MyBoss.BossBulletType2[i].TotalTime++;
                if (MyBoss.BossBulletType2[i].Times == 10)
                {
                    MyBoss.BossBulletType2[i].Times = 0;
                    if (MyPlayer.Location.Y - MyBoss.BossBulletType2[i].Location.Y == 0)
                    {
                        MyBoss.BossBulletType2[i].Location.Y += 2;
                    }
                    if (MyPlayer.Location.X - MyBoss.BossBulletType2[i].Location.X == 0)
                    {
                        MyBoss.BossBulletType2[i].Location.X += 2;
                    }
                    MyBoss.BossBulletType2[i].a = (MyPlayer.Location.Y + 35 - MyBoss.BossBulletType2[i].Location.Y) / (MyPlayer.Location.X + 15 - MyBoss.BossBulletType2[i].Location.X);
                    MyBoss.BossBulletType2[i].b = MyBoss.BossBulletType2[i].Location.Y - (MyBoss.BossBulletType2[i].a * MyBoss.BossBulletType2[i].Location.X);
                    MyBoss.BossBulletType2[i].DeltaX = (MyPlayer.Location.X + 15 - MyBoss.BossBulletType2[i].Location.X) / 30;
                }
                if (MyPlayer.Location.X <= MyBoss.BossBulletType2[i].Location.X + 20 && MyPlayer.Location.X + 42 >= MyBoss.BossBulletType2[i].Location.X && MyPlayer.Location.Y <= MyBoss.BossBulletType2[i].Location.Y + 20 && MyPlayer.Location.Y + 38 >= MyBoss.BossBulletType2[i].Location.Y)
                {
                    MyPlayer.Health--;
                    if (MyPlayer.Health == 0)
                    {
                        GameOver();
                        return;
                    }
                }
            }
            for (int i = 0; i < MyBoss.BossBulletType2.Count; i++)
            {
                if (MyBoss.BossBulletType2[i].TotalTime == 200)
                {
                    MyBoss.BossBulletType2.RemoveAt(i);
                }
            }
            if (MyBoss.BossBulletType2.Count == 0)
            {
                InitalizeBossBulletType2();
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
                    ListAttackEnemy[i].EnemyBullet[j].TotalTime++;
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
                    if (MyPlayer.Location.X <= ListAttackEnemy[i].EnemyBullet[j].Location.X + 12 && MyPlayer.Location.X + 42 >= ListAttackEnemy[i].EnemyBullet[j].Location.X + 7 && MyPlayer.Location.Y <= ListAttackEnemy[i].EnemyBullet[j].Location.Y + 12 && MyPlayer.Location.Y + 38 >= ListAttackEnemy[i].EnemyBullet[j].Location.Y + 7)
                    {
                        MyPlayer.Health--;
                        if (MyPlayer.Health == 0)
                        {
                            GameOver();
                            return;
                        }
                    }
                    if (ListAttackEnemy[i].EnemyBullet[j].TotalTime == 150)
                    {
                        ListAttackEnemy[i].EnemyBullet[j].Location = ListAttackEnemy[i].Location;
                        ListAttackEnemy[i].EnemyBullet[j].Location.X += 15;
                        ListAttackEnemy[i].EnemyBullet[j].Times = 0;
                        ListAttackEnemy[i].EnemyBullet[j].TotalTime = 0;
                        if (MyPlayer.Location.Y - ListAttackEnemy[i].EnemyBullet[j].Location.Y == 0)
                        {
                            ListAttackEnemy[i].EnemyBullet[j].Location.Y += 2;
                        }
                        if (MyPlayer.Location.X - ListAttackEnemy[i].EnemyBullet[j].Location.X == 0)
                        {
                            ListAttackEnemy[i].EnemyBullet[j].Location.X += 2;
                        }
                        ListAttackEnemy[i].EnemyBullet[j].a = (MyPlayer.Location.Y - ListAttackEnemy[i].EnemyBullet[j].Location.Y) / (MyPlayer.Location.X - ListAttackEnemy[i].EnemyBullet[j].Location.X);
                        ListAttackEnemy[i].EnemyBullet[j].b = ListAttackEnemy[i].EnemyBullet[j].Location.Y - (ListAttackEnemy[i].EnemyBullet[j].a * ListAttackEnemy[i].EnemyBullet[j].Location.X);
                        ListAttackEnemy[i].EnemyBullet[j].DeltaX = (MyPlayer.Location.X - ListAttackEnemy[i].EnemyBullet[j].Location.X) / 50;
                    }
                }
            }
        }
        #endregion

        #region Event
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
            IsStart = true;
            Status = 0;
            Cursor.Hide();
            MyBoss = null;
            Star_Timer.Start();
            Enemy_Timer.Start();
            Rock_Timer.Start();
            Bullet_Timer.Start();
            AttackEnemy_Timer.Start();
            AttackEnemyBullet_Timer.Start();
            EnemyBullet_Timer.Start();
            AttackEnemyBullet_Timer.Start();
            LostBullet_Timer.Start();
            if (MyItem.Location.Y > 0)
            {
                Item_Timer.Start();
            }
            if (BossShow == true)
            {
                Boss_Timer.Start();
                BossBullet_Timer.Start();
            }
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
                EnemyBullet_Timer.Stop();
                Bullet_Timer.Stop();
                Rock_Timer.Stop();
                Star_Timer.Stop();
                AttackEnemy_Timer.Stop();
                AttackEnemyBullet_Timer.Stop();
                Boss_Timer.Stop();
                BossBullet_Timer.Stop();
                LostBullet_Timer.Stop();
                Item_Timer.Stop();
            }
        }
        #endregion

        public void GameOver()
        {
            LostBullet_Timer.Stop();
            BossBullet_Timer.Stop();
            Boss_Timer.Stop();
            Enemy_Timer.Stop();
            Rock_Timer.Stop();
            Bullet_Timer.Stop();
            AttackEnemy_Timer.Stop();
            EnemyBullet_Timer.Stop();
            AttackEnemyBullet_Timer.Stop();
            Item_Timer.Stop();
            Start_Button.Show();
            Exit_Button.Show();
            MarkLogo_Label.Hide();
            Mark_Label.Hide();
            Level_Label.Hide();
            LevelLogo_Label.Hide();
           
            Start_Button.Text=("Play Again");
            IsStart = false;
            Status = 1;
            BossShow = false;
            KhoiTao();
            Mark_Label.Text = MyPlayer.Mark.ToString();
            Level_Label.Text = MyPlayer.Level.ToString();
            Cursor.Show();
        }
        public void GameWin()
        {
            LostBullet_Timer.Stop();
            BossBullet_Timer.Stop();
            Boss_Timer.Stop();
            Enemy_Timer.Stop();
            Rock_Timer.Stop();
            Bullet_Timer.Stop();
            AttackEnemy_Timer.Stop();
            EnemyBullet_Timer.Stop();
            AttackEnemyBullet_Timer.Stop();
            Item_Timer.Stop();
            Start_Button.Show();
            Exit_Button.Show();
            MarkLogo_Label.Hide();
            Mark_Label.Hide();
            Level_Label.Hide();
            LevelLogo_Label.Hide();

            Start_Button.Text = ("Play Again");
            IsStart = false;
            Status = 2;
            BossShow = false;
            KhoiTao();
            Mark_Label.Text = MyPlayer.Mark.ToString();
            Level_Label.Text = MyPlayer.Level.ToString();
            Cursor.Show();
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
                if (Status == 0)
                {                 
                    gp.DrawImage(Properties.Resources.Brand, new PointF(80, 120));
                }
                else
                {
                    if (Status == 1)
                    {
                        gp.DrawImage(Properties.Resources.game_over, new PointF(110, 70));
                    }
                    else
                    {
                        if (Status == 2)
                        {
                            gp.DrawImage(Properties.Resources.YouWin, new PointF(60, 80));
                            //gp.DrawString("You Win", new Font("Gill Sans", 80, FontStyle.Bold), new SolidBrush(Color.Red), new Point(30, 110));
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < ListEnemy.Count; i++)
                {
                    gp.FillRectangle(new SolidBrush(Color.Red), ListEnemy[i].Location.X + 13, ListEnemy[i].Location.Y -5, 15, 3);
                    gp.DrawRectangle(new Pen(Color.White), ListEnemy[i].Location.X + 13, ListEnemy[i].Location.Y -5, 15, 3);
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
                for (int i = 0; i < ListTempEnemyBullet.Count; i++)
                {
                    gp.FillRectangle(new SolidBrush(Color.Yellow), ListTempEnemyBullet[i].Location.X, ListTempEnemyBullet[i].Location.Y, 3, 20);
                }
                for (int i = 0; i < ListAttackEnemy.Count; i++)
                {
                  
                    if (ListAttackEnemy[i].EnemyType == 1)
                    {
                        gp.FillRectangle(new SolidBrush(Color.Red), ListAttackEnemy[i].Location.X + 40, ListAttackEnemy[i].Location.Y+8, (ListAttackEnemy[i].Health * 30) / 5, 3);
                        gp.DrawRectangle(new Pen(Color.White), ListAttackEnemy[i].Location.X + 40, ListAttackEnemy[i].Location.Y +8, 30, 3);
                        gp.DrawImage(Properties.Resources.UFO, ListAttackEnemy[i].Location.X, ListAttackEnemy[i].Location.Y);
                    }
                    else
                    {
                        gp.FillRectangle(new SolidBrush(Color.Red), ListAttackEnemy[i].Location.X + 25, ListAttackEnemy[i].Location.Y - 5, (ListAttackEnemy[i].Health *30) / 5, 3);
                        gp.DrawRectangle(new Pen(Color.White), ListAttackEnemy[i].Location.X + 25, ListAttackEnemy[i].Location.Y - 5, 30, 3);
                        gp.DrawImage(Properties.Resources.UFO2, ListAttackEnemy[i].Location.X, ListAttackEnemy[i].Location.Y);
                    }
                    for (int j = 0; j < ListAttackEnemy[i].EnemyBullet.Count; j++)
                    {
                        gp.DrawImage(Properties.Resources.Munition, ListAttackEnemy[i].EnemyBullet[j].Location);
                    }
                }
                for (int i = 0; i < ListTempAttackEnemyBullet.Count; i++)
                {
                    gp.DrawImage(Properties.Resources.Munition, ListTempAttackEnemyBullet[i].Location);
                }
                gp.DrawImage(Properties.Resources.Rock, MyRock.Location);
                if (MyBoss != null)
                {       
                    for (int i = 0; i < MyBoss.BossBulletType1.Count; i++)
                    {
                        if (MyBoss.BossBulletType1[i].TypeBullet == 1)
                        {
                            gp.DrawImage(Properties.Resources.BossBullet2, MyBoss.BossBulletType1[i].Location);
                        }
                        if (MyBoss.BossBulletType1[i].TypeBullet == 2)
                        {
                            gp.DrawImage(Properties.Resources.BossBullet1, MyBoss.BossBulletType1[i].Location);
                        }
                        if (MyBoss.BossBulletType1[i].TypeBullet == 3)
                        {
                            gp.DrawImage(Properties.Resources.BossBullet3_1, MyBoss.BossBulletType1[i].Location);
                        }
                    }
                    for (int i = 0; i < MyBoss.BossBulletType2.Count; i++)
                    {
                        gp.DrawImage(Properties.Resources.BossCircleBullet, MyBoss.BossBulletType2[i].Location);
                    }
                    if (MyBoss.FinalSkill == true)
                    {
                        gp.FillRectangle(new SolidBrush(Color.LightYellow), MyBoss.Location.X + 135, MyBoss.Location.Y + 50, 150, 800);
                    }
                    gp.FillRectangle(new SolidBrush(Color.Red), MyBoss.Location.X+160 , MyBoss.Location.Y - 30, (float)((MyBoss.Health * 100)/500), 10);
                    gp.DrawRectangle(new Pen(Color.White), MyBoss.Location.X +160, MyBoss.Location.Y - 30, 100, 10);
                    gp.DrawImage(Properties.Resources.Boss, MyBoss.Location);
                }
                if(MyItem.Location.Y>0)
                {
                    if (MyItem.TypeItem == 1)
                    {
                        gp.DrawImage(Properties.Resources.Bullet_Item, MyItem.Location);
                    }
                    else
                    {
                        if (MyItem.TypeItem == 0)
                        {
                            gp.DrawImage(Properties.Resources.HeartItem, MyItem.Location);
                        }
                    }
                }
                gp.DrawImage(Properties.Resources.Player, MyPlayer.Location);
                gp.FillRectangle(new SolidBrush(Color.Red), MyPlayer.Location.X+10, MyPlayer.Location.Y + 50, (MyPlayer.Health*22)/10, 3);
                gp.DrawRectangle(new Pen(Color.Black), MyPlayer.Location.X+10, MyPlayer.Location.Y + 50, 22, 2);
                for (int i = 0; i < MyPlayer.MyBullet.Count; i++)
                {
                    gp.DrawImage(Properties.Resources.laserBlue01, MyPlayer.MyBullet[i].Location);
                }
            }
            Main_PictureBox.Image = bitmap;
        }
       
       
        #region Classs
        public class Star
        {
            public PointF Location;
            public int StarType;
        }
        public class Enemy
        {
            public PointF Location;
            public int Health;
            public int EnemyType;
            public List<Bullet> EnemyBullet;
        }
        public class AttackEnemy
        {
            public PointF Location;
            public int Health;
            public int EnemyType;
            public List<Bullet> EnemyBullet;
            public float a;
            public float b;
            public float c;
            public float DeltaX;
        }
        public class Boss
        {
            public PointF Location;
            public int Health;
            public List<Bullet> BossBulletType1;
            public List<Bullet> BossBulletType2;
            public bool FinalSkill;
            public int TotalTime;
        }
        public class Player
        {
            public PointF Location;
            public List<Bullet> MyBullet;
            public int Mark;
            public int Level;
            public int Health;
        }
        public class Bullet
        {
            public PointF Location;
            public int TypeBullet;
            public float a;
            public float b;
            public float Times;
            public int TotalTime;
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
        public class Item
        {
            public PointF Location;
            public int TypeItem;
            public int Times;
        }
        #endregion
    }
}
