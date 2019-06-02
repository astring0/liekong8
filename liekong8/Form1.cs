using Microsoft.VisualBasic.PowerPacks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace liekong8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           
            InitializeComponent();
        }
        int ser = -1;
        float tmp=0,timecost =0,jsd,ts,v0,fsp,hjs, hjjs;
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
        public float hjsd//缓解速度
        {
            get { return hjs; }
            set { hjs = value; showhjsd.Text = value.ToString(); }
        }
        public float hjjsd//缓解加速度
        {
            get { return hjjs; }
            set { hjjs = value; showhjjsd.Text = value.ToString(); }
        }  

        private void button10_Click(object sender, EventArgs e)
        {
            hjjsd = Convert.ToInt16(textBox6.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hjsd = Convert.ToInt16(textBox3.Text);
        }       
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            V0 = 200;
            hjsd = (float)200.0;
            hjjsd = (float)120.0;
            timer1.Interval = 30;
            timecost = (float)1 / (float)timer1.Interval;
            jiasudu = (float)Convert.ToInt16(showhjjsd.Text);
            fspeed = (float)Convert.ToInt16(csd.Text);
            runtype.SelectedIndex = 1;
            init();
            grachatinit();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            ser = -1;
        }

        public void init()
        {
            timer1.Stop();
            timer1.Enabled = false;
            label4.Text = label7.Text = label8.Text = "0";
            crh.Location = new Point(0, 38);
            tmp = timespend = 0;
        }
        public void grachatinit()
        {
            chart1.Titles.Add("s1");
            chart1.Titles[0].Text = "制动曲线";
            
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 400;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 1200;
            chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
            chart1.ChartAreas[0].AxisY.Enabled = AxisEnabled.True;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = true;

        }
        private void chusuduclik_Click(object sender, EventArgs e)
        {
            V0 = (float)Convert.ToInt16(chusudu.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series.Add((++ser).ToString());
            chart1.Series[ser].ChartType = SeriesChartType.Line;
            chart1.Series[ser].BorderWidth = 3;
            chart1.Series[ser].Name = "实际速度曲线"+ser.ToString()+" "+V0.ToString();
            fspeed = V0;
            jiasudu = hjjsd;
            label7.Text = jiasudu.ToString();
            timer1.Start();
            timer1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            init();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {   if(crh.Left> 1000- (int)(fspeed * fspeed / 2 / jiasudu))
            {
                tmp += fspeed * timecost - (float)0.5 * jiasudu * timecost * timecost;
                fspeed -= timecost * jiasudu;
            }
            else if(fspeed<hjsd && runtype.Text == "加速")
            {
                tmp += fspeed * timecost + (float)0.5 * 80 * timecost * timecost;
                fspeed += timecost * 80;
            }
            else
            {
                tmp += fspeed * timecost;
            }
            this.chart1.Series[ser].Points.AddXY(crh.Left, fspeed);
            timespend += timecost;
            if ((int)tmp >= 1)
            {
                crh.Left += (int)tmp;
                tmp -= (float)(int)tmp;                
            }

            if (crh.Location.X > 1107 || fspeed < 0)
            {
                timer1.Stop();
                timer1.Enabled = false;
                label4.Text = "0";
            }
        }

        private void drawanquan(object sender, EventArgs e)
        {
            int aqu = (int)(hjsd * hjsd / 2 / jiasudu);
            List<float> anquanx = new List<float>();
            List<float> anquany = new List<float>();
            anquanx.Add(0);
            anquany.Add(hjsd);
            anquanx.Add(1000 - aqu-1);
            anquany.Add(hjsd);
            for (int i = 1000 - aqu; i < 1001; i++)
            {
                anquanx.Add(i);
                anquany.Add((float)Math.Sqrt(-2 * jiasudu * (i - 1000 + aqu) + hjsd * hjsd));
            }
            float[] x = anquanx.ToArray();
            float[] y = anquany.ToArray();
            chart1.Series.Add((++ser).ToString());
            chart1.Series[ser].ChartType = SeriesChartType.Line;
            chart1.Series[ser].BorderWidth = 3;
            chart1.Series[ser].Points.DataBindXY(x, y);
            chart1.Series[ser].Name = "允许速度曲线"+ser.ToString()+" "+hjsd.ToString();

        }
    }
}
