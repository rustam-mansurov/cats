using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace Externum_ballistics
{
    public class openxml
    {
        /*    public Projectile load(string f, string mes)// Загрузка данных о снаряде из XML файла
           {
               mes = "";
               Projectile snaryad = new Projectile();
               try //Загрузка параметров снаряда
               {
                 openxml.loadxDoc(f);
                   int nn;
                   snaryad.Name = openxml.xml_string("name");
                   snaryad.Caliber = openxml.xml_double("D") / 1000.0;
                   snaryad.Mass = openxml.xml_double("m");
                   snaryad.Length = openxml.xml_double("l") / 1000.0;
                   snaryad.Center_of_mass = openxml.xml_double("l_cm") / 1000.0;//от носа
                   snaryad.Head_length = openxml.xml_double("l_gol") / 1000.0;
                   snaryad.Starting_velocity = openxml.xml_double("Vs0"); //нач.скорость
                   snaryad.Initial_angular_velocity = openxml.xml_double("wxs0"); //нач.угл.скорость
                   snaryad.Start_angle = openxml.xml_double("tet0"); //нач.угол
                   snaryad.I_x = openxml.xml_double("I_x");
                   snaryad.I_z = openxml.xml_double("I_z");
                   snaryad.Rifling_stroke = openxml.xml_double("narez");
                   snaryad.cx_law = openxml.xml_int("cx_law");//закон сопр
                   snaryad.ix = openxml.xml_double("ix");//коэф. формы
                   snaryad.iz = openxml.xml_double("iz");//коэф. бок отклонения
                   snaryad.mx_wx = openxml.xml_double("mx_wx");//коэф. акс.демф.момента
                   nn = openxml.vec_n("Ma") - 1;
                   snaryad.Ma = new double[nn + 1];
                   for (int j = 0; j < nn + 1; j++)
                       snaryad.Ma[j] = openxml.xml_double("Ma", j);
                   snaryad.Cxa = new double[nn, 5];
                   snaryad.Cya = new double[nn, 5];
                   for (int i = 0; i < nn; i++)
                       for (int j = 0; j < 5; j++)
                       {
                           snaryad.Cxa[i, j] = openxml.xml_double("Cxa", i * 5 + j);
                           snaryad.Cya[i, j] = openxml.xml_double("Cya", i * 5 + j);
                       }
                   nn = openxml.vec_n("Cza"); snaryad.Cza = new double[nn];
                   for (int i = 0; i < nn; i++) snaryad.Cza[i] = openxml.xml_double("Cza", i);
                   nn = openxml.vec_n("Mza"); snaryad.Mza = new double[nn];
                   for (int i = 0; i < nn; i++) snaryad.Mza[i] = openxml.xml_double("Mza", i);
                   nn = openxml.vec_n("Mya"); snaryad.Mya = new double[nn];
                   for (int i = 0; i < nn; i++) snaryad.Mya[i] = openxml.xml_double("Mya", i);
                   nn = openxml.vec_n("Mxa"); snaryad.Mxa = new double[nn];
                   for (int i = 0; i < nn; i++) snaryad.Mxa[i] = openxml.xml_double("Mxa", i);
                   nn = openxml.vec_n("Mwa"); snaryad.Mwa = new double[nn];
                   for (int i = 0; i < nn; i++) snaryad.Mwa[i] = openxml.xml_double("Mwa", i);
               }
               catch (Exception e)
               {
                   mes = e.Message;
                   return snaryad;
               }
               return snaryad;
           }
                */
        #region XML
        public static XmlDocument xDoc;
        public static XmlElement xRoot;

        public static void loadxDoc(string s)
        {
            xDoc = new XmlDocument();
            xDoc.Load(s);
            xRoot = xDoc.DocumentElement;
        }
        public static int vec_n(string s)
        {
            string s2;
            s2 = "//var[@name='" + s + "']//structure/count";
            XmlNode nn2 = xRoot.SelectSingleNode(s2);
            return Convert.ToInt32(nn2.InnerText);
        }    
        public static string xml_string(string s)
        {
            string s2;
            s2 = "//var[@name='" + s + "']//structure/data";
            XmlNode nn2 = xRoot.SelectSingleNode(s2);
            return nn2.InnerText;
        }
        public static int xml_int(string s)
        {
            string s2;
            s2 = "//var[@name='" + s + "']//structure/data";
            XmlNode nn2 = xRoot.SelectSingleNode(s2);
            return Convert.ToInt32(nn2.InnerText);
        }

        public static double xml_double(string s, int i)
        {
            string s2;
            s2 = "//var[@name='" + s + "']//structure/data[" + (i + 1) + "]";
            XmlNode nn2 = xRoot.SelectSingleNode(s2);
            return Convert.ToDouble(nn2.InnerText);
        }
        public static double xml_double(string s)
        {
            string s2;
            s2 = "//var[@name='" + s + "']//structure/data";
            XmlNode nn2 = xRoot.SelectSingleNode(s2);
            return Convert.ToDouble(nn2.InnerText);
        }
        #endregion
    }
}
