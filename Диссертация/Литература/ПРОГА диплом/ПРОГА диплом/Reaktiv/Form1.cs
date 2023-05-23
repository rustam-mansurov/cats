using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace Reaktiv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisX.Minimum = 0;
            //chart1.ChartAreas[0].AxisX.Maximum = 4000;
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisY.Minimum = 0;


        }
        double h = 300, g = 9.81, d = 0.03, m = 0.475,V=525, V0 = 525, teta0 = 0,teta=0,psi0=0,psi=0, l = 0.09, Ix = 0.1455, T0 = 11.9 + 273, a0 = 340.7;
        double S;
        double[] y = { 0, 79, 189, 200, 400, 541, 600, 718, 727, 800, 894, 1000, 1071, 1200, 1220, 1400, 1422, 1600, 1800, 2000, 2128, 2200, 2400, 2414, 2600, 2700, 2800, 2817, 2937, 2974, 3000, 3200, 3400, 3543, 3600, 3800, 4000, 4200, 4400, 4529, 4600, 4800, 4897, 5000, 5200, 5400, 5532, 5600, 5783, 5800, 6000, 6101, 6396, 6500, 6505, 6823, 7000, 7094, 7137, 7500, 7807, 7871, 8000, 8500, 8518, 8753, 9000, 9056, 9076, 9500, 10000, 10034, 10272, 10500, 10832, 10926, 11000, 11062 }; //Высота
        double[] p = { 100000, 100000, 98700, 98570, 96240, 94610, 93940, 92600, 92500, 91680, 90630, 89470, 88690, 87350, 87140, 85230, 85000, 83150, 81130, 79130, 77880, 77190, 75280, 75150, 73410, 72480, 71570, 71410, 70330, 70000, 69770, 68020, 66310, 65110, 64630, 62980, 61370, 59780, 58220, 57240, 56700, 55210, 54490, 53740, 52310, 50910, 50000, 49540, 48310, 48200, 46890, 46250, 44400, 43760, 43730, 41820, 40790, 40250, 40000, 37970, 36310, 35970, 35290, 32760, 32670, 31520, 30350, 30090, 30000, 28110, 26050, 25910, 25000, 24150, 22960, 22640, 22380, 22170 };//Давление
        double[] T = { 288.75, 283.8, 285, 284.9, 282.4, 280.9, 280.3, 279.2, 279.2, 279.2, 279.2, 278.9, 278.7, 278, 277.9, 276.8, 276.7, 275.8, 274.8, 274.2, 273.6, 273,275 ,271.1, 270.9, 269.8, 269.4, 269.4, 269.4, 268.9, 268.8, 268.7, 267.9, 266.7, 266.1, 265.5, 263.6, 262.1, 259.8, 257.7, 256.4, 256.2, 254.9, 254.1, 253.2, 252.2, 251.3, 250.2, 249.7, 248.6, 248.6, 247.9, 248.1, 245.5, 244.4, 244.3, 243.1, 240.3, 238.8, 238.6, 236.7, 233.3, 230.8, 230.6, 227.9, 227.9, 223.3, 222.5, 222.2, 222.2, 223.1, 225.3, 225.7, 225.4, 225, 225.6, 224.9, 224.5, 224.2};//Температура
        double[] alfawy = { 10, 30, 30, 22, 56, 38, 41, 46, 46, 47, 48, 252, 254, 253, 253, 249, 248, 253, 252, 260, 257, 258, 256, 256, 264, 262, 262, 262, 263, 262, 262, 261, 259, 258, 258, 257, 255, 255, 254, 254, 253, 254, 253, 253, 253, 253, 252, 252, 253, 253, 252, 251, 249, 249, 249, 249, 251, 250, 250, 252, 253, 252, 252, 251, 251, 247, 247, 247, 247, 248, 252, 252, 248, 249, 246, 247, 246, 246 };//Направление ветра
        double[] wy = { 3, 3, 3, 3, 8, 6, 8, 12, 12, 16, 21, 22, 23, 19, 18, 18, 18, 19, 20, 19, 19, 19, 19, 19, 20, 22, 22, 22, 22, 22, 22, 23, 24, 24, 24, 25, 25, 26, 26, 27, 27, 28, 28, 29, 31, 33, 34, 35, 38, 38, 43, 45, 48, 50, 50, 50, 52, 52, 52, 52, 52, 52, 52, 53, 53, 55, 55, 55, 55, 55, 51, 51, 49, 47, 43, 42, 41, 41 };//Скорость ветра
        double[] alfa0 = { 0.206, 0.173903, 0.371809, 0.085860, 0.033077, 0.139043, 0.37 };
        double[] alfa1 = { 0, 0.106376, -0.307251, 0.084315, 0.139076, 0.84848, 0 };
        double[] alfa2 = { 0, -0.086023, 0.126190, 0.000399, -0.013524, -0.007699, 0 };
        double[] M1 = { 0, 0.6, 0.8, 1.1, 1.5, 2.9, 4.9 };
        double[] M2 = { 0.6, 0.8, 1.1, 1.5, 2.9, 4.9, 100 };
        double[] M3 = { };
        // double[] Cx = { 0.0, 0.8, 1.0, 1.2, 5.0, 0.1860, 0.0000, 0.0000, 0.0000, 3.0, -0.7794, 4.7477, -7.6523, 4.038, 3.5, -17.441, 44.811, -37.284, 10.298, 4.5, 0.7088, -0.2797, 0.0512, -0.0035, 5.0 };
        // double[] Cy = { 0.0,0.4751, 13.278, 1.3619, - 0.20781};
        // double[] Cz = { 0.0, 0.1847, 3.5200, 0, 0 };

        bool chartOn = true;
        double Vx,Vy,Vz;
        int number = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            chartOn = true;
            dataGridView1.Rows.Clear();
            h = Convert.ToDouble(textBox6.Text);
            V0 = 525;
            Vx = Convert.ToDouble(textBox1.Text) / 3.6;
            Vy = Convert.ToDouble(textBox3.Text) / 3.6;
            Vz = Convert.ToDouble(textBox4.Text) / 3.6;
            teta0 = Convert.ToDouble(textBox2.Text) * Math.PI / 180;
            psi0 = Convert.ToDouble(textBox5.Text) * Math.PI / 180;

            
            
            RungeKutte(0);
            number++;
        }

        double mx = 0.006;


        private void Form1_Load(object sender, EventArgs e)
        {
            S = (Math.PI * d*d) / 4;
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
            number = 0;
        }

        

        public double mah(double V, double a)//Число маха
        {
            double f = V / a;
            return f;
        }
        public double q(double a, double mah)//скоростной напор в воздухе
        {
            double f = (a * a * mah * mah * 1.2754) / 2;
            return f;
        }
        public double wxk(double V, double teta, double psi, int i) //Скорость ветра по х
        {
            double f = -wy[i] * Math.Cos(alfawy[i] - (0 - psi)) * Math.Cos(teta);
            return f;
        }

        double[] VxArr = new double[3125];
        double[] VyArr = new double[3125];
        double[] VzArr = new double[3125];
        double[] tetaArr = new double[3125];
        double[] psiArr = new double[3125];

        double VxR, VyR, VzR, tetaR, psiR;

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            int tableNumber = 0;
            chartOn = false;
            int countIter=5;
            for (int i = 0; i < countIter; i++)
            {
                for (int j = 0; j < countIter; j++)
                {
                    for (int k = 0; k < countIter; k++)
                    {
                        for (int n = 0; n < countIter; n++)
                        {
                            for (int m = 0; m < countIter; m++)
                            {
                                VxR = k * 50 / 3.6;
                                if (j < 3)
                                {
                                    VyR = j * 25 / 3.6;
                                }
                                else 
                                {
                                    VyR = -(j - 2) * 25 / 3.6;
                                }

                                if (i < 3)
                                {
                                    VzR = i * 25 / 3.6;
                                }
                                else
                                {
                                    VzR = -(i - 2) * 25 / 3.6;
                                }

                                if (m < 3)
                                {
                                    tetaR = m * 15 * Math.PI / 180;
                                }
                                else
                                {
                                    tetaR = -(m-2) * 15 * Math.PI / 180;
                                }

                                if (n < 3)
                                {
                                    psiR = n * 15 * Math.PI / 180;
                                }
                                else
                                {
                                    psiR = -(n-2) * 15 * Math.PI / 180;
                                }

                                teta0 = tetaR;
                                psi0 = psiR;
                                Vx = VxR;
                                Vy = VyR;
                                Vz = VzR;

                                RungeKutte(0);
                                
                                dataGridView2.Rows.Add();
                                dataGridView2.Rows[tableNumber].Cells[0].Value = Math.Round(tetaR, 4);
                                dataGridView2.Rows[tableNumber].Cells[1].Value = Math.Round(psiR, 4);
                                dataGridView2.Rows[tableNumber].Cells[2].Value = Math.Round(Vx, 4);
                                dataGridView2.Rows[tableNumber].Cells[3].Value = Math.Round(Vy, 4);
                                dataGridView2.Rows[tableNumber].Cells[4].Value = Math.Round(Vz, 4);
                                dataGridView2.Rows[tableNumber].Cells[5].Value = Math.Round(xMax, 4);
                                dataGridView2.Rows[tableNumber].Cells[6].Value = Math.Round(zMax, 4);
                                dataGridView2.Rows[tableNumber].Cells[7].Value = Math.Round(tMax, 4);
                               
                                tableNumber++;
                            }
                        }

                    }
                }
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //создание папки
            string path = @"C:/aaresults";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string results = "";
            for (int i = 0; i <= 3124; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    results += " " + dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
                results += "\n";
            }


            // запись в файл
            using (FileStream fstream = new FileStream("C:/aaresults/params.txt", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(results);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }

        private void button6_Click(object sender, EventArgs e) //Поиск угла для обратной задачи
        {
            chartOn = false;

            Vx = Convert.ToDouble(textBox8.Text) / 3.6;
            Vy = Convert.ToDouble(textBox9.Text) / 3.6;
            Vz = Convert.ToDouble(textBox10.Text) / 3.6;

            double eps = 1e-6;
            eps = Convert.ToDouble(textBox16.Text);

            double xMaxFind = Convert.ToDouble(textBox11.Text);
            double tetaA = -30 * Math.PI / 180;
            double tetaB = 30 * Math.PI / 180;
            double fX = 0;
            double tetaX = 0;

            double zMaxFind = Convert.ToDouble(textBox13.Text);
            double psiA = -30 * Math.PI / 180;
            double psiB = 30 * Math.PI / 180;
            double fZ = 0;
            double psiZ = 0;

            while (Math.Abs(tetaA-tetaB) > (2 * eps)  || Math.Abs(psiA - psiB) > (2 * eps))
            {
                tetaX = (tetaA + tetaB) / 2;
                psiZ = (psiA + psiB) / 2;
                teta0 = tetaX;
                psi0 = psiZ;
                RungeKutte(0);
                fX = xMax;
                fZ = zMax;
                if (fX < xMaxFind)
                {
                    tetaA = tetaX;
                }
                else
                {
                    tetaB = tetaX;
                }
                if (fZ < zMaxFind)
                {
                    psiA = psiZ;
                }
                else
                {
                    psiB = psiZ;
                }
            }
            textBox7.Text = Math.Round((tetaX * 180 / Math.PI),4).ToString();
            textBox12.Text = Math.Round((psiZ * 180 / Math.PI),4).ToString();
            textBox15.Text = Math.Round(fX , 4).ToString();
            textBox14.Text = Math.Round(fZ , 4).ToString();
        }

        public double wyk(double V, double teta, double psi, int i) //Скорость ветра по y
        {
            double f = wy[i] * Math.Cos(alfawy[i] - (0 - psi)) * Math.Sin(teta);
            return f;
        }
        public double wzk(double V, double psi, int i) //Скорость ветра по z
        {
            double f = -wy[i] * Math.Sin(alfawy[i] - (0 - psi));
            return f;
        }
        public double fomega(double mx, double q, double S, double l, double Ix) //Аксильная угловая скорость
        {
            double f = -(mx * q * S * l) / Ix;
            return f;
        }

        public double fT(double fy) //Температура
        {
            int i = 0;
            double f = 0;
            while (fy >= y[i + 1])
            {
                i++;
            }

            f = T[i] + (fy - y[i]) / (y[i + 1] - y[i]) * (T[i + 1] - T[i]);

            return f;
        }
        public double fp(double fy) //Давление
        {
            int i = 0;
            double f = 0;
            while (fy >= y[i + 1])
            {
                i++;
            }

            f = p[i] + (fy - y[i]) / (y[i + 1] - y[i]) * (p[i + 1] - p[i]);

            return f;
        }
        public double fa(double T) //
        {
            double f  = a0 / Math.Sqrt(Math.Abs(T / T0));
            return f;
        }
        public double fx(double V, double teta, double psi) //Дальность в плоскости стрельбы
        {
            double f = V * Math.Cos(teta) * Math.Cos(psi);
            return f;
        }
        public double fy(double V, double teta) //Высота полёта снаряда
        {
            double f = V * Math.Sin(teta);
            return f;
        }
        public double fz(double V, double teta, double psi)  //Боковое отклонение
        {
            double f = -V * Math.Cos(teta) * Math.Sin(psi);
            return f;
        }
        public double fV(double teta, double g, double q, double S, double m)  //Скорость центра масс снаряда
        {
            double f = -g * Math.Sin(teta) - (q * S) / m;
            return f;
        }
        public double fteta(double V, double teta, double g, double q, double S, double m)  //Угол наклона траектории
        {
            double f = -g * Math.Cos(teta) / V - ( q * S) / (m * V);
            return f;
        }
        public double fpsi(double V, double teta, double g, double q, double S, double m)  //Угол направления
        {
            double f = -(0 * q * S) / (m * V * Math.Cos(teta));
            return f;
        }
        public double Func(int n, double[] x, int n1)
        {            
            double f = 0;
            double Q = q(fa(fT(fy(x[4], x[6]))), mah(x[4], fa(fT(fy(x[4], x[6])))));
            switch (n)
                {
                    case 1:
                        f = fx(x[4],x[6],x[5]);
                        break;
                    case 2: 
                        f = fy(x[4],x[6]);
                        break;
                    case 3:
                        f = fz(x[4], x[6], x[5]);
                    break;
                    case 4:
                        f = fV(x[6],g,Q,S,m);
                    break;
                    case 5:
                        f = fpsi(x[4],x[6],g, Q, S,m);
                    break;
                    case 6:
                        f = fteta(x[4],x[6],g,Q, S,m);
                        break;
                    case 7:
                        f = fomega(mx, Q, S,l,Ix);
                    break;              
                        }
           return f;
        }

        double xMax,tMax,zMax;
        public double[,] RungeKutte(double t)//Метод Рунге Кутте
        {// y <= 0 и teta < 0 условие выхода из цикла
            
            V = Math.Sqrt((Math.Pow((Vx + V0 * Math.Cos(teta0)), 2)) + (Math.Pow((Vy + V0 * Math.Sin(teta0)), 2)) + Vz * Vz);
            teta = Math.Atan(((Vy + V0 * Math.Sin(teta0)) / (Vx + V0 * Math.Cos(teta0))));
            psi = -psi0 + Math.Atan((Vz / (Vx + V0 * Math.Cos(teta0))));

            double dt = 0.01;
            int n = 1;
            double[,] X = new double[8,11000];
            double[] X1 = new double[8];
            double[,] K = new double[4,11000];
            //init
            X[0, 0] = t;//t
            X[1, 0] = 0;//x
            X[2, 0] = h;//y
            X[3, 0] = 0;//z
            X[4, 0] = V;//V
            X[5, 0] = psi;//psi
            X[6, 0] = teta;//teta
            X[7, 0] = 2000;//omega

            //calc

            if (chartOn == true)
            {
                chart1.Series.Add(Convert.ToString(number));
                chart1.Series[Convert.ToString(number)].ChartType = SeriesChartType.Line;
                chart2.Series.Add(Convert.ToString(number));
                chart2.Series[Convert.ToString(number)].ChartType = SeriesChartType.Line;
                chart3.Series.Add(Convert.ToString(number));
                chart3.Series[Convert.ToString(number)].ChartType = SeriesChartType.Line;
            }

            for (int j = 0; j < n; j++)
            {
                if (X[2, j] < 0)
                {
                    break;
                }
                else
                {
                    n++;
                }
                if (chartOn == true)
                {
                    chart1.Series[number].Points.AddXY(X[1, j], X[2, j]);
                    chart2.Series[number].Points.AddXY(X[0, j], X[4, j]);
                    chart3.Series[number].Points.AddXY(X[1, j], X[3, j]);
                }

                for (int i = 1; i < 8; i++)
                {
                    X1[i] = X[i, j]; X1[0] = X[0, j];
                }
                dataGridView1.Rows.Add();
                for (int i = 1; i < 8; i++)
                {
                    K[0, i] = dt * Func(i, X1, j);
                    X1[i] = X[i, j] + K[0, i]; X1[0] = X[0, j] + dt / 2;
                    K[1, i] = dt * Func(i, X1, j);
                    X1[i] = X[i, j] + K[1, i] / 2;
                    K[2, i] = dt * Func(i, X1, j);
                    X1[i] = X[i, j] + K[2, i] / 2; X1[0] = X[0, j] + dt;
                    K[3, i] = dt * Func(i, X1, j);
                    X[i, j + 1] = X[i, j] + (K[0, i] + 2 * K[1, i] + 2 * K[2, i] + K[3, i]) / 6;
                    X[0, j + 1] = X[0, j] + dt;

                    if((i-1)<=4||(i-1)==6)
                    {
                        dataGridView1.Rows[j].Cells[i - 1].Value = Math.Round(X[i, j],4);
                    }
                    dataGridView1.Rows[j].Cells[4].Value = Math.Round(X[5, j]* 180 / Math.PI,4); 
                    dataGridView1.Rows[j].Cells[5].Value = Math.Round(X[6, j]* 180 / Math.PI,4);
                    dataGridView1.Rows[j].Cells[7].Value = Math.Round(X[0, j],3);
                }
                xMax = X[1, j];
                tMax = X[0, j];
                zMax = X[3, j];
            }
            if (chartOn == true)
            {
                chart1.Series[number].Name = Convert.ToString(number + 1) + ".Vx = " + textBox1.Text + ", Vy = " + textBox3.Text + ", θ = " + textBox2.Text + "\nx = " + Math.Round(xMax, 2).ToString() + " z = " + Math.Round(zMax, 2).ToString()+ ", t = " + Math.Round(tMax, 2).ToString();
                chart2.Series[number].Name = Convert.ToString(number + 1) + ".Vx = " + textBox1.Text + ", Vy = " + textBox3.Text + ", θ = " + textBox2.Text + "\nx = " + Math.Round(xMax, 3).ToString() + ", t = " + Math.Round(tMax, 2).ToString();
                chart3.Series[number].Name = Convert.ToString(number + 1) + ".Vx = " + textBox1.Text + ", Vy = " + textBox3.Text + ", θ = " + textBox2.Text + "\nx = " + Math.Round(xMax, 3).ToString() + ", t = " + Math.Round(tMax, 2).ToString();
            }
            return X;
        }
    }
    
}      
