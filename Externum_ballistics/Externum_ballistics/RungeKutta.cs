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
        /// 20 - Ix, 21 - mx, 22 - P, 23 - t_delta, 24 - t_start, 25 - alfa, 26 - beta, 27 - sigma, 
        /// 28 - Iz, 29 - Mza, 30 - p, 31 - delta_omega, 32 - Mpx
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
    
        public void update ()
        {
            Y[9] = solver.T(Y[1]);
            Y[30] = solver.p(Y[1]);
            Y[10] = solver.ro(Y[30], Y[9]);
            Y[7] = solver.q(Y[10], Y[3]);
            Y[8] = solver.a(Y[9]);
            Y[11] = solver.g(0, Y[1]);
            Y[13] = solver.Mah(Y[3], Y[8]);
            Y[15] = solver.Cx(Y[13], Y[23], Y[24], t);
            Y[25] = solver.alfa(0.1455, 1.4417, Y[6]);
            Y[26] = solver.beta1(0.8918, Y[10], Y[12], Y[19], 1.4417, Y[3]);
            Y[27] = solver.sigma(Y[25], Y[26]);
            Y[31] = solver.delta_omega(Y[32], Y[22], Y[18], 0.8918, Y[24], Y[23],t);
        }
        
        public void NextStep(double dt)
        {
            int i;
            update();

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
            return Funk(Y[3], Y[4], Y[5], Y[6], Y[7], Y[11], Y[12], Y[14], Y[15], Y[16], Y[17], Y[19], Y[20], Y[21], Y[22], Y[31]);
        }

        /// Создание вектора параметров полёта снаряда
        public double[] Funk(double V, double teta, double psi, double omega, double q,double g, double Sm, double m, double Cx, double Cy, double Cz, double l, double Ix, double mx, double P, double delta_omega)
        {
            double[] F = new double[33];
            F[0] = solver.X(V, teta, psi);
            F[1] = solver.Y(V, teta);
            F[2] = solver.Z(V, teta, psi);
            F[3] = solver.V(g, teta, Cx, q, Sm, m, P);
            F[4] = solver.teta(g, teta, V, Cy, q, Sm, m);
            F[5] = solver.psi(Cz, q, Sm, m, V, teta);
            F[6] = solver.omega(mx, q, Sm, l, Ix, delta_omega);
            return F;
        }
        public List<double[]> Test(uint N, double[] Y0, int n)
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
                double[] result = new double[n];
                if (task.t >= task.Y[24] && task.t <= task.Y[23] + task.Y[24])
                {
                    task.Y[22] =  11560/3;
                }
                else
                {
                    task.Y[22] = 0;
                    task.Y[31] = 0;
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
