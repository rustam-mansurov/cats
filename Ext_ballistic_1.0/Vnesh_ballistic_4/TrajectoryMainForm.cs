using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;


namespace Vnesh_ballistic
{
    public partial class TrajectoryMainForm : Form
    {
        VneshBall VB;
        public string dir;
        public System.Windows.Forms.DataVisualization.Charting.Chart[] arChart;
        int res;
        string mes;      
        public TrajectoryMainForm()
        {
            InitializeComponent();
            StartInit();
        }
        private string GetDataDir()
        {
            string dir = System.Windows.Forms.Application.StartupPath + "\\Data";
            return dir;
        }
        public async void StartInit()
        {
            VB = new VneshBall();
            dir = GetDataDir();
            //init0
            string snaryad0 = "", NU0 = "";
            string f = dir + "\\config.txt";
            FileStream FS = new FileStream(f, FileMode.Open, FileAccess.Read);
            StreamReader SR = new StreamReader(FS, Encoding.Default);//Encoding.Default!!!
            try
            {
                SR.ReadLine(); //начальные параметры запуска
                snaryad0 = SR.ReadLine().Split(' ')[1];
                NU0 = SR.ReadLine().Split(' ')[1];
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                MessageBox.Show("Загрузка начальных параметров запуска программы. Ошибка: " + mes);
                Application.Exit();
            }
            SR.Close(); FS.Close();
            //--
            /*
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстовый Файл|*.txt";
            openDialog.InitialDirectory = dir;
            res = VB.sn.emp.LoadNormFromtxt(dir, ref mes);
            if (res != 0)
            {   MessageBox.Show("Загрузка нормативных коэффициентов. Ошибка: " + mes);
                System.Windows.Forms.Application.Exit(); }
            res = VB.m_ucl.norm.LoadFromtxt(dir, ref mes);
            if (res != 0)
            {   MessageBox.Show("Загрузка метео констант. Ошибка: " + mes);
                System.Windows.Forms.Application.Exit(); }
            res = VB.m_ucl.LoadNormFromtxt(dir, ref mes);
            if (res != 0)
            {   MessageBox.Show("Загрузка стандартных метеоусловий. Ошибка: " + mes);
                System.Windows.Forms.Application.Exit();  }
            res = VB.m_ucl.LoadRealFromtxt(dir + "\\meteo\\real_atmosphere.txt", ref mes);
            if (res != 0)
            {   MessageBox.Show("Загрузка реальных метеоданных. Ошибка: " + mes);
                System.Windows.Forms.Application.Exit();  }
            res = VB.g_ucl.geo.LoadFromtxt(dir, ref mes);
            if (res != 0)
            {   MessageBox.Show("Загрузка геофизических условий. Ошибка: " + mes);
                System.Windows.Forms.Application.Exit();  }
            */
            /*
            LoadSnaryad(dir + "\\snaryad\\" + snaryad0);
            res = VB.LoadNUandParam(dir + "\\initialdata\\" + NU0, ref mes);
            if (res != 0)
            {
                MessageBox.Show("Загрузка начальных условий. Ошибка: " + mes);
                System.Windows.Forms.Application.Exit();
            }
            */
            await LoadNUandParamFromJson(dir + "\\initialdata\\NU_ОФ29.json");
            ZapolnitForm0();
            VB.InitFromNU();
            ZapolnitFormNUandParam();

            radioButton1_SvoyLaw_CheckedChanged(this, new EventArgs());
        }
        public void ZapolnitForm0()
        {
            VB.zapolnitVspomogatArrays();
            dataGridView1_NachUsloviya.Columns.Clear(); //Таблица с начальными условиями
            dataGridView1_NachUsloviya.ColumnCount = 1; dataGridView1_NachUsloviya.RowCount = VB.ChisloUraneniy+1;
            for (int i = 0; i <= VB.ChisloUraneniy; i++)
                dataGridView1_NachUsloviya.Rows[i].HeaderCell.Value = VB.NameVar[i];
            dataGridView1_NachUsloviya.Columns[0].Name = "Значение";  
          dataGridView1_NachUsloviya.Columns[0].DefaultCellStyle.Format = "0.###";
            dataGridView1_NachUsloviya.RowHeadersWidth = 120; dataGridView1_NachUsloviya.Columns[0].Width = 60;
            //for (i = 10; i <= 13; i++) dataGridView1_NachUsloviya.Rows[i].HeaderCell.Value = VB.NameVar[i];
            dataGridView1_Reshenie.Columns.Clear(); //таблица решений
            dataGridView1_Reshenie.ColumnCount = VB.ChisloUraneniy + 1; dataGridView1_Reshenie.RowCount = 1;
            for (int j = 0; j <= VB.ChisloUraneniy; j++) { dataGridView1_Reshenie.Columns[j].Name = VB.NameVar[j]; };
            for (int j = 0; j <= VB.ChisloUraneniy; j++) dataGridView1_Reshenie.Columns[j].Width = 60;
            arChart = new System.Windows.Forms.DataVisualization.Charting.Chart[VB.ChisloUraneniy + 1]; //
            arChart[1] = chart1; arChart[2] = chart2; arChart[3] = chart3; arChart[4] = chart4; arChart[5] = chart5;
            arChart[6] = chart6; arChart[7] = chart7; arChart[8] = chart8; arChart[9] = chart9;
            for (int i = 1; i <= VB.ChisloUraneniy; i++)
           {
               arChart[i].Titles[0].Text = VB.NameVar[i]; arChart[i].ChartAreas[0].AxisX.Title = "t, c";
               tabControl1_Grafiky.TabPages[i-1].Text = VB.NameVar[i];
               arChart[i].ChartAreas[0].AxisX.Minimum = 0;
           }
           chart1_Oxy.ChartAreas[0].AxisX.Title = "x"; chart1_Oxy.ChartAreas[0].AxisY.Title = "y";
           chart1_Oxz.ChartAreas[0].AxisX.Title = "x"; chart1_Oxz.ChartAreas[0].AxisY.Title = "z";
           chart1_Ozy.ChartAreas[0].AxisX.Title = "z"; chart1_Ozy.ChartAreas[0].AxisY.Title = "y";
           chart1_Oxy.ChartAreas[0].AxisX.LabelStyle.Format = "F1";
           chart1_Oxz.ChartAreas[0].AxisX.LabelStyle.Format = "F1";
           chart1_Ozy.ChartAreas[0].AxisX.LabelStyle.Format = "F1";
           chart1_Oxy.ChartAreas[0].AxisX.Minimum = 0;
           chart1_Oxz.ChartAreas[0].AxisX.Minimum = 0;
           chart1_Ozy.ChartAreas[0].AxisX.Minimum = 0;
            //geo
            textBox1_g.Text = VB.g_ucl.geo.g_N0.ToString();
            textBox1_RadiusZemly.Text = VB.g_ucl.geo.R_geo.ToString();
            textBox1_Gg.Text = VB.g_ucl.geo.Gg.ToString();
            textBox1_delta2_g.Text = VB.g_ucl.geo.delta2_g.ToString();
            textBox1_OmegaZemli.Text = VB.g_ucl.geo.W_geo.ToString();
            //meteo
            textBox1_PN0.Text = VB.m_ucl.norm.PN0.ToString();
            textBox1_roN0.Text = VB.m_ucl.norm.roN0.ToString();
            textBox1_aN0.Text = VB.m_ucl.norm.aN0.ToString();
            textBox1_tauN0.Text = VB.m_ucl.norm.tauN0.ToString();
        }
        public void ZapolnitFormNUandParam()
        {
            int i, j;
            //snaryad
            string law = "";
            if (VB.sn.cx_law == 1) law = "ix43"; else law = "ix58";
            label_ix.Text = "Коэффициент формы ( " + law + " )";
            textBox_ix.Text = VB.sn.ix.ToString(); //коэффициент формы
            textBox_iz.Text = VB.sn.iz.ToString();
            textBox_mx.Text = VB.sn.m_x_omega.ToString();
            textBox1_d.Text = VB.sn.D.ToString();
            textBox1_m.Text = VB.sn.m.ToString();
            textBox1_dlinaSnaryada.Text = Math.Round(VB.sn.l, 3).ToString("0.###");
            textBox1_PolojenCentraMassOtDna.Text = Math.Round(VB.sn.l_cm, 3).ToString("0.###");
            textBox1_l_golova.Text = Math.Round(VB.sn.l_gol, 3).ToString("0.###");
            if (VB.sn.I_x > 1e-4) textBox1_I_x.Text = Math.Round(VB.sn.I_x, 4).ToString("0.####");
            else textBox1_I_x.Text = Math.Round(VB.sn.I_x, 6).ToString("0.######");
            textBox1_Hod_narezov.Text = VB.sn.narez.ToString("F0");
            //Param
            if (VB.sn.Ma == null)
            {
                radioButton1_SvoyLaw.Enabled = false;
                radioButton1_1943.Checked = true;
            }
            else radioButton1_SvoyLaw.Enabled = true;
            if (VB.Cx_law == 1) radioButton1_1943.Checked = true;
            if (VB.Cx_law == 2) radioButton1_1958.Checked = true;
            if (VB.Cx_law == 3) radioButton1_SvoyLaw.Checked = true;
            checkBox_U_vrashen_zemli.Checked = (VB.g_ucl.U_VrashZemli == 1);
            checkBox1_U_uchitivat_veter.Checked = (VB.m_ucl.U_wind == 1);
            if (VB.m_ucl.U_wind_type == 1) radioButton_w1.Checked = true;
            if (VB.m_ucl.U_wind_type == 2) radioButton_w2.Checked = true;
            if (VB.m_ucl.U_wind_type == 3) radioButton_w3.Checked = true;
            textBox_w10.Text = VB.m_ucl.w_h10.ToString();
            textBox_aw.Text = VB.m_ucl.a_w_grad.ToString();
            textBox1_h.Text = VB.dt.ToString();
            textBox_iter.Text = VB.diter.ToString();
            textBox_tk.Text = VB.b_ucl.tk.ToString();
            textBox_xk.Text = VB.b_ucl.xk.ToString();
            textBox_yk.Text = VB.b_ucl.yk.ToString();
            radioButton_yk.Checked = true;
            textBox1_alpha_c_direct.Text = VB.b_ucl.a_dir_grad.ToString();
            textBox_h_c.Text = VB.b_ucl.yk.ToString();
            textBox_w_p.Text = VB.m_ucl.w_p.ToString();
            //NU
            dataGridView1_NachUsloviya.RowCount = VB.ChisloUraneniy + 1;
            for (i = 0; i <= VB.ChisloUraneniy; i++)
                dataGridView1_NachUsloviya.Rows[i].Cells[0].Value = VB.ToF(i, VB.X0_[i]).ToString("0.###");
            dataGridView1_Reshenie.Columns.Clear(); //таблица решений
            dataGridView1_Reshenie.ColumnCount = VB.ChisloUraneniy + 1; dataGridView1_Reshenie.RowCount = 1;
            for (j = 0; j <= VB.ChisloUraneniy; j++) { dataGridView1_Reshenie.Columns[j].Name = VB.NameVar[j]; };
            for (j = 0; j <= VB.ChisloUraneniy; j++) dataGridView1_Reshenie.Columns[j].Width = 60;
        }
        public int SchitatParamsAndNU()
        {
            try
            {
                //cx_law
                if (radioButton1_1943.Checked) VB.Cx_law = 1;
                if (radioButton1_1958.Checked) VB.Cx_law = 2;
                if (radioButton1_SvoyLaw.Checked) VB.Cx_law = 3;
                //geo_ucl
                VB.g_ucl.geo.g_N0 = Convert.ToDouble(textBox1_g.Text);
                VB.g_ucl.geo.R_geo = Convert.ToDouble(textBox1_RadiusZemly.Text);
                VB.g_ucl.geo.Gg = Convert.ToDouble(textBox1_Gg.Text);
                VB.g_ucl.geo.W_geo = Convert.ToDouble(textBox1_OmegaZemli.Text);
                VB.g_ucl.geo.delta2_g = Convert.ToDouble(textBox1_delta2_g.Text);
                VB.g_ucl.B_geo = Convert.ToDouble(textBox1_B_shirota.Text) * Math.PI / 180;
                if (radioButton1_Zemlya_shar.Checked) VB.g_ucl.U_FormaZemli = 1; else VB.g_ucl.U_FormaZemli = 0;
                //snaryad
                VB.sn.D = Convert.ToDouble(textBox1_d.Text);
                VB.sn.l = Convert.ToDouble(textBox1_dlinaSnaryada.Text);
                VB.sn.m = Convert.ToDouble(textBox1_m.Text);
                VB.sn.ix = Convert.ToDouble(textBox_ix.Text);
                VB.sn.l_gol = Convert.ToDouble(textBox1_l_golova.Text);
                VB.sn.l_cm = Convert.ToDouble(textBox1_PolojenCentraMassOtDna.Text);
                VB.sn.iz = Convert.ToDouble(textBox_iz.Text);
                VB.sn.I_x = Convert.ToDouble(textBox1_I_x.Text);
                //VB.sn.I_z = Convert.ToDouble(textBox1_I_z.Text);
                //VB.MxConst = checkBox_mx_const.Checked;
                //VB.Gobar = checkBox_Gobar.Checked;
                VB.sn.m_x_omega = Convert.ToDouble(textBox_mx.Text);
                //meteo_ucl
                if (checkBox1_U_uchitivat_veter.Checked) VB.m_ucl.U_wind = 1;
                else VB.m_ucl.U_wind = 0;
                if (VB.m_ucl.U_wind == 1)
                {
                    if (radioButton_w1.Checked) VB.m_ucl.U_wind_type = 1;
                    if (radioButton_w2.Checked) VB.m_ucl.U_wind_type = 2;
                    if (radioButton_w3.Checked) VB.m_ucl.U_wind_type = 3;
                    VB.m_ucl.w_h10 = Convert.ToDouble(textBox_w10.Text);
                    VB.m_ucl.a_w_grad = Convert.ToDouble(textBox_aw.Text);
                    VB.m_ucl.w_p = Convert.ToDouble(textBox_w_p.Text);
                }
                //nominal
                VB.m_ucl.norm.PN0 = Convert.ToDouble(textBox1_PN0.Text);
                VB.m_ucl.norm.roN0 = Convert.ToDouble(textBox1_roN0.Text);
                VB.m_ucl.norm.aN0 = Convert.ToDouble(textBox1_aN0.Text);
                VB.m_ucl.norm.tauN0 = Convert.ToDouble(textBox1_tauN0.Text);
                //real
                VB.m_ucl.P0 = Convert.ToDouble(textBox_P0.Text);
                VB.m_ucl.T0 = Convert.ToDouble(textBox_T0.Text);
                //
                //VB.t0 = Convert.ToDouble(dataGridView1_NachUsloviya.Rows[0].Cells[0].Value);
                VB.dt = Convert.ToDouble(textBox1_h.Text);
                VB.diter = Convert.ToInt32(textBox_iter.Text);
                double xi;
                for (int i = 0; i <= VB.ChisloUraneniy; i++)
                {
                    xi = Convert.ToDouble(dataGridView1_NachUsloviya.Rows[i].Cells[0].Value);
                    VB.X0_[i] = VB.FromF(i, xi);
                }
                //dataGridView1_NachUsloviya.Rows[9].Cells[0].Value=VB.m_ucl.pi_0(VB.b_ucl.y0).ToString("0.###");
                //VB.X0_[9] = Convert.ToDouble(dataGridView1_NachUsloviya.Rows[9].Cells[0].Value);
                VB.InitToNU();
                //ball_ucl
                VB.b_ucl.a_direct = Convert.ToDouble(textBox1_alpha_c_direct.Text) * Math.PI / 180;
                VB.b_ucl.yk = Convert.ToDouble(textBox_yk.Text);
                if (radioButton_tk.Checked) VB.b_ucl.tk = Convert.ToDouble(textBox_tk.Text); else VB.b_ucl.tk = 0;
                if (radioButton_xk.Checked) VB.b_ucl.xk = Convert.ToDouble(textBox_xk.Text); else VB.b_ucl.xk = 0;
                dataGridView1_Reshenie.RowCount = 1;
                if (VB.m_ucl.U_wind == 0)
                {
                    VB.delta_Cxk_ot_eps_veter = 0; VB.delta_Cyk_ot_eps_veter = 0; VB.delta_Czk_ot_eps_veter = 0; VB.delta_V_w_veter = 0;
                }
                VB.InitFromNU();//!!!
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                MessageBox.Show("Ошибка: " + mes);
                return 1;
            }
            return 0;
        }
        private void button_hcNU_Click(object sender, EventArgs e)
        {
            //VertNU(VB.b_ucl.hc_bool);
        }
        private void button1_Click(object sender, EventArgs e)//Расчет!!!
        {
            this.Cursor = Cursors.WaitCursor;
            //----
            //VB.calc = true;
            VB.stop = false;
            label16_Tkonech.Text = ""; label1_Dalnost.Text = ""; label1_y_max.Text = "";

            res = SchitatParamsAndNU();
            if (res == 0) 
            VB.Raschet_Vivod(true, ref arChart, ref chart1_Oxy, ref chart1_Oxz, ref chart1_Ozy, ref dataGridView1_Reshenie);
            
            label16_Tkonech.Text = "Длительность полета составила " + VB.Itogo_T.ToString("0.###") + "  c";
            label1_Dalnost.Text = "Дальность полета снаряда составила " + VB.Itogo_D.ToString("0.###") + "  м";
            label1_y_max.Text = "Максимальная высота подъема снаряда " + VB.Itogo_y_max.ToString("0.###") + "  м";
            this.Cursor = Cursors.Default;
        }
        private void chart1_Click(object sender, EventArgs e) 
        {   
        }
        private void button2_Click(object sender, EventArgs e)  
        { 
            VB.stop = true;
        }
        private void button3_Save_Click(object sender, EventArgs e)
        {           
        }
        private void button3_Click(object sender, EventArgs e)//сохранить в блокнот
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Файл данных (*.csv)|*.csv";
            saveDialog.InitialDirectory = GetDataDir() + "\\result";
            if (saveDialog.ShowDialog() == DialogResult.Cancel) return;
            bool podrobno = podrobno_checkBox.Checked;
            res = VB.SaveTo_Csv(saveDialog.FileName, podrobno, ref mes);
            if (res != 0) MessageBox.Show("Ошибка: " + mes);
            else MessageBox.Show("Данные успешно записаны!");
        }
        private void button1_IzExcel_Click(object sender, EventArgs e)
        {
        }
        private void button4_Click(object sender, EventArgs e)
        {
        }
        private void button5_Raschet_omega_x0_Click(object sender, EventArgs e)
        {
            VB.b_ucl.V0 = Convert.ToDouble(dataGridView1_NachUsloviya.Rows[5].Cells[0].Value);
            VB.sn.narez = Convert.ToDouble(textBox1_Hod_narezov.Text);
            VB.b_ucl.wx0 = VB.sn.Calc_wx0(VB.b_ucl.V0);
            dataGridView1_NachUsloviya.Rows[8].Cells[0].Value = VB.b_ucl.wx0;
        }
        private void radioButton1_1948_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1_1943.Checked)
            {
                VB.Cx_law = 1;
                //checkBox_Gobar.Checked = true;
                textBox_ix.Enabled = true;
                //textBox_iz.Enabled = true;
                //textBox_mx.Enabled = true;
            }
        }
        private void radioButton1_1953_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1_1958.Checked)
            {
                VB.Cx_law = 2;
                //checkBox_Gobar.Checked = true;
                textBox_ix.Enabled = true;
                //textBox_iz.Enabled = true;
                //textBox_mx.Enabled = true;
            }
        }
        private void radioButton1_SvoyLaw_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1_SvoyLaw.Checked)
            {
                VB.Cx_law = 3;
                checkBox_mx_const.Checked = false;
                //checkBox_Gobar.Checked = false;
                textBox_ix.Enabled = false;
                //textBox_iz.Enabled = false;
                //textBox_mx.Enabled = false;
            }
        }
        private void checkBox_U_vrashen_zemli_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_U_vrashen_zemli.Checked) VB.g_ucl.U_VrashZemli = 1;
            else VB.g_ucl.U_VrashZemli = 0;
            if (checkBox_U_vrashen_zemli.Checked) { groupBox_geo.Enabled = true; radioButton1_Zemlya_shar.Checked = true; }
            else { groupBox_geo.Enabled = false; radioButton1_ZemlyaPloskaya.Checked = true; }
        }
        private void button1_I_x_Click(object sender, EventArgs e)
        {
            double chislo2 = VB.sn.Calc_I_x();
            if (chislo2 > 1e-4) textBox1_I_x.Text = chislo2.ToString("0.####");
            else textBox1_I_x.Text = chislo2.ToString("0.######");
        }
        private void button1_Iz_Click(object sender, EventArgs e)
        {
            double chislo2 = VB.sn.Calc_I_z();
            if (chislo2 > 1e-4) textBox1_I_z.Text = chislo2.ToString("0.####");
            else textBox1_I_z.Text = chislo2.ToString("0.######");
        }

        public async Task LoadNUandParamFromJson(string fname) //загрузка данных*************************************************************************************
        {
            try
            {
                using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate))
                {
                    VB = await JsonSerializer.DeserializeAsync<VneshBall>(fs);
                    fs.Close();
                }
                await VB.init(dir);
            }
            catch (Exception e)
            {
                //mes = e.Message;
                //return 1;
            }
            //return 0;
        }

        private async void button1_OpenNU_and_params_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файл данных (*.json)|*.json";
            openDialog.InitialDirectory = dir + "\\initialdata";
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
            await LoadNUandParamFromJson(openDialog.FileName);
            res = 0;//???
            if (res != 0)
            {
                MessageBox.Show("Ошибка: " + mes);
                return;
            }
            ZapolnitFormNUandParam();
        }
        private void button1_SaveNU_Click(object sender, EventArgs e)
        {
            SchitatParamsAndNU();
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Файл данных (*.json)|*.json";
            saveDialog.InitialDirectory = GetDataDir() + "\\initialdata";
            if (saveDialog.ShowDialog() == DialogResult.Cancel) return;
            VB.SaveNUandParamToJson(saveDialog.FileName);
        }
        double ToDouble(string s)
        {
            if (s == null) return 0;
            if (s == "") return 0;
            double a = 0;
            try
            {
                a = double.Parse(s);
            }
            catch
            {
                if (s.IndexOf(',')>-1)
                a = double.Parse(s.Replace(',','.'));
                else a = double.Parse(s.Replace('.', ','));
            }
            return a;
        }

        async void LoadSnaryad(string f)
        {
            await VB.LoadSnaryadfromJson(f, dir);
            res = 0;
            if (res != 0)
            {
                MessageBox.Show("Загрузка параметров снаряда. Ошибка: " + mes);
                System.Windows.Forms.Application.Exit();
            }
            VB.b_ucl.V0 = VB.sn.Vs0;//!!!
            VB.b_ucl.teta0_grad = 10;
            dataGridView1_NachUsloviya.Rows[5].Cells[0].Value = VB.sn.Vs0;
            dataGridView1_NachUsloviya.Rows[6].Cells[0].Value = 10;
            VB.InitFromNU();//!!!
            string law = "";
            if (VB.sn.cx_law == 1) law = "ix43"; else law = "ix58";
            label_ix.Text = "Коэффициент формы ( " + law + " )";
            textBox_ix.Text = VB.sn.ix.ToString(); //коэффициент формы
            textBox_iz.Text = VB.sn.iz.ToString();
            textBox_mx.Text = VB.sn.m_x_omega.ToString();
            textBox1_d.Text = VB.sn.D.ToString();
            textBox1_m.Text = VB.sn.m.ToString();
            textBox1_dlinaSnaryada.Text = Math.Round(VB.sn.l, 3).ToString("0.###");
            textBox1_PolojenCentraMassOtDna.Text = Math.Round(VB.sn.l_cm, 3).ToString("0.###");
            textBox1_l_golova.Text = Math.Round(VB.sn.l_gol, 3).ToString("0.###");
            if (VB.sn.I_x > 1e-4) textBox1_I_x.Text = Math.Round(VB.sn.I_x, 4).ToString("0.####");
            else textBox1_I_x.Text = Math.Round(VB.sn.I_x, 6).ToString("0.######");
            //if (VB.sn.I_z > 1e-4) textBox1_I_z.Text = Math.Round(VB.sn.I_z, 4).ToString("0.####");
            //else textBox1_I_z.Text = Math.Round(VB.sn.I_z, 6).ToString("0.######");
            textBox1_Hod_narezov.Text = VB.sn.narez.ToString("F0");
            button5_Raschet_omega_x0_Click(this, new EventArgs());
            if (VB.sn.Ma == null)
            { radioButton1_SvoyLaw.Enabled = false;
                radioButton1_1943.Checked = true;
            }
            else radioButton1_SvoyLaw.Enabled = true;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файл данных (*.json)|*.json";
            openDialog.InitialDirectory = dir + "\\snaryad";
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
            LoadSnaryad(openDialog.FileName);            
        }
        private void buttonSave_sn_Click(object sender, EventArgs e)
        {
            SchitatParamsAndNU();
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Файл данных (*.json)|*.json";
            saveDialog.InitialDirectory = GetDataDir() + "\\snaryad";
            if (saveDialog.ShowDialog() == DialogResult.Cancel) return;
            VB.sn.SaveToJson(saveDialog.FileName);
            //VB.sn.emp.SaveToJson(GetDataDir() + "\\cx\\emp_aero_func.json");
        }

        private void button_meteo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файл данных (*.csv)|*.csv";
            openDialog.InitialDirectory = dir + "\\meteo";
            if (openDialog.ShowDialog() == DialogResult.Cancel) return;
            res = VB.m_ucl.LoadRealFromCsv(openDialog.FileName, ref mes);
            if (res != 0) MessageBox.Show("Ошибка: " + mes);
        }

        private void textBox_h_c_TextChanged(object sender, EventArgs e)
        {
            textBox_yk.Text = textBox_h_c.Text;
        }
        private void radioPk()
        {
            textBox_yk.Enabled = radioButton_yk.Checked;
            textBox_tk.Enabled = radioButton_tk.Checked;
            textBox_xk.Enabled = radioButton_xk.Checked;
            if (!radioButton_tk.Checked) textBox_tk.Text = "";
            if (!radioButton_xk.Checked) textBox_xk.Text = "";
        }
        private void radioButton_yk_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_yk.Checked) radioPk();
        }
        private void radioButton_tk_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_tk.Checked) radioPk();
        }
        private void radioButton_xk_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_xk.Checked) radioPk();
        }

        private void checkBox1_U_uchitivat_veter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1_U_uchitivat_veter.Checked) groupBox_veter.Enabled = true;
            else groupBox_veter.Enabled = false;
        }

        private void radioButton_w3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_w3.Checked)
            {
                textBox_w10.Enabled = false;
                textBox_aw.Enabled = false;
            }
            else
            {
                textBox_w10.Enabled = true;
                textBox_aw.Enabled = true;
            }
        }

        private void button_meteo1_Click(object sender, EventArgs e)
        {
            button_meteo_Click(sender, e);
        }

        private void radioButton_atm_nom_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_atm_nom.Checked) VB.m_ucl.U_norm_real = 0;
            else VB.m_ucl.U_norm_real = 1;
        }

        private void textBox_P0mm_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double P0mm = Convert.ToDouble(textBox_P0mm.Text);
                textBox_P0.Text = (P0mm * 133.322).ToString("F1"); 
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void button_Down_Click(object sender, EventArgs e)
        {
            dataGridView1_Reshenie.FirstDisplayedCell = dataGridView1_Reshenie.Rows[dataGridView1_Reshenie.Rows.Count - 1].Cells[0];
        }

        private void button_Up_Click(object sender, EventArgs e)
        {
            dataGridView1_Reshenie.FirstDisplayedCell = dataGridView1_Reshenie.Rows[0].Cells[0];
        }

        private void textBox_T0C_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double T0C = Convert.ToDouble(textBox_T0C.Text);
                textBox_T0.Text = (T0C + 273.15).ToString("F2");
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

    }//программный енд
}




