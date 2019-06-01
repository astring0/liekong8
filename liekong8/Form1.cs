using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace liekong8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CRH crh1;
        Graphics g;
        Pen pen1;

        public class CRH
        {
            public CRH(PictureBox a, Graphics g2, Pen pen2, Label l1)
            {
                showspeed = l1;
                g = g2;
                pen1 = pen2;
                car = a;
                move = new Timer();
                move.Tick += new EventHandler(movetimer);
                move.Interval  = 50;
                speed = (float)1 / (float)move.Interval;
                init();
            }
            public Label showspeed { get; set; }
            public Graphics g { get; set; }
            public Pen pen1 { get; set; }

            public Timer move { get; set; }
            public PictureBox car { get; set; }
            public Label showjiasudu { get; set; }
            public Label showtime { get; set; }
            public Label showm { get; set; }
            public float speed { get; set; }
            public float fspeed { get; set; }
            public float jiasudu {get;set;}
            private float tmp { get; set; }
            private float timespend { get; set; }
            public void init()
            {
                car.Location = new Point(0, 38);
                fspeed = (float)200;
                jiasudu = (float)20;
                tmp = timespend=0;
                
                
            }
            public void movetimer(object sender, EventArgs e)
            {
                
                tmp += fspeed * speed - (float)0.5*jiasudu* speed* speed;
                fspeed -= speed * jiasudu;
                timespend += speed;
                showtime.Text = timespend.ToString();
                if ((int)tmp>=1)
                {
                    car.Left += (int)tmp;
                    tmp -= (float)(int)tmp;
                    showspeed.Text = fspeed.ToString();
                    g.DrawLine(pen1, car.Left + (float)55, -fspeed+ (float)370, car.Left+ (float)56, -fspeed + (float)370);
                }              
                
                if (car.Location.X > 1107||fspeed<0)
                {
                    move.Stop();
                    move.Enabled = false;
                    showspeed.Text = "0";
                }
                
            }
            public void kaiche()
            {
                showjiasudu.Text = jiasudu.ToString();
                move.Start();
                move.Enabled = true;
            }
            public void tingche()
            {
                move.Stop();
                move.Enabled = false;
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            crh1.tingche();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = label7.Text = label8.Text = "0";
            g = panel2.CreateGraphics();
            pen1 = new Pen(Color.Black, 4);
            crh1 = new CRH(pictureBox1,g,pen1,label4);
            crh1.showjiasudu = label7;
            crh1.showtime = label8;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            crh1.kaiche();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            crh1.init();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            crh1.jiasudu = (float)50;
            label7.Text = "50";
        }
    }
}
