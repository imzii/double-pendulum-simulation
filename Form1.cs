using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 물II_이중진자프로그램
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(TimerCallback);
            DoubleBuffered = true;
        }

        float r1 = 200; //진자2 줄 길이
        float r2 = 200; //진자2 줄 길이
        float m1 = 40; //진자1 질량
        float m2 = 40; //진자2 질량
        float a1 = Convert.ToSingle(Math.PI / 2); //각1
        float a2 = Convert.ToSingle(Math.PI); //각2
        float a1_v = 0; //진자1 각속도
        float a2_v = 0; //진자2 각속도
        double g = 0.7; //중력상수

        float px2 = -1;
        float py2 = -1;

        float x1p, y1p, x2p, y2p;

        private void TimerCallback(object sender, EventArgs e)
        {
            float num1 = Convert.ToSingle(-g * (2 * m1 + m2) * Math.Sin(a1));
            float num2 = Convert.ToSingle(-m2 * g * Math.Sin(a1 - 2 * a2));
            float num3 = Convert.ToSingle(-2 * Math.Sin(a1 - a2) * m2);
            float num4 = Convert.ToSingle(a2_v * a2_v * r2 + a1_v * a1_v * r1 * Math.Cos(a1 - a2));
            float den = Convert.ToSingle(r1 * (2 * m1 + m2 - m2 * Math.Cos(2 * a1 - 2 * a2)));
            float a1_a = (num1 + num2 + num3 * num4) / den;

            num1 = Convert.ToSingle(2 * Math.Sin(a1 - a2));
            num2 = Convert.ToSingle((a1_v * a1_v * r1 * (m1 + m2)));
            num3 = Convert.ToSingle(g * (m1 + m2) * Math.Cos(a1));
            num4 = Convert.ToSingle(a2_v * a2_v * r2 * m2 * Math.Cos(a1 - a2));
            den = Convert.ToSingle(r2 * (2 * m1 + m2 - m2 * Math.Cos(2 * a1 - 2 * a2)));
            float a2_a = Convert.ToSingle((num1 * (num2 + num3 + num4)) / den);


            float x1 = Convert.ToSingle(r1 * Math.Sin(a1));
            float y1 = Convert.ToSingle(r1 * Math.Cos(a1));

            float x2 = Convert.ToSingle(x1 + r2 * Math.Sin(a2));
            float y2 = Convert.ToSingle(y1 + r2 * Math.Cos(a2));

            x1p = x1;
            y1p = y1;
            x2p = x2;
            y2p = y2;

            a1_v += a1_a;
            a2_v += a2_a;
            a1 += a1_v;
            a2 += a2_v;

            px2 = x2;
            py2 = y2;
            this.Invalidate();
            return;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.DrawLine(new Pen(Brushes.Black), this.Width / 2, this.Height / 2, x1p + this.Width / 2, y1p + this.Height / 2);
            g.DrawEllipse(new Pen(Brushes.Black), new Rectangle((int)x1p + this.Width / 2 - (int)m1 / 2, (int)y1p - (int)m1 / 2 + this.Height / 2, (int)m1, (int)m1));
            g.DrawLine(new Pen(Brushes.Black), x1p + this.Width / 2, y1p + this.Height / 2, x2p + this.Width / 2, y2p + this.Height / 2);
            g.DrawEllipse(new Pen(Brushes.Black), new Rectangle((int)x2p + this.Width / 2 - (int)m2 / 2, (int)y2p - (int)m2 / 2 + this.Height / 2, (int)m2, (int)m2));
            base.OnPaint(e);
        }
    }
}
