using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace Vnesh_ballistic
{
    public class snaryad
    {
        public string name;
        public int tipf;//0-нарезной, 1-оперенный
        public double m;
        public double D;
        public double l, l_cm, l_gol;
        public double Sm { get { return Math.PI * D * D / 4; }  } 
        public double I_x, I_z;
        public double Vs0, tet0;//rad
        public double narez;//
        //рассеивание
        public double dm, dix;
        //аэродинамические коэффициенты
        public int cx_law;
        //эмпирические
        public List<double> a0_43 = new List<double>();
        public List<double> a1_43 = new List<double>();
        public List<double> a2_43 = new List<double>();
        public List<double> a3_43 = new List<double>();
        public List<double> M1_43 = new List<double>();
        public List<double> M2_43 = new List<double>();
        public List<double> b0_58 = new List<double>();
        public List<double> b1_58 = new List<double>();
        public List<double> b2_58 = new List<double>();
        public List<double> b3_58 = new List<double>();
        public List<double> M1_58 = new List<double>();
        public List<double> M2_58 = new List<double>();
        public List<double> a0_K_NM = new List<double>();
        public List<double> a1_K_NM = new List<double>();
        public List<double> a2_K_NM = new List<double>();
        public List<double> M1_K_NM = new List<double>();
        public List<double> M2_K_NM = new List<double>();
        public double m_x_omega;
        //расчетные
        public double[] Ma;
        public double[,] Cxa;
        public double ix, iz;

        public void Init(string p_n, int p_tf, double p_m, double p_D, double p_l, double p_cm, double p_gol,
            double p_Ix, double p_Iz)
        {
            name = p_n;
            tipf = p_tf;
        }
        public double Calc_I_x()
        {
            double chislo1, chislo2;
            chislo1 = l_gol / (3 * l - 2 * l_gol);
            chislo2 = chislo1 * m * D * D * 3 / 40 + m * (1 - chislo1) * D * D / 8;
            return chislo2;
        }
        public double Calc_I_z()
        {
            double chislo1, chislo2;
            chislo1 = l_gol / (3 * l - 2 * l_gol);
            chislo2 = chislo1 * m * (D * D / 16 + l_gol * l_gol) * 3 / 5 +
                m * (1 - chislo1) * (3 * D * D / 4 + (l - l_gol) * (l - l_gol)) / 12;
            return chislo2;
        }
        public double Calc_wx0(double V0)
        {
            double omega_x0 = 0;
            if (narez > 0) omega_x0 = 2 * Math.PI * V0 / (narez * D);
            return omega_x0; 
        }
        public int LoadNormFromtxt(string path, ref string mes)
        {
            mes = "";
            string f = path + "\\Cx\\Cx_otM_ET_1943.txt";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS);
            try
            {
                string st = SR.ReadLine(); st = SR.ReadLine(); st = SR.ReadLine();
                M1_43.Clear(); M2_43.Clear(); a0_43.Clear(); a1_43.Clear(); a2_43.Clear(); a3_43.Clear();
                while (st != null)
                {
                    string[] s = st.Split();
                    M1_43.Add(Convert.ToDouble(s[0]));
                    M2_43.Add(Convert.ToDouble(s[1]));
                    a0_43.Add(Convert.ToDouble(s[2]));
                    a1_43.Add(Convert.ToDouble(s[3]));
                    a2_43.Add(Convert.ToDouble(s[4]));
                    a3_43.Add(Convert.ToDouble(s[5]));
                    st = SR.ReadLine();
                }
                SR.Close(); FS.Close();
                f = path + "\\Cx\\Cx_otM_ET_1958.txt";
                FS = new FileStream(f, FileMode.Open, FileAccess.Read);
                SR = new StreamReader(FS);
                st = SR.ReadLine(); st = SR.ReadLine(); st = SR.ReadLine();
                M1_58.Clear(); M2_58.Clear(); b0_58.Clear(); b1_58.Clear(); b2_58.Clear(); b3_58.Clear();
                while (st != null)
                {
                    string[] s = st.Split();
                    M1_58.Add(Convert.ToDouble(s[0]));
                    M2_58.Add(Convert.ToDouble(s[1]));
                    b0_58.Add(Convert.ToDouble(s[2]));
                    b1_58.Add(Convert.ToDouble(s[3]));
                    b2_58.Add(Convert.ToDouble(s[4]));
                    b3_58.Add(Convert.ToDouble(s[5]));
                    st = SR.ReadLine();
                }
                SR.Close(); FS.Close();
                f = path + "\\cx\\K_NM.txt";
                FS = new FileStream(f, FileMode.Open, FileAccess.Read);
                SR = new StreamReader(FS);
                st = SR.ReadLine(); st = SR.ReadLine(); st = SR.ReadLine();
                M1_K_NM.Clear(); M2_K_NM.Clear(); a0_K_NM.Clear(); a1_K_NM.Clear(); a2_K_NM.Clear();
                while (st != null)
                {
                    string[] s = st.Split();
                    M1_K_NM.Add(Convert.ToDouble(s[0]));
                    M2_K_NM.Add(Convert.ToDouble(s[1]));
                    a0_K_NM.Add(Convert.ToDouble(s[2]));
                    a1_K_NM.Add(Convert.ToDouble(s[3]));
                    a2_K_NM.Add(Convert.ToDouble(s[4]));
                    st = SR.ReadLine();
                }
            }
            catch (Exception e)
            {
                mes = e.Message;
                return 1;
                //System.Windows.Forms.MessageBox.Show("Выбран неверный файл!");
                //System.Windows.Forms.Application.Exit();
            }
            SR.Close(); FS.Close();
            return 0;
        }
        public int LoadRealFromtxt(string f, ref string mes)
        {
            mes = "";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS, Encoding.Default);//Encoding.Default!!!
            try
            {
                string st, law;
                name = SR.ReadLine();
                D = Convert.ToDouble(SR.ReadLine().Split(' ')[1]) / 1000.0;
                m = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                l = Convert.ToDouble(SR.ReadLine().Split(' ')[1]) / 1000.0;
                l_cm = Convert.ToDouble(SR.ReadLine().Split(' ')[1]) / 1000.0;//от носа!!!
                l_gol = Convert.ToDouble(SR.ReadLine().Split(' ')[1]) / 1000.0;
                Vs0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]); //нач.скорость
                tet0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]); //нач.угол
                I_x = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                I_z = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                narez = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                st = SR.ReadLine();
                law = st.Split(' ')[0];
                if (law == "ix43") cx_law = 1;
                if (law == "ix58") cx_law = 2;
                ix = Convert.ToDouble(st.Split(' ')[1]);//коэф. формы
                iz = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);//коэф. бок отклонения
                m_x_omega = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);//коэф. аксиал.момента
                st = SR.ReadLine();//Cxa
                Ma = new double[] { Convert.ToDouble(st.Split(' ')[1]), Convert.ToDouble(st.Split(' ')[2]), Convert.ToDouble(st.Split(' ')[3]), Convert.ToDouble(st.Split(' ')[4]), Convert.ToDouble(st.Split(' ')[5]) };
                Cxa = new double[4, 5];
                for (int i = 0; i <= 3; i++)
                {
                    st = SR.ReadLine();//Data
                    for (int j = 0; j <= 4; j++)
                        Cxa[i, j] = Convert.ToDouble(st.Split(' ')[j]);
                }
            }
            catch (Exception e)
            {
                mes = e.Message;
                return 1;
                //System.Windows.Forms.MessageBox.Show("Выбран неверный файл!");
                //System.Windows.Forms.Application.Exit();
            }
            SR.Close(); FS.Close();
            return 0;
        }
        public void SaveTotxt(string f)
        {
            //сохранить в файл
        }
        public double Cx_k_a(double M, double alfa2)
        {
            int i = 0;
            for (i = 1; i <= Ma.Count()-2; i++) { if (Ma[i]>M) break; }
            return Cxa[i-1, 0] + Cxa[i-1, 1] * M + Cxa[i-1, 2] * M * M + Cxa[i-1, 3] * M * M * M + Cxa[i-1, 4] * alfa2;
        }
        public double Cx_otM_et(double M, int law)
        {
            int i; int ii = 0; double result = 0;
            if (law == 1)//1943
            {
                for (i = 0; i <= M1_43.Count - 1; i++)
                    if ((M >= M1_43[i]) & (M < M2_43[i])) ii = i;
                if (M > M2_43[M2_43.Count - 1]) ii = M2_43.Count - 1;
                result = a0_43[ii] + a1_43[ii] * M + a2_43[ii] * M * M + a3_43[ii] * M * M * M;
            }
            else//1958
            {
                for (i = 0; i <= M1_58.Count - 1; i++)
                    if ((M >= M1_58[i]) & (M < M2_58[i])) ii = i;
                if (M > M2_58[M2_58.Count - 1]) ii = M2_58.Count - 1;
                result = b0_58[ii] + b1_58[ii] * M + b2_58[ii] * M * M + b3_58[ii] * M * M * M;
            }
            return result;
        }
        public double K_NM(double M)
        {
            int i; int ii = 0; double result = 0;
            for (i = 0; i <= M1_K_NM.Count - 1; i++)
                if ((M >= M1_K_NM[i]) & (M < M2_K_NM[i])) ii = i;
            if (M > M1_K_NM[M1_K_NM.Count - 1]) ii = M1_K_NM.Count - 1;
            result = a0_K_NM[ii] + a1_K_NM[ii] * M + a2_K_NM[ii] * M * M;
            return result;
        }

    }//class snaryad

    public class ball_ucl
    {
        public double t0 { get { return X0[0]; } set { X0[0] = value; } }
        public double x0 { get { return X0[2]; } set { X0[2] = value; if (value == 0) X0[1] = 0; } } //S!!! 
        public double y0 { get { return X0[3]; } set { X0[3] = value; } }
        public double z0 { get { return X0[4]; } set { X0[4] = value; } }
        public double tk, xk, yk, zk, Vkk;
        public double V0 { get { return X0[5]; } set { X0[5] = value; } }
        public double teta0 { get { return X0[6]; } set { X0[6] = value; } }
        public double teta0_grad { get { return teta0 * 180 / Math.PI; } set { teta0 = value * Math.PI / 180; } }
        public double psy0 { get { return X0[7]; } set { X0[7] = value; } }
        public double psy0_grad { get { return psy0 * 180 / Math.PI; } set { psy0 = value * Math.PI / 180; } }
        public double wx0 { get { return X0[8]; } set { X0[8] = value; } }
        public double pi0 { get { return X0[9]; } set { X0[9] = value; } }
        public double a_direct;//rad
        public double a_dir_grad { get { return a_direct * 180 / Math.PI; } set { a_direct = value * Math.PI / 180; } }
        public double[] X0 = new double[14 + 1];//нач_условия

        public void Init(double p_V, double p_t, double p_p, double p_wx)
        {
            V0 = p_V;
            teta0 = p_t;

        }
        public int LoadFromtxt(string f, ref string mes)
        {
            mes = "";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS);
            string st = SR.ReadLine();//snaryad
            try
            {
                t0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                x0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                y0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                z0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                tk = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                xk = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                yk = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                zk = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                V0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                wx0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                teta0_grad = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                psy0_grad = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                a_dir_grad = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                //Params
            }
            catch (Exception e)
            {
                mes = e.Message;
                return 1;
                //System.Windows.Forms.MessageBox.Show("Выбран неверный файл!");
                //System.Windows.Forms.Application.Exit();
            }
            SR.Close(); FS.Close();
            return 0;
        }
        public void SaveTotxt(string f)
        {
            //сохранить в файл
        }

    }//class ball_ucl

    public class geo_ucl
    {
        public double g_N0;
        public double Gg;
        public double R_geo;
        public double w_geo;
        public double B_shirota;//широта
        public int U1_FormaZemli;
        public double delta2_g;

        public void Init(double p_g, double p_Rz, double p_wz, double p_B)
        {
            g_N0 = p_g; R_geo = p_Rz; w_geo = p_wz; B_shirota = p_B;
            
        }
        public int LoadFromtxt(string path, ref string mes)
        {
            mes = "";
            string f = path + "\\meteo\\geo_const.txt";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS);
            try
            {
                SR.ReadLine();//заголовок
                g_N0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                Gg = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                R_geo = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                w_geo = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                delta2_g = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                return 1;
            }
            SR.Close(); FS.Close();
            return 0;
        }
        public void SaveTotxt(string f)
        {
            //сохранить в файл
        }
        public double g(double y)//ускорение свободного падения, функция от у
        {
            return g_N0 - Gg * y + U1_FormaZemli * (0.0517 * Math.Sin(B_shirota) * Math.Sin(B_shirota) + delta2_g);
        }

    }//class geo_ucl

    public class meteo_ucl
    {
        public double R;//удел.газ.постоянная
        public int U2_nomin_real;//0-nominal, 1-real
        //номинальные
        public double aN0, tauN0, roN0, PN0;
        //реальные
        public double T0, P0, hum;
        public double ro0 { get { return P0/(T0*R); } }
        public double weter, a_weter;
        public double a_weter_grad { get { return a_weter * 180 / Math.PI; } set { a_weter = value * Math.PI / 180; } }
        public int wind_type = 2;//1-постоянный, 2-степенной, 3-реальный
        public double w_veter10, w_p;
        //рассеивание
        public double dT0, dP0, dhum, dw, daw, dTz;
        public double kw, kaw, wmax;//разброс
        //распределения
        public List<double> y_real_ar = new List<double>();
        public List<double> y_tnorm_ar = new List<double>();
        public List<double> y_pnorm_ar = new List<double>();
        public List<double> T_ar = new List<double>();
        public List<double> tau_real_ar = new List<double>();
        public List<double> tau_norm_ar = new List<double>();
        public List<double> tau_k1 = new List<double>();
        public List<double> tau_k2 = new List<double>();
        public List<double> p_real_ar = new List<double>();
        public List<double> p_norm_ar = new List<double>();
        public List<double> w_real_ar = new List<double>();
        public List<double> aw_real_ar = new List<double>();

        public void Init(double p_T, double p_P, double p_vl, double p_w, double p_aw)
        {
            T0 = p_T; P0 = p_P; hum = p_vl; weter = p_w; a_weter = p_aw;

        }
        public int LoadFromtxt(string path, ref string mes)
        {
            mes = "";
            string f = path + "\\meteo\\meteo_const.txt";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS);
            try
            {
                SR.ReadLine();//заголовок
                R = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                roN0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                aN0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                tauN0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                PN0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                return 1;
            }
            SR.Close(); FS.Close();
            P0 = PN0; T0 = tauN0; //real=nominal!!!
            return 0;
        }
        public int LoadNormFromtxt(string path, ref string mes)
        {
            mes = "";
            string f = path + "\\meteo\\P_ot_y_nominal.txt";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS);
            try
            {
                string st = SR.ReadLine(); st = SR.ReadLine();
                y_pnorm_ar.Clear(); p_norm_ar.Clear();
                st = SR.ReadLine();
                while (st != null)
                {
                    string[] s = st.Split();
                    y_pnorm_ar.Add(Convert.ToDouble(s[0]));
                    p_norm_ar.Add(Convert.ToDouble(s[1]));
                    st = SR.ReadLine();
                }
                SR.Close(); FS.Close();
                f = path + "\\meteo\\tau_ot_y_nominal.txt";
                FS = new FileStream(f, FileMode.Open, FileAccess.Read);
                SR = new StreamReader(FS);
                st = SR.ReadLine(); st = SR.ReadLine();
                y_tnorm_ar.Clear(); tau_norm_ar.Clear();
                tau_k1.Clear(); tau_k2.Clear();
                st = SR.ReadLine();
                while (st != null)
                {
                    string[] s = st.Split();
                    y_tnorm_ar.Add(Convert.ToDouble(s[0]));
                    tau_norm_ar.Add(Convert.ToDouble(s[1]));
                    tau_k1.Add(Convert.ToDouble(s[2]));
                    tau_k2.Add(Convert.ToDouble(s[3]));
                    st = SR.ReadLine();
                }
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                return 1;
            }
            SR.Close(); FS.Close();
            return 0;
        }
        public int LoadRealFromtxt(string f, ref string mes)
        {
            mes = "";
            //string f = path + "\\meteo\\real_atmosphere.txt";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS);
            try
            {
                string st = SR.ReadLine(); st = SR.ReadLine();
                y_real_ar.Clear(); p_real_ar.Clear(); tau_real_ar.Clear();
                aw_real_ar.Clear(); w_real_ar.Clear();
                while (st != null)
                {
                    string[] s = st.Split();
                    y_real_ar.Add(Convert.ToDouble(s[0]));
                    p_real_ar.Add(Convert.ToDouble(s[1]));
                    tau_real_ar.Add(Convert.ToDouble(s[2]) + 273.15);//перевод в кельвины
                    double alpha_w = Convert.ToDouble(s[3]);  // alpha_w = 250.0;//!!!
                    aw_real_ar.Add(alpha_w * Math.PI / 180);//перевод в радианы                                
                    w_real_ar.Add(Convert.ToDouble(s[4]));
                    st = SR.ReadLine();
                }
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                return 1;
            }
            SR.Close(); FS.Close();
            return 0;
        }
        public void SaveTotxt(string f)
        {
            //сохранить в файл
        }
        public double P_y(double y) //зависимость давления от высоты, функция y  
        {
            double r = 0;
            if (U2_nomin_real == 0)   //nominal
            {
                int j = 0; int p_Count = p_norm_ar.Count();
                for (int i = 0; i <= p_Count - 2; i++) if ((y >= y_pnorm_ar[i]) & (y < y_pnorm_ar[i + 1])) { j = i; break; }
                if (y > y_pnorm_ar[p_Count - 1]) { j = p_Count - 1; }
                // (y-y1)/(y2-y1)=(x-x1)/(x2-x1) => y=y1+(x-x1)*(y2-y1)/(x2-x1)
                if (j < p_Count - 1)
                    r = p_norm_ar[j] + (y - y_pnorm_ar[j]) * (p_norm_ar[j + 1] - p_norm_ar[j]) / (y_pnorm_ar[j + 1] - y_pnorm_ar[j]);
                else r = p_norm_ar[p_Count - 1];
            }
            if (U2_nomin_real == 1)  //real data
            {
                int j = 0; int p_Count = p_real_ar.Count();
                for (int i = 0; i <= p_Count - 2; i++)
                    if ((y >= y_real_ar[i]) & (y < y_real_ar[i + 1])) { j = i; break; }
                if (y > y_real_ar[p_Count - 1]) { j = p_Count - 1; }
                // (y-y1)/(y2-y1)=(x-x1)/(x2-x1) => y=y1+(x-x1)*(y2-y1)/(x2-x1)
                if (j < p_Count - 1)
                    r = p_real_ar[j] + (y - y_real_ar[j]) * (p_real_ar[j + 1] - p_real_ar[j]) / (y_real_ar[j + 1] - y_real_ar[j]);
                else r = p_real_ar[p_Count - 1];
            }
            return r;
        }
        public double tau_y(double y)//зависимость температуры от высоты, функция y            
        {
            double r = 0;
            if (U2_nomin_real == 0)   //nominal
            {
                int j = 0;
                for (int i = 0; i <= 4; i++)
                    if ((y >= y_tnorm_ar[i]) & (y < y_tnorm_ar[i + 1])) { j = i; break; }
                if (y > y_tnorm_ar[5]) { j = 5; }
                r = tau_k1[j] + tau_k2[j] * (y - y_tnorm_ar[j]); //G_tau_j
                r = tau_norm_ar[j] + r * (y - y_tnorm_ar[j]);
                r = r + (T0 - tauN0); //delta_tau(y) !?!
            }
            if (U2_nomin_real == 1)  //real data
            {
                int j = 0;
                int t_Count = tau_real_ar.Count();
                for (int i = 0; i <= t_Count - 2; i++)
                    if ((y >= y_real_ar[i]) & (y < y_real_ar[i + 1])) { j = i; break; }
                if (y > y_real_ar[t_Count - 1]) { j = t_Count - 1; }
                // (y-y1)/(y2-y1)=(x-x1)/(x2-x1) => y=y1+(x-x1)*(y2-y1)/(x2-x1)
                if (j < t_Count - 1)
                    r = tau_real_ar[j] +
                        (y - y_real_ar[j]) * (tau_real_ar[j + 1] - tau_real_ar[j]) / (y_real_ar[j + 1] - y_real_ar[j]);
                else r = tau_real_ar[t_Count - 1];
            }
            return r;
        }
        public double alpha_w_veter_real(double y)//зависимость реального дирекционного угла ветра от высоты, функция y
        {
            int j = 0; double r = 0; int aw_Count = aw_real_ar.Count();
            for (int i = 0; i <= aw_Count - 2; i++)
                if ((y >= y_real_ar[i]) & (y < y_real_ar[i + 1])) { j = i; break; }
            if (y > y_real_ar[aw_Count - 1]) { j = aw_Count - 1; }
            // (y-y1)/(y2-y1)=(x-x1)/(x2-x1) => y=y1+(x-x1)*(y2-y1)/(x2-x1)
            if (j < aw_Count - 1)
            {
                r = aw_real_ar[j] +
                    (y - y_real_ar[j]) * (aw_real_ar[j + 1] - aw_real_ar[j]) / (y_real_ar[j + 1] - y_real_ar[j]);
            }
            else { r = aw_real_ar[aw_Count - 1]; }
            return r;
        }
        public double w_veter_real(double y)//зависимость реальной скорости ветра от высоты, функция y   
        {
            int j = 0; double r = 0; int w_Count = w_real_ar.Count();
            for (int i = 0; i <= w_Count - 2; i++)
                if ((y >= y_real_ar[i]) & (y < y_real_ar[i + 1])) { j = i; break; }
            if (y > y_real_ar[w_Count - 1]) { j = w_Count - 1; }
            // (y-y1)/(y2-y1)=(x-x1)/(x2-x1) => y=y1+(x-x1)*(y2-y1)/(x2-x1)
            if (j < w_Count - 1)
            {
                r = w_real_ar[j] +
                    (y - y_real_ar[j]) * (w_real_ar[j + 1] - w_real_ar[j]) / (y_real_ar[j + 1] - y_real_ar[j]);
            }
            else { r = w_real_ar[w_Count - 1]; }
            return r;
        }
        public double w_veter_step(double y)//степенной закон
        {
            double yy = y;
            if (yy < 0) yy = 0;
            return w_veter10 * Math.Pow(yy / 10, w_p);
        }
        public double deltaP0()
        {
            double res = 0;
            if (U2_nomin_real == 0) res = P0 - PN0;
            else res = P_y(0) - PN0;
            return res;
        }
        public double pi_0(double y0)
        {
            double chislo = 1 - (-6.328e-3 * y0) / tauN0;  //
            chislo = Math.Pow(chislo, 5.4) * (1 + deltaP0() / PN0);
            return chislo;
        }
        public double w_vertical_veter(double y)//функция от y, для ветра, скорость вертикальных токов воздуха 
        {
            return 0;
        }
        public void veter_param(double y, ref double w_veter, ref double alpha_w, ref double w_vert_veter)
        {
            //veter_type == 1
            w_veter = w_veter10;
            alpha_w = a_weter;
            w_vert_veter = 0;
            if (wind_type == 2)
            {
                w_veter = w_veter_step(y);
            }
            if (wind_type == 3)
            {
                alpha_w = alpha_w_veter_real(y);
                w_veter = w_veter_real(y);
                w_vert_veter = w_vertical_veter(y);
            }
            //разброс
            w_veter = w_veter * (1 + kw);
            alpha_w = alpha_w + kaw;

        }
        public double a(double y)
        {//скорость звука в воздухе, функция y
            return aN0 * Math.Sqrt(tau_y(y) / tauN0);
        }

    }//meteo_ucl

    public class VneshBall  //vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
    {
        public snaryad sn;
        public ball_ucl b_ucl;
        public geo_ucl g_ucl;
        public meteo_ucl m_ucl;
        //параметры расчета
        public int Cx_law;
        public bool MxConst;
        public bool Gobar;
        public int return_code;
        public string return_message
        { get
            { string st = "";
                switch (return_code)
                {   case 0: st = "Завершено успешно"; break;
                    case 1: st = "Ошибка задания исходных данных"; break;
                    case 2: st = "Не выполнено условие завершения расчета"; break;
                    case 3: st = "Ошибка вычислений"; break;
                    default: st = "Неизвестная ошибка"; break;   }
                return st; }
        }
        public bool stop;
        public double t0, dt;
        public int diter;
        public int chisloUzlovSetky;
        public int ChisloUraneniy;
        //переменные и параметры
        public List<List<double>> RRR = new List<List<double>>();
        public double t, S, x, y, z;
        public double Vk, teta, psy, wx, pi_y;
        public double M;
        public double[] X0_;//нач_условия
        public bool U_uchitivat_vrashenie_zemli;
        public bool U_uchitivat_veter;
        public double eps_wind1, eps_wind2;
        public double w_x_veter, w_y_veter, w_z_veter, delta_V_w_veter;
        public double delta_Cxk_ot_eps_veter, delta_Cyk_ot_eps_veter, delta_Czk_ot_eps_veter;
        public double f3, f6, f7, Cxk, Cyk, Czk, q_M, wxs;
        //конечные
        public double Itogo_T, Itogo_D, Itogo_y_max, Itogo_X, Itogo_Y, Itogo_Z, Itogo_teta;
        //ввод-вывод
        public string[] NameVar, NameVarForBlocknot;
        //

        public VneshBall()
        {
            return_code = 0;
            ChisloUraneniy = 9;
            sn = new snaryad();
            b_ucl = new ball_ucl();
            g_ucl = new geo_ucl();
            m_ucl = new meteo_ucl();
            X0_ = b_ucl.X0;
            U_uchitivat_vrashenie_zemli = true;
            U_uchitivat_veter = true;
            m_ucl.U2_nomin_real = 0;
            dt = 0.05;
            diter = 0;
        }
        public void InitFromNU()
        {
            X0_[0] = b_ucl.t0; X0_[1] = 0; //S
            X0_[2] = b_ucl.x0; X0_[3] = b_ucl.y0; X0_[4] = b_ucl.z0; 
            X0_[5] = b_ucl.V0; X0_[6] = b_ucl.teta0; X0_[7] = b_ucl.psy0;
            /*b_ucl.wx0 = sn.Calc_wx0(b_ucl.V0);*/ X0_[8] = b_ucl.wx0;
            b_ucl.pi0 = m_ucl.pi_0(b_ucl.y0); X0_[9] = b_ucl.pi0;
        }
        public void InitToNU()
        {
            b_ucl.t0 = X0_[0]; // X0_[1] = 0; //S
            b_ucl.x0 = X0_[2]; b_ucl.y0 = X0_[3]; b_ucl.z0 = X0_[4];
            b_ucl.V0 = X0_[5]; b_ucl.teta0 = X0_[6]; b_ucl.psy0 = X0_[7];
            b_ucl.wx0 = X0_[8]; b_ucl.pi0 = X0_[9];
        }
        public double ToF(int i, double xi)
        {
            if ((i == 6) || (i == 7) || (i == 13) || (i == 14)) return xi * 180.0 / Math.PI;
            else return xi;
        }
        public double FromF(int i, double xi)
        {
            if ((i == 6) || (i == 7) || (i == 13) || (i == 14)) return xi * Math.PI / 180.0;
            else return xi;
        }
        public void raschet_vlyania_vetra()
        {
            if (U_uchitivat_veter)
            {
                double w_veter=0, alpha_w=0, w_vert_veter=0;
                m_ucl.veter_param(y, ref w_veter, ref alpha_w, ref w_vert_veter);
                //vertolet
                w_x_veter = -w_veter * Math.Cos(alpha_w - (b_ucl.a_direct - psy)) * Math.Cos(teta) + w_vert_veter * Math.Sin(teta);
                w_y_veter = w_veter * Math.Cos(alpha_w - (b_ucl.a_direct - psy)) * Math.Sin(teta) + w_vert_veter * Math.Cos(teta);
                w_z_veter = -w_veter * Math.Sin(alpha_w - (b_ucl.a_direct - psy));
                double V_vozdushnaya_skorost_snaryada = Math.Sqrt(Vk * Vk - 2 * w_x_veter * Vk + w_veter * w_veter + w_vert_veter * w_vert_veter);
                delta_V_w_veter = V_vozdushnaya_skorost_snaryada - Vk;
                double sin_eps_w2_veter = - w_y_veter / V_vozdushnaya_skorost_snaryada;
                //eps_w2_veter=Math.Asin(sin_eps_w2_veter);//от 0 до 2пи таких углов 2
                double eps_w2_veter = sin_eps_w2_veter; //приближенно 
                double cos_eps_w2_veter = Math.Cos(eps_w2_veter);
                double sin_eps_w1_veter = w_z_veter / (V_vozdushnaya_skorost_snaryada * cos_eps_w2_veter);
                //eps_w1_veter=Math.Asin(sin_eps_w1_veter); //???
                double eps_w1_veter = sin_eps_w1_veter; //приближенно
                double cos_eps_w1_veter = Math.Cos(eps_w1_veter);
                M = Mah();
                double chislo = sn.Cx_k_a(M,0); //sn.ix * sn.Cx_otM_et(M);//Cxk_ot_M
                delta_Cxk_ot_eps_veter = chislo * (cos_eps_w1_veter * cos_eps_w2_veter - 1);
                delta_Cyk_ot_eps_veter = chislo * sin_eps_w2_veter;
                delta_Czk_ot_eps_veter = chislo * sin_eps_w1_veter * cos_eps_w2_veter;
            };
        }
        public double delta_pi_ot_y(double y)//функция от y 
        {
            double res = 0;
            if (m_ucl.U2_nomin_real == 1) res = m_ucl.P_y(y) / m_ucl.P_y(0) - pi_y;
            else res = 0;
            return res;
        }
        public double delta_tau_ot_y(double y)//функция от y, для ветра
        {
            return 0;// m_ucl.tau_real(y) - m_ucl.tau_nominal(y);
        }
        public double a(double y)
        {
            return m_ucl.a(y);
        }
        public double Mah()//число Маха фунция y,a,Vk
        {
            return (Vk + delta_V_w_veter) / a(y);
        }
        public double q(double M)
        {//скоростной напоp воздуха, функция pi_ot_y            
            return m_ucl.roN0 * m_ucl.aN0 * m_ucl.aN0 * (pi_y + delta_pi_ot_y(y)) * M * M / 2.0;
        }
        public double f_zk()//коэффициент деривации
        {
                double h = 1;
                //if (Gobar) 
                h = (sn.l_cm - sn.l_gol) + 0.57 * sn.l_gol - 0.16 * sn.D;
                return sn.iz * sn.l * sn.l * sn.K_NM(M) / (sn.D * h);
        }
        public double Vpr(double y) //функция vk,teta,y
        {
            double y0 = b_ucl.y0;
            return Vk * Math.Cos(teta) * (g_ucl.R_geo + y0) / (g_ucl.R_geo + y) + (1 - g_ucl.U1_FormaZemli) * Vk * Math.Cos(teta) * (y - y0) / (g_ucl.R_geo + y);
        }
        public double delta_tetaZemli()
        {
            //if (U_uchitivat_vrashenie_zemli)
                return 2 * g_ucl.U1_FormaZemli * g_ucl.w_geo * Math.Cos(g_ucl.B_shirota) * Math.Sin(b_ucl.a_direct - psy) -
                 (1 - g_ucl.U1_FormaZemli) * Vk * Math.Cos(teta) / (g_ucl.R_geo + y);
            //else return 0;
        }
        public double delta_psyZemli()
        {
            if (U_uchitivat_vrashenie_zemli)
                return -2 * g_ucl.U1_FormaZemli * g_ucl.w_geo * (Math.Sin(g_ucl.B_shirota) - Math.Cos(g_ucl.B_shirota) * Math.Cos(b_ucl.a_direct - psy) * Math.Tan(teta));
            else return 0;
        }
        public double wx_s()//безразмерная
        {  
            return wx * sn.l / (M * m_ucl.aN0);
        }
        public double mx(double M, double wxs)
        {
            //if (MxConst) 
            return sn.m_x_omega * wxs;
        }
        public double C_xk(double M)
        {
            double result = 0;
            if (Cx_law == 3)
            {
                double alfa2 = 0;
                result = sn.Cx_k_a(M, alfa2);
                result += delta_Cxk_ot_eps_veter;
                return result;
            }
            else result = sn.ix * sn.Cx_otM_et(M, Cx_law) + delta_Cxk_ot_eps_veter;
            return result;
        }
        public double C_yk(double M, double wxs)
        {
            double result = delta_Cyk_ot_eps_veter;
            return result;
        }
        public double C_zk(double M, double wxs)
        {
            double result = delta_Czk_ot_eps_veter;
            return result;
        }

        public double f(int i, double[] X, ref int exc)
        {
            exc = 0;
            double result = 0;
            t = X[0]; S = X[1]; x = X[2]; y = X[3]; z = X[4]; Vk = X[5]; teta = X[6]; psy = X[7]; wx = X[8]; pi_y = X[9];
            try
            {
                switch (i)
                {
                    case 1: result = Vk; break; //S
                    case 2: result = Vpr(y) * Math.Cos(psy); break; //x
                    case 3: f3 = Vk * Math.Sin(teta); result = f3; break; //y
                    case 4: result = -Vpr(y) * Math.Sin(psy); ; break; //z
                    case 5: raschet_vlyania_vetra(); M = Mah(); q_M = q(M); //Vk
                        result = -g_ucl.g(y) * Math.Sin(teta) - (C_xk(M) * q_M * sn.Sm) / sn.m; break;
                    case 6: wxs = wx_s(); f6 = -g_ucl.g(y) * Math.Cos(teta) / Vk - (C_yk(M, wxs) * q_M * sn.Sm) / (sn.m * Vk) +
                               Vk * Math.Cos(teta) / (g_ucl.R_geo + y) + delta_tetaZemli();
                        result = f6; break;    //teta                
                    case 7: f7 = - (C_zk(M, wxs) * q_M * sn.Sm) / (sn.m * Vk * Math.Cos(teta)) + delta_psyZemli();
                        result = f7; break; //psy
                    case 8: result = - mx(M, wxs) * q_M * sn.Sm * sn.l / sn.I_x; break;//omega_x
                    case 9: result = - g_ucl.g(y) * pi_y * f3 / (m_ucl.R * m_ucl.tau_y(y)); break; //pi(y)
                }
            }
            catch { /* MessageBox.Show("деление на 0"); System.Windows.Forms.Application.Exit();*/ exc = 1; }
            return result;
        }
        public int konech_kriteri(double t, double x, double y, double z, double Vk, double teta)
        {
            int kon_krit = -1;
            if ((teta < 0) && (y <= b_ucl.yk)) return 3;
            if ((b_ucl.tk > 0) && (t >= b_ucl.tk)) return 0;
            if ((b_ucl.xk > 0) && (x >= b_ucl.xk)) return 2;
            if ((b_ucl.zk > 0) && (z >= b_ucl.zk)) return 4;
            if ((b_ucl.Vkk > 0) && (Vk <= b_ucl.Vkk)) return 5;
            return kon_krit;
        }
        public int RungeKutta4(ref string mes)
        {
            mes = "";
            return_code = 0;
            int erc = 0;
            try
            {
                //param
                int i, k = 1;
                t = X0_[0]; x = X0_[2]; y = X0_[3]; z = X0_[4]; teta = X0_[6]; double y_max = y;
                double[,] ark = new double[ChisloUraneniy + 1, 5];
                double[] arF1 = new double[ChisloUraneniy + 1];
                double[] arF2 = new double[ChisloUraneniy + 1];
                //нач.условия
                eps_wind1 = 0; eps_wind2 = 0;
                for (i = 0; i <= ChisloUraneniy; i++) arF1[i] = X0_[i];
                RRR.Clear(); RRR.Add(new List<double>());
                for (i = 0; i <= ChisloUraneniy; i++)
                    //if ((i == 6) | (i == 7) | (i == 13) | (i == 14)) RRR[0].Add(X0_[i] * 180 / Math.PI);
                    RRR[0].Add(X0_[i]);
                chisloUzlovSetky = 1;
                int kon_krit = 0;
                //расчет
                int iter = 0;
                do
                {
                    iter++;
                    //System.Windows.Forms.Application.DoEvents();
                    for (i = 1; i <= ChisloUraneniy; i++) { ark[i, 1] = f(i, arF1, ref erc) * dt; }
                    arF2[0] = arF1[0] + dt / 2.0;
                    for (i = 1; i <= ChisloUraneniy; i++) arF2[i] = arF1[i] + ark[i, 1] / 2.0;
                    for (i = 1; i <= ChisloUraneniy; i++) ark[i, 2] = f(i, arF2, ref erc) * dt;
                    for (i = 1; i <= ChisloUraneniy; i++) arF2[i] = arF1[i] + ark[i, 2] / 2.0;
                    for (i = 1; i <= ChisloUraneniy; i++) ark[i, 3] = f(i, arF2, ref erc) * dt;
                    arF2[0] = arF1[0] + dt;
                    for (i = 1; i <= ChisloUraneniy; i++) arF2[i] = arF1[i] + ark[i, 3];
                    for (i = 1; i <= ChisloUraneniy; i++) ark[i, 4] = f(i, arF2, ref erc) * dt;
                    arF2[0] = arF1[0] + dt;
                    for (i = 1; i <= ChisloUraneniy; i++) arF2[i] = arF1[i] + (ark[i, 1] + 2 * ark[i, 2] + 2 * ark[i, 3] + ark[i, 4]) / 6.0;
                    for (i = 0; i <= ChisloUraneniy; i++) arF1[i] = arF2[i];

                    k++; t = arF1[0]; x = arF1[2]; y = arF1[3]; z = arF1[4]; Vk = arF1[5]; teta = arF1[6];
                    if (y_max < y) y_max = y;
                    RRR.Add(new List<double>());
                    for (i = 0; i <= ChisloUraneniy; i++)
                       // if ((i == 6) | (i == 7) | (i == 13) | (i == 14)) RRR[k - 1].Add(arF1[i] * 180 / Math.PI);
                        RRR[k - 1].Add(arF1[i]);
                    chisloUzlovSetky = chisloUzlovSetky + 1;
                    //konech_v_kriterii
                    if (y < -1000) { return_code = 2; break; }//не сработало условие выхода!!!!
                    if (erc > 0) { return_code = 3; break; }//ошибка вычислений!!!!
                    if (Vk < 0) { return_code = 3; break; }//ошибка
                    if (stop) return return_code;
                    if ((diter > 0) && (iter >= diter)) break;//iter
                    kon_krit = konech_kriteri(t, x, y, z, Vk, teta);
                }
                while (kon_krit < 0);
                if (diter==0) interp_konech_znach(kon_krit);
                k = chisloUzlovSetky;
                Itogo_T = RRR[k - 1][0];
                Itogo_X = RRR[k - 1][2]; Itogo_Y = RRR[k - 1][3]; Itogo_Z = RRR[k - 1][4]; Itogo_teta = RRR[k - 1][6];
                Itogo_y_max = y_max;
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                //System.Windows.Forms.MessageBox.Show("Ошибка: " + ex.Message);
                return_code = 3;
                return return_code;
            }
            return return_code;
        }
        public void interp_konech_znach(int kon_krit)
        {
            if (kon_krit < 0) return;
            double[] peremen1 = new double[ChisloUraneniy + 1];
            double[] peremen2 = new double[ChisloUraneniy + 1];
            double[] peremen_approxim = new double[ChisloUraneniy + 1];
            for (int i = 0; i <= ChisloUraneniy; i++)
            {
                peremen1[i] = RRR[chisloUzlovSetky - 2][i];
                peremen2[i] = RRR[chisloUzlovSetky - 1][i];
            }
            // (y-y1)/(y2-y1)=(x-x1)/(x2-x1) => y=y1+(x-x1)*(y2-y1)/(x2-x1)
            double y1 = RRR[chisloUzlovSetky - 2][kon_krit];
            double y2 = RRR[chisloUzlovSetky - 1][kon_krit];
            double yk = b_ucl.yk;
            switch (kon_krit)
            {
                case 0: yk = b_ucl.tk; break;
                case 2: yk = b_ucl.xk; break;
                case 3: yk = b_ucl.yk; break;
                case 4: yk = b_ucl.zk; break;
                case 5: yk = b_ucl.Vkk; break;
            }
            if (y1 < y2) if (!((y1 <= yk) && (yk <= y2))) return;//extra
            if (y1 > y2) if (!((y1 >= yk) && (yk >= y2))) return;//extra
            for (int i = 0; i <= ChisloUraneniy; i++)
                peremen_approxim[i] = peremen1[i] + (yk - y1) * (peremen2[i] - peremen1[i]) / (y2 - y1);
            for (int i = 0; i <= ChisloUraneniy; i++)
                RRR[chisloUzlovSetky - 1][i] = peremen_approxim[i];
        }
        public void zapolnitGraphiki(ref System.Windows.Forms.DataVisualization.Charting.Chart[] arChart,
            ref System.Windows.Forms.DataVisualization.Charting.Chart chart1_Oxy,
            ref System.Windows.Forms.DataVisualization.Charting.Chart chart1_Oxz,
            ref System.Windows.Forms.DataVisualization.Charting.Chart chart1_Ozy)
        {
            int step1 = 10;
            int step2 = 10;
            int i, j; double xx, yy, zz;
            for (j = 0; j <= chisloUzlovSetky - 1; j++)
            {
                if ((j % step1 == 0)||(j==(chisloUzlovSetky-1)))
                {
                    for (i = 1; i <= ChisloUraneniy; i++)
                        arChart[i].Series[0].Points.AddXY(RRR[j][0], ToF(i, RRR[j][i]));
                    xx = RRR[j][2]; yy = RRR[j][3]; zz = RRR[j][4];
                    chart1_Oxy.Series[0].Points.AddXY(xx, yy);
                    chart1_Oxz.Series[0].Points.AddXY(xx, zz);
                    chart1_Ozy.Series[0].Points.AddXY(zz, yy);
                }
            }
        }
        public void zapolnitTablicy(ref System.Windows.Forms.DataGridView dataGridView1_Reshenie)
        {
            string st;
            int step3 = 1;
            int j1 = 0;
            int i, j;
            bool flag = true;
            dataGridView1_Reshenie.Rows.Clear();
            for (j = 1; j <= chisloUzlovSetky; j += step3)
            {
                dataGridView1_Reshenie.Rows.Add(); j1++;
                for (i = 0; i <= ChisloUraneniy; i++)
                    dataGridView1_Reshenie.Rows[j1 - 1].Cells[i].Value = ToF(i, RRR[j - 1][i]).ToString("0.###");
                if (j == chisloUzlovSetky) flag = false;
            }
            //last row
            j = chisloUzlovSetky;
            if (flag)
            {
                dataGridView1_Reshenie.Rows.Add();
                j1 = dataGridView1_Reshenie.RowCount - 1;
                for (i = 0; i <= ChisloUraneniy; i++)
                    dataGridView1_Reshenie.Rows[j1 - 1].Cells[i].Value = ToF(i, RRR[j - 1][i]).ToString("0.###");
            }
        }
        public void Raschet_Vivod(bool output,
            ref System.Windows.Forms.DataVisualization.Charting.Chart[] arChart,
            ref System.Windows.Forms.DataVisualization.Charting.Chart chart1_Oxy,
            ref System.Windows.Forms.DataVisualization.Charting.Chart chart1_Oxz,
            ref System.Windows.Forms.DataVisualization.Charting.Chart chart1_Ozy,
            ref System.Windows.Forms.DataGridView dataGridView1_Reshenie)
        {
            //очистить графики
            for (int i = 1; i <= ChisloUraneniy; i++)
                arChart[i].Series[0].Points.Clear();
            chart1_Oxy.Series[0].Points.Clear();
            chart1_Oxz.Series[0].Points.Clear();
            chart1_Ozy.Series[0].Points.Clear();
            System.Windows.Forms.Application.DoEvents();
            //*****
            string mes = "";
            RungeKutta4(ref mes);
            //*****
            if (output) zapolnitGraphiki(ref arChart, ref chart1_Oxy, ref chart1_Oxz, ref chart1_Ozy);
            if (output) zapolnitTablicy(ref dataGridView1_Reshenie);
        }
        public bool CalculateVariant(double azimuth, double pteta, double y_end)
        {
            b_ucl.a_direct = azimuth * Math.PI / 180.0f;
            b_ucl.teta0 = pteta * Math.PI / 180.0f;
            double y_end_last = b_ucl.yk;
            b_ucl.yk = y_end;
            string mes = "";
            RungeKutta4(ref mes);
            b_ucl.yk = y_end_last;
            return true;
        }
        public int LoadNUandParam(string f, ref string mes)
        {
            mes = "";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS);
            int i;
            string st = SR.ReadLine();//snaryad
            try
            {
                //initial data
                b_ucl.t0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.x0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.y0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.z0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.tk = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.xk = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.yk = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.zk = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.V0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.wx0 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.teta0_grad = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.psy0_grad = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                b_ucl.a_dir_grad = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);
                //Params             
                st = SR.ReadLine();//Params
                i = Convert.ToInt32(SR.ReadLine().Split(' ')[1]);//blok_1 geo
                if (i == 0) U_uchitivat_vrashenie_zemli = false; else U_uchitivat_vrashenie_zemli = true;
                i = Convert.ToInt32(SR.ReadLine().Split(' ')[1]);//blok_2 meteo
                if (i == 0) U_uchitivat_veter = false; else U_uchitivat_veter = true;
                m_ucl.wind_type = Convert.ToInt32(SR.ReadLine().Split(' ')[1]);//wind_type
                m_ucl.w_veter10 = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);//w_veter10
                m_ucl.a_weter_grad = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);//a_weter
                m_ucl.w_p = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);//w_p
                Cx_law = Convert.ToInt32(SR.ReadLine().Split(' ')[1]);//cx_law
                dt = Convert.ToDouble(SR.ReadLine().Split(' ')[1]);//dt
                diter = Convert.ToInt32(SR.ReadLine().Split(' ')[1]);//diter
            }
            catch (Exception e)
            {
                mes = e.Message;
                return 1;
            }
            SR.Close(); FS.Close();
            return 0;
        }
        string fst(bool bo)
        {
            if (bo) return "1"; else return "0";
        }
        public int SaveNUandParam(string f, ref string mes)
        {
            mes = "";
            FileStream FS = new FileStream(f, FileMode.Create, FileAccess.Write);
            StreamWriter SW = new StreamWriter(FS);
            try
            {
                SW.WriteLine("Название_снаряда " + sn.name.ToString());
                SW.WriteLine("t0(c) " + b_ucl.t0.ToString());
                SW.WriteLine("x0(м) " + b_ucl.x0.ToString());
                SW.WriteLine("y0(м) " + b_ucl.y0.ToString());
                SW.WriteLine("z0(м) " + b_ucl.z0.ToString());
                SW.WriteLine("tk(c) " + b_ucl.tk.ToString());
                SW.WriteLine("xk(м) " + b_ucl.xk.ToString());
                SW.WriteLine("yk(м) " + b_ucl.yk.ToString());
                SW.WriteLine("zk(м) " + b_ucl.zk.ToString());
                SW.WriteLine("V0(м/c) " + b_ucl.V0.ToString("0.0##"));
                SW.WriteLine("wx0(рад/с) " + b_ucl.wx0.ToString("0.0##"));
                SW.WriteLine("teta0(град) " + b_ucl.teta0_grad.ToString("0.0##"));
                SW.WriteLine("psy0(град) " + b_ucl.psy0_grad.ToString("0.0##"));
                SW.WriteLine("a_direct(град) " + b_ucl.a_dir_grad.ToString());
                SW.WriteLine("Параметры расчета:");
                SW.WriteLine("Учет_вращения_Земли(блок_1) " + fst(U_uchitivat_vrashenie_zemli));
                SW.WriteLine("Учет_метеоусловий(блок_2) " + fst(U_uchitivat_veter));
                SW.WriteLine("Тип_задания_ветра(1-пост,2-степ,3-реал) " + m_ucl.wind_type.ToString());
                SW.WriteLine("Скорость_ветра_Н=10м(м/с) " + m_ucl.w_veter10.ToString());
                SW.WriteLine("Направление_ветра(град) " + m_ucl.a_weter_grad.ToString());
                SW.WriteLine("Показатель_степ_закона " + m_ucl.w_p.ToString());
                SW.WriteLine("Закон_сопротивления(1-1943г,2-1958г,3-расч) " + Cx_law.ToString());
                SW.WriteLine("Временной_шаг_dt(c) " + dt.ToString());
                SW.WriteLine("Количество_итераций 0");
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                return 1;
            }
            SW.Close(); FS.Close();
            //System.Windows.Forms.MessageBox.Show("Файл успешно сохранен.");
            return 0;
        }
        public int SaveTo_txt(string f, bool podrobno, ref string mes)
        {
            mes = "";
            FileStream FS = new FileStream(f, FileMode.Create, FileAccess.Write);
            StreamWriter SW = new StreamWriter(FS);            
            int i, j;
            bool flag = true;
            int step = 1;
            try
            {
                for (i = 1; i <= 10; i++)
                    SW.Write(NameVarForBlocknot[i] + " ");
                SW.WriteLine();
                for (j = 0; j <= chisloUzlovSetky - 1; j += step)//for (i = 0; i <= VB.ChisloUraneniy; i++) { SW.Write(VB.RRR[j - 1][i].ToString("0.######") + "  "); }; 
                {
                    SW.Write(RRR[j][0].ToString("0.000#") + " ");//t
                    SW.Write(RRR[j][2].ToString("0.0000") + " "); SW.Write(RRR[j][3].ToString("0.0000") + " "); SW.Write(RRR[j][4].ToString("0.0000") + " "); //x y z
                    SW.Write(RRR[j][5].ToString("0.0000") + " "); SW.Write(ToF(6, RRR[j][6]).ToString("0.0000") + " "); SW.Write(ToF(7, RRR[j][7]).ToString("0.0000") + " "); //Vk teta psy
                    SW.Write(RRR[j][8].ToString("0.0000") + " "); SW.Write(RRR[j][9].ToString("0.0000") + " "); SW.Write(RRR[j][10].ToString("0.0000") + " "); //wx pi m
                    SW.WriteLine();
                    if (j == chisloUzlovSetky - 1) flag = false; 
                }
                //last row
                j = chisloUzlovSetky - 1;
                if (flag)
                {
                    SW.Write(RRR[j][0].ToString("0.000#") + " ");//t
                    SW.Write(RRR[j][2].ToString("0.0000") + " "); SW.Write(RRR[j][3].ToString("0.0000") + " "); SW.Write(RRR[j][4].ToString("0.0000") + " "); //x y z
                    SW.Write(RRR[j][5].ToString("0.0000") + " "); SW.Write(ToF(6, RRR[j][6]).ToString("0.0000") + " "); SW.Write(ToF(7, RRR[j][7]).ToString("0.0000") + " "); //Vk teta psy
                    SW.Write(RRR[j][8].ToString("0.0000") + " "); SW.Write(RRR[j][9].ToString("0.0000") + " "); SW.Write(RRR[j][10].ToString("0.0000") + " "); //wx pi m
                    SW.WriteLine();
                }
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                return 1;
            }
            SW.Close(); FS.Close();
            //System.Windows.Forms.MessageBox.Show("Файл успешно сохранен.");
            return 0;
        }
        public int SaveTo_xls(string f, bool podrobno, ref string mes)
        {
            mes = "";            
            Microsoft.Office.Interop.Excel.Application ObjExcel;
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
            ObjExcel = new Microsoft.Office.Interop.Excel.Application(); //Приложение самого Excel           
            ObjWorkBook = ObjExcel.Workbooks.Add(); //Книга
            ObjWorkSheet = ObjExcel.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet; //Таблица.            
            Microsoft.Office.Interop.Excel.Sheets mSheets = ObjWorkBook.Sheets;//Ячейка                      
            Microsoft.Office.Interop.Excel.Worksheet mSheet = (Microsoft.Office.Interop.Excel.Worksheet)mSheets.get_Item(1);
            ObjWorkSheet.Columns.Clear();
            bool flag = true;
            int i, j, k;
            int j1 = 0;
            int step = 1;
            try
            {
                object[,] objData = new Object[(chisloUzlovSetky / step) + 3, ChisloUraneniy + 1];
                for (i = 0; i <= ChisloUraneniy; i++)
                    objData[0, i] = NameVar[i];
                j1 = 1;
                for (j = 0; j <= chisloUzlovSetky - 1; j += step)
                {
                    for (i = 0; i <= ChisloUraneniy; i++)
                        objData[j1, i] = ToF(i, RRR[j][i]);
                    j1++;
                    if (j == chisloUzlovSetky - 1) flag = false;
                }
                //last row
                j = chisloUzlovSetky - 1;
                if (flag)
                {
                    for (i = 0; i <= ChisloUraneniy; i++)
                        objData[j1, i] = ToF(i, RRR[j][i]);
                }
                //--
                k = chisloUzlovSetky + 2;
                Range mRange = mSheet.get_Range("A1");
                mRange = mRange.get_Resize((chisloUzlovSetky / step) + 3, ChisloUraneniy + 1);
                mRange.Value = objData;
                ObjWorkBook.SaveAs(f);
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                ObjWorkBook.Close();//Закрытие книгу Excel.            
                ObjExcel.Quit(); //Закрытие приложения Excel.
                                 //Обнуляем созданые объекты
                ObjWorkBook = null;
                ObjWorkSheet = null;
                ObjExcel = null;
                GC.Collect();//Вызываем сборщик мусора для их уничтожения и освобождения памяти 
                return 1;
            }
            ObjWorkBook.Close();//Закрытие книгу Excel.            
            ObjExcel.Quit(); //Закрытие приложения Excel.
            //Обнуляем созданые объекты
            ObjWorkBook = null;
            ObjWorkSheet = null;
            ObjExcel = null;
            GC.Collect();//Вызываем сборщик мусора для их уничтожения и освобождения памяти 
            //System.Windows.Forms.MessageBox.Show("Файл успешно сохранен.");
            return 0;
        }
        public void zapolnitVspomogatArrays()
        {
            NameVar = new string[ChisloUraneniy + 1];
            NameVar[0] = "t, c"; NameVar[1] = "S, м"; NameVar[2] = "х, м"; NameVar[3] = "y, м"; NameVar[4] = "z, м";
            NameVar[5] = "Vк, м/с"; NameVar[6] = "teta, град"; NameVar[7] = "psy, град"; NameVar[8] = "omega_x, рад/с";
            NameVar[9] = "pi(y)";
            NameVarForBlocknot = new string[ChisloUraneniy + 1];
            NameVarForBlocknot[1] = "t(c)"; NameVarForBlocknot[2] = "x(м)"; NameVarForBlocknot[3] = "y(м)"; NameVarForBlocknot[4] = "z(м)";
            NameVarForBlocknot[5] = "Vk(м/с)"; NameVarForBlocknot[6] = "teta(град)"; NameVarForBlocknot[7] = "psy(град)";
            NameVarForBlocknot[8] = "omega_x(рад/с)"; NameVarForBlocknot[9] = "pi(y)";
        }

    }//VneshBall
}
