using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Externum_ballistics
{
    public class Parametrs
    {

        [Category("Положение в пространстве"), DescriptionAttribute("Описание"), DisplayName("X, м")]
        public double X { get; set; }

        [Category("Положение в пространстве"), DescriptionAttribute("Описание"), DisplayName("Y, м")]
        public double Y { get; set; }

        [Category("Положение в пространстве"), DescriptionAttribute("Описание"), DisplayName("Z, м")]
        public double Z { get; set; }

        #region Начальные условия

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("V, м")]
        public double Starting_velocity { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("omega, рад/с")]
        public double Initial_angular_velocity { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("teta, градусов")]
        public double Start_angle { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("g, м/с")]
        public double g { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Скоростной напор в воздухе")]
        public double q { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Угол направления")]
        public double psi { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Площадь Миделева сечения")]
        public double Sm { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("m, кг")]
        public double Mass { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Плотность воздуха")]
        public double ro { get; set; }


        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Давление воздуха")]
        public double p { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Температура")]
        public double T { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Число маха")]
        public double Mah { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Скорость звука, м")]
        public double a { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Длина, м")]
        public double Length{ get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Описание"), DisplayName("Диаметр, м")]
        public double d { get; set; }

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

        public double[] Get_Initial_Conditions(int N, Parametrs parametrs)// Получить начальные параметры
        {
            double[] Y0 = new double [N];
            Y0[0] = parametrs.X;
            Y0[1] = parametrs.Y;
            Y0[2] = parametrs.Z;
            Y0[3] = parametrs.Starting_velocity;
            Y0[4] = parametrs.Start_angle;
            Y0[5] = parametrs.psi;
            Y0[6] = parametrs.Initial_angular_velocity;
            Y0[7] = parametrs.q;
            Y0[8] = parametrs.a;
            Y0[9] = parametrs.T;
            Y0[10] = parametrs.ro;
            Y0[11] = parametrs.g;
            Y0[12] = parametrs.Sm;
            Y0[13] = parametrs.Mah;
            Y0[14] = parametrs.Mass;
            Y0[15] = parametrs.Cx;
            Y0[16] = 1;//Cy 
            Y0[17] = 1;//Cz
            Y0[18] = parametrs.d;
            Y0[19] = parametrs.Length;
            Y0[20] = parametrs.I_x;
            Y0[21] = 0.000617;//???
            return Y0;
        }
    }
}
