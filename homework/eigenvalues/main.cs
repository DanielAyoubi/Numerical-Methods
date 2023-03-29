using System;
using static System.Console;
using static System.Math;

public class main{ 
	public static void Main(){
        jacobi_decomposition();
        /* hydrogen_atom(); */



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
    public static void hydrogen_atom(){
        


    }

}
