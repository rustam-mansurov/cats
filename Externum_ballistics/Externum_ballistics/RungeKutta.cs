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
        /// <summary>
        /// Текущее время
        /// </summary>
        public double t;
        /// <summary>
        /// Искомое решение Y[0] — само решение, Y[i] — i-я производная решения
        /// 0 - x, 1 - y, 2 - z, 3 - V, 4 - teta, 5 - psi, 6 - omega, 7 - q, 8 - a, 9 - T, 10 - ro, 
        /// 11 - g, 12 - Sm, 13 - Mah, 14 - m, 15 - Cx, 16 - Cy, 17 - Cz, 18 - d, 19 - l,
        /// 20 - Ix, 21 - mx, 22 - P, 23 - t_delta, 24 - t1, 25 - alfa, 26 - beta, 27 - sigma, 28 - Iz, 29 - Mza, 30 - p
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
        public void SetInit(double t0, double[] Y0)
        {
            t = t0;
            for (int i = 0; i < Y.Length; i++)
            {
                Y[i] = Y0[i];
            }
        }
        /// <summary>
        /// Расчет правых частей системы
        /// </summary>
        /// <param name="t">текущее время</param>
        /// <param name="Y">вектор решения</param>
        /// <returns>правая часть</returns>
        abstract public double[] F(double t, double[] Y);
    
        public void update (double ro, double V, double T, double h, double g, double a, double M, double Ix, double Iy, double omega, double mz, double q, double Sm, double l, double beta, double alfa, double p)
        {
            Y[7] = solver.q(ro, V);
            Y[8] = solver.a(T);
            Y[9] = solver.T(h);
            Y[30] = solver.p(h);
            Y[10] = solver.ro(Y[30],T);
            Y[11] = solver.g(0, h);
            Y[13] = solver.Mah(V, a);
            Y[15] = solver.Cx(M);
            Y[25] = solver.alfa(0.1455, 1.4417, omega);
            Y[26] = solver.beta1(0.8918, ro, Sm, l, 1.4417, V);
            Y[27] = solver.sigma(beta, alfa);
        }
        
        public void NextStep(double dt)
        {
            int i;
            update(Y[10], Y[3], Y[9], Y[1], Y[11], Y[8], Y[13], Y[20], Y[28], Y[6], Y[29], Y[7], Y[12], Y[19], Y[26], Y[25], Y[30]);

            if (dt < 0) return;

            // рассчитать Y1
            Y1 = F(t, Y);

            for (i = 0; i < Y.Length; i++)
                YY[i] = Y[i] + Y1[i] * (dt / 2.0);

            // рассчитать Y2
            Y2 = F(t + dt / 2.0, YY);

            for (i = 0; i < Y.Length; i++)
                YY[i] = Y[i] + Y2[i] * (dt / 2.0);  

            // рассчитать Y3
            Y3 = F(t + dt / 2.0, YY);

            for (i = 0; i < Y.Length; i++)
                YY[i] = Y[i] + Y3[i] * dt;

            // рассчитать Y4
            Y4 = F(t + dt, YY);

            // рассчитать решение на новом шаге
            for (i = 0; i < Y.Length; i++)
                Y[i] = Y[i] + dt / 6.0 * (Y1[i] + 2.0 * Y2[i] + 2.0 * Y3[i] + Y4[i]);

            // рассчитать текущее время
            t = t + dt;        
        }
    }
    public class TMyRK : RungeKutta
    {
        public TMyRK(uint N) : base(N) { }

        /// <summary>
        /// Расчёт правых частей
        /// </summary>
        /// <param name="t"> Время</param>
        /// <param name="Y"> Вектор параметров</param>
        /// <returns></returns>
        public override double[] F(double t, double[] Y)
        {
            return Funk(Y[0],Y[1],Y[2], Y[3], Y[4], Y[5], Y[6], Y[7], Y[8], Y[9], Y[10], Y[11], Y[12], Y[13], Y[14], Y[15], Y[16], Y[17], Y[18], Y[19], Y[20], Y[21], Y[22], Y[23], Y[24], Y[25], Y[26], Y[27], Y[28], Y[29], Y[30]);
        }

        /// Создание вектора параметров полёта снаряда
        public double[] Funk(double X, double Y, double Z, double V, double teta, double psi, double omega, double q, double a, double T, double ro, double g, double Sm, double Mah, double m, double Cx, double Cy, double Cz, double d, double l, double Ix, double mx, double P, double t_delta, double t1, double alfa, double beta, double sigma, double Iz, double Mza, double p)
        {
            double[] F = new double[31];
            F[0] = solver.X(V, teta, psi);
            F[1] = solver.Y(V, teta);
            F[2] = solver.Z(V, teta, psi);
            F[3] = solver.V(g, teta, Cx, q, Sm, m, P);
            F[4] = solver.teta(g, teta, V, Cy, q, Sm, m);
            F[5] = solver.psi(Cz, q, Sm, m, V, teta);
            F[6] = solver.omega(mx, q, Sm, l, Ix);
            return F;
        }
        public List<double[]> Test(uint N, double[] Y0)
        {
            List<double[]> res = new List<double[]>();
            // Шаг по времени
            double dt = 0.05;
            // Объект метода
            TMyRK task = new TMyRK(N);
            // Установим начальные условия задачи
            task.SetInit(0, Y0);
            // решаем до того как высота снаряда не станет меньше нуля
            int j = 0;
            while (task.Y[1] >= 0 )
            {
                double[] result = new double[31];
                if (task.Y[23] >= task.t-task.Y[24] && task.Y[23] <= task.t)
                {
                    task.Y[22] =  11560/3;
                    task.Y[15] = solver.Cx(Y[13]) - 0.12;
                    task.Y[14] = 55.6 - 5;
                }
                else
                {
                    task.Y[22] = 0;
                }
                for (int i = 0; i < N-1; i++)
                {
                    result[0] = task.t;
                    result[i+1] = task.Y[i];
                }
                res.Add(result);
                task.NextStep(dt);
            }
            return res;
        }
    }
}
