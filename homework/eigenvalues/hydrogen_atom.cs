using System;
using static System.Console;
using static System.Math;

public class hydrogen_atom{
    static void Main(string[] args){

    }
    
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

    public static void numerical_eigenfunctions(matrix H, matrix V, double rmax, double dr, int nEigenvectors){
    int npoints = (int)(rmax/dr)-1;
    vector r = new vector(npoints);
    for (int i = 0; i < npoints; i++) r[i] = dr * (i + 1);

        for (int i = 0; i < V.size1; i++){
            for (int j = 0; j < V.size2; j++){
                V[i, j] *= Math.Sqrt(1.0 / dr);
            }
        }
    
    using (var outfile = new System.IO.StreamWriter("numerical_eigenfunctions.data")) {
        for (int i = 0; i < npoints; i++) {
            outfile.Write($"{r[i]} ");
            for (int j = 0; j < nEigenvectors; j++) {
                outfile.Write($"{V[j, i] * V[j, i]} ");
            }
            outfile.WriteLine();
        }
    }


    }

    public static void analytical_eigenfunctions(int nEigenvectors, double rmax, double dr){
    int npoints = (int)(rmax / dr) - 1;
    vector r = new vector(npoints);
    for (int i = 0; i < npoints; i++) r[i] = dr * (i + 1);

    var outfile1 = new System.IO.StreamWriter("analytical_eigenfunctions.data");
    for (int i = 0; i < npoints; i++) {
        outfile1.Write($"{r[i]} ");
        for (int n = 1; n <= nEigenvectors; n++) {
            double psi_r = radial(r[i], n, 0);
            outfile1.Write($"{psi_r * psi_r} ");
        }
        outfile1.WriteLine();
    }

    }

    public static double factorial(int n ){
        double x = 1;
        for (int i = 1; i < n; i++){
            x *= i;
        }
        return x;
    }

    public static double laguerre(int q, int p, double x){
        double sum = 0;
        for (int r = 0; r <= q; r++){
            double coeff = Math.Pow(-1, r) * factorial(q + p) / (factorial(r) * factorial(q - r) * factorial(p + r));
            sum += coeff * Math.Pow(x, r);
        }
        return sum;
    }

       public static double radial(double r, int n, int l){
        double a0 = 0.529; // Bohr radius in Angstroms
        double rho = 2 * r / (n * a0);
        double normalization = Math.Sqrt(Math.Pow(2 / (n * a0), 3) * factorial(n - l - 1) / (2 * n * Math.Pow(factorial(n + l), 3)));
        double R = normalization * Math.Pow(rho, l) * Math.Exp(-rho / 2) * laguerre(n - l - 1, 2 * l + 1, rho);
        return R;
    }
}

