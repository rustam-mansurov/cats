using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Externum_ballistics
{
    public class BallisticSolver
    {
        double a0 = 340.7;// Начальная скорость звука
        double T0 = 288.9;// Начальная температура   
        double A1 = 0.6523864;// Коэффициент для формулы Бори

        #region Дифференциальные уравнения
        public double X(double V, double teta, double psi)// Дальность в плоскости стрельбы
        {
            return V * Math.Cos(teta * Math.PI / 180) * Math.Cos(psi * Math.PI / 180);
        }

        public double Y(double V, double teta)// Высота полёта снаряда   
        {
            return V * Math.Sin(teta * Math.PI / 180);
        }

        public double Z(double V, double teta, double psi)// Боковое отклонение
        {
            return -V * Math.Cos(teta * Math.PI / 180) * Math.Sin(psi * Math.PI / 180);
        }

        public double V(double g, double teta, double Cx, double q, double Sm, double m, double P)// Скорость центра масс снаряда
        {
            return -g * Math.Sin(teta * Math.PI / 180) + (P - Cx * q * Sm) / m;
        }

        public double teta(double g, double teta, double V, double Cy, double q, double Sm, double m)// Угол наклона траектории
        {
            return (180 / Math.PI) * -(g * Math.Cos(teta * Math.PI / 180) / V) - (Cy * q * Sm) / (m * V);
        }

        public double psi(double Cz, double q, double Sm, double m, double V, double teta)// Угол направления
        {
            return -(0 * q * Sm) / (m * V * Math.Cos(teta * Math.PI / 180));
        }

        public double omega(double mx, double q, double Sm, double l, double Ix, double delta_omega)// Аксиальная угловая скорость
        {
            return -mx * q * Sm * l / Ix + delta_omega;
        }

        public double delta_omega(double Mpx, double P, double d, double Ix, double t_start, double t_delta, double t)// Аксиальная угловая скорость
        {
            if (t >= t_start && t <= t_start+t_delta)
            {
                return ((Mpx * P * d) / 2) / Ix;          
            }

            else 
            { 
                return 0; 
            }
        }
        #endregion

        #region Реактивный двигатель

        public double Sv(double dv)// Площадь выходного сечения сопла
        {
            return Math.PI*dv*dv/4;
        }

        public double Psigma(double G, double uv, double Sv, double pv, double pn)// Общая реактивная тяга двигателя
        {
            double P = 0;
            P = G * uv + Sv * (pv - pn);
            return P;
        }

        public double P(double Psigma, double nu, double beta)// Тяга с учетом вращения
        {
            return Psigma*((1-nu)+ nu*Math.Cos(beta*Math.PI/180));
        }

        public double Mpx(double Psigma, double nu, double re, double beta)// Момент вращения
        {
            return Psigma*nu*re*Math.Sin(beta*Math.PI/180);
        }

        public double re(double dv)// Радиус расположения ребер сопла
        {
            return dv/2;
        }

        public double It(double P, double t)// Суммарный импульс
        {
            return P*t;
        }

        public double u(double u1, double pk, double nu)// Скорость горения
        {
            double u = 0;
            u = u1 * pk * nu;
            return Math.Round(u,2);
        }

        public double pk(double u1, double Sg, double Hi, double R, double Tk, double fc, double Skr, double v)// Давление в камере сгорания (Формула Бори)
        {
            double pk = 0;
            pk = Math.Pow((1600 * u1 * Sg * Math.Sqrt(0.98 * R * Tk)) / (0.98 * Skr * A1), 1 / (1 - v));
            return Math.Round(pk,2);
        }

        public double G(double Skr, double pk, double A, double R, double Tk)// Расход продуктов горения через сопло
        {
            return (Skr * pk * A) / (Math.Sqrt(R * Tk));
        }

        public double A(double k)// Коэффициент для формулы Бори
        {
            return Math.Sqrt(k*Math.Pow(2/(k+1),(k+1)/(k-1)));
        }

        public double beta1(double mz, double ro, double Sm, double l, double Iy, double V)// коэффициент аэродинамического момента
        {
            return (mz * ro * V * V / 2 * Sm * l) / Iy;
        }

        public double sigma(double alfa, double beta1)// Критерий устойчивости
        {
            return (1 - beta1 / (alfa*alfa));
        }

        public double alfa(double Ix, double Iy, double omega)// Коэффициент гироскопического момента
        {
            return Ix / (2 * Iy) * omega;
        }

        public double uv (double akr, double lambda)// Скорость газов в выходном сечении
        {
            return Math.Round(akr * lambda,2);
        }

        public double akr (double k, double R, double T)// Скорость звука в критическом сечении
        {
            return Math.Round(Math.Sqrt(2 * k / (k + 1) * R * T),2);
        }

        public double pv (double pk, double k, double lambda)
        {
            double pv = 0;
            pv = pk * Math.Pow((1 - (k - 1) / (k + 1) * lambda * lambda), (1 / (k - 1)));
            return Math.Round(pv,2);
        }

        public double m(double G, double t_start, double t)
        {
            return -G ;
        }
        #endregion

        #region Линейные уравнения характеристик снаряда

        public double Mah (double V, double a)// Число Маха
        {
            return Math.Round(V / a,2);
        }

        public double Cx (double M, double t_delta, double t_start,  double t)
        {
            double [] a = new double[4];
            double Res = 0;
           
            if(M > 0 && M <= 0.8)
            {
                a[0] = 0.1860;
                a[1] = 0;
                a[2] = 0;
                a[3] = 0;
            }

            else if(M > 0.8 && M <= 1)
            {
                a[0] = -0.7794;
                a[1] = 4.7477;
                a[2] = -7.6523;
                a[3] = 4.038;
            }

            else if (M > 1 && M <= 1.2)
            {
                a[0] = -17.441;
                a[1] = 44.811;
                a[2] = -37.284;
                a[3] = 10.298;
            }

            else if (M > 1.2)
            {
                a[0] = 0.7088;
                a[1] = -0.2797;
                a[2] = 0.0512;
                a[3] = -0.0035;
            }

            Res = a[0] + a[1] * M + a[2] * Math.Pow(M, 2) + a[3] * Math.Pow(M, 3);
            return Math.Round(Res,2);
        }
        public double q(double ro, double V)// Скоростной напор в воздухе
        {
            return Math.Round(ro*V*V/2,2);
        }
        public double ro(double p, double T)// Плотность воздуха
        {
            double M = 29;
            double R = 8.31;
            return Math.Round((p*M)/(R*T),2);
        }

        public double a(double T)// Скорость звука
        {
            return Math.Round(a0 * Math.Sqrt(T / T0),2);
        }

        public double T(double height)// Температура на высоте h
        {
            return Math.Round(5e-8 * height * height - 0.00682858 * height + 288.72637363,2);
        }

        public double p(double height)// Давление на высоте h
        {
            return Math.Round((-1e-8 * height * height * height + 0.00055417 * height * height - 11.96119603 * height + 101310.54945055) / 10e+2, 2);
        }

        public double g(double phi, double h)
        {
            return Math.Round(9.780318 * (1 + 0.005302 * Math.Sin(phi * Math.PI / 180) - 0.000006 * Math.Sin(2 * phi * Math.PI / 180) * Math.Sin(2 * phi * Math.PI / 180)) - 0.000003086 * h,2);
        }

        public double Sm(double d)// Площадь миделева сечения снаряда
        {
            return Math.Round(Math.PI * d * d / 4,2);
        }

        public double mz(double M, double omega)
        {
            double[] b = new double[3];
            b[0] = 0.000617;
            b[1] = -0.00022;
            b[2] = 2.92e-5;
            return (b[0] + b[1]*M + b[2] * M * M) * omega;
        }
        #endregion
    }
}
