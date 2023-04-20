using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Externum_ballistics
{
    public class Optimization
    {
        static uint N = 8;
        static int n = 9;
        Externum_ballistics test = new Externum_ballistics(N);
        ExternumParametrs parametrs = new ExternumParametrs();
        double eps = 0;
        double [] gamma = {1.5, 1.5};
        double [] x = new double[2];
        double delta = 0.5;

        public double[] decrease (double[] delta, double gamma)
        {
            for (int i = 0; i < delta.Length; i++)
            {
                delta[i] = delta[i] / gamma;
            }
            return delta;
        }

        public double Normal_vector(double[] delta)
        {
            double sum = 0;
            for (int i = 0; i < delta.Length; i++)
            {
                sum += delta[i]*delta[i];
            }

            return Math.Sqrt(sum);
        }

        public double step_forward (double x, double delta)
        {
            double y = 0;
            y = x + delta;
            return y;
        }

        public double step_back(double x, double delta)
        {
            double y = 0;
            y = x - delta;
            return y;
        }

        public double[] desicion (double [] x, double [] delta, double gamma, double eps)
        {
            List<double[]> result = new List<double[]>();
            double f_pred = 0;
            double f = 0;
            double norma = Normal_vector(delta);
            parametrs = parametrs.Get_Initial_Conditions(parametrs);// Задаем параметры для расчёта
            result = test.Test(N, parametrs, n);// Производим расчёт
            int last = result.Count;
            f_pred = result[last][1];
            // Подставляем вместо старых значений новые
            x[0] = step_forward(x[0], delta[0]);
            int j = 0;
            while (norma > eps)
            {
                parametrs.teta = x[0];
                parametrs.t_start = x[1];
                result = test.Test(N, parametrs, n);
                last = result.Count;
                f = result[last][1];

                if (f_pred > f)
                {
                    if(j == 0)
                    {
                        j = 1;
                    }
                    else
                    {
                        j = 0;
                    }
                    x[j] = step_back(x[j], delta[j]);
                }

                else
                {
                    delta = decrease(delta, gamma);
                    x[j] = step_forward(x[j], delta[j]);

                }
            }
            return x;
        }
    }
}
