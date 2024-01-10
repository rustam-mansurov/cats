using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rayon1DataSet.Узлы". При необходимости она может быть перемещена или удалена.
            this.узлыTableAdapter.Fill(this.rayon1DataSet.Узлы);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rayon1DataSet.Районы". При необходимости она может быть перемещена или удалена.
            this.районыTableAdapter.Fill(this.rayon1DataSet.Районы);
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
            panel1.Hide();
            color = Color.FromArgb(255,0,255,0);
        }

        void Card()
        {
            if (checkBox2.Checked == true) gMapControl1.MapProvider = GMapProviders.GoogleMap;
            else gMapControl1.MapProvider = GMapProviders.EmptyProvider;        
        }
        void Polygon()
        {
            panel1.Refresh();
            if (checkBox1.Checked == true)
            {
                //gMapControl1.Overlays.Clear();
                panel1.Show();
                DrawPolygon();
                gMapControl1.Zoom += 0.0001;
                gMapControl1.Zoom -= 0.0001;
                gMapControl1.Refresh();
            }

            else 
            {
                panel1.Hide();
                gMapControl1.Overlays.Clear();
                gMapControl1.Refresh();
            } 

        }

        private void DrawPolygon()
        {
            GMapOverlay polygonOverlay = new GMapOverlay("polygon");
            GMarkerGoogle marker;
            GMapOverlay markerOverlay = new GMapOverlay("markers");
            List<PointLatLng> listPoints = new List<PointLatLng>();
            GMapPolygon mappolygon;
            int j = 0;
            //легенда
            Legend(out Color[] colorPolygon, out double min, out double max, out int grids, out double step);
            /////////
            int countItems = dataGridView1.RowCount - 1;
            int[] v = new int[2];
            Color colorBoundary = Color.FromArgb(200, 255, 0, 0);
            while (j < countItems)
            {
                listPoints.Clear();
                v[0] = Convert.ToInt32(dataGridView1[5, j].Value);
                v[1] = Convert.ToInt32(dataGridView1[6, j].Value);
                for (int i = v[0] - 1; i < v[1] - 1; i++)
                {
                    listPoints.Add(new PointLatLng((Convert.ToDouble(dataGridView3[1, i].Value) / 1000000), Convert.ToDouble(dataGridView3[2, i].Value) / 1000000));
                }
                mappolygon = new GMapPolygon(listPoints, "polygon" + j.ToString())
                {
                    Stroke = new Pen(colorBoundary)
                };
                int n = Getnumber(Convert.ToDouble(dataGridView1[2, j].Value), min, max, grids, step);
                mappolygon.Fill = new SolidBrush(colorPolygon[n]);

                polygonOverlay.Polygons.Add(mappolygon);
                marker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(dataGridView1[3, j].Value) / 1000000,
                    Convert.ToDouble(dataGridView1[4, j].Value) / 1000000), GMarkerGoogleType.green)
                {
                    ToolTipText = Convert.ToString(dataGridView1[1, j].Value),
                    ToolTipMode = MarkerTooltipMode.Always
                };
                /*if (checkBox1.Checked == true)
                {
                    marker.ToolTipMode = MarkerTooltipMode.Always;
                }
                else
                {
                    //gMapControl1.Overlays.Clear();
                    marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                }*/
                markerOverlay.Markers.Add(marker);
                j++;
            }

            gMapControl1.Overlays.Add(polygonOverlay);
            gMapControl1.Overlays.Add(markerOverlay);

            /*GMapProviders.GoogleMap.ApiKey = @"AIzaSyBDOSrmPItAIPLL9EjPFh_hDXuw2wO_hwI";//@"AIzaSyCuy98k2KM7vUyF9sI0-R7X68QdIE-YynY";
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gMapControl1.CacheLocation = @"cache";
            PointLatLng start = new PointLatLng(0, 0);
            PointLatLng end = new PointLatLng(1, 1);
            GetDistanceByRoute(start.Lat, start.Lng, end.Lat, end.Lng);*/
        }

        private void Legend(out Color[] colorPolygon, out double min, out double max, out int grids, out double step)
        {
            int red, green, blue;
            int hred, hgreen, hblue;
            Color color1 = Color.FromArgb(255, 255, 255, 255);
            Pen pen = new Pen(Color.Black,0.002f);
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
                //colorPolygon[i] = Color.FromArgb(255, 0, 255 - (int)(255 * i * step / max), 0);
                /*colorPolygon[i] = Color.FromArgb(255, Convert.ToInt32(color.R * (1 - i * step / max)), 
                    Convert.ToInt32(color.G * (1-i*step/max)), Convert.ToInt32(color.B * (1 - i * step / max)));*/
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

        public static double GetDistanceByRoute(double startLat, double startLng, double endLat, double endLng)
        {
            GoogleMapProvider.Instance.ApiKey = "AIzaSyCuy98k2KM7vUyF9sI0-R7X68QdIE-YynY";

            //GMap.NET.MapProviders.YandexMapProvider.Instance
            //GMap.NET.MapProviders.BingMapProvider.Instance. .Instance.ApiKey = "Your Api Key";
            PointLatLng start = new PointLatLng(startLat, startLng);
            PointLatLng end = new PointLatLng(endLat, endLng);
            MapRoute route = GMap.NET.MapProviders.GoogleMapProvider.Instance.GetRoute(start, end, false, false, 15);
            return route.Distance;
        }

        void Marshrut(double startLat, double startLng, double endLat, double endLng)
        {
            PointLatLng start = new PointLatLng(startLat, startLng);
            PointLatLng end = new PointLatLng(endLat, endLng);
            List<PointLatLng> listroute = new List<PointLatLng>
            {
                start,
                end
            };
            GMapRoute r = new GMapRoute(listroute, "My route");
            GMapOverlay routesOverlay = new GMapOverlay("routes");
            routesOverlay.Routes.Add(r);
            gMapControl1.Overlays.Add(routesOverlay);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Marshrut(Convert.ToDouble(textBox1.Text)/1000000, Convert.ToDouble(textBox2.Text) / 1000000,
                Convert.ToDouble(textBox3.Text) / 1000000, Convert.ToDouble(textBox4.Text) / 1000000);           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Polygon();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Card();
        }

        Color color;
        private void panel1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                color = colorDlg.Color;
                Polygon();
            }
        }
    }
}
