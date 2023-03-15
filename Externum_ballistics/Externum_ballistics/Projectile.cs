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
        [Category("Характеристики снаряда"), DescriptionAttribute("Описание"),DisplayName("Имя снаряда")]
        public string Name { get; set; }

        [Category("Характеристики снаряда"), DescriptionAttribute("Описание"), DisplayName("Калибр")]
        public double Caliber { get; set; }

        [Category("Характеристики снаряда"), DescriptionAttribute("Описание"), DisplayName("Масса")]
        public double Mass { get; set; }

        [Category("Характеристики снаряда"), DescriptionAttribute("Описание"), DisplayName("Длина")]
        public double Length { get; set; }

        [Category("Характеристики снаряда"), DescriptionAttribute("Описание"), DisplayName("Длина головной части")]
        public double Head_length { get; set; }

        [Category("Характеристики снаряда"),DescriptionAttribute("Описание"), DisplayName("Центр масс от носа")]
        public double Center_of_mass { get; set; }

        [Category("Характеристики снаряда"), DescriptionAttribute("Описание"), DisplayName("Коэффициент формы")]
        public double ix { get; set; }
        #endregion
    }
}
