using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Externum_ballistics
{
    public class Optimization
    {
        ExternumParametrs parametrs = new ExternumParametrs();
        Externum_ballistics test = new Externum_ballistics(8);
        double[] x = { 45, 15 };
        int n = 9;
        int j = 0;
        double [] y = new double[2];
        double [] delta = {1,1};

        public double[] CoordinateSearchDetectionAlgorithm ()
        {
            while (j < 2)
            {
                y = Minus(x, delta);
                if (f(y) > f(x))
                {
                    x = y;
                }

                else
                {
                    y = Plus(x, delta);
                    if (f(y) > f(x))
                    {
                        x = y;
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

        }

        public double[] Minus (double [] x, double [] delta)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = x[i] - delta[i];
            }

            return x;
        }

        public double[] Plus(double[] x, double[] delta)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = x[i] + delta[i];
            }

            return x;
        }

        public double f(double [] x)
        {
            parametrs = parametrs.Get_Initial_Conditions(parametrs);
            parametrs.teta = x[0];
            parametrs.t_start = x[1];
            List<double[]> result = new List<double[]>();          
            result = test.Test(8, parametrs, n);
            int last = result.Count;
            return result[last][1];
        }
    }
}
