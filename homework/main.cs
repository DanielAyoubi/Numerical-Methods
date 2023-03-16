public static class QRGS
{
    public static void decomp(matrix a, matrix r)
    {
        int n = a.size1;
        int m = a.size2;

        for (int j = 0; j < m; j++)
        {
            double norm = 0;

            for (int i = 0; i < n; i++)
            {
                norm += a[i, j] * a[i, j];
            }

            norm = Math.Sqrt(norm);
            r[j, j] = norm;

            for (int i = 0; i < n; i++)
            {
                a[i, j] /= norm;
            }

            for (int k = j + 1; k < m; k++)
            {
                double dot = 0;

                for (int i = 0; i < n; i++)
                {
                    dot += a[i, j] * a[i, k];
                }

                r[j, k] = dot;

                for (int i = 0; i < n; i++)
                {
                    a[i, k] -= r[j, k] * a[i, j];
                }
            }
        }
    }

    public static vector solve(matrix Q, matrix R, vector b)
    {
        int n = Q.size1;
        int m = Q.size2;

        // Calculate Q^T * b
        vector y = new vector(m);
        for (int j = 0; j < m; j++)
        {
            double dot = 0;

            for (int i = 0; i < n; i++)
            {
                dot += Q[i, j] * b[i];
            }

            y[j] = dot;
        }

        // Solve Rx = y using back-substitution
        vector x = new vector(m);
        for (int j = m - 1; j >= 0; j--)
        {
            double dot = 0;

            for (int k = j + 1; k < m; k++)
            {
                dot += R[j, k] * x[k];
            }

            x[j] = (y[j] - dot) / R[j, j];
        }

        return x;
    }

    public static double det(matrix R)
    {
        double det = 1;

        for (int i = 0; i < R.size1; i++)
        {
            det *= R[i, i];
        }

        return det;
    }
}

