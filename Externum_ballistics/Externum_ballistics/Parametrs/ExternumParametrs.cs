using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Externum_ballistics
{
    public class ExternumParametrs
    {
        BallisticSolver solver = new BallisticSolver();
        double R = 346.9;
        #region Положение в пространстве
        [Category("Положение в пространстве"), DescriptionAttribute("Описание"), DisplayName("X, м")]
        public double X { get; set; }

        [Category("Положение в пространстве"), DescriptionAttribute("Описание"), DisplayName("Y, м")]
        public double Y { get; set; }

        [Category("Положение в пространстве"), DescriptionAttribute("Описание"), DisplayName("Z, м")]
        public double Z { get; set; }
        #endregion
        #region Начальные условия

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("V, м")]
        public double V { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("omega, рад/с")]
        public double Omega { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("teta, градусов")]
        public double teta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("g, м/с")]
        public double g { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Скоростной напор в воздухе, кг/м^2")]
        public double q { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Угол направления, град")]
        public double psi { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Площадь Миделева сечения, м^2")]
        public double Sm { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("m, кг")]
        public double Mass { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Плотность воздуха, кг/м^3")]
        public double ro { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Давление воздуха, кПа")]
        public double p { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Температура, К")]
        public double T { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Число маха")]
        public double Mah { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Скорость звука, м/с")]
        public double a { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Длина, м")]
        public double Length{ get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Диаметр, м")]
        public double d { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Масса")]
        public double m { get; set; }

        #endregion
        #region Моменты и коэффициенты
        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Аксиальный момент инерции")]
        public double I_x { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Экваториальный момент инерции")]
        public double I_z { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Длина хода нарезов")]
        public double Rifling_stroke { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Закон сопротивления")]
        public double cx_law { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Коэффициент бокового отклонения")]
        public double iz { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Коэффициент аксиального демпф. момента")]
        public double mx_wx { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Диапозон чисел Маха")]
        public double[] Ma { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Сха")]
        public double[,] Cxa { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Сх")]
        public double Cx { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Сx")]
        public double Cy { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("Сz")]
        public double Cz { get; set; }

        [Category("Различные моменты и коэффициенты"), DescriptionAttribute("Описание"), DisplayName("mz")]
        public double mz { get; set; }
        #endregion
        #region Реактивный двигаетель
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

        [Category("Сопло с ребрами"), DescriptionAttribute("Описание"), DisplayName("Ускорение угловой скорости, Рад/с")]
        public double delta_omega { get; set; }
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

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Время старта работы двигателя, сек")]
        public double t_start { get; set; }

        [Category("Константы"), DescriptionAttribute("Описание"), DisplayName("Время работы двигателя, сек")]
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
        [Category("Устойчивость"), DescriptionAttribute("Описание"), DisplayName("Коэффициент аэродинамического момента")]
        public double IsARS { get; set; }

        public ExternumParametrs Get_Initial_Conditions(ExternumParametrs parametrs)// Получить начальные параметры
        {
            parametrs.g = solver.g(0, 0);
            parametrs.T = solver.T(0);
            parametrs.a = solver.a(parametrs.T);
            parametrs.d = 0.152;
            parametrs.mx_wx = 0.0004;
            parametrs.Sm = solver.Sm(parametrs.d);
            parametrs.p = solver.p(0);
            parametrs.ro = solver.ro(parametrs.p, parametrs.T);
            parametrs.Mah = solver.Mah(parametrs.V, parametrs.a);
            parametrs.q = solver.q(parametrs.ro, parametrs.V);
            parametrs.V = 960;
            parametrs.X = 0;
            parametrs.Y = 1;
            parametrs.Z = 0;
            parametrs.psi = solver.psi(parametrs.Cz, parametrs.q, parametrs.Sm, parametrs.Mass, parametrs.V, parametrs.teta);
            parametrs.Cx = solver.Cx(parametrs.Mah, parametrs.t_start, parametrs.t_delta, 0);
            parametrs.Cy = 0;
            parametrs.Cz = 0;
            parametrs.mz = 0.8918; //solver.mz(parametrs.Mah, parametrs.Initial_angular_velocity);
            parametrs.I_x = 0.1455;
            parametrs.I_z = 1.4417;
            parametrs.Omega = 1560;
            parametrs.m = 46;
            parametrs.G = solver.G(parametrs.Skr, parametrs.pk, parametrs.A, R, parametrs.Tk);
            parametrs.teta = 52;
            parametrs.Length = 0.709878;
            parametrs.t_start = 22;
            parametrs.t_delta = 3;// Время работы РД
            parametrs.h = 0.026;// Высота ребер
            parametrs.dv = 0.04; // Выходной диаметр
            parametrs.beta = 15;// Угол наклона ребер
            parametrs.pn = 0.101325;// Нормальное атмосферное давление
            parametrs.nu = 0.5;// Доля тяги на вращение
            parametrs.u1 = 6.53 * 1e-6f;// Единичная скорость горения
            parametrs.pT = 1600;// Плотность топлива
            parametrs.Sg = 0.015394;// Площадь горящего свода
            parametrs.Tk = 2478;// Температура???
            parametrs.Skr = 0.000115;// Площадь критического сопла
            parametrs.A = 0.652;// Коэффициент расхода сопла
            parametrs.lambda = 2.376;// Лямбда
            parametrs.k = 1.22;// Показатель адиабаты
            parametrs.Sv = Math.Round(solver.Sv(parametrs.dv), 2);// Площадь внешняя
            parametrs.pk = solver.pk(parametrs.u1, parametrs.Sg, 0.98, R, parametrs.Tk, 0.98, parametrs.Skr, parametrs.nu);// Давление в камере
            parametrs.u = solver.u(parametrs.u1, parametrs.pk, parametrs.nu);// Скорость горения топлива
            parametrs.G = Math.Round(solver.G(parametrs.Skr, parametrs.pk, parametrs.A, R, parametrs.Tk), 2);// Массовый расход топлива в секунду
            parametrs.akr = solver.akr(parametrs.k, R, parametrs.Tk);// Скорость звука в критическом срезе
            parametrs.uv = solver.uv(parametrs.akr, parametrs.lambda);// Внешнее u
            parametrs.re = solver.re(parametrs.dv);// радиус сопла
            parametrs.pv = solver.pv(parametrs.pk, parametrs.k, parametrs.lambda);// Внешнее давление
            parametrs.Psigma = Math.Round(solver.Psigma(parametrs.G, parametrs.uv, parametrs.Sv, parametrs.pv, parametrs.pn), 2);// Суммарная тяга с учетом вращения
            parametrs.P = Math.Round(solver.P(parametrs.Psigma, parametrs.nu, parametrs.beta), 2);// Тяга без учета вращения
            parametrs.It = Math.Round(solver.It(parametrs.Psigma, parametrs.t_delta), 2);// Импульс двигателя
            parametrs.Mpx = Math.Round(solver.Mpx(parametrs.Psigma, parametrs.nu, parametrs.re, parametrs.beta), 2);// Коэффициент тяги на вращение        
            parametrs.alfa = Math.Round(solver.alfa(parametrs.I_x, parametrs.I_z, parametrs.Omega), 2);
            parametrs.beta1 = Math.Round(solver.beta1(parametrs.mz, parametrs.ro, parametrs.Sm, parametrs.Length, parametrs.I_z, parametrs.V), 2);
            parametrs.sigma = Math.Round(solver.sigma(parametrs.alfa, parametrs.beta1), 2);
            parametrs.psi = 0;
            parametrs.IsARS = 1;
            return parametrs;
        }
    }
}
