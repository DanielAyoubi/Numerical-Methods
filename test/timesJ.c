#include <stdio.h>
#include <math.h>

#define N 2

void timesJ(double A[N][N], double J[N][N], double result[N][N])
{
    int i, j, k;
    for (i = 0; i < N; i++) {
        for (j = 0; j < N; j++) {
            result[i][j] = 0;
            for (k = 0; k < N; k++) {
                result[i][j] += A[i][k] * J[k][j];
            }
        }
    }
}

void getJacobiMatrix(double p, double q, double theta, double J[N][N])
{
    J[0][0] = cos(theta);
    J[0][1] = -sin(theta);
    J[1][0] = sin(theta);
    J[1][1] = cos(theta);
}

int main(void)
{
    double A[N][N] = {{1, 2}, {3, 4}};
    double p = 1;
    double q = 2;
    double theta = 90;
    double J[N][N];
    double result[N][N];
    getJacobiMatrix(p, q, theta, J);
    timesJ(A, J, result);
    printf("Result:\n");
    int i, j;
    for (i = 0; i < N; i++) {
        for (j = 0; j < N; j++) {
            printf("%f ", result[i][j]);
        }
        printf("\n");
    }
    return 0;
}

