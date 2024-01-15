using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MapSettings();
        }
        private void MapSettings()
        {
            gMapControl1.Bearing = 0;
            gMapControl1.CanDragMap = true;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.GrayScaleMode = true;
            gMapControl1.MaxZoom = 30;
            gMapControl1.MinZoom = 1;
            gMapControl1.PolygonsEnabled = true;
            gMapControl1.MarkersEnabled = true;
            gMapControl1.RoutesEnabled = true;
            gMapControl1.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            gMapControl1.NegativeMode = false;
            gMapControl1.ShowTileGridLines = false;
            gMapControl1.Zoom = 9;
            gMapControl1.Position = new PointLatLng(56, 52);
            GMaps.Instance.Mode = AccessMode.ServerOnly;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            panel1.Hide();
            color = Color.FromArgb(255, 0, 255, 0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) gMapControl1.MapProvider = GMapProviders.GoogleMap;
            else gMapControl1.MapProvider = GMapProviders.EmptyProvider;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                DrawBound();
                gMapControl1.Zoom += 0.0001;
                gMapControl1.Zoom -= 0.0001;
            }
            else
            {
                gMapControl1.Overlays.Remove(boundOverlay);
            }
            gMapControl1.Refresh();
        }

        OleDbConnection myConnection;
        GMapOverlay boundOverlay = new GMapOverlay("bound");
        GMapOverlay polygonOverlay = new GMapOverlay("polygon");
        GMapOverlay nodeOverlay = new GMapOverlay("node");
        GMapOverlay markerOverlay = new GMapOverlay("markers");
        GMapOverlay roadOverlay = new GMapOverlay("road");
        GMarkerGoogle markerNode;
        GMarkerGoogle roadNode;
        readonly Pen penBoundary = new Pen(Color.FromArgb(255, 255, 0, 0), 1);
        public static string connectString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=Roads3.mdb";    

        private void DrawBound()
        {
            gMapControl1.Refresh();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            string query = "SELECT Районы.[Код района], Районы.[Название], Районы.[Лесистость, %], " +
                "Районы.[ШиротаРайона], Районы.[ДолготаРайона], Районы.[Узлыслева], Районы.[Узлысправа] " +
                "FROM Районы";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(dt);
            //dataGridView1.DataSource = dt;
            query = "SELECT Узлы.[Код узла], Узлы.[Широта], Узлы.[Долгота] FROM Узлы";
            command.CommandText = query;
            da.SelectCommand = command;
            da.Fill(dt2);            
            query = "SELECT Границы.[Номер п/п], Границы.[Код района], Границы.[Код узла] FROM Границы";
            command.CommandText = query;
            da.SelectCommand = command;
            da.Fill(dt3);
            //dataGridView1.DataSource = dt3;
            int countItems3 = dt3.Rows.Count;
            int countItems = dt.Rows.Count;
            List<PointLatLng>[] listPoints = new List<PointLatLng>[countItems];
            for (int i = 0; i < listPoints.Length; i++)
            {
                listPoints[i] = new List<PointLatLng>();
            }
            GMapPolygon mappolygon;
            for (int j = 0; j < countItems3; j++)
            {
                int t1 = (int)dt3.Rows[j][1];
                int t2 = (int)dt3.Rows[j][2];
                listPoints[t1-1].Add(
                    new PointLatLng((Convert.ToDouble(dt2.Rows[t2-1][1]) / 1000000), Convert.ToDouble(dt2.Rows[t2-1][2]) / 1000000));
            }
            for (int j = 0; j < countItems; j++)
            {
                mappolygon = new GMapPolygon(listPoints[j], "polygon" + j.ToString());
                mappolygon.Stroke = penBoundary;
                mappolygon.Fill = new SolidBrush(Color.Transparent);
                boundOverlay.Polygons.Add(mappolygon);
            }
            gMapControl1.Overlays.Add(boundOverlay);
         }

        private void DrawPolygon()
        {
            GMarkerGoogle marker;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            string query = "SELECT Районы.[Код района], Районы.[Название], Районы.[Лесистость, %], " +
                 "Районы.[ШиротаРайона], Районы.[ДолготаРайона], Районы.[Узлыслева], Районы.[Узлысправа] " +
                 "FROM Районы";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(dt);
            query = "SELECT Узлы.[Код узла], Узлы.[Широта], Узлы.[Долгота] FROM Узлы";
            command.CommandText = query;
            da.SelectCommand = command;
            da.Fill(dt2);
            //dataGridView2.DataSource = dt2;
            query = "SELECT Границы.[Номер п/п], Границы.[Код района], Границы.[Код узла] FROM Границы";
            command.CommandText = query;
            da.SelectCommand = command;
            da.Fill(dt3);
            int countItems3 = dt3.Rows.Count;
            int countItems = dt.Rows.Count;
            List<PointLatLng>[] listPoints = new List<PointLatLng>[countItems];
            for (int i = 0; i < listPoints.Length; i++)
            {
                listPoints[i] = new List<PointLatLng>();
            }
            GMapPolygon mappolygon1;
            Legend(out Color[] colorPolygon, out double min, out double max, out int grids, out double step);
            Pen penTransparent = new Pen(Color.FromArgb(0, 0, 0, 0), 0);
            for (int j = 0; j < countItems3; j++)
            {
                int t1 = (int)dt3.Rows[j][1];
                int t2 = (int)dt3.Rows[j][2];
                listPoints[t1 - 1].Add(
                    new PointLatLng((Convert.ToDouble(dt2.Rows[t2 - 1][1]) / 1000000), Convert.ToDouble(dt2.Rows[t2 - 1][2]) / 1000000));
            }
            for (int j = 0; j < countItems; j++)
            {
                mappolygon1 = new GMapPolygon(listPoints[j], "polygon" + j.ToString());
                if (checkBox2.Checked == true)
                    mappolygon1.Stroke = penBoundary;
                else
                    mappolygon1.Stroke = penTransparent;
                int n = Getnumber(Convert.ToDouble(dt.Rows[j][2]), min, max, grids, step);
                mappolygon1.Fill = new SolidBrush(colorPolygon[n]);
                polygonOverlay.Polygons.Add(mappolygon1);
                marker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(dt.Rows[j][3]) / 1000000,
                    Convert.ToDouble(dt.Rows[j][4]) / 1000000), GMarkerGoogleType.green)
                {
                    ToolTipText = Convert.ToString(dt.Rows[j][1]),
                    ToolTipMode = MarkerTooltipMode.Always
                };
                markerOverlay.Markers.Add(marker);
            }
             gMapControl1.Overlays.Add(polygonOverlay);
             gMapControl1.Overlays.Add(markerOverlay);
         }

         private void checkBox3_CheckedChanged(object sender, EventArgs e)
         {
             panel1.Refresh();
             if (checkBox3.Checked == true)
             {            
                 panel1.Show();
                 DrawPolygon();
                 gMapControl1.Zoom += 0.0001;
                 gMapControl1.Zoom -= 0.0001;

             }
             else
             {
                 panel1.Hide();
                 gMapControl1.Overlays.Remove(markerOverlay);
                 gMapControl1.Overlays.Remove(polygonOverlay);
             }
             gMapControl1.Refresh();
         }

         private void Form1_FormClosing(object sender, FormClosingEventArgs e)
         {
             myConnection.Close();
         }

         private void Legend(out Color[] colorPolygon, out double min, out double max, out int grids, out double step)
         {
             int red, green, blue;
             int hred, hgreen, hblue;
             Color color1 = Color.FromArgb(255, 255, 255, 255);
             Pen pen = new Pen(Color.Black, 0.002f);
             grids = 5;
             int w = panel1.Width / grids;
             min = 0;
             max = 100;
             step = max / grids;
             Graphics g = panel1.CreateGraphics();
             Rectangle[] rect = new Rectangle[grids];
             Brush[] br = new SolidBrush[grids];
             colorPolygon = new Color[grids];
             Font arialFont = new Font("Arial", 10);
             Font timesFont = new Font("Times New Roman", 12);
             hred = (color1.R - color.R) / grids;
             hgreen = (color1.G - color.G) / grids;
             hblue = (color1.B - color.B) / grids;
             for (int i = 0; i < grids; i++)
             {
                 red = color1.R - hred * i;
                 green = color1.G - hgreen * i;
                 blue = color1.B - hblue * i;
                 colorPolygon[i] = Color.FromArgb(255, red, green, blue);
            rect[i] = new Rectangle(w * i, 0, w, 20);
                br[i] = new SolidBrush(colorPolygon[i]);
                g.FillRectangle(br[i], rect[i]);
                g.DrawRectangle(pen, rect[i]);
                g.DrawString(Convert.ToString(step * i), arialFont, Brushes.Blue, w * i, 20);
            }
            g.DrawString("Лесистость территории, %    Сменить цвет", timesFont, Brushes.Black, 0, 35);
        }

        int Getnumber(double load, double min, double max, int grids, double step)
        {
            double left, right;
            int k = -1;
            for (int i = 0; i < grids; i++)
            {
                left = min + step * i;
                right = left + step;
                if ((load >= left) && (load < right)) k = i;
            }
            if (load <= min) k = 0;
            if (load >= max) k = grids - 1;
            return k;
        }

        Color color;

        private void panel1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                color = colorDlg.Color;
                checkBox3_CheckedChanged(sender,e);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                DrawNodes();
                gMapControl1.Zoom += 0.0001;
                gMapControl1.Zoom -= 0.0001;
            }
            else
            {
                nodeOverlay.Markers.Remove(markerNode);
                gMapControl1.Overlays.Remove(nodeOverlay);
            }
            gMapControl1.Refresh();
        }

        private void DrawNodes()
        {
            DataTable dt3 = new DataTable();
            string query = "SELECT УзлыДорог.[Код узла], УзлыДорог.Широта, УзлыДорог.Долгота, Пункты.Наименование " +
                "FROM Пункты INNER JOIN УзлыДорог ON Пункты.[Код пункта] = УзлыДорог.[Код пункта];";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataAdapter da1 = new OleDbDataAdapter(command);
            da1.Fill(dt3);
            int countPunkt = dt3.Rows.Count;
            for (int j = 0; j < countPunkt; j++)
            {
                markerNode = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(dt3.Rows[j][1]) / 1000000,
        Convert.ToDouble(dt3.Rows[j][2]) / 1000000), GMarkerGoogleType.blue_small)
                {
                    ToolTipText = Convert.ToString(dt3.Rows[j][3]),
                    ToolTipMode = MarkerTooltipMode.OnMouseOver
                    //ToolTipMode = MarkerTooltipMode.Always
                };
                nodeOverlay.Markers.Add(markerNode);
            }

            gMapControl1.Overlays.Add(nodeOverlay);
        }

        void DrawRoads()
        {
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            string query = "SELECT Дороги.[Код дороги], Дороги.[Код маршрута], УзлыДорог.Широта, УзлыДорог.Долгота " +
                "FROM УзлыДорог INNER JOIN Дороги ON УзлыДорог.[Код узла] = Дороги.[Код узла] ORDER BY Дороги.[Код дороги]; ";
        OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(dt4);
            int countItems4 = dt4.Rows.Count;
            
            query = "SELECT Маршруты.[Код маршрута], Маршруты.[Код типа дороги] FROM Маршруты";
            command.CommandText = query;
            da.SelectCommand = command;
            da.Fill(dt5);
            int countItems5 = dt5.Rows.Count;

            /*query = "SELECT Расстояния.[Код расстояния], Пункты_1.Наименование, Пункты_2.Наименование, Расстояния.[Код маршрута] " +
                "FROM(Расстояния INNER JOIN Пункты AS Пункты_1 ON Расстояния.[Код пункта 1] = Пункты_1.[Код пункта]) " +
                "INNER JOIN Пункты AS Пункты_2 ON Расстояния.[Код пункта 2] = Пункты_2.[Код пункта] ORDER BY Расстояния.[Код расстояния]; ";*/
            query = "SELECT * FROM Расстояния ORDER BY Расстояния.[Код расстояния]; ";
            command.CommandText = query;
            da.SelectCommand = command;
            da.Fill(dt6);
            int countItems6 = dt6.Rows.Count;
            
            List<PointLatLng>[] listPoints5 = new List<PointLatLng>[countItems5];
            for (int i = 0; i < listPoints5.Length; i++)
            {
                listPoints5[i] = new List<PointLatLng>();
            }
            for (int j = 0; j < countItems4; j++)
            {
                int t1 = (int)dt4.Rows[j][1];
                listPoints5[t1 - 1].Add(
                    new PointLatLng((Convert.ToDouble(dt4.Rows[j][2]) / 1000000), Convert.ToDouble(dt4.Rows[j][3]) / 1000000));
            }

            GMapRoute[] marshrut = new GMapRoute[countItems5];
            Color colorRoad = Color.FromArgb(255, 0, 0, 0);
            PointLatLng pll,pll2;
            Random random = new Random();
            Bitmap bm = new Bitmap(10, 10);
            for (int j = 0; j < countItems5; j++)
            {
                colorRoad = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                marshrut[j] = new GMapRoute(listPoints5[j], "O" + (j+1).ToString());
                //pll = new PointLatLng(j,j);
                pll = new PointLatLng(listPoints5[j].FirstOrDefault().Lat, listPoints5[j].FirstOrDefault().Lng);
                roadNode = new GMarkerGoogle(pll, bm)
                {
                    ToolTipText = Convert.ToString(marshrut[j].Name),
                    ToolTipMode = MarkerTooltipMode.OnMouseOver
                };
                marshrut[j].Stroke = new Pen(colorRoad, 1f);
                marshrut[j].Stroke.Width = 1f;
                roadOverlay.Routes.Add(marshrut[j]);
                roadOverlay.Markers.Add(roadNode);
                pll2 = new PointLatLng(listPoints5[j].Last().Lat, listPoints5[j].Last().Lng);
                roadNode = new GMarkerGoogle(pll2, bm)
                {
                    ToolTipText = Convert.ToString(j+1),
                    ToolTipMode = MarkerTooltipMode.OnMouseOver
                };
                roadOverlay.Markers.Add(roadNode);
            }

            /*List<int>[,] listrasst = new List<int>[18,18];
for (int i = 0; i < 18; i++)
    for (int j = 0; j < 18; j++)
    {
        listrasst[i, j] = new List<int>();
    }
    for (int j = 0; j < countItems6; j++)
    {
        int t1 = (int)dt6.Rows[j][1];
        int t2 = (int)dt6.Rows[j][2];
        listrasst[t1-1, t2-1].Add((int)dt6.Rows[j][3]);
    }*/
            //расстояния
            int kolpunkt = (int)dt6.Rows[countItems6 - 1][1];
            double[,] masrasst = new double[kolpunkt, kolpunkt];
            for (int j = 0; j < countItems6; j++)
            {
                int t1 = (int)dt6.Rows[j][1];
                int t2 = (int)dt6.Rows[j][2];
                int t3 = (int)dt6.Rows[j][3];
                masrasst[t1 - 1, t2 - 1] += marshrut[t3 - 1].Distance;
            }
            dataGridView1.RowCount = kolpunkt;
            dataGridView1.ColumnCount = kolpunkt;
            for (int i = 0; i < kolpunkt; i++)
                for (int j = 0; j < kolpunkt; j++)
                {
                    dataGridView1[i,j].Value = masrasst[i,j];
                }
            //label1.Text = Convert.ToString(marshrut[0].Distance);
            gMapControl1.Overlays.Add(roadOverlay);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                DrawRoads();
                gMapControl1.Zoom += 0.0001;
                gMapControl1.Zoom -= 0.0001;
            }
            else
            {
                gMapControl1.Overlays.Remove(roadOverlay);
            }
            gMapControl1.Refresh();
        }

    }
}