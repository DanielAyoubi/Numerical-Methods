using System;
using static System.Console;
using System.IO;
using static System.Math;

public class main{ 
	public static void Main(){
        double rmax = 10;
        double dr = 0.1;
        jacobi_decomposition();
        investigateconvergence();
        eigenfunctions(rmax, dr, 3);
        
    }

    public static void jacobi_decomposition(){
        // Task A.
		Random rnd = new System.Random(1);
		int n = 4;
		matrix A = new matrix(n,n);
		for (int i = 0; i< A.size1; i++){
			for (int j = 0; j<A.size2; j++){
                double r = rnd.NextDouble();
				A[i,j] = r;
				A[j,i] = r;
			}
  		}
        matrix V = matrix.id(A.size1);
        Console.WriteLine("Original matrix:");
        A.print();
        matrix A_original = A.copy();
        jacobi.cyclic(A, V);  // Eigenvalue decomposiiton
        Console.WriteLine("Transformed matrix:");
        A.print();

        Write("Checking if V^(T)AV==D --> ");
        if((V.transpose()*A_original*V).approx(A)) WriteLine("true");
        else WriteLine("false");
        
        Write("Checking if VDV^(T)==A --> ");
        if((V*A*V.transpose()).approx(A_original)) WriteLine("true");
        else WriteLine("false");
        

        Write("Checking if V^(T)V==1 --> ");
        if((V.transpose()*V).approx(matrix.id(A.size1))) WriteLine("true");
        else WriteLine("false");

        Write("Checking if VV^(T)==1 --> ");
        if((V*V.transpose()).approx(matrix.id(A.size1))) WriteLine("true");
        else WriteLine("false");
    }

    // Task B
    public static void investigateconvergence(){
    double rmax_fixed = 10;
    double[] dr_values = {0.1, 0.2, 0.3, 0.4, 0.5 , 0.6, 0.7, 0.8, 0.9, 1.0};
    var outfile = new StreamWriter("different_dr.data");

    foreach (double dr in dr_values){
        matrix H = hydrogen_atom.diagonlize(rmax_fixed, dr);
        double epsilon0 = hydrogen_atom.lowesteigenvalue(H);
        outfile.WriteLine($"{dr} {epsilon0}");
    }
    outfile.Close();

    double dr_fixed = 0.3;
    double[] rmax_values = {1,2,3,4,5,6,7,8,9,10};
    var outfile1 = new StreamWriter("different_rmax.data");

    foreach (double rmax in rmax_values) {
        matrix H = hydrogen_atom.diagonlize(rmax, dr_fixed);
        double epsilon0 = hydrogen_atom.lowesteigenvalue(H);
        outfile1.WriteLine($"{rmax} {epsilon0}");
    }
    outfile1.Close();
    }

    public static void eigenfunctions(double rmax, double dr, int nStates){
        vector eigenfunction;
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
        matrix V = matrix.id(H.size1);
        jacobi.cyclic(H,V);

        double min = H[0,0];
        int index = 0;
        for(int i = 0; i < H.size1; i++){
            if(H[i,i] < min){ 
                min = H[i,i];
                index = i;
            }
        }
        eigenfunction = V[index];

        vector analytical_solution = new vector(npoints);
        double a_0 = 1.0; // Bohr radius

        for (int i = 0; i < npoints; i++) {
            analytical_solution[i] = 1 / (Sqrt(PI) * Pow(a_0, 1.5)) * Math.Exp(-r[i] / a_0);
        }

        var outfile2 = new StreamWriter("eigenfunctions.data");
        for(int i = 0; i < r.size; i++){
            outfile2.WriteLine($"{r[i]} {eigenfunction[i]} {eigenfunction[i]/r[i]} {analytical_solution[i]} ");
        }
        outfile2.Close();

    }
}

