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
        Projectile OFM29 = new Projectile();// �������� ��������� ������ ��� ������� ���29
        BallisticSolver solver = new BallisticSolver();
        ExternumParametrs parametrs = new ExternumParametrs();
        InletParametrs inletParametrs = new InletParametrs();
        Jetparametrs jetparametrs = new Jetparametrs();
        Externum_ballistics CalcExternumBall = new Externum_ballistics(8);
        Inlet_ballistics CalcInletBall = new Inlet_ballistics(4);
        Optimization optimizer = new Optimization();

        public Form1()
        {
            InitializeComponent(); 
        }
        #region ���������� � �������� ������
        private void jsonToolStripMenuItem_Click(object sender, EventArgs e)//��������� ������ ������� � JSON
        {
            JsonSerializer serializer = new JsonSerializer();
            SaveFileDialog openDialog = new SaveFileDialog();
            openDialog.Filter = "���� ������|*.json";
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
            using (StreamWriter sw = new StreamWriter(openDialog.FileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
                serializer.Serialize(writer, OFM29);
        }

        private void jsonToolStripMenuItem1_Click(object sender, EventArgs e)//��������� ������ ������� � JSON
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "���� ������|*.json";
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
            path = openDialog.FileName;
            string text = File.ReadAllText(path);
            OFM29 = JsonConvert.DeserializeObject<Projectile>(text);
            propertyGrid1.SelectedObject = OFM29;
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)//��������� ������ ������� � XML
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "���� ������|*.snl";
            openxml snaryad = new openxml();
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
        //    OFM29 = snaryad.load(openDialog.FileName, "");
            propertyGrid1.SelectedObject = OFM29;
        }

        #endregion

        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)//������ ����������
        {
            N = 8;
            n = 9;
            List<double[]> result = new List<double[]>();
            result = CalcExternumBall.CalcExternum(N, parametrs, n);
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
                    dataGridView1.Rows[i].Cells[j].Value = Math.Round(result[i][j],2);
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
            formsPlot1.Plot.XLabel("X, ������");
            formsPlot1.Plot.YLabel("Y, ������");
            formsPlot1.Refresh();
            formsPlot1.Plot.AddScatter(dataX2, dataY2, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot1.Refresh();
            formsPlot2.Plot.AddScatter(datat, dataV, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot2.Plot.XLabel("t, ������");
            formsPlot2.Plot.YLabel("V, �/�");
            formsPlot2.Refresh();
            formsPlot2.Plot.AddScatter(datat2, dataV2, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot2.Refresh();
            formsPlot3.Plot.AddScatter(datat, dataOmega, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot3.Plot.XLabel("t, ������");
            formsPlot3.Plot.YLabel("Omega, ���/�");
            formsPlot3.Refresh();
            formsPlot3.Plot.AddScatter(datat2, dataOmegaJet2, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot3.Refresh();
            formsPlot4.Plot.AddScatter(datat, dataSigma, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot4.Plot.AddHorizontalLine(0.6);
            formsPlot4.Plot.AddHorizontalLine(0.9);
            formsPlot4.Plot.XLabel("t, ������");
            formsPlot4.Plot.YLabel("�������� ������������");
            formsPlot4.Refresh();
        }


        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)// ������� ��������� �������
        {
            parametrs = parametrs.Get_Initial_Conditions(parametrs);
            propertyGrid1.SelectedObject = parametrs;

        }

        private void Form1_Load(object sender, EventArgs e)// ������ ����� ������� ��� ������� 
        {
            path = @"net6.0-windowsOFM29.json";
            string text = File.ReadAllText(path);
            OFM29 = JsonConvert.DeserializeObject<Projectile>(text);
            propertyGrid1.SelectedObject = OFM29;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formsPlot1.Plot.Clear();
            formsPlot2.Plot.Clear();
            formsPlot3.Plot.Clear();
            formsPlot4.Plot.Clear();
        }

        private void �����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = OFM29;
        }

        private void ��������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            N = 4;
            List<double[]> result = new List<double[]>();
            result = CalcInletBall.CalcInlet(4, inletParametrs, 5);

            for (int i = 0; i < result.Count; i++)
            {
                dataGridView2.Rows.Add();
                for (int j = 0; j < 15; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = result[i][j];
                }
            }
            double[] datat = new double[result.Count];
            double[] datap = new double[result.Count];
            double[] datap_sn = new double[result.Count];
            double[] datap_kn = new double[result.Count];
            double[] dataV = new double[result.Count];
            double[] datax = new double[result.Count];
            double[] dataz = new double[result.Count];
            double[] dataPsi = new double[result.Count];
            double[] dataW = new double[result.Count];

            for (int i = 0; i < result.Count; i++)
            {
                dataGridView1.Rows.Add();
                datat[i] = result[i][0];
                datap[i] = result[i][6];
                datap_sn[i] = result[i][7];
                datap_kn[i] = result[i][8];
                datax[i] = result[i][4];
                dataV[i] = result[i][3];
                dataz[i] = result[i][1];
                dataPsi[i] = result[i][2];
                dataW[i] = result[i][13];
            }

            formsPlot1.Plot.AddScatter(datat, datap, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3, label: "p");
            formsPlot1.Plot.XLabel("t, ������");
            formsPlot1.Plot.YLabel("������� ��������, ���");
            formsPlot1.Refresh();
            formsPlot1.Plot.AddScatter(datat, datap_sn, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3, label: "p_��");
            formsPlot1.Refresh();
            formsPlot1.Plot.Legend();
            formsPlot1.Plot.AddScatter(datat, datap_kn, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3, label: "p_��");
            formsPlot1.Refresh();
            formsPlot2.Plot.AddScatter(datat, datax, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot2.Plot.XLabel("t, ������");
            formsPlot2.Plot.YLabel("x, �");
            formsPlot2.Refresh();
            formsPlot3.Plot.AddScatter(datat, dataV, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot3.Plot.XLabel("t, ������");
            formsPlot3.Plot.YLabel("V, �/�");
            formsPlot3.Refresh();
            formsPlot4.Plot.AddScatter(datat, dataPsi, markerShape: ScottPlot.MarkerShape.none, lineWidth: 3);
            formsPlot4.Plot.XLabel("t, ������");
            formsPlot4.Plot.YLabel("psi, ���� ���������� ������");
            formsPlot4.Refresh();
        }

        private void �����������������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] x = new double[3];
            x = optimizer.Optimize();
            MessageBox.Show(" ����������� ���� = " + x[0].ToString() + " ��������; " + " ����������� ����� ������ ��������� = " + x[1].ToString() + " ������; " + " ������������ ��������� = " + x[2].ToString() + " �. ");
        }

        private void ��������������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inletParametrs = inletParametrs.Get_Initial_Conditions(inletParametrs);
            propertyGrid1.SelectedObject = inletParametrs;
        }

        private void ���������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JsonSerializer serializer = new JsonSerializer();
            SaveFileDialog openDialog = new SaveFileDialog();
            openDialog.Filter = "���� ������|*.json";
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
            using (StreamWriter sw = new StreamWriter(openDialog.FileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
                serializer.Serialize(writer, parametrs);
        }

        private void ���������������������������ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            JsonSerializer serializer = new JsonSerializer();
            SaveFileDialog openDialog = new SaveFileDialog();
            openDialog.Filter = "���� ������|*.json";
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
            using (StreamWriter sw = new StreamWriter(openDialog.FileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
                serializer.Serialize(writer, inletParametrs);
        }

        private void Calculation_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}