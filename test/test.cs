using System;

namespace MatrixMultiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] A = { { 1, 2 }, { 3, 4 } };
            double p = 1;
            double q = 2;
            double theta = 90;
            double[,] J = GetJacobiMatrix(p, q, theta);
            double[,] result = TimesJ(A, J);
            Console.WriteLine("Result: ");
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Console.Write(result[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static double[,] TimesJ(double[,] A, double[,] J)
        {
            int n = A.GetLength(0);
            double[,] result = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        result[i, j] += A[i, k] * J[k, j];
                    }
                }
            }
            return result;
        }

        static double[,] GetJacobiMatrix(double p, double q, double theta)
        {
            double[,] J = { { Math.Cos(theta), -Math.Sin(theta) }, { Math.Sin(theta), Math.Cos(theta) } };
            return J;
        }
    }
}

