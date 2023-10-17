#include <math.h>
#include "fcn.h"
// n=4 - ����� ���������� : L, phi - ������� ����������; v0, h - ���������

void fcn(int n, double t, double* ymas, double* f)
{
	//	double L;
	double phi, v0, h;
	//    L=ymas[0];	 // �����
	phi = ymas[1]; // ������� <-> [2-12] ��������
	v0 = ymas[2];	 // �/� [300 - 500]
	h = ymas[3];   // � [100 - 1000]

	f[0] = v0 * cos(phi);
	f[1] = v0 * sin(phi) * sin(phi) / h;
	// ��������� �������� � ������ ����.���.�� ��� ���������� ��������� ������� ����������� ���������
	f[2] = 0.0;
	f[3] = 0.0;

	return;
}
