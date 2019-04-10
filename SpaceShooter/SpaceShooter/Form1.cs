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
        Star[] ListStar= new Star[13];
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
        }

        private void StarTimer_Tick(object sender, EventArgs e)
        {
            rd = new Random();          
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
        public void Draw()
        {
            gp.Clear(Color.White);

            gp.FillRectangle(new SolidBrush(Color.FromArgb(255, (byte)0, (byte)0, (byte)56)), 0, 0, Main_PictureBox.Width, Main_PictureBox.Height);
          
            for (int i = 0; i < ListStar.Length; i++)
            {
                if (ListStar[i].StarType == 1)
                {
                    gp.DrawImage(Properties.Resources.star1, ListStar[i].Location.X, ListStar[i].Location.Y);
                }
                else
                {
                    if (ListStar[i].StarType == 0 || ListStar[i].StarType == 2)
                        gp.DrawImage(Properties.Resources.star2, ListStar[i].Location.X, ListStar[i].Location.Y);
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
                MyPlayer.Location.X = e.Location.X + 25;
                MyPlayer.Location.Y = e.Location.Y + 25;
            }
            //Draw();
        }
        private void Start_Button_Click(object sender, EventArgs e)
        {
            Start_Button.Hide();
            Exit_Button.Hide();
            Cursor.Hide();
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
            public int  StarType;
        }
        public class Emeny
        {
            public PointF Location;
            public int EmenyType;
        }
        public class Player
        {
            public PointF Location;            
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
