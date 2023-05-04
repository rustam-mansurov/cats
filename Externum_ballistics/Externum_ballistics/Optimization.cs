using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Externum_ballistics
{
    public static class ArrayExtensions
    {
        public static double[] GetCopy(this double[] x)
        {
            var copy = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                copy[i] = x[i];
            }

            return copy;
        }
    }

    public class Optimization
    {
        ExternumParametrs parametrs = new ExternumParametrs();
        Externum_ballistics test = new Externum_ballistics(8);
        double[] x0 = {52, 22};
        int n = 9;
        int j = 0;
        double[] x0_ = new double[2];
        double[] x1 = new double[2];
        double[] y = new double[2];
        double[] delta = { 1, 0.5 };
        double gamma = 0.7;
        double eps = 0.001;
        double[] answer = new double[3];
        bool IsAccuracyReached = false;

        public double[] CoordinateSearchDetectionAlgorithm(double[] x)
        {
            while (j < 2)
            {
                y = Plus(x, delta);
                if (f(y) > f(x))
                {
                    x = y.GetCopy();
                }

                else
                {
                    y = Minus(x, delta);
                    if (f(y) > f(x))
                    {
                        x = y.GetCopy();
                    }

                    else
                    {
                        j++;
                    }
                }
            }
            return x;
        }

        public double[] Optimize()
        {
            while (IsAccuracyReached == false)
            {
                Step1();
            }
            answer[0] = x1[0];
            answer[1] = x1[1];
            answer[2] = f(x1);
            return answer;
        }

        public double[] Step1()
        {
            x0_ = CoordinateSearchDetectionAlgorithm(x0);
            if (x0_ != x0)
            {
                Step3();
            }

            else
            {
                Step2();
            }
            return x0_;
        }

        public void Step2()
        {
            double u = norma(delta);
            if (norma(delta) < eps)
            {
                x1 = x0.GetCopy();
                IsAccuracyReached = true;
            }

            else
            {
                delta = product(delta, gamma);
                Step1();
            }
        }

        public double[] Step3()
        {
            x1 = Move(x0_, x0);
            Step4();
            return x1;
        }

        public void Step4()
        {
            x1 = CoordinateSearchDetectionAlgorithm(x1);
            if (f(x1) > f(x0_))
            {
                x0 = x0_.GetCopy();
                x0_ = x1.GetCopy();
                Step3();
            }

            else
            {
                x0 = x1.GetCopy();
                Step1();
            }
        }

        public double[] Move(double[] x0_, double[] x0)
        {
            return Minus(product(x0_, 2).ToArray(), x0);
        }

        public double[] Minus(double[] x, double[] delta)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = x[i] - delta[i];
            }

            return x;
        }

        public double[] product(double[] delta, double gamma)
        {
            var y = new double[delta.Length];

            for (int i = 0; i < delta.Length; i++)
            {
                y[i] = delta[i] * gamma;
            }

            return y;
        }


        public double[] Plus(double[] x, double[] delta)
        {
            var y = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = x[i] + delta[i];
            }

            return y;
        }

        public double norma(double[] x)
        {
            double sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += x[i];
            }
            sum = Math.Sqrt(sum);
            return sum;
        }

        public double f(double[] x)
        {
            parametrs = parametrs.Get_Initial_Conditions(parametrs);
            parametrs.teta = x[0];
            parametrs.t_start = x[1];
            List<double[]> result = new List<double[]>();
            result = test.Test(8, parametrs, n);
            int last = result.Count - 1;
            return result[last][1];
        }
    }
}
