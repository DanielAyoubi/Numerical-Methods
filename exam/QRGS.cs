using System;
using static System.Console;
using static System.Math;

public static class QRGS{
    public static (matrix,matrix) decomp(matrix A){
        matrix Q = A.copy();
        matrix R = new matrix(Q.size2, Q.size2);

        for (int i = 0; i < Q.size2; i++){
            var norm = Q[i].norm();
            if(Abs(norm) < 1e-9){
                WriteLine($"Warning: Norm of column {i} is close to zero.");
                R[i,i] = norm; // prevent division by zero
            }
            else{
                R[i,i] = norm;
            }
            Q[i] /= R[i,i];
            for (int j = i+1; j < Q.size2; j++){
                R[i,j] = Q[i].dot(Q[j]);
                Q[j] -= Q[i] * R[i,j];
            }            
        }
        return (Q,R);
    }

    public static vector solve(matrix A, vector b){
        (matrix Q, matrix R) = QRGS.decomp(A);
        vector c = Q.transpose() * b;
        for (int i = c.size-1; i >= 0; i--){
            double sum = 0;
            for (int k = i+1; k < c.size; k++) sum+= R[i,k]*c[k];
            if (Abs(R[i,i]) < 1e-9){
                WriteLine($"Warning: Diagonal element R[{i},{i}] is close to zero.");
                c[i] = 0; // prevent division by zero
            } 
            else {
                c[i] = (c[i]-sum) / R[i,i];
            }

        }
        return c;
    }

    public static double det(matrix A){
        matrix R = QRGS.decomp(A).Item2;
        double detQ = 1;
        double detR = 1;
        for (int i = 0; i < R.size2; i++){
            if (Abs(R[i,i]) < 1e-9) {
                WriteLine($"Warning: Diagonal element R[{i},{i}] is close to zero.");
            }
            detR *= R[i,i];
        }
        double detA = detQ * detR;
        WriteLine($"The determinant of matrix {A.ToString()} is {detA}");
        return detA;
    }

    public static matrix inverse(matrix A){
        double detA = det(A);
        if (Abs(detA) < 1e-9){
            WriteLine("Warning: Matrix is singular or nearly singular. Inverse might not exist.");
            return null; // or return the original matrix, or throw an exception
        }

        matrix R = QRGS.decomp(A).Item2;
        matrix A_inverse = new matrix(R.size1, R.size2);
        for (int i = 0; i < R.size1; i++){
            vector e = new vector(R.size1); e[i] = 1;
            A_inverse[i] = solve(A,e);
        }
        return A_inverse;
    }
}
