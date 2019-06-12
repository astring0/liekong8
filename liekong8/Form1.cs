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
        List<Button> light = new List<Button>();
        int ser = -1, weizhi = 0;
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
            for (int i = 1; i < 14; ++i)
            {
                light.Add((Button)Controls.Find("L" + i, true)[0]);
            }

            V0 = 250;
            hjsd = (float)250.0;
            hjjsd = (float)120.0;
            timer1.Interval = 30;
            timecost =  (float)timer1.Interval/1000;
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
            foreach (Button a in light)
            {
                a.BackColor = Color.FromArgb(0, 64, 0);
                a.Text = "L5";
            }
            timer1.Stop();
            timer1.Enabled = false;
            label4.Text = label7.Text = label8.Text = "0";
            crh.Location = new Point(0, 38);
            tmp = timespend = weizhi=0;
        }
        public void grachatinit()
        {
            chart1.Titles.Add("s1");
            chart1.Titles[0].Text = "制动曲线";            
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 400;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 12000;
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
        {
            if (crh.Left> 1111 - (int)(fspeed * fspeed / 2 / jiasudu))
            {
                tmp += fspeed * timecost - (float)0.5 * jiasudu * timecost * timecost;
                fspeed -= timecost * jiasudu;
            }
            else if(fspeed<hjsd && runtype.Text == "加速")
            {
                tmp += fspeed * timecost + (float)0.5 * 80 * timecost * timecost;
                fspeed += timecost * 80;
            }
            else if(fspeed> hjsd)
            {
                tmp += fspeed * timecost - (float)0.5 * jiasudu * timecost * timecost;
                fspeed -= timecost * jiasudu;
            }
            else
            {
                tmp += fspeed * timecost;
            }

            if ( weizhi < 12 && crh.Left + 64 > light[weizhi].Left )
            {
                light[weizhi].BackColor = Color.Red;
                light[weizhi].Text = "HU";
                light[weizhi - 1 < 0 ? 12 : weizhi - 1].BackColor = Color.Yellow;
                light[weizhi - 1 < 0 ? 12 : weizhi - 1].Text = "LU";
                light[weizhi - 2 < 0 ? 12 : weizhi - 2].BackColor = Color.FromArgb(128, 255, 128);
                light[weizhi - 2 < 0 ? 12 : weizhi - 2].Text = "L";
                light[weizhi - 3 < 0 ? 12 : weizhi - 3].BackColor = Color.Lime;
                light[weizhi - 3 < 0 ? 12 : weizhi - 3].Text = "L1";
                light[weizhi - 4 < 0 ? 12 : weizhi - 4].BackColor = Color.FromArgb(0, 192, 0);
                light[weizhi - 4 < 0 ? 12 : weizhi - 4].Text = "L2";
                light[weizhi - 5 < 0 ? 12 : weizhi - 5].BackColor = Color.Green;
                light[weizhi - 5 < 0 ? 12 : weizhi - 5].Text = "L3";
                light[weizhi - 6 < 0 ? 12 : weizhi - 6].BackColor = Color.FromArgb(0, 64, 0);
                light[weizhi - 6 < 0 ? 12 : weizhi - 6].Text = "L4";
                light[weizhi - 7 < 0 ? 12 : weizhi - 7].BackColor = Color.FromArgb(0, 56, 0);
                light[weizhi - 7 < 0 ? 12 : weizhi - 7].Text = "L5";
                weizhi++;
            }

            this.chart1.Series[ser].Points.AddXY(crh.Left*10, fspeed);
            timespend += timecost;
            if ((int)tmp >= 1)
            {
                crh.Left += (int)tmp;
                tmp -= (float)(int)tmp;                
            }

            if (crh.Location.X > 1111 || fspeed < 0)
            {
                timer1.Stop();
                timer1.Enabled = false;
                label4.Text = "0";
                this.chart1.Series[ser].Points.AddXY(11129, 0);
            }
        }

        private void drawanquan(object sender, EventArgs e)
        {
            int aqu = (int)(hjsd * hjsd / 2 / jiasudu);
            List<float> anquanx = new List<float>();
            List<float> anquany = new List<float>();
            anquanx.Add(0);
            anquany.Add(hjsd);
            anquanx.Add((1111 - aqu-1)*10);
            anquany.Add(hjsd);
            for (int i = 1111 - aqu; i < 1111; i++)
            {
                anquanx.Add(i*10);
                anquany.Add((float)Math.Sqrt(-2 * jiasudu * (i - 1111 + aqu) + hjsd * hjsd));
            }
            anquanx.Add(11110);
            anquany.Add(0);
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
