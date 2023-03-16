using System.Reflection.Metadata;
using System;
using static System.Console;
using static System.Math;

public static class QRGS{
    public static void decomp(matrix a, matrix r){
        int n = a.size1;  // number of rows
        int m = a.size2;  // number of columns
        
        for (int i = 0; i < m; i++){
            double vector_norm = 0.0;
            for (int j = 0; j < n; j++){
                vector_norm += Sqrt(a[i,j] * a[i,j]);
                r[i,i] = vector_norm;
                a[i,j] /= vector_norm;
                
            }
            
        
        }


    }

        

    
}
