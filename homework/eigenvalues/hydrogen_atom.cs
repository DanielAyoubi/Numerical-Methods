using System;
using static System.Console;
using static System.Math;

public class hydrogen_atom{
    public static matrix diagonlize(double rmax, double dr){

        // Build Hamiltonian matrix
        int npoints = (int)(rmax/dr)-1;
        vector r = new vector(npoints);
        for(int i=0;i<npoints;i++)r[i]=dr*(i+1);
        matrix H = new matrix(npoints,npoints);
        for(int i=0;i<npoints-1;i++){
        H[i,i]  =-2;
        H[i,i+1]= 1;
        H[i+1,i]= 1;
        }
        H[npoints-1,npoints-1]=-2;
        H *=-0.5/dr/dr;
        for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i];
        
        // Diagonlize Hamiltonian with Jacobi decomposition
        matrix V = matrix.id(H.size1);
        jacobi.cyclic(H,V);

        return H;

    } // diagonalize

    public static double lowesteigenvalue(matrix A){
        double min = A[0,0];
        for(int i = 0; i < A.size1; i++){
            if(A[i,i] < min) min = A[i,i];
        } 
        return min;
    }
}



