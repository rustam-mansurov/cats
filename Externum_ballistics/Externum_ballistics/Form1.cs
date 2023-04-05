using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Converters;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Externum_ballistics
{
    public partial class Form1 : Form
    {
        uint N = 31;
        int n = 31;
        double R = 346.9;
        string path;
        double[] Y0 = new double[31];
        Projectile OFM29 = new Projectile();// Создадим экземпляр класса для снаряда ОФМ29
        BallisticSolver solver = new BallisticSolver();
        Parametrs parametrs = new Parametrs();
        Jetparametrs jetparametrs = new Jetparametrs();
        TMyRK test = new TMyRK(31);

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
            Y0[22] = jetparametrs.Psigma;
            Y0[23] = jetparametrs.t;
            Y0[24] = jetparametrs.t_delta;
            Y0[25] = jetparametrs.alfa;
            Y0[26] = jetparametrs.beta1;
            Y0[27] = jetparametrs.sigma;
            Y0[28] = parametrs.I_z;
            Y0[29] = parametrs.mz;

            List<double[]> result = new List<double[]>();
            result = test.Test(N, Y0);
            double [] datax = new double[result.Count];
            double[] datat = new double[result.Count];
            double [] datay = new double[result.Count];
            double[] dataV = new double[result.Count];
            double[] dataOmega = new double[result.Count];
            double[] dataSigma = new double[result.Count];
            double[] koef1 = new double[result.Count];
            double[] koef2 = new double[result.Count];
            List<double> dataxJet = new List<double>();
            List<double> datayJet = new List<double>();
            for (int i = 0; i < result.Count; i++)
            {
                dataGridView1.Rows.Add();
                datat[i] = result[i][0];
                datax[i] = result[i][1];
                datay[i] = result[i][2];
                dataV[i] = result[i][4];
                dataOmega[i] = result[i][7];
                dataSigma[i] = result[i][28];

                if (result[i][0] >= jetparametrs.t_delta-result[i][24] && result[i][0] <= jetparametrs.t_delta + result[i][24])
                {
                    dataxJet.Add(result[i][1]);
                    datayJet.Add(result[i][2]);
                }

                for (int j = 0; j < N-1; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = result[i][j];
                }
            }
            double[] dataY2 = new double[datayJet.Count];
            double[] dataX2 = new double[dataxJet.Count];

            for (int k = 0; k < dataxJet.Count; k++)
            {
                dataX2[k] = dataxJet[k];
                dataY2[k] = datayJet[k];
            }
            for (int i = 0; i < koef1.Length; i++)
            {
                koef1[i] = 0.9;
                koef2[i] = 0.6;
            }
            formsPlot1.Plot.AddScatter(datax, datay);
            formsPlot1.Plot.XLabel("X, метров");
            formsPlot1.Plot.YLabel("Y, метров");
            formsPlot1.Refresh();
            formsPlot1.Plot.AddScatter(dataX2, dataY2);
            formsPlot1.Refresh();
            formsPlot2.Plot.AddScatter(datat, dataV);
            formsPlot2.Plot.XLabel("t, секунд");
            formsPlot2.Plot.YLabel("V, м/с");
            formsPlot2.Refresh();
            formsPlot3.Plot.AddScatter(datat, dataOmega);
            formsPlot3.Plot.XLabel("t, секунд");
            formsPlot3.Plot.YLabel("Omega, рад/с");
            formsPlot3.Refresh();
            formsPlot4.Plot.AddScatter(datat, dataSigma);
            formsPlot4.Plot.AddScatter(datat, koef1);
            formsPlot4.Plot.AddScatter(datat, koef2);
            formsPlot4.Plot.XLabel("t, секунд");
            formsPlot4.Plot.YLabel("Критерий устойчивости");
            formsPlot4.Refresh();
        }


        private void начальныеУсловияToolStripMenuItem_Click(object sender, EventArgs e)// Задание начальных условий
        {
            string text = File.ReadAllText(path);
            parametrs = JsonConvert.DeserializeObject<Parametrs>(text);
            parametrs.g = solver.g(0, 0);
            parametrs.T = solver.T(0);
            parametrs.a = solver.a(parametrs.T);
            parametrs.Sm = solver.Sm(OFM29.Caliber);
            parametrs.p = solver.p(0);
            parametrs.ro = solver.ro(parametrs.p,parametrs.T);
            parametrs.Mah = solver.Mah(parametrs.Starting_velocity, parametrs.a);
            parametrs.q = solver.q(parametrs.ro, parametrs.Starting_velocity);
            parametrs.X = 0;
            parametrs.Y = 0;
            parametrs.Z = 0;
            parametrs.d = OFM29.Caliber;
            parametrs.psi = solver.psi(parametrs.Cz, parametrs.q, parametrs.Sm, parametrs.Mass, parametrs.Starting_velocity, parametrs.Start_angle);
            parametrs.Cx = solver.Cx(parametrs.Mah);
            parametrs.Cy = 0;  
            parametrs.Cz = 0;
            parametrs.mz = 0.8918; //solver.mz(parametrs.Mah, parametrs.Initial_angular_velocity);
            parametrs.I_x = 0.1455;
            parametrs.I_z = 1.4417;
            parametrs.Initial_angular_velocity = 1560;
            parametrs.Start_angle = 52;
            propertyGrid1.SelectedObject = parametrs;
        }

        private void Form1_Load(object sender, EventArgs e)// Чтение файла снаряда при запуске 
        {
            path = @"net6.0-windowsOFM29.json";
            string text = File.ReadAllText(path);
            OFM29 = JsonConvert.DeserializeObject<Projectile>(text);
            propertyGrid1.SelectedObject = OFM29;
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
            jetparametrs.t = 3;// Время работы РД
            jetparametrs.u1 = 6.53*1e-6f;// Единичная скорость горения
            jetparametrs.pT = 1600;// Плотность топлива
            jetparametrs.Sg = 0.015394;// Площадь горящего свода
            jetparametrs.Tk = 2478;// Температура???
            jetparametrs.Skr = 0.000115;// Площадь критического сопла
            jetparametrs.A = 0.652;// Коэффициент расхода сопла
            jetparametrs.lambda = 2.376;// Лямбда
            jetparametrs.k = 1.22;// Показатель адиабаты
            jetparametrs.Sv = Math.Round(solver.Sv(jetparametrs.dv),2);// Площадь внешняя
            jetparametrs.pk = solver.pk(jetparametrs.u1, jetparametrs.Sg, 0.98, R, jetparametrs.Tk, 0.98, jetparametrs.Skr, jetparametrs.nu);// Давление в камере
            jetparametrs.u = solver.u(jetparametrs.u1, jetparametrs.pk, jetparametrs.nu);// Скорость горения топлива
            jetparametrs.G = Math.Round(solver.G(jetparametrs.Skr, jetparametrs.pk, jetparametrs.A, R, jetparametrs.Tk),2);// Массовый расход топлива в секунду
            jetparametrs.akr = solver.akr(jetparametrs.k, R, jetparametrs.Tk);// Скорость звука в критическом срезе
            jetparametrs.uv = solver.uv(jetparametrs.akr, jetparametrs.lambda);// Внешнее u
            jetparametrs.re = solver.re(jetparametrs.dv);// радиус сопла
            jetparametrs.pv = solver.pv(jetparametrs.pk, jetparametrs.k, jetparametrs.lambda);// Внешнее давление
            jetparametrs.Psigma = Math.Round(solver.Psigma(jetparametrs.G, jetparametrs.uv, jetparametrs.Sv, jetparametrs.pv, jetparametrs.pn),2);// Суммарная тяга с учетом вращения
            jetparametrs.P = Math.Round(solver.P(jetparametrs.Psigma, jetparametrs.nu, jetparametrs.beta),2);// Тяга без учета вращения
            jetparametrs.It = Math.Round(solver.It(jetparametrs.Psigma, jetparametrs.t),2);// Импульс двигателя
            jetparametrs.Mpx = Math.Round(solver.Mpx(jetparametrs.Psigma, jetparametrs.nu, jetparametrs.re, jetparametrs.nu),2);// Коэффициент тяги на вращение
            jetparametrs.t_delta = 22;
            jetparametrs.alfa = Math.Round(solver.alfa(parametrs.I_x, parametrs.I_z, parametrs.Initial_angular_velocity),2);
            jetparametrs.beta1 = Math.Round(solver.beta1(parametrs.mz, parametrs.q, parametrs.Sm, parametrs.Length, parametrs.I_z),2);
            jetparametrs.sigma = Math.Round(solver.sigma(jetparametrs.alfa, jetparametrs.beta1),2);
            propertyGrid1.SelectedObject = jetparametrs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Clear();
            formsPlot2.Plot.Clear();
            formsPlot3.Plot.Clear();
            formsPlot4.Plot.Clear();
        }
    }
}