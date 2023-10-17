#include <math.h>
#include "fcn.h"
// n=4 - число переменных : L, phi - фазовые переменные; v0, h - параметры

void fcn(int n, double t, double* ymas, double* f)
{
	//	double L;
	double phi, v0, h;
	//    L=ymas[0];	 // метры
	phi = ymas[1]; // радианы <-> [2-12] градусов
	v0 = ymas[2];	 // м/с [300 - 500]
	h = ymas[3];   // м [100 - 1000]

	f[0] = v0 * cos(phi);
	f[1] = v0 * sin(phi) * sin(phi) / h;
	// параметры включены в состав сист.диф.ур для сохранения структуры вызовов стандартной программы
	f[2] = 0.0;
	f[3] = 0.0;

	return;
}
