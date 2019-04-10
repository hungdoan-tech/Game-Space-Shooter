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
        star[] ListStar= new star[13];  
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(Main_PictureBox.Width, Main_PictureBox.Height);
            gp = Graphics.FromImage(bitmap);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            rd = new Random();
            for (int i = 0; i < ListStar.Length; i++)
            {
                ListStar[i] = new star();
                ListStar[i].x = rd.Next(0, 500);
                ListStar[i].y = rd.Next(100, 600);
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
                    ListStar[i].y += 3;
                }
                else
                {
                    ListStar[i].y += 5;
                }
            }
            for (int i = 0; i < ListStar.Length; i++)
            {
                if (ListStar[i].y >= Main_PictureBox.Height)
                {
                    ListStar[i].x = rd.Next(0, 500);
                    ListStar[i].y = 0;
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
                    gp.DrawImage(Properties.Resources.star1, ListStar[i].x, ListStar[i].y);
                }
                else
                {
                    if (ListStar[i].StarType == 0 || ListStar[i].StarType == 2)
                        gp.DrawImage(Properties.Resources.star2, ListStar[i].x, ListStar[i].y);
                }
            }
            Main_PictureBox.Image = bitmap;
        }
        public class star
        {
            public float x;
            public float y;
            public int StarType;
        }

        private void Start_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 ab = new Form2();
            ab.ShowDialog();   
        }

        private void Exit_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
