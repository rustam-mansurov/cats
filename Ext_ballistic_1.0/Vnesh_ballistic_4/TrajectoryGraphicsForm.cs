using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using VneshBallistic;

namespace Vnesh_ballistic
{
    public partial class TrajectoryGraphicsForm : Form
    {
        public static bool stop = true;
        public static int step = 1;

        public TrajectoryGraphicsForm(string NameForm, VneshBall VB)
        {
            InitializeComponent();
            this.Text = NameForm;
            /*chart_Oxy = chart1_Oxy;
            chart_Oxz = chart1_Oxz;*/
            this.VB = VB;
            label_t = label1_t; label_x = label1_x; label_y = label1_y; label_z = label1_z; label_D = label1_D;
            label_V = label1_V; label_psy = label1_psy; label_teta = label1_teta;
            chart_Oxy.ChartAreas[0].AxisX.Title = "x, м"; chart_Oxy.ChartAreas[0].AxisY.Title = "y, м";
            chart_Oxz.ChartAreas[0].AxisX.Title = "x, м"; chart_Oxz.ChartAreas[0].AxisY.Title = "z, м";
            chart_Oxy.Series[0].Points.Clear(); chart_Oxz.Series[0].Points.Clear();
            chart_Oxy.ChartAreas[0].AxisX.LabelStyle.Format = "F0";
            chart_Oxz.ChartAreas[0].AxisX.LabelStyle.Format = "F0";
            chart_Oxy.ChartAreas[0].AxisX.Minimum = 0; chart_Oxz.ChartAreas[0].AxisX.Minimum = 0;
            double xMax = VB.RRR[VB.chisloUzlovSetky - 1][2];
            xMax = 5000 * (Math.Truncate(xMax / 5000) + 1);
            chart_Oxy.ChartAreas[0].AxisX.Maximum = xMax;
            chart_Oxz.ChartAreas[0].AxisX.Maximum = xMax;
            double xx, yy, zz; step = 1;
            for (int j = step; j <= VB.chisloUzlovSetky - 1; j++)
            {
                xx = VB.RRR[j - 1][2]; yy = VB.RRR[j - 1][3]; zz = VB.RRR[j - 1][4];
                chart_Oxy.Series[0].Points.AddXY(xx, yy); chart_Oxz.Series[0].Points.AddXY(xx, zz);
            }
            label_t.Text = "t = " + VB.RRR[0][0].ToString("0.00") + " с";
            label_x.Text = "x = " + VB.RRR[0][2].ToString("0.0") + " м";
            label_y.Text = "y = " + VB.RRR[0][3].ToString("0.0") + " м";
            label_z.Text = "z = " + VB.RRR[0][4].ToString("0.0") + " м";
            label_D.Text = "D = " + (Math.Sqrt(VB.RRR[0][2] * VB.RRR[0][2] + VB.RRR[0][4] * VB.RRR[0][4])).ToString("0.0") + " м";
            label_V.Text = "V = " + VB.RRR[0][5].ToString("0.0") + " м/с";
            label_teta.Text = "θ = " + VB.RRR[0][6].ToString("0.00") + " град.";
            label_psy.Text = "ψ = " + VB.RRR[0][7].ToString("0.00") + " град.";
            try
            {
                chart_Oxy.Series.Add("Tochka");
                chart_Oxy.Series["Tochka"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                chart_Oxy.Series["Tochka"].MarkerColor = Color.Red;
                chart_Oxy.Series["Tochka"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                chart_Oxy.Series["Tochka"].MarkerSize = 8;                
                chart_Oxy.Series["Tochka"].Points.AddXY(0, 0);
            }
            catch
            { }
            try
            {
                chart_Oxz.Series.Add("Tochka");
                chart_Oxz.Series["Tochka"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                chart_Oxz.Series["Tochka"].MarkerColor = Color.Red;
                chart_Oxz.Series["Tochka"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                chart_Oxz.Series["Tochka"].MarkerSize = 8;
                chart_Oxz.Series["Tochka"].Points.AddXY(0, 0);
            }
            catch
            { }
        }

        /*public Chart chart_Oxy;
        public Chart chart_Oxz;*/
        public Label label_t, label_x, label_y, label_z, label_D, label_V, label_psy, label_teta;
        public VneshBall VB = new VneshBall();
        private void button1_Click(object sender, EventArgs e){    }
        void Paint_T()
        {
            try
            {
                if (VB.chisloUzlovSetky < 2)
                    MessageBox.Show("Отcутcтвуют данные");
                else
                {
                    double xx, yy, zz;
                    for (int j = step; j <= VB.chisloUzlovSetky - 1; j++)
                    {
                        xx = VB.RRR[j - 1][2]; yy = VB.RRR[j - 1][3]; zz = VB.RRR[j - 1][4];
                        chart_Oxy.Series["Tochka"].Points[0].XValue = xx;
                        chart_Oxy.Series["Tochka"].Points[0].YValues[0] = yy;
                        chart_Oxz.Series["Tochka"].Points[0].XValue = xx;
                        chart_Oxz.Series["Tochka"].Points[0].YValues[0] = zz;

                        if ((j - 1) % 10 == 0)
                        {
                            label_t.Text = "t = " + VB.RRR[j - 1][0].ToString("0.00") + " с";
                            label_x.Text = "x = " + VB.RRR[j - 1][2].ToString("0.0") + " м";
                            label_y.Text = "y = " + VB.RRR[j - 1][3].ToString("0.0") + " м";
                            label_z.Text = "z = " + VB.RRR[j - 1][4].ToString("0.0") + " м";
                            label_D.Text = "D = " + (Math.Sqrt(xx * xx + zz * zz)).ToString("0.0") + " м";
                            label_V.Text = "V = " + VB.RRR[j - 1][5].ToString("0.0") + " м/с";
                            label_teta.Text = "θ = " + VB.RRR[j - 1][6].ToString("0.00") + " град.";
                            label_psy.Text = "ψ = " + VB.RRR[j - 1][7].ToString("0.00") + " град.";
                        }
                        if (stop)
                        {
                            step = j;
                            break;
                        }
                        if (j == VB.chisloUzlovSetky - 1)
                        {
                            label_t.Text = "t = " + VB.RRR[j-1][0].ToString("0.00") + " с";
                            label_x.Text = "x = " + VB.RRR[j-1][2].ToString("0.0") + " м";
                            label_y.Text = "y = " + VB.RRR[j-1][3].ToString("0.0") + " м";
                            label_z.Text = "z = " + VB.RRR[j-1][4].ToString("0.0") + " м";
                            label_D.Text = "D = " + (Math.Sqrt(xx * xx + zz * zz)).ToString("0.0") + " м";
                            label_V.Text = "V = " + VB.RRR[j-1][5].ToString("0.0") + " м/с";
                            label_teta.Text = "θ = " + VB.RRR[j - 1][6].ToString("0.00") + " град.";
                            label_psy.Text = "ψ = " + VB.RRR[j-1][7].ToString("0.00") + " град.";
                            step = 1;
                            stop = true;
                            button1.Text = "Пуск";
                        }                        
                        Thread.Sleep(4);
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
            catch
            { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {            
            if(stop)
            {
                stop = !stop;
                button1.Text = "Стoп";
                Paint_T();                
            }
            else
            {
                stop = !stop;
                button1.Text = "Пуск";
            }            
        }
        private void Form_dopolnit_FormClosed(object sender, FormClosedEventArgs e)
        {
            chart_Oxy.Series[0].Points.Clear();
            chart_Oxz.Series[0].Points.Clear();
            this.DestroyHandle();
        }
    }
}
