using System;
using static System.Console;
using static System.Math;
using System.Diagnostics;

class Program {
    public static matrix random_matrix(int n, int m){
        var rnd = new System.Random(1);
        matrix A = new matrix(n, m);
        for (int i = 0; i < n; i++){
            for (int j = 0; j < m; j++){
                A[i,j] = rnd.NextDouble();                
            }  
        }
        return A;}

    
    static void Main(string[] args) {
        int N = 0;
        foreach (var arg in args) {
            var word = arg.Split(":");
            if (word[0] == "-size") {
                N = int.Parse(word[1]);   
            }
        
        matrix A = random_matrix(N, N);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        (matrix Q, matrix R) = QRGS.decomp(A);
        sw.Stop();

        using (var file = new System.IO.StreamWriter("out.times.data", true))
        {
            file.WriteLine($"{N} {sw.Elapsed.TotalSeconds}");
        }
        }
        Check_decomp_function();
        Check_solve_function();
        Check_inverse_function();

    }    
    
    public static void Check_decomp_function(){
        int n = 4;
        int m = 3;

        matrix A = random_matrix(n,m);
        QRGS.det(A);
        (matrix Q, matrix R) = QRGS.decomp(A);
        
        WriteLine("Checking that R is upper triangular");
        bool checker = true;
        for (int i = 0; i < R.size2; i++){
            if (matrix.approx(R[i,i], 0)) checker = false;
            for (int j = 0; j > i ; j++){
                if (matrix.approx(R[i,j], 0)){ 
                    checker = false;
                }
            }
        }
        if (checker == true) WriteLine("R is upper triangular matrix");
        else WriteLine("R is not triangular matrix");
        

        WriteLine("Checking that Q^TQ = 1");
        if ((Q.transpose() * Q).approx(matrix.id(Q.size1))) WriteLine("Q^TQ = 1 is true");
        else{WriteLine("Q^TQ is not equal to identity");}

        WriteLine("Checking if QR = A");
        matrix QR = Q * R;
        if (QR.approx(A)) WriteLine("QR = A is true");
        else{WriteLine("QR = A is false");}
    } // decomp_function

    public static void Check_solve_function(){
        var rnd = new System.Random(1);
        int n = rnd.Next(2,5);
        matrix A = new matrix(n, n);
        vector b = new vector(n);
        for (int i = 0; i < A.size1; i++){  //iterate rows
            for (int j = 0; j < A.size2; j++){
                A[i,j] = rnd.NextDouble();
                b[i] = rnd.NextDouble();
            }  
        }
        WriteLine("Solving QRx=b");
        vector x = QRGS.solve(A, b);
        Write("x=");
        x.print();
        WriteLine("Checking if Ax=b");
        if((A*x).approx(b)) WriteLine("Ax=b is true");
        else WriteLine("Ax=b is false");
    } // solve_function

    public static void Check_inverse_function(){
        Random rnd = new Random();
        int n = rnd.Next(2,5);
        matrix A = random_matrix(n, n);
        matrix B = QRGS.inverse(A);
        Console.WriteLine("Matrix A");
        A.print();
        Console.WriteLine("Matrix B");
        B.print();
    } // inverse_function
}
