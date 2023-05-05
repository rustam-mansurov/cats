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

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("S, м^2")]
        public double S { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("Lambda0")]
        public double Lambda0 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("P")]
        public double P { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("Q")]
        public double Q { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("beta")]
        public double beta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("lambda")]
        public double lambda { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk")]
        public double uk { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("mu")]
        public double mu { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("J1")]
        public double J1 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("J2")]
        public double J2 { get; set; }
        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("J3")]
        public double J3 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("l_n")]
        public double[] l_n { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("L_k")]
        public double L_k { get; set; }
        #endregion

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k")]
        public double k { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("z")]
        public double z { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("psiP")]
        public double psiP { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("psi")]
        public double psi { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("teat")]
        public double teta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("p")]
        public double p { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("T")]
        public double T { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("p_kn")]
        public double p_kn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("p_sn")]
        public double p_sn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("W_sn")]
        public double W_sn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("W_km")]
        public double W_km { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("eta")]
        public double eta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("V")]
        public double V { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("sigma")]
        public double sigma { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("S_sn")]
        public double S_sn { get; set; }
        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("S0")]
        public double S0 { get; set; }
        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("p_f")]
        public double p_f { get; set; }
        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("x")]
        public double x { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("L0")]
        public double L0 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("S_km")]
        public double S_km { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("d_km")]
        public double[] d_km { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("S_kn")]
        public double S_kn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("d_kn")]
        public double d_kn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("alfa")]
        public double alfa { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("f")]
        public double f { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("cv")]
        public double cv { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("cp")]
        public double cp { get; set; }

        public InletParametrs Get_Initial_Conditions(InletParametrs parametrs)// Получить начальные параметры
        {
            double[] d_m = { 0.214, 0.196, 0.164, 0.155, 0.155, 0.1524 };
            double[] l_m = { 0, 0.85, 0.960, 1.015, 1.045, 1.1225 };
            parametrs.psi = 0;
            parametrs.z = 0;
            parametrs.V = 0;
            parametrs.x = 0;
            parametrs.f = 900000;
            parametrs.d0 = 0.0009;
            parametrs.D0 = 0.0115;
            parametrs.L0 = 0.019;
            parametrs.d_km = d_m;
            parametrs.d_kn = 0.1524;
            parametrs.c_poroh = 1298;
            parametrs.l_n = l_m;
            parametrs.S_kn = solver.S(d_kn);
            parametrs.L_k = 1.015;
            parametrs.S_sn = solver.S(d_km[5]);
            parametrs.Lambda0 = solver.Lambda0(parametrs.D0, parametrs.d0, parametrs.L0);
            parametrs.Q = solver.Q(parametrs.D0, parametrs.d0, parametrs.L0);
            parametrs.e1 = 0.0009;
            parametrs.alfa = 0.00095;
            parametrs.cv = 1497;
            parametrs.cp = 1838;
            parametrs.omega = 19;
            parametrs.omegaV = 0.810;
            parametrs.teta = solver.teta(parametrs.cv, parametrs.cp);
            parametrs.m = 46;
            parametrs.S_sn = solver.S(0.152);
            parametrs.delta = 1520;
            parametrs.J1 = 1/3f;
            parametrs.J2 = 1/2f;
            parametrs.uk = 5.9e-10;
            parametrs.p_f = 25000000;
            parametrs.eta = 0;
            parametrs.J3 = 1/6f;
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
            parametrs.p_sn = solver.p_sn(parametrs.p, parametrs.omega, parametrs.omegaV, parametrs.m, parametrs.J1, parametrs.J2, parametrs.J3, parametrs.V, parametrs.W_sn);
            parametrs.p_kn = solver.p_kn(parametrs.p_sn, parametrs.omega, parametrs.omegaV, parametrs.m, parametrs.J2, parametrs.V, parametrs.W_sn);
            parametrs.S0 = solver.S(parametrs.d0);
            return parametrs;
        }
    }
}
