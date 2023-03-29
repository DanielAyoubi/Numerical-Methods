using System;
using static System.Console;
using static System.Math;

public class main{ 
	public static void Main(){
		
		WriteLine("Part A:");
		

		WriteLine("Generate a random matrix A (8x3)");
		var A1 = matrix.random_matrix(8,3);
		A1.print("A=");
		WriteLine("");
		WriteLine("Factorize it into QR by decomposing with call to QRGS.dec(A)");
		var (Q1, R1) = QRGS.dec(A1);
        	Q1.print("Q = ");
        	R1.print("R = ");
		WriteLine("");
		WriteLine("Then Q^TQ=1 and QR=A is checked");
		WriteLine("");
		var test1 = Q1.T * Q1;
		test1.print("Q^TQ= ");
		WriteLine("");
		var test2 = Q1*R1;
		//test2.print("QR=");
		WriteLine($"QR=A ?=> {test2.approx(A1)}");
		WriteLine("");
		WriteLine("#####################");
		WriteLine("");
		WriteLine("Generate a square matrix A (5x5)");
		var A2 = matrix.random_matrix(5,5);
		A2.print("A = ");
		WriteLine("Generate a random vector b with 5 rows");
		var b2 = matrix.random_vector(5);
		b2.print("b = ");
		WriteLine("");
		WriteLine("solve Ax=b with QRx=b");
		var (Q2,R2) = QRGS.dec(A2);
		var x = QRGS.solve(Q2,R2,b2);
		x.print("x = ");
		WriteLine("");
		var test3 = A2*x;
		WriteLine($"Ax=b ?=> {test3.approx(b2)}");  
	
		WriteLine("");
		WriteLine("Part b:");
		WriteLine("Using square matrix A from above and the factorized QR aswell");
		WriteLine("Calculates inverse of A refered to as B");
		var B = QRGS.inverse(Q2,R2);
		var I = QRGS.inverse(Q2,R2);
		B.print("B = ");
		var test4 = A2*B;
		I.set_identity();
		WriteLine($"A*B = I ? => {test4.approx(I)}"); 
				
	}//Main	
}//classmain
