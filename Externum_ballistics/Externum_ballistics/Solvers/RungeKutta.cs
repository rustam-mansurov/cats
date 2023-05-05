using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Externum_ballistics
{
    public abstract class RungeKutta
    {
        public BallisticSolver solver = new BallisticSolver();
        public InletBallisticSolver Inlet_solver = new InletBallisticSolver();
        public ExternumParametrs parametrs = new ExternumParametrs();
        public InletParametrs inletparametrs = new InletParametrs();
        /// <summary>
        /// Текущее время
        /// </summary>
        public double t;
        public bool IsThisExternumBallistic = true;
        /// <summary>
        /// Искомое решение Y[0] — само решение, Y[i] — i-я производная решения
        /// </summary>
        public double[] Y;
        /// <summary>
        /// Внутренние переменные 
        /// </summary>
        double[] YY, Y1, Y2, Y3, Y4;
        protected double[] FY;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="N">размерность системы</param>
        public RungeKutta(uint N)
        {
            Init(N);
        }
        /// <summary>
        /// Выделение памяти под рабочие массивы
        /// </summary>
        /// <param name="N">Размерность массивов</param>
          public void Init(uint N)
        {
            Y = new double[N];
            YY = new double[N];
            Y1 = new double[N];
            Y2 = new double[N];
            Y3 = new double[N];
            Y4 = new double[N];
            FY = new double[N];
        }
        /// <summary>
        /// Установка начальных условий
        /// </summary>
        /// <param name="t0">Начальное время</param>
        /// <param name="Y0">Начальное условие</param>
        public void SetInit(double t0, ExternumParametrs start_parametrs)
        {
            parametrs = start_parametrs;
            t = t0;
            Y[0] = parametrs.X;
            Y[1] = parametrs.Y;
            Y[2] = parametrs.Z;
            Y[3] = parametrs.V;
            Y[4] = parametrs.teta;
            Y[5] = parametrs.psi;
            Y[6] = parametrs.Omega;
        }

        public void SetInit(double t0, InletParametrs start_parametrs)
        {
            inletparametrs = start_parametrs;
            t = t0;
            Y[0] = inletparametrs.z;
            Y[1] = inletparametrs.psi;
            Y[2] = inletparametrs.V;
            Y[3] = inletparametrs.x;
        }

        /// <summary>
        /// Расчет правых частей системы
        /// </summary>
        /// <param name="t">текущее время</param>
        /// <param name="Y">вектор решения</param>
        /// <returns>правая часть</returns>
        abstract public double[] F(double t);

        public void update(bool IsThisExternum)
        {
            parametrs.T = solver.T(parametrs.Y);
            parametrs.p = solver.p(parametrs.Y);
            parametrs.ro = solver.ro(parametrs.p, parametrs.T);
            parametrs.q = solver.q(parametrs.ro, parametrs.V);
            parametrs.a = solver.a(parametrs.T);
            parametrs.g = solver.g(0, parametrs.Y);
            parametrs.Mah = solver.Mah(parametrs.V, parametrs.a);
            parametrs.Cx = solver.Cx(parametrs.Mah, parametrs.t_delta, parametrs.t_start, t);
            parametrs.alfa = solver.alfa(parametrs.I_x, parametrs.I_z, parametrs.Omega);
            parametrs.beta1 = solver.beta1(parametrs.mz, parametrs.ro, parametrs.Sm, parametrs.Length, parametrs.I_z, parametrs.V);
            parametrs.sigma = solver.sigma(parametrs.alfa, parametrs.beta1);
        }

        public void update()
        {
            inletparametrs.sigma = Inlet_solver.sigma(inletparametrs.lambda, inletparametrs.z, inletparametrs.mu, inletparametrs.psiP, inletparametrs.psi);
            inletparametrs.p = Inlet_solver.p(inletparametrs.W_sn, inletparametrs.alfa, inletparametrs.psi, inletparametrs.omega, inletparametrs.omegaV, inletparametrs.f, inletparametrs.m, inletparametrs.J1, inletparametrs.teta, inletparametrs.V, inletparametrs.delta);
            inletparametrs.T = Inlet_solver.T(inletparametrs.W_sn, inletparametrs.alfa, inletparametrs.psi, inletparametrs.omega, inletparametrs.omegaV, inletparametrs.delta, inletparametrs.cp, inletparametrs.cv, inletparametrs.p);
            inletparametrs.p_sn = Inlet_solver.p_sn(inletparametrs.p, inletparametrs.omega, inletparametrs.omegaV, inletparametrs.m, inletparametrs.J1, inletparametrs.J2, inletparametrs.J3, inletparametrs.V, inletparametrs.W_sn);
            inletparametrs.p_kn = Inlet_solver.p_kn(inletparametrs.p_sn, inletparametrs.omega, inletparametrs.omegaV, inletparametrs.m, inletparametrs.J2, inletparametrs.V, inletparametrs.W_sn);
            inletparametrs.eta = Inlet_solver.eta(inletparametrs.p_sn, inletparametrs.p_f);
        }

        public void Initialize(double [] YY, bool IsThis)
        {
            parametrs.X = YY[0];
            parametrs.Y = YY[1];
            parametrs.Z = YY[2];
            parametrs.V = YY[3];
            parametrs.teta = YY[4];
            parametrs.psi = YY[5];
            parametrs.Omega = YY[6];
        }

        public void Initialize(double[] YY)
        {
            inletparametrs.z = YY[0];
            inletparametrs.psi = YY[1];
            inletparametrs.V = YY[2];
            inletparametrs.x = YY[3];
        }

        public void NextStep(double dt, Externum_ballistics task)
        {
            int i;
            update();
            if (dt < 0) return;

            // рассчитать Y1
            Y1 = F(t);

            for (i = 0; i < Y.Length; i++)
                YY[i] = Y[i] + Y1[i] * (dt / 2.0);
            Initialize(YY, IsThisExternumBallistic);
            // рассчитать Y2
            Y2 = F(t + dt / 2.0);

            for (i = 0; i < Y.Length; i++)
                YY[i] = Y[i] + Y2[i] * (dt / 2.0);
            Initialize(YY, IsThisExternumBallistic);
            // рассчитать Y3
            Y3 = F(t + dt / 2.0);

            for (i = 0; i < Y.Length; i++)
                YY[i] = Y[i] + Y3[i] * dt;
            Initialize(YY, IsThisExternumBallistic);
            // рассчитать Y4
            Y4 = F(t + dt);

            // рассчитать решение на новом шаге
            for (i = 0; i < Y.Length; i++)
                Y[i] = Y[i] + dt / 6.0 * (Y1[i] + 2.0 * Y2[i] + 2.0 * Y3[i] + Y4[i]);

            // рассчитать текущее время
            t = t + dt;        
        }

        public void NextStep(double dt, Inlet_ballistics task)
        {
            int i;
            update();
            if (dt < 0) return;

            // рассчитать Y1
            Y1 = F(t);

            for (i = 0; i < Y.Length; i++)
                YY[i] = Y[i] + Y1[i] * (dt / 2.0);
            Initialize(YY);
            // рассчитать Y2
            Y2 = F(t + dt / 2.0);

            for (i = 0; i < Y.Length; i++)
                YY[i] = Y[i] + Y2[i] * (dt / 2.0);
            Initialize(YY);
            // рассчитать Y3
            Y3 = F(t + dt / 2.0);

            for (i = 0; i < Y.Length; i++)
                YY[i] = Y[i] + Y3[i] * dt;
            Initialize(YY);
            // рассчитать Y4
            Y4 = F(t + dt);

            // рассчитать решение на новом шаге
            for (i = 0; i < Y.Length; i++)
                Y[i] = Y[i] + dt / 6.0 * (Y1[i] + 2.0 * Y2[i] + 2.0 * Y3[i] + Y4[i]);

            // рассчитать текущее время
            t = t + dt;
        }
    }


    public class Externum_ballistics : RungeKutta
    {
        public Externum_ballistics(uint N) : base(N) { }

        /// <summary>
        /// Расчёт правых частей
        /// </summary>
        /// <param name="t"> Время</param>
        /// <param name="Y"> Вектор параметров</param>
        /// <returns></returns>
        public override double[] F(double t)
        {
            double[] F = new double[8];
            F[0] = solver.X(parametrs.V, parametrs.teta, parametrs.psi);
            F[1] = solver.Y(parametrs.V, parametrs.teta);
            F[2] = solver.Z(parametrs.V, parametrs.teta, parametrs.psi);
            F[3] = solver.V(parametrs.g, parametrs.teta, parametrs.Cx, parametrs.q, parametrs.Sm, parametrs.m, parametrs.P);
            F[4] = solver.teta(parametrs.g, parametrs.teta, parametrs.V, parametrs.Cy, parametrs.q, parametrs.Sm, parametrs.m);
            F[5] = solver.psi(parametrs.Cz, parametrs.q, parametrs.Sm, parametrs.m, parametrs.V, parametrs.teta);
            F[6] = solver.omega(parametrs.mx_wx, parametrs.q, parametrs.Sm, parametrs.Length, parametrs.I_x, parametrs.Mpx);
            return F;
        }
        public List<double[]> CalcExternum(uint N, ExternumParametrs initial_parametrs, int n)
        {
            List<double[]> res = new List<double[]>();
            // Шаг по времени
            double dt = 0.05;
            // Объект метода
            Externum_ballistics task = new Externum_ballistics(N);
            // Установим начальные условия задачи
            SetInit(0, initial_parametrs);
            // решаем до того как высота снаряда не станет меньше нуля
            while (parametrs.Y >= 0 )
            {
                double[] result = new double[n];
                if (t >= parametrs.t_start && t <= parametrs.t_start + parametrs.t_delta)
                {
                    parametrs.P = 11560 / 3;
                    parametrs.Mpx = solver.Mpx(parametrs.Psigma, parametrs.nu, parametrs.re, parametrs.beta);
                }
                else
                {
                    parametrs.P = 0;
                    parametrs.Mpx = 0;
                }
                for (int i = 0; i <= N-1; i++)
                {
                    result[0] = t;
                    result[i+1] = Y[i];
                }
                result[N] = parametrs.sigma;
                res.Add(result);
                NextStep(dt, task);
            }
            return res;
        }
    }

    public class Inlet_ballistics : RungeKutta
    {
        public Inlet_ballistics(uint N) : base(N) { }  
        /// <summary>
        /// Расчёт правых частей
        /// </summary>
        /// <param name="t"> Время</param>
        /// <param name="Y"> Вектор параметров</param>
        /// <returns></returns>
        public override double[] F(double t)
        {
            double[] F = new double[4];
            F[0] = Inlet_solver.z(inletparametrs.uk, inletparametrs.e1, inletparametrs.p);
            F[1] = Inlet_solver.psi(inletparametrs.z, inletparametrs.psiP, inletparametrs.psi, inletparametrs.uk, inletparametrs.e1, inletparametrs.sigma, inletparametrs.k, inletparametrs.S0, inletparametrs.Lambda0, inletparametrs.p);
            F[2] = Inlet_solver.V(inletparametrs.m, inletparametrs.p_sn, inletparametrs.S_kn, inletparametrs.eta, inletparametrs.p_f);
            F[3] = Inlet_solver.x(inletparametrs.V);
            return F;
        }
        public List<double[]> CalcInlet(uint N, InletParametrs start_parametrs, int n)
        {
            List<double[]> res = new List<double[]>();
            // Шаг по времени
            double dt = 10e-6;
            // Объект метода
            Inlet_ballistics task = new Inlet_ballistics(N);
            // Установим начальные условия задачи
            SetInit(0, start_parametrs);
            //Y[3] <= 6322
            int iter = 0;
            while (iter <= 10000)
            {
                iter++;
                int i = 0;
                double[] result = new double[n+10];

                result[0] = t;
                result[1] = Y[0];
                result[2] = Y[1];
                result[3] = Y[2];
                result[4] = Y[3];
                result[5] = inletparametrs.sigma;
                result[6] = inletparametrs.p;
                result[7] = inletparametrs.p_sn;
                result[8] = inletparametrs.p_kn;
                result[9] = inletparametrs.eta;
                result[10] = inletparametrs.psiP;
                result[11] = inletparametrs.T;
                result[12] = inletparametrs.W_km;
                result[13] = inletparametrs.W_sn;
                result[14] = inletparametrs.teta;

                res.Add(result);
                NextStep(dt,task);
            }
            return res;
        }
    }
}
