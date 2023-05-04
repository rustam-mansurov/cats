using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Externum_ballistics
{
    public class InletParametrs
    {
        InletBallisticSolver solver = new InletBallisticSolver(); 
        #region Начальные условия
        [Category("Начальные условия"), DescriptionAttribute("Масса пороха"), DisplayName("omega, кг")]
        public double omega { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Число каналов в пороховом элементе"), DisplayName("n")]
        public double n { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Внешний диаметр порохового элемента"), DisplayName("D0, м")]
        public double D0 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Диаметр канала порохового элемента"), DisplayName("d0, м")]
        public double d0 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Толщина горящего свода"), DisplayName("e1, м")]
        public double e1 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Теплоемкость пороха"), DisplayName("c_poroh, Дж/кг")]
        public double c_poroh { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Масса воспламенителя"), DisplayName("omegaV, кг")]
        public double omegaV { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Плотность пороха"), DisplayName("delta, кг/м^3")]
        public double delta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Масса снаряда"), DisplayName("m, кг")]
        public double m { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double S { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double Lambda0 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double P { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double Q { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double beta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double lambda { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double uk { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double mu { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double J1 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double J2 { get; set; }
        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double J3 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double[] l_n { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double l_k { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double L_k { get; set; }
        #endregion

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double k { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double z { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double psiP { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double psi { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double teta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double p { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double T { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double p_kn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double p_sn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double W_sn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double W_km { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double eta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double V { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double sigma { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double S_sn { get; set; }
        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double S0 { get; set; }
        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double p_f { get; set; }
        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double x { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double L0 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double S_km { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double[] d_km { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double S_kn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double d_kn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double alfa { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double f { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double cv { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double cp { get; set; }


        public InletParametrs Get_Initial_Conditions(int N, InletParametrs parametrs)// Получить начальные параметры
        {
            parametrs.psi = 0;
            parametrs.z = 0;
            parametrs.V = 0;
            parametrs.x = 0;
            parametrs.f = 900000;
            parametrs.d0 = 0.0009;
            parametrs.D0 = 0.0115;
            parametrs.L0 = 0.019;
            parametrs.d_km[0] = 0.214;
            parametrs.d_km[1] = 0.196;
            parametrs.d_km[2] = 0.164;
            parametrs.d_km[3] = 0.155;
            parametrs.d_km[4] = 0.155;
            parametrs.d_km[5] = 0.1524;
            parametrs.d_kn = 0.1524;
            parametrs.l_n[0] = 0;
            parametrs.l_n[1] = 0.85;
            parametrs.l_n[2] = 0.960;
            parametrs.l_n[3] = 1.015;
            parametrs.l_n[4] = 1.045;
            parametrs.l_n[5] = 1.1225;
            parametrs.S_kn = solver.S(d_kn);
            parametrs.L_k = 1.015;
            parametrs.S_sn = solver.S(d_km[5]);
            parametrs.Lambda0 = solver.Lambda0(parametrs.D0, parametrs.d0, parametrs.L0);
            parametrs.Q = solver.Q(parametrs.D0, parametrs.d0, parametrs.L0);
            parametrs.e1 = 0.0011;
            parametrs.alfa = 0.00095;
            parametrs.cv = 1497;
            parametrs.cp = 1838;
            parametrs.omega = 19;
            parametrs.omegaV = 0.810;
            parametrs.teta = solver.teta(parametrs.cv, parametrs.cp);
            parametrs.m = 46;
            parametrs.delta = 1520;
            parametrs.J1 = solver.J1();
            parametrs.J2 = solver.J2();
            parametrs.J3 = solver.J3();
            parametrs.beta = solver.beta(parametrs.e1, parametrs.L0);
            parametrs.P = solver.P(parametrs.D0, parametrs.d0, parametrs.L0);
            parametrs.k = solver.kappa(parametrs.P, parametrs.Q, parametrs.beta);
            parametrs.lambda = solver.lambda(parametrs.P, parametrs.Q, parametrs.beta);
            parametrs.mu = solver.mu(parametrs.P, parametrs.Q, parametrs.beta);
            parametrs.psiP = solver.psiP(parametrs.k, parametrs.lambda, parametrs.mu);
            parametrs.W_km = solver.W_km(parametrs.l_n, parametrs.S_kn, parametrs.L_k, parametrs.d_km);
            parametrs.W_sn = solver.W_sn(parametrs.W_km, parametrs.S_sn, parametrs.x, parametrs.L_k);
            parametrs.sigma = solver.sigma(parametrs.lambda, parametrs.z, parametrs.mu, parametrs.psiP, parametrs.psi);
            parametrs.p = solver.p(parametrs.W_sn, parametrs.alfa, parametrs.psi, parametrs.omega, parametrs.omegaV, parametrs.f, parametrs.m, parametrs.J1, parametrs.teta, parametrs.V, parametrs.delta);
            parametrs.T = solver.T(parametrs.W_sn, parametrs.alfa, parametrs.psi, parametrs.omega, parametrs.omegaV, parametrs.delta, parametrs.cp, parametrs.cv, parametrs.p);
            parametrs.p_sn = 
            parametrs.p_kn = 
            parametrs.S0 = solver.S(parametrs.d0);

            return parametrs;
        }
    }
}
