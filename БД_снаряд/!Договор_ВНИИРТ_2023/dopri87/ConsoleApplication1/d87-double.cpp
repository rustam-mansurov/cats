#include <cstdio>
#include <corecrt_math.h>
#include "fcn.h"
#include "d87-double.h"
#define _CRT_SECURE_NO_WARNINGS

// ввел названия как на фортране
double dmax4(double ay, double ay1, double ay2, double ay3)
{
	double w;
	w = ay;
	if (ay1 > w) w = ay1;
	if (ay2 > w) w = ay2;
	if (ay3 > w) w = ay3;
	return w;
}

double dmax1(double a, double b)
{
	if (a > b) return a;
	else      return b;
}

double dmin1(double a, double b)
{
	if (a < b) return a;
	else      return b;
}

double c2 = 1.e0 / 18.e0,
c3 = 1.e0 / 12.e0,
c4 = 1.e0 / 8.e0,
c5 = 5.e0 / 16.e0,
c6 = 3.e0 / 8.e0,
c7 = 59.e0 / 400.e0,
c8 = 93.e0 / 200.e0,
c9 = 5490023248.e0 / 9719169821.e0,
c10 = 13.e0 / 20.e0,
c11 = 1201146811.e0 / 1299019798.e0,
c12 = 1.e0,
c13 = 1.e0,
a21 = 1.e0 / 18.e0,
a31 = 1.e0 / 48.e0,
a32 = 1.e0 / 16.e0,
a41 = 1.e0 / 32.e0,
a43 = 3.e0 / 32.e0,
a51 = 5.e0 / 16.e0,
a53 = -75.e0 / 64.e0,
a54 = 75.e0 / 64.e0,
a61 = 3.e0 / 80.e0,
a64 = 3.e0 / 16.e0,
a65 = 3.e0 / 20.e0,
a71 = 29443841.e0 / 614563906.e0,
a74 = 77736538.e0 / 692538347.e0,
a75 = -28693883.e0 / 1125.e6,
a76 = 23124283.e0 / 18.e8,
a81 = 16016141.e0 / 946692911.e0,
a84 = 61564180.e0 / 158732637.e0,
a85 = 22789713.e0 / 633445777.e0,
a86 = 545815736.e0 / 2771057229.e0,
a87 = -180193667.e0 / 1043307555.e0,
a91 = 39632708.e0 / 573591083.e0,
a94 = -433636366.e0 / 683701615.e0,
a95 = -421739975.e0 / 2616292301.e0,
a96 = 100302831.e0 / 723423059.e0,
a97 = 790204164.e0 / 839813087.e0,
a98 = 800635310.e0 / 3783071287.e0,
a101 = 246121993.e0 / 1340847787.e0,
a104 = -37695042795.e0 / 15268766246.e0,
a105 = -309121744.e0 / 1061227803.e0,
a106 = -12992083.e0 / 490766935.e0,
a107 = 6005943493.e0 / 2108947869.e0,
a108 = 393006217.e0 / 1396673457.e0,
a109 = 123872331.e0 / 1001029789.e0,
a111 = -1028468189.e0 / 846180014.e0,
a114 = 8478235783.e0 / 508512852.e0,
a115 = 1311729495.e0 / 1432422823.e0,
a116 = -10304129995.e0 / 1701304382.e0,
a117 = -48777925059.e0 / 3047939560.e0,
a118 = 15336726248.e0 / 1032824649.e0,
a119 = -45442868181.e0 / 3398467696.e0,
a1110 = 3065993473.e0 / 597172653.e0,
a121 = 185892177.e0 / 718116043.e0,
a124 = -3185094517.e0 / 667107341.e0,
a125 = -477755414.e0 / 1098053517.e0,
a126 = -703635378.e0 / 230739211.e0,
a127 = 5731566787.e0 / 1027545527.e0,
a128 = 5232866602.e0 / 850066563.e0,
a129 = -4093664535.e0 / 808688257.e0,
a1210 = 3962137247.e0 / 1805957418.e0,
a1211 = 65686358.e0 / 487910083.e0,
a131 = 403863854.e0 / 491063109.e0,
a134 = -5068492393.e0 / 434740067.e0,
a135 = -411421997.e0 / 543043805.e0,
a136 = 652783627.e0 / 914296604.e0,
a137 = 11173962825.e0 / 925320556.e0,
a138 = -13158990841.e0 / 6184727034.e0,
a139 = 3936647629.e0 / 1978049680.e0,
a1310 = -160528059.e0 / 685178525.e0,
a1311 = 248638103.e0 / 1413531060.e0,
b1 = 14005451.e0 / 335480064.e0,
b6 = -59238493.e0 / 1068277825.e0,
b7 = 181606767.e0 / 758867731.e0,
b8 = 561292985.e0 / 797845732.e0,
b9 = -1041891430.e0 / 1371343529.e0,
b10 = 760417239.e0 / 1151165299.e0,
b11 = 118820643.e0 / 751138087.e0,
b12 = -528747749.e0 / 2220607170.e0,
b13 = 1.e0 / 4.e0,
bh1 = 13451932.e0 / 455176623.e0,
bh6 = -808719846.e0 / 976000145.e0,
bh7 = 1757004468.e0 / 5645159321.e0,
bh8 = 656045339.e0 / 265891186.e0,
bh9 = -3867574721.e0 / 1518517206.e0,
bh10 = 465885868.e0 / 322736535.e0,
bh11 = 53011238.e0 / 667516719.e0,
bh12 = 2.e0 / 45.e0;

int dopri8(int n, double x, double* y, double xend, double eps, double hmax, double h)
{
	int i, reject;
	double* k1 = new double[n];
	double* k2 = new double[n];
	double* k3 = new double[n];
	double* k4 = new double[n];
	double* k5 = new double[n];
	double* k6 = new double[n];
	double* k7 = new double[n];
	double* y1 = new double[n];

	double y11s, y12s;
	double posneg, // направление рассчета (1 или -1)
		t = 1.e-10, // ограничение снизу начального шага
		uround = 2.e-16, // некоторая минимальная погрешность
		err, // оценка локальной погрешности на шаге
		fac, denon, hnew, xph;
	FILE* fout = NULL;


	// Подготовительные действия
	fout = fopen("data.txt", "w");
	fprintf(fout, "%lf %lf %lf %lf %lf\n", x, y[0], y[1], y[2], y[3]);
	if (xend - x > 0) posneg = 1.0; else posneg = -1.0;
	hmax = fabs(hmax);
	h = dmin1(dmax1(t, fabs(h)), hmax);
	h = h * posneg;
	if (eps < 2.e-15)eps = 2.e-15;	// ограничение на точность на шаге   

	// printf("dopri8 start\n");

	// основной цикл по времени
	while (eps < (xend - x) * posneg && eps * fabs(xend) < (xend - x) * posneg)
	{
		if ((x + h - xend) * posneg > 0.e0) h = xend - x; // выбор шага, чтобы попасть в конец отрезка
		fcn(n, x, y, k1);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * a21 * k1[i];
		fcn(n, x + c2 * h, y1, k2);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (a31 * k1[i] + a32 * k2[i]);
		fcn(n, x + c3 * h, y1, k3);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (a41 * k1[i] + a43 * k3[i]);
		fcn(n, x + c4 * h, y1, k4);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (a51 * k1[i] + a53 * k3[i] + a54 * k4[i]);
		fcn(n, x + c5 * h, y1, k5);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (a61 * k1[i] + a64 * k4[i] + a65 * k5[i]);
		fcn(n, x + c6 * h, y1, k6);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (a71 * k1[i] + a74 * k4[i] + a75 * k5[i] + a76 * k6[i]);
		fcn(n, x + c7 * h, y1, k7);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (a81 * k1[i] + a84 * k4[i] + a85 * k5[i] + a86 * k6[i] + a87 * k7[i]);
		fcn(n, x + c8 * h, y1, k2);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (a91 * k1[i] + a94 * k4[i] + a95 * k5[i] + a96 * k6[i] + a97 * k7[i] + a98 * k2[i]);
		fcn(n, x + c9 * h, y1, k3);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (a101 * k1[i] + a104 * k4[i] + a105 * k5[i] + a106 * k6[i] + a107 * k7[i] + a108 * k2[i] + a109 * k3[i]);
		// Экономия памяти и пересохранение данных	       
		//--------------------------------------------	
		for (i = 0; i < n; i++)
		{
			y11s = a111 * k1[i] + a114 * k4[i] + a115 * k5[i] + a116 * k6[i] + a117 * k7[i] + a118 * k2[i] + a119 * k3[i];
			y12s = a121 * k1[i] + a124 * k4[i] + a125 * k5[i] + a126 * k6[i] + a127 * k7[i] + a128 * k2[i] + a129 * k3[i];
			k4[i] = a131 * k1[i] + a134 * k4[i] + a135 * k5[i] + a136 * k6[i] + a137 * k7[i] + a138 * k2[i] + a139 * k3[i];
			k5[i] = b1 * k1[i] + b6 * k6[i] + b7 * k7[i] + b8 * k2[i] + b9 * k3[i];
			k6[i] = bh1 * k1[i] + bh6 * k6[i] + bh7 * k7[i] + bh8 * k2[i] + bh9 * k3[i];
			k2[i] = y11s;
			k3[i] = y12s;
		}
		//------------------------------------------------------------
		fcn(n, x + c10 * h, y1, k7);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (k2[i] + a1110 * k7[i]);
		fcn(n, x + c11 * h, y1, k2);
		xph = x + h;
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (k3[i] + a1210 * k7[i] + a1211 * k2[i]);
		fcn(n, xph, y1, k3);
		for (i = 0; i < n; i++) y1[i] = y[i] + h * (k4[i] + a1310 * k7[i] + a1311 * k2[i]);
		fcn(n, xph, y1, k4);
		for (i = 0; i < n; i++)
		{
			k5[i] = y[i] + h * (k5[i] + b10 * k7[i] + b11 * k2[i] + b12 * k3[i] + b13 * k4[i]);
			k6[i] = y[i] + h * (k6[i] + bh10 * k7[i] + bh11 * k2[i] + bh12 * k3[i]);
		}
		// вычисление погрешности 
		err = 0.0;
		for (i = 0; i < n; i++)
		{
			denon = dmax4(1.e-6, fabs(k5[i]), fabs(y[i]), 2.e0 * uround / eps);
			err += ((k5[i] - k6[i]) / denon) * ((k5[i] - k6[i]) / denon);
		}
		err = sqrt(err / n);
		fac = dmax1((1.e0 / 6.e0), dmin1(3.e0, pow(err / eps, 0.125) / .9e0));
		// горизонтальная процедура автоматического выбора шага
		hnew = h / fac;
		if (err <= eps)
		{
			for (i = 0; i < n; i++) y[i] = k5[i];
			x = xph;
			// Здесь можно вывести траекторию на экран
			fprintf(fout, "%lf %lf %lf %lf %lf\n", x, y[0], y[1], y[2], y[3]);

			if (fabs(hnew) > hmax) hnew = posneg * hmax;
			if (reject) hnew = posneg * dmin1(fabs(hnew), fabs(h));
			reject = 0;
			h = hnew;
		}
		else
		{
			// это отладочная печать для проверки непринятых шагов
			//     printf("%lf %lf\n",x,hnew);
			reject = 1;
			h = hnew;
		}
	}
	return 0;
}
