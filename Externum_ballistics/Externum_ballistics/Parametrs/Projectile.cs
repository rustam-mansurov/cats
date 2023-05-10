using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Externum_ballistics
{
    [Category("Характеристики снаряда")]
    [Serializable]
    public class Projectile
    {
        #region Характеристики снаряда
        [Category("Характеристики снаряда"), DescriptionAttribute("Имя снаряда"),DisplayName("Имя снаряда")]
        public string Name { get; set; }

        [Category("Характеристики снаряда"), DescriptionAttribute("Калибр снаряда, клб"), DisplayName("Калибр")]
        public double Caliber { get; set; }

        [Category("Характеристики снаряда"), DescriptionAttribute("Масса снаряда, кг"), DisplayName("Масса")]
        public double Mass { get; set; }

        [Category("Характеристики снаряда"), DescriptionAttribute("Длина снаряда, м"), DisplayName("Длина")]
        public double Length { get; set; }

        [Category("Характеристики снаряда"), DescriptionAttribute("Длина головной части, м"), DisplayName("Длина головной части")]
        public double Head_length { get; set; }

        [Category("Характеристики снаряда"),DescriptionAttribute("Центр масс от носа, м"), DisplayName("Центр масс от носа")]
        public double Center_of_mass { get; set; }

        [Category("Характеристики снаряда"), DescriptionAttribute("Коэффициент формы"), DisplayName("Коэффициент формы")]
        public double ix { get; set; }
        #endregion
    }
}
