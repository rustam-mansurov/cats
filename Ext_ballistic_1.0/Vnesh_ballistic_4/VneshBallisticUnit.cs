using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vnesh_ballistic
{
    public class emp_aero
    {
        public string f1 { get; set; }
        public List<double> M1_43 { get; set; }
        public List<double> M2_43 { get; set; }
        public List<double> a0_43 { get; set; }
        public List<double> a1_43 { get; set; }
        public List<double> a2_43 { get; set; }
        public List<double> a3_43 { get; set; }
        public string f2 { get; set; }
        public List<double> M1_58 { get; set; }
        public List<double> M2_58 { get; set; }
        public List<double> b0_58 { get; set; }
        public List<double> b1_58 { get; set; }
        public List<double> b2_58 { get; set; }
        public List<double> b3_58 { get; set; }
        public string f3 { get; set; }
        public List<double> M1_K_NM { get; set; }
        public List<double> M2_K_NM { get; set; }
        public List<double> a0_K_NM { get; set; }
        public List<double> a1_K_NM { get; set; }
        public List<double> a2_K_NM { get; set; }

        public emp_aero()
        {
            f1 = "Закон 1943 года";
            M1_43 = new List<double>();
            M2_43 = new List<double>();
            a0_43 = new List<double>();
            a1_43 = new List<double>();
            a2_43 = new List<double>();
            a3_43 = new List<double>();
            f2 = "Закон 1958 года";
            M1_58 = new List<double>();
            M2_58 = new List<double>();
            b0_58 = new List<double>();
            b1_58 = new List<double>();
            b2_58 = new List<double>();
            b3_58 = new List<double>();
            f3 = "Функция сопротивления K_NM";
            M1_K_NM = new List<double>();
            M2_K_NM = new List<double>();
            a0_K_NM = new List<double>();
            a1_K_NM = new List<double>();
            a2_K_NM = new List<double>();
        }

        public async void SaveToJson(string f)
        {
            //сохранить в файл
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            using (FileStream fs = new FileStream(f, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<emp_aero>(fs, this, options);
            }
        }
    } // class emp_aero

    public class snaryad
    {
        public string name { get; set; }
        public double D { get; set; }
        public double m { get; set; }
        public double l { get; set; }
        public double l_cm { get; set; }
        public double l_gol { get; set; }
        [JsonIgnore]
        public double Sm { get { return Math.PI * D * D / 4; }  }
        public double I_x { get; set; }
        public double Vs0 { get; set; }//
        public double narez { get; set; }//
        //аэродинамические коэффициенты
        public int cx_law { get; set; }//0 - индивид., 1 - закон 1943, 2 - закон 1958
        public double ix { get; set; }
        public double iz { get; set; }
        public double m_x_omega { get; set; }
        //баллистический коэффициент
        public double BC { get { return 1000.0 * ix * D * D / m; } }
        //эмпирические зависимости
        public emp_aero emp;
        //расчетные зависимости
        public double[] Ma { get; set; }
        public double[,] Cxa;
        public double[] a0_ind { get; set; }
        public double[] a1_ind { get; set; }
        public double[] a2_ind { get; set; }
        public double[] a3_ind { get; set; }

        public snaryad()
        {
            emp = new emp_aero();
        }
        public async Task init(string path)
        {
            try
            {
                string f = path + "\\cx\\emp_aero_func.json";
                using (FileStream fs = new FileStream(f, FileMode.OpenOrCreate))
                {
                    emp = await JsonSerializer.DeserializeAsync<emp_aero>(fs);
                    fs.Close();
                }
            }
            catch (Exception e)
            {
            }
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
        public async void SaveToJson(string fname)
        {
            //сохранить в файл
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<snaryad>(fs, this, options);
            }
        }
        public double Cx_k_a(double M)
        {
            int i = 0;
            for (i = 1; i <= Ma.Count()-2; i++) { if (Ma[i]>M) break; }
            return a0_ind[i-1] + a1_ind[i-1] * M + a2_ind[i-1] * M * M + a3_ind[i-1] * M * M * M;
        }
        public double Cx_otM_et(double M, int law)
        {
            int i; int ii = 0; double result = 0;
            if (law == 1)//1943
            {
                for (i = 0; i <= emp.M1_43.Count - 1; i++)
                    if ((M >= emp.M1_43[i]) & (M < emp.M2_43[i])) ii = i;
                if (M > emp.M2_43[emp.M2_43.Count - 1]) ii = emp.M2_43.Count - 1;
                result = emp.a0_43[ii] + emp.a1_43[ii] * M + emp.a2_43[ii] * M * M + emp.a3_43[ii] * M * M * M;
            }
            else//1958
            {
                for (i = 0; i <= emp.M1_58.Count - 1; i++)
                    if ((M >= emp.M1_58[i]) & (M < emp.M2_58[i])) ii = i;
                if (M > emp.M2_58[emp.M2_58.Count - 1]) ii = emp.M2_58.Count - 1;
                result = emp.b0_58[ii] + emp.b1_58[ii] * M + emp.b2_58[ii] * M * M + emp.b3_58[ii] * M * M * M;
            }
            return result;
        }
        public double K_NM(double M)
        {
            int i; int ii = 0; double result = 0;
            for (i = 0; i <= emp.M1_K_NM.Count - 1; i++)
                if ((M >= emp.M1_K_NM[i]) & (M < emp.M2_K_NM[i])) ii = i;
            if (M > emp.M1_K_NM[emp.M1_K_NM.Count - 1]) ii = emp.M1_K_NM.Count - 1;
            result = emp.a0_K_NM[ii] + emp.a1_K_NM[ii] * M + emp.a2_K_NM[ii] * M * M;
            return result;
        }

    }//class snaryad

    public class ball_ucl
    {
        public double t0 { get { return X0[0]; } set { X0[0] = value; } }
        public double x0 { get { return X0[2]; } set { X0[2] = value; if (value == 0) X0[1] = 0; } } //S!!! 
        public double y0 { get { return X0[3]; } set { X0[3] = value; } }
        public double z0 { get { return X0[4]; } set { X0[4] = value; } }
        public double tk { get; set; }
        public double xk { get; set; }
        public double yk { get; set; }
        public double V0 { get { return X0[5]; } set { X0[5] = value; } }
        [JsonIgnore] 
        public double teta0 { get { return X0[6]; } set { X0[6] = value; } }
        public double teta0_grad { get { return teta0 * 180 / Math.PI; } set { teta0 = value * Math.PI / 180; } }
        [JsonIgnore]
        public double psy0 { get { return X0[7]; } set { X0[7] = value; } }
        public double psy0_grad { get { return psy0 * 180 / Math.PI; } set { psy0 = value * Math.PI / 180; } }
        public double wx0 { get { return X0[8]; } set { X0[8] = value; } }
        public double pi0 { get { return X0[9]; } set { X0[9] = value; } }
        [JsonIgnore]
        public double a_direct;//rad
        public double a_dir_grad { get { return a_direct * 180 / Math.PI; } set { a_direct = value * Math.PI / 180; } }
        [JsonIgnore]
        public double[] X0;//нач_условия

        public ball_ucl()
        {
            X0 = new double[14 + 1];
        }
        public async void SaveToJson(string f)
        {
            //сохранить в файл
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            using (FileStream fs = new FileStream(f, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<ball_ucl>(fs, this, options);
            }
        }

    }//class ball_ucl

    public class geo_const
    {
        public double g_N0 { get; set; }
        public double Gg { get; set; }
        public double R_geo { get; set; }
        public double W_geo { get; set; }
        public double delta2_g { get; set; }

        public geo_const()
        {

        }

    }//class geo_const

    public class geo_ucl
    {
        //const
        public geo_const geo;
        //params
        public double B_geo { get; set; } //широта
        public int U_FormaZemli { get; set; } // 0 - плоская, 1 - шар
        public int U_VrashZemli { get; set; } // 0 - не вращ, 1 - вращ

        public geo_ucl()
        {
            geo = new geo_const();
            U_FormaZemli = 0;
            U_VrashZemli = 0;
        }
        public async Task init(string path)
        {
            try
            {
                string f = path + "\\meteo\\geo_const.json";
                using (FileStream fs = new FileStream(f, FileMode.OpenOrCreate))
                {
                    geo = await JsonSerializer.DeserializeAsync<geo_const>(fs);
                    fs.Close();
                }
            }
            catch (Exception e)
            {
            }
        }
        public async void SaveToJson(string f)
        {
            //сохранить в файл
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            using (FileStream fs = new FileStream(f, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<geo_ucl>(fs, this, options);
            }
        }
        public double g(double y)//ускорение свободного падения, функция от у
        {
            return geo.g_N0 - geo.Gg * y + U_FormaZemli * (0.0517 * Math.Sin(B_geo) * Math.Sin(B_geo) + geo.delta2_g);
        }
        public double r_g(double y0, double y)//коэффициент учета формы Земли
        {
            if (U_FormaZemli == 0) return 1;
            else return (geo.R_geo + y0) / (geo.R_geo + y);
        }

    }//class geo_ucl

    public class meteo_norm
    {
        public double R { get; set; } //удел.газ.постоянная
        public double aN0 { get; set; }
        public double tauN0 { get; set; }
        public double roN0 { get; set; }
        public double PN0 { get; set; }

        public meteo_norm()
        {

        }

    }//class meteo_norm

    public class meteo_ucl
    {
        //const
        public meteo_norm norm;
        //params
        public int U_norm_real { get; set; } //0-normal, 1-real
        public double T0 { get; set; }
        public double P0 { get; set; }
        [JsonIgnore]
        public double ro0 { get { return P0 / (T0 * norm.R); } }
        public int U_wind { get; set; } //0-не учит, 1-учет ветра
        public int U_wind_type { get; set; } //1-постоянный, 2-степенной, 3-реальный
        public double w_h10 { get; set; }
        public double w_p { get; set; }
        [JsonIgnore] 
        public double a_w { get; set; }
        public double a_w_grad { get { return a_w * 180 / Math.PI; } set { a_w = value * Math.PI / 180; } }
        //распределения
        public List<double> y_tnorm_ar;
        public List<double> tau_norm_ar;
        public List<double> tau_k1;
        public List<double> tau_k2;
        public List<double> y_pnorm_ar;
        public List<double> p_norm_ar;
        //реальные
        public string datafile { get; set; } //файл данных
        public List<double> y_real_ar;
        public List<double> tau_real_ar;
        public List<double> p_real_ar;
        public List<double> w_real_ar;
        public List<double> aw_real_ar;

        public meteo_ucl()
        {
            norm = new meteo_norm();
            U_norm_real = 0;
            U_wind = 0;
            U_wind_type = 1;
            y_tnorm_ar = new List<double>();
            tau_real_ar = new List<double>();
            tau_norm_ar = new List<double>();
            tau_k1 = new List<double>();
            tau_k2 = new List<double>();
            y_pnorm_ar = new List<double>();
            p_norm_ar = new List<double>();
            //реальные
            y_real_ar = new List<double>();
            p_real_ar = new List<double>();
            w_real_ar = new List<double>();
            aw_real_ar = new List<double>();
        }
        public async Task init(string path)
        {
            string mes = "";
            try
            {
                string f = path + "\\meteo\\meteo_const.json";
                using (FileStream fs = new FileStream(f, FileMode.OpenOrCreate))
                {
                    norm = await JsonSerializer.DeserializeAsync<meteo_norm>(fs);
                    fs.Close();
                }
                LoadNormFromCsv(path, ref mes);
                f = path + "\\meteo\\" + datafile;
                LoadRealFromCsv(f, ref mes);
            }
            catch (Exception e)
            {
            }
        }
        public int LoadNormFromCsv(string path, ref string mes)
        {
            mes = "";
            string f = path + "\\meteo\\P_ot_y_nominal.csv";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS);
            try
            {
                string st = SR.ReadLine(); //заголовок
                y_pnorm_ar.Clear(); p_norm_ar.Clear();
                st = SR.ReadLine();
                while (st != null)
                {
                    string[] s = st.Split(';');
                    y_pnorm_ar.Add(Convert.ToDouble(s[0]));//y
                    p_norm_ar.Add(Convert.ToDouble(s[1]));//p
                    st = SR.ReadLine();
                }
                SR.Close(); FS.Close();
                f = path + "\\meteo\\tau_ot_y_nominal.csv";
                FS = new FileStream(f, FileMode.Open, FileAccess.Read);
                SR = new StreamReader(FS);
                st = SR.ReadLine(); //заголовок
                y_tnorm_ar.Clear(); tau_norm_ar.Clear();
                tau_k1.Clear(); tau_k2.Clear();
                st = SR.ReadLine();
                while (st != null)
                {
                    string[] s = st.Split(';');
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
        public int LoadRealFromCsv(string f, ref string mes)
        {
            mes = "";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS);
            try
            {
                datafile = Path.GetFileName(f); //файл данных
                string st = SR.ReadLine(); //заголовок
                y_real_ar.Clear(); p_real_ar.Clear(); tau_real_ar.Clear();
                aw_real_ar.Clear(); w_real_ar.Clear();
                st = SR.ReadLine();
                while (st != null)
                {
                    string[] s = st.Split(';');
                    y_real_ar.Add(Convert.ToDouble(s[0]));
                    p_real_ar.Add(Convert.ToDouble(s[1]));
                    tau_real_ar.Add(Convert.ToDouble(s[2]) + 273.15);//перевод в кельвины
                    double alpha_w = Convert.ToDouble(s[3]);
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
        public async void SaveToJson(string f)
        {
            //сохранить в файл
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            using (FileStream fs = new FileStream(f, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<meteo_ucl>(fs, this, options);
            }
        }
        public double P_y(double y) //зависимость давления от высоты, функция y  
        {
            double r = 0;
            if (U_norm_real == 0)   //normal
            {
                int j = 0; int p_Count = p_norm_ar.Count();
                for (int i = 0; i <= p_Count - 2; i++) if ((y >= y_pnorm_ar[i]) & (y < y_pnorm_ar[i + 1])) { j = i; break; }
                if (y > y_pnorm_ar[p_Count - 1]) { j = p_Count - 1; }
                // y=y1+(x-x1)*(y2-y1)/(x2-x1) - лин.интерп
                if (j < p_Count - 1)
                    r = p_norm_ar[j] + (y - y_pnorm_ar[j]) * (p_norm_ar[j + 1] - p_norm_ar[j]) / (y_pnorm_ar[j + 1] - y_pnorm_ar[j]);
                else r = p_norm_ar[p_Count - 1];
            }
            if (U_norm_real == 1)  //real data
            {
                int j = 0; int p_Count = p_real_ar.Count();
                for (int i = 0; i <= p_Count - 2; i++)
                    if ((y >= y_real_ar[i]) & (y < y_real_ar[i + 1])) { j = i; break; }
                if (y > y_real_ar[p_Count - 1]) { j = p_Count - 1; }
                // y=y1+(x-x1)*(y2-y1)/(x2-x1) - лин.интерп
                if (j < p_Count - 1)
                    r = p_real_ar[j] + (y - y_real_ar[j]) * (p_real_ar[j + 1] - p_real_ar[j]) / (y_real_ar[j + 1] - y_real_ar[j]);
                else r = p_real_ar[p_Count - 1];
            }
            return r;
        }
        public double tau_y(double y)//зависимость температуры от высоты, функция y            
        {
            double r = 0;
            if (U_norm_real == 0)   //normal
            {
                int j = 0;
                for (int i = 0; i <= 4; i++)
                    if ((y >= y_tnorm_ar[i]) & (y < y_tnorm_ar[i + 1])) { j = i; break; }
                if (y > y_tnorm_ar[5]) { j = 5; }
                r = tau_k1[j] + tau_k2[j] * (y - y_tnorm_ar[j]); //G_tau_j
                r = tau_norm_ar[j] + r * (y - y_tnorm_ar[j]);
                r = r + (T0 - norm.tauN0); //delta_tau(y) !?!
            }
            if (U_norm_real == 1)  //real data
            {
                int j = 0;
                int t_Count = tau_real_ar.Count();
                for (int i = 0; i <= t_Count - 2; i++)
                    if ((y >= y_real_ar[i]) & (y < y_real_ar[i + 1])) { j = i; break; }
                if (y > y_real_ar[t_Count - 1]) { j = t_Count - 1; }
                // y=y1+(x-x1)*(y2-y1)/(x2-x1) - лин.интерп
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
            // y=y1+(x-x1)*(y2-y1)/(x2-x1) - лин.интерп
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
            // y=y1+(x-x1)*(y2-y1)/(x2-x1) - лин.интерп
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
            return w_h10 * Math.Pow(yy / 10, w_p);
        }
        public double deltaP0()
        {
            double res = 0;
            if (U_norm_real == 0) res = P0 - norm.PN0;
            else res = P_y(0) - norm.PN0;
            return res;
        }
        public double pi_0(double y0)//нач.условие
        {
            double chislo = 1 - (-6.328e-3 * y0) / norm.tauN0;  
            chislo = Math.Pow(chislo, 5.4) * (1 + deltaP0() / norm.PN0);
            return chislo;
        }
        public double w_vertical_veter(double y)//функция от y, для ветра, скорость вертикальных токов воздуха 
        {
            return 0;
        }
        public void veter_param(double y, ref double w_veter, ref double alpha_w, ref double w_vert_veter)
        {
            //veter_type == 1
            w_veter = w_h10;
            alpha_w = a_w;
            w_vert_veter = 0;
            if (U_wind_type == 2)
            {
                w_veter = w_veter_step(y);
            }
            if (U_wind_type == 3)
            {
                alpha_w = alpha_w_veter_real(y);
                w_veter = w_veter_real(y);
                w_vert_veter = w_vertical_veter(y);
            }
        }
        public double a(double y)
        {//скорость звука в воздухе, функция y
            return norm.aN0 * Math.Sqrt(tau_y(y) / norm.tauN0);
        }

    }//meteo_ucl

    public class VneshBall  //vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
    {
        public snaryad sn { get; set; }
        public ball_ucl b_ucl { get; set; }
        public geo_ucl g_ucl { get; set; }
        public meteo_ucl m_ucl { get; set; }
        //параметры расчета
        public int Cx_law { get; set; }
        public int return_code;
        [JsonIgnore]
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
        //public double t0;
        public double dt { get; set; }
        public int metod { get; set; }// 1 - RK-4, 2 -  RK5_6
        public int diter;
        public int chisloUzlovSetky;
        public int ChisloUraneniy;
        //переменные и параметры
        public List<List<double>> RRR = new List<List<double>>();
        public double t, S, x, y, z;
        public double Vk, teta, psy, wx, pi_y;
        public double M;
        public double[] X0_;//нач_условия!!!
        public double delta_V_w_veter;
        public double delta_Cxk_ot_eps_veter, delta_Cyk_ot_eps_veter, delta_Czk_ot_eps_veter;
        public double f3, f6, f7, Cxk, Cyk, Czk, q_M, wxs;
        //конечные
        public double Itogo_T, Itogo_D, Itogo_y_max, Itogo_X, Itogo_Y, Itogo_Z, Itogo_teta;
        //ввод-вывод
        public string[] NameVar;
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
            m_ucl.U_norm_real = 0;
            dt = 0.05;
            diter = 0;
            metod = 1;
        }
        public async Task init(string path)
        {
            await sn.init(path);
            X0_ = b_ucl.X0;
            await g_ucl.init(path);
            await m_ucl.init(path);
            zapolnitVspomogatArrays();
        }
        public async Task LoadSnaryadfromJson(string fname, string path)
        {
            try
            {
                using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate))
                {
                    sn = await JsonSerializer.DeserializeAsync<snaryad>(fs);
                }
                await sn.init(path);
            }
            catch (Exception e)
            {
            }
        }

        public async void SaveNUandParamToJson(string fname)
        {
            //сохранить в файл
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<VneshBall>(fs, this, options);
                fs.Close();
            }
        }
        public async Task LoadNUandParamFromJson(string fname) // в TrajectoryMainForm!!!
        {
            //здесь не работает!
            try
            {
                using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate))
                {
                    sn = await JsonSerializer.DeserializeAsync<snaryad>(fs);
                    b_ucl = await JsonSerializer.DeserializeAsync<ball_ucl>(fs);
                    g_ucl = await JsonSerializer.DeserializeAsync<geo_ucl>(fs);
                    m_ucl = await JsonSerializer.DeserializeAsync<meteo_ucl>(fs);
                    fs.Close();
                }
            }
            catch (Exception e)
            {
            }
        }
        public void InitFromNU()
        {
            X0_[0] = b_ucl.t0; X0_[1] = 0; //S
            X0_[2] = b_ucl.x0; X0_[3] = b_ucl.y0; X0_[4] = b_ucl.z0; 
            X0_[5] = b_ucl.V0; X0_[6] = b_ucl.teta0; X0_[7] = b_ucl.psy0;
            b_ucl.wx0 = sn.Calc_wx0(b_ucl.V0); X0_[8] = b_ucl.wx0;
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
            if (m_ucl.U_wind==1)
            {
                double w_veter=0, alpha_w=0, w_vert_veter=0;
                m_ucl.veter_param(y, ref w_veter, ref alpha_w, ref w_vert_veter);
                double w_x_veter = -w_veter * Math.Cos(alpha_w - (b_ucl.a_direct - psy)) * Math.Cos(teta) + w_vert_veter * Math.Sin(teta);
                double w_y_veter = w_veter * Math.Cos(alpha_w - (b_ucl.a_direct - psy)) * Math.Sin(teta) + w_vert_veter * Math.Cos(teta);
                double w_z_veter = -w_veter * Math.Sin(alpha_w - (b_ucl.a_direct - psy));
                double V_vozdushnaya_skorost_snaryada = Math.Sqrt(Vk * Vk - 2 * w_x_veter * Vk + w_veter * w_veter + w_vert_veter * w_vert_veter);
                delta_V_w_veter = V_vozdushnaya_skorost_snaryada - Vk;
                double sin_eps_w2_veter = - w_y_veter / V_vozdushnaya_skorost_snaryada;
                double eps_w2_veter = Math.Asin(sin_eps_w2_veter);
                double cos_eps_w2_veter = Math.Cos(eps_w2_veter);
                double sin_eps_w1_veter = w_z_veter / (V_vozdushnaya_skorost_snaryada * cos_eps_w2_veter);
                double eps_w1_veter = Math.Asin(sin_eps_w1_veter);
                double cos_eps_w1_veter = Math.Cos(eps_w1_veter);
                M = Mah();
                double chislo = 0;
                if (Cx_law == 3)
                    chislo = sn.Cx_k_a(M);
                else
                    chislo = sn.ix * sn.Cx_otM_et(M, Cx_law);
                delta_Cxk_ot_eps_veter = chislo * (cos_eps_w1_veter * cos_eps_w2_veter - 1);
                delta_Cyk_ot_eps_veter = chislo * sin_eps_w2_veter;
                delta_Czk_ot_eps_veter = chislo * sin_eps_w1_veter * cos_eps_w2_veter;
            };
        }
        public double delta_pi_ot_y(double y)//функция от y 
        {
            double res = 0;
            if (m_ucl.U_norm_real == 1) res = m_ucl.P_y(y) / m_ucl.P_y(0) - pi_y;
            else res = 0;
            return res;
        }
        public double delta_tau_ot_y(double y)//функция от y
        {
            return 0;// учтено в tau_y(double y)!!!
        }
        public double a(double y)
        {
            return m_ucl.a(y);
        }
        public double Mah()//число Маха фунция y,a,Vk
        {
            return (Vk + delta_V_w_veter) / a(y);
        }
        public double q(double M)//скоростной напоp воздуха, функция pi_ot_y 
        {           
            return m_ucl.norm.roN0 * m_ucl.norm.aN0 * m_ucl.norm.aN0 * (pi_y + delta_pi_ot_y(y)) * M * M / 2.0;
        }
        public double f_zk()//коэффициент деривации
        {
            double h = (sn.l_cm - sn.l_gol) + 0.57 * sn.l_gol - 0.16 * sn.D;
            return sn.iz * sn.l * sn.l * sn.K_NM(M) / (sn.D * h);
        }
        public double Vpr(double y) //приведенная скорость
        {
            double y0 = b_ucl.y0;
            return Vk * Math.Cos(teta) * g_ucl.r_g(y0, y);
        }
        public double delta_tetaZemli()
        {
            if (g_ucl.U_FormaZemli==0) return 0;
            if (g_ucl.U_FormaZemli == 1) 
                return Vk * Math.Cos(teta) / (g_ucl.geo.R_geo + y);
            if (g_ucl.U_VrashZemli == 1)
                return 2 * g_ucl.geo.W_geo * Math.Cos(g_ucl.B_geo) * Math.Sin(b_ucl.a_direct - psy) + Vk * Math.Cos(teta) / (g_ucl.geo.R_geo + y);
            return 0;
        }
        public double delta_psyZemli()
        {
            if (g_ucl.U_FormaZemli == 0) return 0;
            if (g_ucl.U_VrashZemli == 1)
                return -2 * g_ucl.geo.W_geo * (Math.Sin(g_ucl.B_geo) - Math.Cos(g_ucl.B_geo) * Math.Cos(b_ucl.a_direct - psy) * Math.Tan(teta));
            return 0;
        }
        public double wx_s()
        {  
            return wx * sn.l / (M * m_ucl.norm.aN0);
        }
        public double mx(double M, double wxs)
        {
            return sn.m_x_omega * wxs;
        }
        public double C_xk(double M)
        {
            if (Cx_law == 3)
                return sn.Cx_k_a(M) + delta_Cxk_ot_eps_veter;
            else
                return sn.ix * sn.Cx_otM_et(M, Cx_law) + delta_Cxk_ot_eps_veter;
        }
        public double C_yk(double M, double wxs)
        {
            return delta_Cyk_ot_eps_veter;
        }
        public double C_zk(double M, double wxs)
        {
            return delta_Czk_ot_eps_veter;
        }

        public double f(int i, double tt, double[] X, ref int exc)
        {
            exc = 0;
            double result = 0;
            t = tt; // t = X[0]; 
            S = X[1]; x = X[2]; y = X[3]; z = X[4]; Vk = X[5]; teta = X[6]; psy = X[7]; wx = X[8]; pi_y = X[9];
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
                               Vk * Math.Cos(teta) / (g_ucl.geo.R_geo + y) + delta_tetaZemli();
                        result = f6; break;    //teta                
                    case 7: f7 = - (C_zk(M, wxs) * q_M * sn.Sm) / (sn.m * Vk * Math.Cos(teta)) + delta_psyZemli();
                        f7 += f_zk() * sn.I_x * wx * f6 / (sn.l * sn.m * Vk * Math.Cos(teta));
                        result = f7; break; //psy
                    case 8: result = - mx(M, wxs) * q_M * sn.Sm * sn.l / sn.I_x; break;//omega_x
                    case 9: result = - g_ucl.g(y) * pi_y * f3 / (m_ucl.norm.R * m_ucl.tau_y(y)); break; //pi(y)
                }
            }
            catch { /* MessageBox.Show("деление на 0"); System.Windows.Forms.Application.Exit();*/ exc = 1; }
            return result;
        }
        public int Calc_metod(int metod, ref string mes)
        {
            mes = "";
            return_code = 0;
            int erc = 0;
            try
            {
                //param
                int i, k = 1;
                t = X0_[0]; x = X0_[2]; y = X0_[3]; z = X0_[4]; teta = X0_[6]; double y_max = y;
                double[] X1 = new double[ChisloUraneniy + 1];
                double[] X2 = new double[ChisloUraneniy + 1];
                //нач.условия
                for (i = 0; i <= ChisloUraneniy; i++) X1[i] = X0_[i];
                RRR.Clear(); RRR.Add(new List<double>());
                for (i = 0; i <= ChisloUraneniy; i++)
                    RRR[0].Add(X0_[i]);
                chisloUzlovSetky = 1;
                int kon_krit = 0;
                //расчет
                int iter = 0;
                do
                {
                    iter++;
                    if (metod == 1)
                        RK_4(t, X1, ref X2, ref erc);
                    if (metod == 2)
                        RK5_6(t, X1, ref X2, ref erc);
 
                    X2[0] = X1[0] + dt;
                    for (i = 0; i <= ChisloUraneniy; i++) X1[i] = X2[i];

                    k++; t = X1[0]; x = X1[2]; y = X1[3]; z = X1[4]; Vk = X1[5]; teta = X1[6];
                    if (y_max < y) y_max = y;
                    RRR.Add(new List<double>());
                    for (i = 0; i <= ChisloUraneniy; i++)
                        RRR[k - 1].Add(X1[i]);
                    chisloUzlovSetky = chisloUzlovSetky + 1;
                    //konech_v_kriterii
                    if (y < -1000) { return_code = 2; break; }//не сработало условие выхода!!!!
                    if (erc > 0) { return_code = 3; break; }//ошибка вычислений!!!!
                    if (Vk < 0) { return_code = 3; break; }//ошибка
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
        public int konech_kriteri(double t, double x, double y, double z, double Vk, double teta)
        {
            int kon_krit = -1;
            if ((teta < 0) && (y <= b_ucl.yk)) return 3;
            if ((b_ucl.tk > 0) && (t >= b_ucl.tk)) return 0;
            if ((b_ucl.xk > 0) && (x >= b_ucl.xk)) return 2;
            return kon_krit;
        }
        public void RK_4(double t, double[] xk, ref double[] xk1, ref int erc) //метод РК 4-го порядка
        {
            int n = ChisloUraneniy;
            double h = dt;
            double[] z = new double[n + 1]; double[,] kf = new double[4, n + 1];
            int i = 0;
            for (i = 1; i <= n; i++) kf[0, i] = f(i, t, xk, ref erc);
            for (i = 1; i <= n; i++) z[i] = xk[i] + h * kf[0, i] / 2;
            for (i = 1; i <= n; i++) kf[1, i] = f(i, t + h / 2, z, ref erc);
            for (i = 1; i <= n; i++) z[i] = xk[i] + h * kf[1, i] / 2;
            for (i = 1; i <= n; i++) kf[2, i] = f(i, t + h / 2, z, ref erc);
            for (i = 1; i <= n; i++) z[i] = xk[i] + h * kf[2, i];
            for (i = 1; i <= n; i++) kf[3, i] = f(i, t + h, z, ref erc);
            for (i = 1; i <= n; i++) xk1[i] = xk[i] + h / 6 * (kf[0, i] + 2 * kf[1, i] + 2 * kf[2, i] + kf[3, i]);
        }
        public void RK5_6(double t, double[] xk, ref double[] xk1, ref int erc) //метод РК 6-го порядка
        {
            int n = ChisloUraneniy;
            double h = dt;
            double[] z = new double[n + 1]; double[,] kf = new double[8, n + 1]; double[] err = new double[n + 1];
            int i = 0;
            for (i = 1; i <= n; i++) kf[0, i] = f(i, t, xk, ref erc);
            for (i = 1; i <= n; i++) z[i] = xk[i] + h * kf[0, i] / 18;
            for (i = 1; i <= n; i++) kf[1, i] = f(i, t + h / 18, z, ref erc);
            for (i = 1; i <= n; i++) z[i] = xk[i] + h / 12 * (-kf[0, i] + 3 * kf[1, i]);
            for (i = 1; i <= n; i++) kf[2, i] = f(i, t + h / 6, z, ref erc);
            for (i = 1; i <= n; i++) z[i] = xk[i] + h / 81 * (-2 * kf[0, i] + 12 * kf[1, i] + 8 * kf[2, i]);
            for (i = 1; i <= n; i++) kf[3, i] = f(i, t + 2 * h / 9, z, ref erc);
            for (i = 1; i <= n; i++) z[i] = xk[i] + h / 33 * (40 * kf[0, i] - 12 * kf[1, i] - 168 * kf[2, i] + 162 * kf[3, i]);
            for (i = 1; i <= n; i++) kf[4, i] = f(i, t + 2 * h / 3, z, ref erc);
            for (i = 1; i <= n; i++) z[i] = xk[i] + h / 1752 * (-8856 * kf[0, i] + 1728 * kf[1, i] + 43040 * kf[2, i] - 36855 * kf[3, i] + 2695 * kf[4, i]);
            for (i = 1; i <= n; i++) kf[5, i] = f(i, t + h, z, ref erc);
            for (i = 1; i <= n; i++) z[i] = xk[i] + h / 891 * (-8716 * kf[0, i] + 1968 * kf[1, i] + 39520 * kf[2, i] - 33696 * kf[3, i] + 1716 * kf[4, i]);
            for (i = 1; i <= n; i++) kf[6, i] = f(i, t + 8 * h / 9, z, ref erc);
            for (i = 1; i <= n; i++) z[i] = xk[i] + h / 9984 * (117585 * kf[0, i] - 22464 * kf[1, i] - 540032 * kf[2, i] + 466830 * kf[3, i]
                                      - 14014 * kf[4, i] + 2079 * kf[6, i]);
            for (i = 1; i <= n; i++) kf[7, i] = f(i, t + h, z, ref erc);
            for (i = 1; i <= n; i++) xk1[i] = xk[i] + h / 5600 * (210 * kf[0, i] + 896 * kf[2, i] + 1215 * kf[3, i] + 2695 * kf[4, i] + 584 * kf[5, i]);
            for (i = 1; i <= n; i++) err[i] = h / 291200 * (15015 * kf[0, i] - 118272 * kf[2, i] + 115830 * kf[3, i] - 30030 * kf[4, i]
                                      - 30368 * kf[5, i] + 31185 * kf[6, i] + 16640 * kf[7, i]);
            for (i = 1; i <= n; i++) xk1[i] = xk1[i] + err[i];
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
            // y=y1+(x-x1)*(y2-y1)/(x2-x1)
            double y1 = RRR[chisloUzlovSetky - 2][kon_krit];
            double y2 = RRR[chisloUzlovSetky - 1][kon_krit];
            double yk = b_ucl.yk;
            switch (kon_krit)
            {
                case 0: yk = b_ucl.tk; break;
                case 2: yk = b_ucl.xk; break;
                case 3: yk = b_ucl.yk; break;
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
            Calc_metod(metod, ref mes);
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
            Calc_metod(metod, ref mes);
            b_ucl.yk = y_end_last;
            return true;
        }
        public int SaveTo_Csv(string f, bool podrobno, ref string mes)
        {
            mes = "";
            FileStream FS = new FileStream(f, FileMode.Create, FileAccess.Write);
            StreamWriter SW = new StreamWriter(FS);            
            int i, j;
            bool flag = true;
            int step = 1;
            try
            {
                for (i = 0; i <= 9; i++)
                    if (i != 1)
                    SW.Write(NameVar[i] + "; ");
                SW.WriteLine();
                for (j = 0; j <= chisloUzlovSetky - 1; j += step)//for (i = 0; i <= VB.ChisloUraneniy; i++) { SW.Write(VB.RRR[j - 1][i].ToString("0.######") + "  "); }; 
                {
                    SW.Write(RRR[j][0].ToString("0.000#") + "; ");//t
                    SW.Write(RRR[j][2].ToString("0.0000") + "; "); SW.Write(RRR[j][3].ToString("0.0000") + "; "); SW.Write(RRR[j][4].ToString("0.0000") + "; "); //x y z
                    SW.Write(RRR[j][5].ToString("0.0000") + "; "); SW.Write(ToF(6, RRR[j][6]).ToString("0.0000") + "; "); SW.Write(ToF(7, RRR[j][7]).ToString("0.0000") + "; "); //Vk teta psy
                    SW.Write(RRR[j][8].ToString("0.0000") + "; "); SW.Write(RRR[j][9].ToString("0.0000") + "; "); //wx pi
                    SW.WriteLine();
                    if (j == chisloUzlovSetky - 1) flag = false; 
                }
                //last row
                j = chisloUzlovSetky - 1;
                if (flag)
                {
                    SW.Write(RRR[j][0].ToString("0.000#") + "; ");//t
                    SW.Write(RRR[j][2].ToString("0.0000") + "; "); SW.Write(RRR[j][3].ToString("0.0000") + "; "); SW.Write(RRR[j][4].ToString("0.0000") + "; "); //x y z
                    SW.Write(RRR[j][5].ToString("0.0000") + "; "); SW.Write(ToF(6, RRR[j][6]).ToString("0.0000") + "; "); SW.Write(ToF(7, RRR[j][7]).ToString("0.0000") + "; "); //Vk teta psy
                    SW.Write(RRR[j][8].ToString("0.0000") + "; "); SW.Write(RRR[j][9].ToString("0.0000") + "; "); //wx pi
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
        public void zapolnitVspomogatArrays()
        {
            NameVar = new string[ChisloUraneniy + 1];
            NameVar[0] = "t, c"; NameVar[1] = "S, м"; NameVar[2] = "x, м"; NameVar[3] = "y, м"; NameVar[4] = "z, м";
            NameVar[5] = "Vk, м/с"; NameVar[6] = "teta, град"; NameVar[7] = "psy, град"; NameVar[8] = "omega_x, рад/с";
            NameVar[9] = "pi(y)";
        }

    }//VneshBall
}
