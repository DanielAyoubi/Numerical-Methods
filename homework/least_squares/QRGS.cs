using System;
using static System.Console;
using static System.Math;

public static class QRGS{
        // Task A. //
    public static (matrix,matrix) decomp(matrix A){
        matrix Q = A.copy();
        matrix R = new matrix(Q.size2, Q.size2);

        for (int i = 0; i < Q.size2; i++){
            R[i,i] = Q[i].norm();
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
            c[i] = (c[i]-sum) / R[i,i]; 
        }
        return c;
    }

    public static double det(matrix A){
        #pragma warning disable CS0219
        (matrix Q, matrix R) = QRGS.decomp(A);
        double detQ = 1; // orthogonal -> always 1
        double detR = 1;
        for (int i = 0; i < R.size2; i++){
            detR *= R[i,i];
        }
        double detA = detQ * detR;
        WriteLine($"The determinant of matrix {A.ToString()} is {detA}");
        return detQ * detR;
    }



    public static matrix inverse(matrix A){
        (matrix Q, matrix R) = QRGS.decomp(A);
        matrix A_inverse = new matrix(R.size1, R.size2);
        for (int i = 0; i < R.size1; i++){
            vector e = new vector(R.size1); e[i] = 1;
            A_inverse[i] = solve(R.transpose(), Q * e);
        }
        return A_inverse;


    }
}
