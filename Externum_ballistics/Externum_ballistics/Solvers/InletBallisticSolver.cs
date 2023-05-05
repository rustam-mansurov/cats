using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Externum_ballistics
{
    public class InletBallisticSolver
    {

        double omega = 19; // Масса пороха
        double omegaV = 0.81;// масса воспламенителя
        double delta = 1520;// Плотность материала пороха
        double m = 40;// Масса снаряда
        double uk = 5.9e-10;// Единичная скорость горения пороха в канале ствола

        #region Дифференциальные уравнения
        public double psi(double z, double psiP, double psi, double uk, double e1, double sigma, double k, double S0, double Lambda0, double p) // Относительная доля сгоревшего пороха
        {
            double res = 0;
            if (z <= 1 || psi <= psiP)// До фазы распада пороховых элементов
            {
                res = (k / e1) * sigma * uk*p;
                return res;
            }

            else // После фазы распада пороховых элементов 
            {
                res = S0 / Lambda0 * sigma * uk * p;
                return res;
            }
        }

        public double z(double uk, double e1, double p)// Относительная толщина горящего свода
        {
            double res = (uk * p / e1);
            return res;
        }

        public double V(double m, double p_sn, double S, double eta, double p_f)// Уравнение дульной скорости снаряда
        {
            return (p_sn * S * eta * (p_sn - p_f))/m;
        }

        public double x (double V)// Уравнение движения снаряда
        {
            return V;
        }

        #endregion
        #region Константы
        public double S(double d)// Площадь сечений
        {
            return Math.PI * (d * d) / 4;
        }

        public double Lambda0(double D0, double d0, double L0)// Площадь сечений
        {
            return Math.PI / 4 * (D0 * D0 - 7 * d0 * d0) * L0;
        }

        public double P(double D0, double d0, double L0)// Площадь сечений
        {
            return (D0 + 7 * d0) / L0;
        }

        public double Q(double D0, double d0, double L0)// Площадь сечений
        {
            return (D0 * D0 - 7 * d0 * d0) / (L0 * L0);
        }

        public double beta(double e1, double L0)// Площадь сечений
        {
            return (2 * e1) / L0;
        }

        public double kappa(double P, double Q, double beta)// Площадь сечений
        {
            return (Q + 2 * P) / Q * beta;
        }

        public double lambda(double P, double Q, double beta)// Площадь сечений
        {
            return (2 * (3 - P)) / (Q + 2 * P) * beta;
        }

        public double mu(double P, double Q, double beta)// Площадь сечений
        {
            return (-6 * beta * beta) / (Q + 2 * P);
        }

        public double J1()
        {
            return 1 / 3;
        }
        public double J2()
        {
            return 1 / 2;
        }
        public double J3()
        {
            return 1 / 6;
        }
        #endregion
        #region Линейные уравнения

        public double psiP(double k, double lambda, double mu)// Относительная доля сгоревшего пороха P
        {
            return k * (1 + lambda + mu);
        }

        public double sigma(double lambda, double z, double mu, double psiP, double psi) // Уравнение горения
        {
            if (z <= 1 || psi <= psiP)// До фазы распада пороховых элементов
            {
                return 1 + 2 * lambda * z + 3 * mu * z * z;
            }

            else // После фазы распада пороховых элементов 
            {
                return 1 + 2 * lambda + 3 * mu * Math.Sqrt((1 - psi) / (1 - psiP));
            }
        }
        public double p(double W, double alfa, double psi, double omega, double omegaV, double f, double m, double J1, double teta, double V, double delta)// Уравнение энергии (Среднее давление в стволе)
        {
            return ((omega * psi + omegaV) * f - (1 + (omega + omegaV) / m * J1) * teta * m * V / 2) / (W - omega / delta * (1 - psi) - alfa * (omega * psi + omegaV));
        }
        public double T(double W, double alfa, double psi, double omega, double omegaV, double delta, double cp, double cv, double p)// Уравнение состояния (Определение температуры)
        {
            return p*(W - omega / delta * (1 - psi) - alfa * (omega * psi + omegaV))/ ((omega * psi + omegaV)*(cp-cv));
        }

        public double p_kn(double p_sn, double omega, double omega_v, double m, double J2, double V, double W)// Давление на дно канала
        {
            return p_sn*(1+(omega+omega_v)/m*J2)+(omega + omega_v)*V*V/W*(1/2-J2);
        }

        public double p_sn(double p, double omega, double omega_v, double m, double J1, double J2, double J3, double V, double W)// Давление на дно снаряда
        {
            return (p+(omega+omega_v)*V*V/W*(1/2*J1+J2-J3-1/2))/(1+(omega+omega_v)/m*(J2-J3));
        }

        public double W_sn (double W_km, double S_sn, double x, double L)// Объём заснарядного пространства
        {
            return W_km + S_sn*(x - L);
        }

        public double W_km (double[] l_n, double S_kn, double L_k, double [] d_km)// Объём каморы
        {
            double sum = 0;
            for (int i = 1; i < l_n.Length; i++)
            {
                sum += S(d_km[i-1]) * l_n[i-1] + 1 / 3 * (l_n[i-1] - l_n[i]) * (S(d_km[i-1]) + Math.Sqrt(S(d_km[i-1]) + S_kn) + S_kn) + S_kn * (L_k - l_n[i]);
            }
            return sum;
        }

        
        public double eta (double p_sn, double p_f)// Функция Хевисайда
        {
            if ((p_sn - p_f) <= 0)
            {
                return 0;
            }

            else
            {
                return 1;
            }
        }

        public double teta(double cv, double cp)
        {
            return (cp-cv)/cv;
        }
        #endregion 
    }
}
