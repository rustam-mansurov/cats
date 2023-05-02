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
        /// <summary>
        /// Расчет правых частей системы
        /// </summary>
        /// <param name="t">текущее время</param>
        /// <param name="Y">вектор решения</param>
        /// <returns>правая часть</returns>
        abstract public double[] F(double t);

        public void update()
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

        public void Initialize(double [] YY)
        {
            parametrs.X = YY[0];
            parametrs.Y = YY[1];
            parametrs.Z = YY[2];
            parametrs.V = YY[3];
            parametrs.teta = YY[4];
            parametrs.psi = YY[5];
            parametrs.Omega = YY[6];
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
        public List<double[]> Test(uint N, ExternumParametrs initial_parametrs, int n)
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
            return Funk(Y[0], Y[1], Y[2], Y[3], Y[4], Y[5], Y[6], Y[7], Y[8], Y[9], Y[10], Y[11], Y[12], Y[13], Y[14]);
        }

        /// Создание вектора параметров полёта снаряда
        public double[] Funk(double uk, double e1, double psiP, double sigma, double k, double S0, double Lambda0, double z, double psi, double eta, double p_sn, double S_sn, double V, double m, double p_f)
        {
            double[] F = new double[4];
            F[0] = Inlet_solver.z(uk,e1);
            F[1] = Inlet_solver.psi(z, psiP, psi, uk, e1, sigma, k, S0, Lambda0);
            F[2] = Inlet_solver.V(m, p_sn, S_sn, eta, p_f);
            F[3] = Inlet_solver.x(V);
            return F;
        }
        public List<double[]> Test(uint N, ExternumParametrs start_parametrs, int n)
        {
            List<double[]> res = new List<double[]>();
            // Шаг по времени
            double dt = 0.05;
            // Объект метода
            Externum_ballistics task = new Externum_ballistics(N);
            // Установим начальные условия задачи
            task.SetInit(0, start_parametrs);
            while (task.Y[1] <= 1)
            {
                double[] result = new double[n];
                for (int i = 0; i < N - 1; i++)
                {
                    result[0] = task.t;
                    result[i + 1] = task.Y[i];
                }
                res.Add(result);
                NextStep(dt, task);
            }
            return res;
        }
    }
}
