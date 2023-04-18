using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Converters;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Externum_ballistics
{
    public partial class Form1 : Form
    {
        static uint N = 8;
        static int n = 9;
        double R = 346.9;
        string path;
        double[] Y0 = new double[n];
        Projectile OFM29 = new Projectile();// Создадим экземпляр класса для снаряда ОФМ29
        BallisticSolver solver = new BallisticSolver();
        ExternumParametrs parametrs = new ExternumParametrs();
        Jetparametrs jetparametrs = new Jetparametrs();
        Externum_ballistics test = new Externum_ballistics(N);

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
            List<double[]> result = new List<double[]>();
            result = test.Test(N, parametrs, n);
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
            List<double> dataJetV = new List<double>();
            List<double> dataJett = new List<double>();
            List<double> dataOmegaJet = new List<double>();
            for (int i = 0; i < result.Count; i++)
            {
                dataGridView1.Rows.Add();
                datat[i] = result[i][0];
                datax[i] = result[i][1];
                datay[i] = result[i][2];
                dataV[i] = result[i][4];
                dataOmega[i] = result[i][7];
                dataSigma[i] = result[i][8];

                if (result[i][0] >= parametrs.t_start && result[i][0] <= parametrs.t_start + parametrs.t_delta )
                {
                    dataxJet.Add(result[i][1]);
                    datayJet.Add(result[i][2]);
                    dataJetV.Add(result[i][4]);
                    dataJett.Add(result[i][0]);
                    dataOmegaJet.Add(result[i][7]);
                }

                for (int j = 0; j < N-1; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = result[i][j];
                }
            }
            double[] dataY2 = new double[datayJet.Count];
            double[] dataX2 = new double[dataxJet.Count];
            double[] dataV2 = new double[dataxJet.Count];
            double[] datat2 = new double[dataxJet.Count];
            double[] dataOmegaJet2 = new double[dataxJet.Count];

            for (int k = 0; k < dataxJet.Count; k++)
            {
                dataX2[k] = dataxJet[k];
                dataY2[k] = datayJet[k];
                dataV2[k] = dataJetV[k];
                datat2[k] = dataJett[k];
                datat2[k] = dataJett[k];
                dataOmegaJet2[k] = dataOmegaJet[k];
            }
            for (int i = 0; i < koef1.Length; i++)
            {
                koef1[i] = 0.9;
                koef2[i] = 0.6;
            }
            formsPlot1.Plot.AddScatter(datax, datay,markerShape:ScottPlot.MarkerShape.none, lineWidth:3);
            formsPlot1.Plot.XLabel("X, метров");
            formsPlot1.Plot.YLabel("Y, метров");
            formsPlot1.Refresh();
            formsPlot1.Plot.AddScatter(dataX2, dataY2, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot1.Refresh();
            formsPlot2.Plot.AddScatter(datat, dataV, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot2.Plot.XLabel("t, секунд");
            formsPlot2.Plot.YLabel("V, м/с");
            formsPlot2.Refresh();
            formsPlot2.Plot.AddScatter(datat2, dataV2, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot2.Refresh();
            formsPlot3.Plot.AddScatter(datat, dataOmega, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot3.Plot.XLabel("t, секунд");
            formsPlot3.Plot.YLabel("Omega, рад/с");
            formsPlot3.Refresh();
            formsPlot3.Plot.AddScatter(datat2, dataOmegaJet2, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot3.Refresh();
            formsPlot4.Plot.AddScatter(datat, dataSigma, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot4.Plot.AddHorizontalLine(0.6);
            formsPlot4.Plot.AddHorizontalLine(0.9);
            formsPlot4.Plot.XLabel("t, секунд");
            formsPlot4.Plot.YLabel("Критерий устойчивости");
            formsPlot4.Refresh();
        }


        private void начальныеУсловияToolStripMenuItem_Click(object sender, EventArgs e)// Задание начальных условий
        {
            parametrs = parametrs.Get_Initial_Conditions(parametrs);
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
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Clear();
            formsPlot2.Plot.Clear();
            formsPlot3.Plot.Clear();
            formsPlot4.Plot.Clear();
        }

        private void изменитьПараметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = OFM29;
        }

        private void внутренняяБаллистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void оптимизацияВнешнебаллистическихПараметровToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}