using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Externum_ballistics
{
    public class BallisticSolver
    {
        double R = 287;// Удельная газовая постоянная
        double a0 = 340.7;// Начальная скорость звука
        double T0 = 288.9;// Начальная температура   
        double ro = 1800;// Плотность топлива
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
            return (180 / Math.PI) * (-g * Math.Cos(teta * Math.PI / 180) / V) - (0 * q * Sm) / (m * V);
        }

        public double psi(double Cz, double q, double Sm, double m, double V, double teta)// Угол направления
        {
            return -(0 * Cz * q * Sm) / (m * V * Math.Cos(teta * Math.PI / 180));
        }

        public double omega(double mx, double q, double Sm, double l, double Ix)// Аксиальная угловая скорость
        {
            return -mx * q * Sm * l / Ix;
        }
        #endregion

        #region Реактивный двигатель

        public double Sv(double dv)// Площадь выходного сечения сопла
        {
            return Math.PI*dv*dv/4;
        }

        public double Psigma(double G, double u, double Sv, double pv, double pn)// Общая реактивная тяга двигателя
        {
            return G * u + Sv * (pv - pn);
        }

        public double P(double Psigma, double nu, double beta)// Тяга с учетом вращения
        {
            return Psigma*((1-nu)+ nu*Math.Cos(beta*Math.PI/180));
        }

        public double Mpx(double Psigma, double nu, double re, double beta)// Тяга с учетом вращения
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

        public double u(double u1, double pk)// Скорость горения
        {
            return u1 * pk;
        }

        public double pk(double pT, double u1, double Sg, double Hi, double R, double Tk, double fc, double Skr, double v)// Давление в камере сгорания (Формула Бори)
        {
            return Math.Pow((pT * u1 * Sg * Math.Sqrt(0.98 * R * Tk)) / (0.98 * Skr * A1), 1 / (1 - v));
        }

        public double G(double Skr, double ptk, double A, double R, double Tk)// Расход продуктов горения через сопло
        {
            return (Skr * ptk * A) / (Math.Sqrt(R * Tk));
        }

        public double A(double k)// Расход продуктов горения через сопло
        {
            return Math.Sqrt(k*Math.Pow(2/(k+1),(k+1)/(k-1)));
        }

        public double uv (double akr, double lambda)// Скорость газов в выходном сечении
        {
            return akr * lambda;
        }

        public double akr (double k, double R, double T)// Скорость звука в критическом сечении
        {
            return Math.Sqrt(2 * k / (k + 1) * R * T);
        }

        public double pv (double pk, double k, double lambda)
        {
            return pk * Math.Pow((1 - (k - 1) / (k + 1)) * lambda * lambda, k / (k - 1));
        }
        #endregion

        #region Линейные уравнения характеристик снаряда

        public double Mah (double V, double a)// Число Маха
        {
            return V / a;
        }

        public double Cx (double M)
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

            else if (M > 1.2 && M <= 5)
            {
                a[0] = 0.7088;
                a[1] = -0.2797;
                a[2] = 0.0512;
                a[3] = -0.0035;
            }

            Res = a[0] + a[1] * M + a[2] * Math.Pow(M, 2) + a[3] * Math.Pow(M, 3);

            return Res;
        }

        public double q(double p, double Mah, double a, double T)// Скоростной напор в воздухе
        {
            return p * Mah * Mah * a * a / (2*R*T);
        }

        public double a(double T)// Скорость звука
        {
            return a0 * Math.Sqrt(T / T0);
        }

        public double T(double height)// Температура на высоте h
        {
            return 5e-8 * height * height - 0.00682858 * height + 288.72637363;
        }

        public double p(double height)// Давление на высоте h
        {
            return -1e-8 * height * height * height + 0.00055417 * height * height - 11.96119603 * height + 101310.54945055;
        }

        public double g(double phi, double h)
        {
            return 9.780318 * (1 + 0.005302 * Math.Sin(phi * Math.PI / 180) - 0.000006 * Math.Sin(2 * phi * Math.PI / 180) * Math.Sin(2 * phi * Math.PI / 180)) - 0.000003086 * h;
        }

        public double Sm(double d)// Площадь миделева сечения снаряда
        {
            return Math.PI * d * d / 4;
        }
        #endregion
    }
}
