using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Converters;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Externum_ballistics
{
    public partial class Form1 : Form
    {
        uint N = 23;
        int n = 23;
        double R = 287;// Универсальная газовая постоянная
        string path;
        double[] Y0 = new double[23];
        Projectile OFM29 = new Projectile();// Создадим экземпляр класса для снаряда ОФМ29
        BallisticSolver solver = new BallisticSolver();
        Parametrs parametrs = new Parametrs();
        Jetparametrs jetparametrs = new Jetparametrs();
        TMyRK test = new TMyRK(23);

        public Form1()
        {
            InitializeComponent(); 
        }
        #region Сохранение и загрузка данных
        private void jsonToolStripMenuItem_Click(object sender, EventArgs e)//Сохранить данные снаряда в JSON
        {
            JsonSerializer serializer = new JsonSerializer();
            SaveFileDialog openDialog = new SaveFileDialog();
            openDialog.Filter = "Файл данных|*.json";
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
            using (StreamWriter sw = new StreamWriter(openDialog.FileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
                serializer.Serialize(writer, OFM29);
        }

        private void jsonToolStripMenuItem1_Click(object sender, EventArgs e)//Загрузить данные снаряда в JSON
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файл данных|*.json";
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
            path = openDialog.FileName;
            string text = File.ReadAllText(path);
            OFM29 = JsonConvert.DeserializeObject<Projectile>(text);
            propertyGrid1.SelectedObject = OFM29;
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)//Загрузить данные снаряда в XML
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файл данных|*.snl";
            openxml snaryad = new openxml();
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
        //    OFM29 = snaryad.load(openDialog.FileName, "");
            propertyGrid1.SelectedObject = OFM29;
        }

        #endregion

        private void начатьToolStripMenuItem_Click(object sender, EventArgs e)//Начать вычисления
        {
            Y0 = parametrs.Get_Initial_Conditions(n, parametrs);// Начальные условия

            List<double[]> result = new List<double[]>();
            result = test.Test(N, Y0);
            double [] dataX = new double[result.Count];
            double [] dataY = new double[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataX[i] = result[i][1];
                dataY[i] = result[i][2];
                for (int j = 0; j < N-1; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = result[i][j];
                }
            }
            formsPlot1.Plot.AddScatter(dataX, dataY);
            formsPlot1.Refresh();
        }

        private void начальныеУсловияToolStripMenuItem_Click(object sender, EventArgs e)// Задание начальных условий
        {
            string text = File.ReadAllText(path);
            parametrs = JsonConvert.DeserializeObject<Parametrs>(text);
            parametrs.g = solver.g(0, 0);
            parametrs.T = solver.T(0);
            parametrs.a = solver.a(parametrs.T);
            parametrs.Sm = solver.Sm(OFM29.Caliber);
            parametrs.ro = solver.p(0);
            parametrs.Mah = solver.Mah(parametrs.Starting_velocity, parametrs.a);
            parametrs.q = solver.q(parametrs.ro, parametrs.Mah, parametrs.a, parametrs.T);
            parametrs.X = 0;
            parametrs.Y = 0;
            parametrs.Z = 0;
            parametrs.d = OFM29.Caliber;
            parametrs.psi = solver.psi(parametrs.Cza[0], parametrs.q, parametrs.Sm, parametrs.Mass, parametrs.Starting_velocity, parametrs.Start_angle);
            parametrs.Cx = solver.Cx(parametrs.Mah);
            propertyGrid1.SelectedObject = parametrs;
        }

        private void Form1_Load(object sender, EventArgs e)// Чтение файла снаряда при запуске 
        {
            path = @"net6.0-windowsOFM29.json";
            string text = File.ReadAllText(path);
            OFM29 = JsonConvert.DeserializeObject<Projectile>(text);
            propertyGrid1.SelectedObject = OFM29;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void начальныеПараметрыРеактивногоДвигателяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = File.ReadAllText(path);
            jetparametrs = JsonConvert.DeserializeObject<Jetparametrs>(text);
            jetparametrs.h = 0.026;// Высота ребер
            jetparametrs.dv = 0.04; // Выходной диаметр
            jetparametrs.beta = 15;// Угол наклона ребер
            jetparametrs.pn = 0.101325;// Нормальное атмосферное давление
            jetparametrs.nu = 0.5;// Доля тяги на вращение
            jetparametrs.t = 3;// Время работы РД массу топлива на расход
            jetparametrs.u1 = 0.0653*1e-4;// Единичная скорость горения
            jetparametrs.pT = 1600;// Плотность топлива
            jetparametrs.Sg = 0.015394;// Площадь горящего свода
            jetparametrs.Tk = 2478;// Температура???
            jetparametrs.Skr = 0.000115;// Площадь критического сопла
            jetparametrs.A = 0.652;// Коэффициент расхода сопла
            jetparametrs.lambda = 2.376;// Лямбда
            jetparametrs.k = 1.22;// Показатель адиабаты
            jetparametrs.Sv = solver.Sv(jetparametrs.dv);
            jetparametrs.Psigma = solver.Psigma(jetparametrs.G, jetparametrs.u, jetparametrs.Sv,jetparametrs.pv, jetparametrs.pn);// Суммарная тяга с учетом вращения
            jetparametrs.P = solver.P(jetparametrs.Psigma, jetparametrs.nu,jetparametrs.beta);// Тяга без учета вращения
            jetparametrs.Mpx = solver.Mpx(jetparametrs.Psigma, jetparametrs.nu, jetparametrs.re, jetparametrs.nu);// Коэффициент тяги на вращение
            jetparametrs.It = solver.It(jetparametrs.P, jetparametrs.t);// Импульс двигателя
            jetparametrs.u = solver.u(jetparametrs.u1,jetparametrs.pk);// Скорость горения топлива
            jetparametrs.pk = solver.pk(jetparametrs.pT, jetparametrs.u, jetparametrs.Sg, 0.98, 287, jetparametrs.Tk, 0.98, jetparametrs.Skr, jetparametrs.nu);// Давление в камере
            jetparametrs.G = solver.G(jetparametrs.Skr, jetparametrs.pk, jetparametrs.A, R, jetparametrs.Tk);// Массовый расход топлива в секунду
            jetparametrs.uv = solver.uv(jetparametrs.akr, jetparametrs.lambda);// Внешнее u
            jetparametrs.akr = solver.akr(jetparametrs.k, R, jetparametrs.Tk);// Скорость звука в критическом срезе
            jetparametrs.re = solver.re(jetparametrs.dv);// радиус сопла
            jetparametrs.pv = solver.pv(jetparametrs.pk, jetparametrs.k, jetparametrs.lambda);// Внешнее давление

            propertyGrid1.SelectedObject = jetparametrs;
        }
    }
}