using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Externum_ballistics
{
    public class InletParametrs
    {
        #region Начальные условия
        [Category("Начальные условия"), DescriptionAttribute("Масса пороха"), DisplayName("omega, кг")]
        public double omega { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Число каналов в пороховом элементе"), DisplayName("n")]
        public double n { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Внешний диаметр порохового элемента"), DisplayName("D0, м")]
        public double D0 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Диаметр канала порохового элемента"), DisplayName("d0, м")]
        public double d0 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Толщина горящего свода"), DisplayName("e1, м")]
        public double e1 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Теплоемкость пороха"), DisplayName("c_poroh, Дж/кг")]
        public double c_poroh { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Масса воспламенителя"), DisplayName("omegaV, кг")]
        public double omegaV { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Плотность пороха"), DisplayName("delta, кг/м^3")]
        public double delta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Масса снаряда"), DisplayName("m, кг")]
        public double m { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double S { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double Lambda0 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double P { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double Q { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double beta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double lambda { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double uk { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double mu { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double J1 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double J2 { get; set; }
        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double J3 { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double l_n { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double l_k { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Единичная скорость горения пороха"), DisplayName("uk, м^3/(Н·с)")]
        public double L_k { get; set; }
        #endregion

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double k { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double z { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double psiP { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double psi { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double teta { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double p { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double T { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double p_kn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double p_sn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double W_sn { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double W_km { get; set; }

        [Category("Начальные условия"), DescriptionAttribute("Показатель адиабаты"), DisplayName("k, ???")]
        public double eta { get; set; }

        public double[] Get_Initial_Conditions(int N, InletParametrs parametrs)// Получить начальные параметры
        {
            double[] Y0 = new double[N];
            Y0[0] = parametrs.omega;
            return Y0;
        }
    }
}
