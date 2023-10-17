using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Vnesh_ballistic
{
    public class polynom
    {
        public string _objectType;
        public int N;
        public int maxPower;
        public double[] X;
        public double[,] Y;
        public polynom(int pN=0,int pm=0)
        {
            _objectType = "type-polynom-gost";
            N = pN; maxPower = pm;
            if (N>0)
            {
                X = new double[N];
                Y = new double[N, maxPower + 1];
            }
        }
        static double powint(double x, int m)
        {
            double pow = 1;
            for (int j = 1; j <= m; j++)
                pow *= x;
            return pow;
        }
        public double getY(double x)
        {
            int ii = 0;
            for(int i=0;i<N;i++)
                if (x>X[i]) { ii = i; break; }
            double sum = 0;
            for (int j = 0; j <= maxPower; j++)
                sum += Y[ii, j] * powint(x, j);
            return sum;
        }
        static public double[] setcoef2(double[] x, double[] y)
        {
            Mat3 M = new Mat3(1, x[0], x[0] * x[0], 1, x[1], x[1] * x[1], 1, x[2], x[2] * x[2]);
            double detM = Mat3.det(M);
            M = new Mat3(y[0], x[0], x[0] * x[0], y[1], x[1], x[1] * x[1], y[2], x[2], x[2] * x[2]);
            double det0 = Mat3.det(M);
            M = new Mat3(1, y[0], x[0] * x[0], 1, y[1], x[1] * x[1], 1, y[2], x[2] * x[2]);
            double det1 = Mat3.det(M);
            M = new Mat3(1, x[0], y[0], 1, x[1], y[1], 1, x[2], y[2]);
            double det2 = Mat3.det(M);
            double[] a = new double[3];
            a[0] = det0 / detM; a[1] = det1 / detM; a[2] = det2 / detM;
            return a;
        }
        public void Topoly(string st)
        {
            if ((st==null)||(st == "")) return;
            string[] s = st.Split('"');
            char[] ch = new char[2] { ':', ',' };
            _objectType = s[3];
            string nst = s[8].Trim(ch);
            N = Convert.ToInt32(nst);
            string mst = s[10].Trim(ch);
            maxPower = Convert.ToInt32(mst);
            X = new double[N];
            Y = new double[N, maxPower+1];
            //ch = new char[3] { ':', '[', ']' };
            string xst = s[12].Replace(':', ' ');
            xst = xst.Replace('[', ' ');
            xst = xst.Replace(']', ' ');
            string[] x = xst.Split(',');
            for (int i = 0; i < N; i++)
            {
                xst = x[i].Replace('.', ',');
                X[i] = Convert.ToDouble(xst);
            }
            string yst = s[14].Replace(':', ' ');
            yst = yst.Replace('[', ' ');
            yst = yst.Replace(']', ' ');
            yst = yst.Replace('}', ' ');
            string[] y = yst.Split(',');
            for (int i = 0; i < N; i++)
                for (int j = 0; j <= maxPower; j++)
                {
                    yst = y[i * (maxPower + 1) + j].Replace('.', ',');
                    Y[i, j] = Convert.ToDouble(yst);
                }
        }
        public string Frompoly()
        {
            string xst = "[";
            for (int i = 0; i < N; i++)
                xst += (Math.Round(X[i],5).ToString()).Replace(',','.') + ",";
            xst = xst.TrimEnd(','); xst += "]";
            string yst = "[";
            for (int i = 0; i < N; i++)
            {
                yst += "[";
                for (int j = 0; j <= maxPower; j++)
                    yst += (Math.Round(Y[i,j], 5).ToString()).Replace(',', '.') + ",";
                yst = yst.TrimEnd(','); yst += "],";
            }
            yst = yst.TrimEnd(','); yst += "]";
            string st = "\"{\\\"_objectType\\\":\\\"" + _objectType + "\\\",\\\"value\\\":{\\\"N\\\":\\\"" + N.ToString() + 
                "\\\",\\\"X\\\":" + xst + ",\\\"Y\\\":" + yst + ",\\\"maxPower\\\":\\\"" + maxPower.ToString() + "\\\"}}\\n\"";
            return st;
            // "{\"S\":\"{\\\"_objectType\\\":\\\"type-polynom-gost\\\",\\\"value\\\":{\\\"N\\\":\\\"40\\\",\\\"X\\\":
            //[0.0010,0.005589956294988587,0.01495766109793960,0.03318776469320874,0.07005359215798766,0.1556158426939401,0.3535430301277960,0.7530635961695711,1.320081568532780,1.977983322294579,2.759177997581378,3.456553324915549,3.616045458037695,3.619978319511712,3.620546399502402,3.620559571795649,3.620560137197620,3.620562351652966,3.621040674007585,3.724358302605365,5.448880285902086,7.393570990833309,9.554694408580113,11.72144048395582,13.75647940673881,15.68023422261669,17.36087259797806,18.71173322684108,20.52186744888455,22.39881749094739,24.35209050364604,26.38227715206637,28.45559649934204,30.68617010812630,33.19043416411247,35.85456103578922,38.22584338053828,40.77740151213844,43.80041843891758,45.0]
            //,\\\"Y\\\":[[3.644230924830372e-009,299.9999937189099,-8.661863974476241],[2.204067720976079e-007,299.9999131172872,-8.653966668061363],[3.306961551363501e-006,299.9994801918726,-8.638319844754394],[3.413290664820365e-005,299.9975479084562,-8.607324271582337],[0.0003258284548629739,299.9889952984948,-8.543339175840977],[0.003506597681873364,299.9468762911213,-8.400582226329341],[0.03542812435312470,299.7584795311196,-8.115602073361465],[0.2408142852887067,299.1736187440090,-7.690366336514501],[0.9019692962361887,298.1016015994892,-7.250959936219488],[2.380485924646042,296.5278950097958,-6.829513857316391],[5.402544099356519,294.2396567757803,-6.394379239312859],[8.114658112995270,292.6085498817425,-6.149062698956653],[8.589040844342447,292.3445994397514,-6.112347391047360],[8.604988397079405,292.3357868271539,-6.111129929320445],[8.602639256005428,292.3370844900938,-6.111309136214778],[14.30705647310620,289.1859596872820,-5.676138147444488],[20.85435923022098,285.5692259789367,-5.176666716141533],[8.599107327463564,292.3390355157062,-6.111578571234108],[8.639031089069524,292.3169906692803,-6.108535426020384],[12.89513315748348,290.1121305386197,-5.823352788543316],[32.65559292229190,282.4861778933218,-5.083356852883332],[70.14978837657199,271.9214037815572,-4.336343172844277],[135.2259414331615,257.7934464370720,-3.567498592267471],[229.4262312241336,241.2258425533219,-2.837880002382331],[355.5139465742286,222.4417203641741,-2.137549131311918],[508.303793422450,202.5373291479706,-1.488860559815235],[664.9663893007062,184.1593928407648,-0.9496764347229512],[838.4500344657054,165.4399979093097,-0.4445663312495336],[1077.837135515915,141.7565530890317,0.1414357204669545],[1338.556921110154,118.1412944283857,0.6763669819085059],[1609.402465940183,95.57814903118052,1.146428286048206],[1866.003425431626,75.84741559564455,1.525823434254037],[2085.973760223964,60.17260326162926,1.805136707303850],[2255.948088590197,48.92651270290352,1.991205453941368],[2339.744229420913,43.76848219089729,2.070598004376177],[2298.756500424607,46.04606511547362,2.038959084484733],[2146.605461627024,54.06093219589745,1.933392335021018],[1834.726408433811,69.48032379432803,1.742769202645931],[1360.696273147984,91.40419309652809,1.489218850876761],[0.0,0.0,0.0]]
            //,\\\"maxPower\\\":\\\"2\\\"}}\\n\"}"
        }
    }//class polynom
    public class Weapon
    {
        public int id;
        public double passive_mass;
        public double caliber;
        public double length;
        public double center_to_bottom;
        public double axial_inertia_moment;
        public double head_length;
        public double shape_coefficient;
        public double derivation_coefficient;
        public string name;
        public int c_x_m;
        public cxm_coef cxm_c = new cxm_coef();
        public polynom cxm;
        public int material;
        public string m_x_omega_x;
        public polynom mxomegax;
        public string geometry;
        public string scheme;
        public void initpoly()
        {
            mxomegax = new polynom();
            mxomegax.Topoly(m_x_omega_x);
            cxm = new polynom();
            cxm.Topoly(cxm_c.value);
        }
    }
    public class cxm_coef
    {
        public int id;
        public string model;
        public string value;
    }
    public class meteo
    {
        public int id;
        public int unit;
        public string time;
        public double lat;
        public double lon;
        public string tau_y;
        public polynom tauy;
        public string p_y;
        public polynom py;
        public string omega_y;
        public polynom omegay;
        public string alpha_omega_y;
        public polynom alphaomegay;
        public string omega_ver_y;
        public polynom omegavery;
        public void initpoly()
        {
            tauy = new polynom();
            tauy.Topoly(tau_y);
            py = new polynom();
            py.Topoly(p_y);
            omegay = new polynom();
            omegay.Topoly(omega_y);
            alphaomegay = new polynom();
            alphaomegay.Topoly(alpha_omega_y);
            omegavery = new polynom();
            omegavery.Topoly(omega_ver_y);
        }
    }
    public class outball_task
    {
        public int id;
        public int status;
        public string Params;
        public string name;
        public int authorId;
        public DateTime activeFrom;
        public DateTime activeTill;
        public int priority;
        public string memo;

        const int STATUS_CANCELLED = 0; // Задача отменена
        const int STATUS_NEW = 1; // Задача только создана
        const int STATUS_CANCELLED_WITH_ERROR = 2; // Задача выполнена с ошибкой. Комментарий к ошибке в поле memo
        const int STATUS_PROCESSING = 50; // Задача выполняется модулем
        const int STATUS_COMPLETED = 100; // Задача выполнена

    }
    public class outball_task_params
    {
        public int id;
        public double t0, x0, y0, z0;
        public double V0, theta0, psy0;
        public double omega_x, alpha_c;
        public int artmount_id;
        public int weapon_id;
        public int meteo_id;
    }
    public class trajectory_result
    {
        public int id;
        public int task_id;
        public polynom S_, D_, X_, Y_, Z_, Vk_, theta_, psi_, psi_theta_, omegax_;
        public string S, D, X, Y, Z, Vk, theta, psi, psi_theta, omegax;
        public int N;
        public int maxPower;
        public trajectory_result(int pN=40, int pm=2)
        {
            N = pN; maxPower = pm;
            S_ = new polynom(N, maxPower);
            D_ = new polynom(N, maxPower);
            X_ = new polynom(N, maxPower);
            Y_ = new polynom(N, maxPower);
            Z_ = new polynom(N, maxPower);
            Vk_ = new polynom(N, maxPower);
            theta_ = new polynom(N, maxPower);
            psi_ = new polynom(N, maxPower);
            psi_theta_ = new polynom(N, maxPower);
            omegax_ = new polynom(N, maxPower);
        }
        public string Tostr(int task)
        {
            task_id = task;
            S = S_.Frompoly();
            D = D_.Frompoly();
            X = X_.Frompoly();
            Y = Y_.Frompoly();
            Z = Z_.Frompoly();
            Vk = Vk_.Frompoly();
            theta = theta_.Frompoly();
            psi = psi_.Frompoly();
            psi_theta = psi_theta_.Frompoly();
            omegax = omegax_.Frompoly();
            string st = "{\"task_id\":" + task_id.ToString() + ",\"S\":" + S + ",\"D\":" + D + ",\"x\":" + X + ",\"y\":" + Y + ",\"z\":" + Z +
                ",\"Vk\":" + Vk + ",\"theta\":" + theta + ",\"psi\":" + psi + ",\"psi_theta\":" + psi_theta + ",\"omegax\":" + omegax + "}";
            return st;
        }
    }
    public class InterfaceDB
    {
        HttpClient client = null;
        string url = "http://api.model.fgrep.ru/";
        String user = "vsufiy";
        String pass = "khDy3wPEPRd4rq";
        string log = "izhgtu_outside_ballistics";
        string key = "ZgDSz4JLfdOx_pGQ66P-eTdyrcmfbqQq";
        public Weapon weap = new Weapon();
        public meteo met = new meteo();
        public outball_task task = new outball_task();
        public outball_task_params task_params = new outball_task_params();
        public trajectory_result res = new trajectory_result();

        public async Task LoadtaskFromDB(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            String base64string = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(log + ":" + key));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64string);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string qstr = "/v1/data-tables/izhgtu_out_ball_task/" + id.ToString() + "?fields=" +
                "id,t0,x0,y0,z0,V0,theta0,psy0,omega_x,alpha_c,weapon_id,meteo_id," + " HTTP/1.1";
            HttpResponseMessage response = await client.GetAsync(qstr);

            if (response.IsSuccessStatusCode)
            {
                task_params = await response.Content.ReadAsAsync<outball_task_params>();
                //***
                await LoadweapFromDB(task_params.weapon_id);
                await LoadmeteoFromDB(task_params.meteo_id);
            }
            else
            {
                String x = "HTTP Status: " + response.StatusCode.ToString() + " - Reason: " + response.ReasonPhrase;
                Console.WriteLine(x);
                x = await response.Content.ReadAsStringAsync();
                System.Windows.Forms.MessageBox.Show(x);
            }
        }
        public async Task LoadtaskparamsFromDB(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            String base64string = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(log + ":" + key));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64string);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string qstr = "/v1/data-tables/izhgtu_out_ball_task/" + id.ToString() + "?fields=" +
                "id,t0,x0,y0,z0,V0,theta0,psy0,omega_x,alpha_c,weapon_id,meteo_id," + " HTTP/1.1";
            HttpResponseMessage response = await client.GetAsync(qstr);

            if (response.IsSuccessStatusCode)
            {
                task_params = await response.Content.ReadAsAsync<outball_task_params>();
                //***
                await LoadweapFromDB(task_params.weapon_id);
                await LoadmeteoFromDB(task_params.meteo_id);
            }
            else
            {
                String x = "HTTP Status: " + response.StatusCode.ToString() + " - Reason: " + response.ReasonPhrase;
                Console.WriteLine(x);
                x = await response.Content.ReadAsStringAsync();
                System.Windows.Forms.MessageBox.Show(x);
            }
        }
        public async Task LoadweapFromDB(int id)
        {
            string qstr = "/v1/data-tables/weapons/" + id.ToString() + "?fields=" +
                "id,passive_mass,caliber,length,center_to_bottom,axial_inertia_moment,head_length," +
                "shape_coefficient,derivation_coefficient,name,C_x_M,material,m_x_omega_x,geometry,scheme" + " HTTP/1.1";
            HttpResponseMessage response = await client.GetAsync(qstr);

            if (response.IsSuccessStatusCode)
            {
                weap = await response.Content.ReadAsAsync<Weapon>();
                //***
                await LoadcxmFromDB(weap.c_x_m);
                weap.initpoly();
            }
            else
            {
                String x = "HTTP Status: " + response.StatusCode.ToString() + " - Reason: " + response.ReasonPhrase;
                Console.WriteLine(x);
                x = await response.Content.ReadAsStringAsync();
                System.Windows.Forms.MessageBox.Show(x);
            }
        }
        public async Task LoadcxmFromDB(int id)
        {
            string qstr = "/v1/data-tables/cxm/" + id.ToString() + "?fields=" +
                "id,model,value," + " HTTP/1.1";
            HttpResponseMessage response = await client.GetAsync(qstr);

            if (response.IsSuccessStatusCode)
            {
                weap.cxm_c = await response.Content.ReadAsAsync<cxm_coef>();
            }
            else
            {
                String x = "HTTP Status: " + response.StatusCode.ToString() + " - Reason: " + response.ReasonPhrase;
                Console.WriteLine(x);
                x = await response.Content.ReadAsStringAsync();
                System.Windows.Forms.MessageBox.Show(x);
            }
        }
        public async Task LoadmeteoFromDB(int id)
        {
            string qstr = "/v1/data-tables/meteo/" + id.ToString() + "?fields=" +
                "id,unit,time,lat,lon,TAU_y,omega_y,alpha_omega_y,P_y,omega_ver_y," + " HTTP/1.1";
            HttpResponseMessage response = await client.GetAsync(qstr);

            if (response.IsSuccessStatusCode)
            {
                met = await response.Content.ReadAsAsync<meteo>();
                //***
                met.initpoly();
            }
            else
            {
                String x = "HTTP Status: " + response.StatusCode.ToString() + " - Reason: " + response.ReasonPhrase;
                Console.WriteLine(x);
                x = await response.Content.ReadAsStringAsync();
                Console.WriteLine(x);
            }
        }
        public async Task SaveResToDB()
        {
            string st = res.Tostr(task.id);
            string qstr = "/v1/data-tables/trajectory_results?fields=" +
                 "task_id,S,D,x,y,z,Vk,theta,psi,psi_theta,omegax," + " HTTP/1.1";
            /*
            string qstr1 = "/v1/data-tables/trajectory_results/8?fields=" +
                "task_id,S,D,x,y,z,Vk,theta,psi,psi_theta,omegax," + " HTTP/1.1";
            HttpResponseMessage response1 = await client.GetAsync(qstr1);
            st = await response1.Content.ReadAsStringAsync();
            */
            StringContent content = new StringContent(st, System.Text.Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");//!!!

            HttpResponseMessage response = await client.PostAsync(qstr, content);

            if (response.IsSuccessStatusCode)
            {
                //System.Windows.Forms.MessageBox.Show("Данные успешно сохранены в БД.");
            }
            else
            {
                String x = "HTTP Status: " + response.StatusCode.ToString() + " - Reason: " + response.ReasonPhrase;
                Console.WriteLine(x);
                x = await response.Content.ReadAsStringAsync();
                System.Windows.Forms.MessageBox.Show(x);
            }
        }

    }
}
