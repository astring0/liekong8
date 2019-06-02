using Microsoft.VisualBasic.PowerPacks;
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
        Graphics g;
        Pen pen1;
        int stage = 0;
        int quduan;
        float tmp=0,timecost =0,jsd,ts,v0,fsp,jjx,cyx,hjs,jjxzjs, cyxzjs, hjjs;
        public float jiasudu//加速度
        {
            get { return jsd; }
            set { jsd = value; label7.Text = value.ToString(); }
        }
        public float timespend//时间间隔
        {
            get { return ts; }
            set { ts = value;label8.Text = value.ToString(); }
        }
        public float V0//初速度
        {
            get { return v0; }
            set { v0 = value; csd.Text = value.ToString(); }
        }
        public float fspeed//速度
        {
            get { return fsp; }
            set { fsp = value; label4.Text = value.ToString(); }
        }
        public float jjxzsd//紧急限制速度
        {
            get { return jjx; }
            set { jjx = value; showjjxzsd.Text = value.ToString(); }
        }
        public float cyxzsd//常用限制速度
        {
            get { return cyx; }
            set { cyx = value; showcyxzsd.Text = value.ToString(); }
        }
        public float hjsd//缓解速度
        {
            get { return hjs; }
            set { hjs = value; showhjsd.Text = value.ToString(); }
        }

        

        public float jjxzjsd//紧急限制加速度
        {
            get { return jjxzjs; }
            set { jjxzjs = value; showjjxzjsd.Text = value.ToString(); }
        }        
        public float cyxzjsd//常用限制加速度
        {
            get { return cyxzjs; }
            set { cyxzjs = value; showcyxzjsd.Text = value.ToString(); }
        }
        public float hjjsd//缓解加速度
        {
            get { return hjjs; }
            set { hjjs = value; showhjjsd.Text = value.ToString(); }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            jjxzjsd = Convert.ToInt16(textBox4.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cyxzjsd = Convert.ToInt16(textBox5.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            hjjsd = Convert.ToInt16(textBox6.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hjsd = Convert.ToInt16(textBox3.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cyxzsd = Convert.ToInt16(textBox2.Text);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            jjxzsd = Convert.ToInt16(textBox1.Text);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            runtype.SelectedIndex = 1;
            g = panel2.CreateGraphics();
            pen1 = new Pen(Color.Black, 4);            
            init();
        }
        public void init()
        {
            stage = 0;
            V0 = 200;
            jjxzsd = (float)500.0;
            cyxzsd = (float)300.0;
            hjsd = (float)200.0;
            jjxzjsd = (float)50.0;
            cyxzjsd = (float)30.0;
            hjjsd = (float)20.0;
            timer1.Stop();
            timer1.Enabled = false;
            label4.Text = label7.Text = label8.Text = "0";
            crh.Location = new Point(0, 38);
            timer1.Interval = 100;
            timecost = (float)1 / (float)timer1.Interval;
            jiasudu = (float)Convert.ToInt16(showhjjsd.Text);
            fspeed = (float)Convert.ToInt16(csd.Text);
            tmp = timespend = 0;
        }

        private void chusuduclik_Click(object sender, EventArgs e)
        {
            V0 = (float)Convert.ToInt16(chusudu.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            fspeed = V0;
            jiasudu = hjjsd;
            label7.Text = jiasudu.ToString();
            quduan = calcquduan();
            timer1.Start();
            timer1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            init();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            jiasudu = (float)50;
            label7.Text = "50";
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {   if(crh.Left> 1000-quduan)
            {
                tmp += fspeed * timecost - (float)0.5 * jiasudu * timecost * timecost;
                fspeed -= timecost * jiasudu;
            }
            else
            {
                tmp += fspeed * timecost;
            }
            
            timespend += timecost;
            if ((int)tmp >= 1)
            {
                crh.Left += (int)tmp;
                tmp -= (float)(int)tmp;
                g.DrawLine(pen1, crh.Left + (float)55, -fspeed + (float)360, crh.Left + (float)56, -fspeed + (float)360);
            }

            if (crh.Location.X > 1107 || fspeed < 0)
            {
                timer1.Stop();
                timer1.Enabled = false;
                label4.Text = "0";
            }
        }
        private void timer1_Tick2(object sender, EventArgs e)
        {
            if (fspeed > jjxzsd){stage = 1;}
            if (fspeed > cyxzsd && fspeed < jjxzsd){stage = 2;}
            if (fspeed > hjsd && fspeed < cyxzsd) { stage = 3; }
            if (fspeed < hjsd) { stage = 4; }
        }
        private int calcquduan()
        {
            return (int)(fspeed * fspeed / 2 / jiasudu);
        }
    }
}
