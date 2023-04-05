using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Externum_ballistics
{
    public class Jetparametrs
    {

        #region Сопло с ребрами
        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Площадь выходного сечения сопла, м^2")]
        public double Sv { get; set; }

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Общая сила тяги реактивного двигателя, кг/с")]
        public double Psigma { get; set; }

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Сила тяги реактивного двигателя с учетом вращения, кг/с")]
        public double P { get; set; }

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Момент вращения двигателя")]
        public double Mpx { get; set; }

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Суммарный импульс тяги двигателя, м/с")]
        public double It { get; set; }

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Скорость горения, м/с")]
        public double u { get; set; }

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Давление в камере сгорания, Па")]
        public double pk { get; set; }

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Расход продуктов горения через сопло, кг/с")]
        public double G { get; set; }

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Скорость горения в выходном сечении, м/с")]
        public double uv { get; set; }

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Скорость звука в выходном сечении, м/с")]
        public double akr { get; set; }


        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Радиус расположения ребер сопла")]
        public double re { get; set; }

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Внешнее давление, Па")]
        public double pv { get; set; }
        #endregion
        #region Константы
        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Высота ребер на внутренней поверхности сопла, м")]
        public double h { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Диаметр выходного сечения сопла, м")]
        public double dv { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Угол наклона ребер, град")]
        public double beta { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Внутреннее давление, Па")]
        public double pn { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Доля тяги на устойчивость")]
        public double nu { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Время работы двигателя, сек")]
        public double t { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Время старта работы двигателя, сек")]
        public double t_delta { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Единичная скорость горения, м/с")]
        public double u1 { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Плотность топлива, кг/м^3")]
        public double pT { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Площадь горящего свода, м^2")]
        public double Sg { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Температура камеры, К")]
        public double Tk { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Площадь критического сечения, м^2")]
        public double Skr { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Коэффициент расхода сопла")]
        public double A { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Лямбда")]
        public double lambda { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Показатель адиабаты")]
        public double k { get; set; }
        #endregion
        #region Устойчивость

        [Category("Устойчивость"), DescriptionAttribute("Описание"), DisplayName("Критерий устойчивости")]
        public double sigma { get; set; }

        [Category("Устойчивость"), DescriptionAttribute("Описание"), DisplayName("Коэффициент гироскопического момента")]
        public double alfa { get; set; }

        [Category("Устойчивость"), DescriptionAttribute("Описание"), DisplayName("Коэффициент аэродинамического момента")]
        public double beta1 { get; set; }
        #endregion 

        public double[] Get_Initial_Conditions(int N, Jetparametrs jetparametrs)// Получить начальные параметры
        {
            double[] Y0 = new double[N];
            Y0[0] = jetparametrs.h;
            Y0[1] = jetparametrs.dv;
            Y0[2] = jetparametrs.beta;
            Y0[3] = jetparametrs.pn;
            Y0[4] = jetparametrs.nu;
            Y0[5] = jetparametrs.t;
            Y0[6] = jetparametrs.u1;
            Y0[7] = jetparametrs.pT;
            Y0[8] = jetparametrs.Sg;
            Y0[9] = jetparametrs.Tk;
            Y0[10] = jetparametrs.Skr;
            Y0[11] = jetparametrs.A;
            Y0[12] = jetparametrs.lambda;
            Y0[13] = jetparametrs.k;
            Y0[14] = jetparametrs.Sv;
            Y0[15] = jetparametrs.Psigma;
            Y0[16] = jetparametrs.P;
            Y0[17] = jetparametrs.Mpx;
            Y0[18] = jetparametrs.It;
            Y0[19] = jetparametrs.u;
            Y0[20] = jetparametrs.pk;
            Y0[21] = jetparametrs.G;
            Y0[22] = jetparametrs.uv;
            Y0[23] = jetparametrs.akr;
            Y0[24] = jetparametrs.re;
            Y0[25] = jetparametrs.pv;
            return Y0;
        }
    }
}
