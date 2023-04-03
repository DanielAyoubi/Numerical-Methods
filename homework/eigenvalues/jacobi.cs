using System;
using static System.Console;

public static class jacobi{
	public static void timesJ(matrix A, int p, int q, double theta){
		double c = Math.Cos(theta);
		double s = Math.Sin(theta);
		for(int i=0;i<A.size1;i++){
			double aip = A[i,p];
			double aiq = A[i,q];
			A[i,p] = c*aip - s*aiq;
			A[i,q] = s*aip + c*aiq;
		}
	}

	public static void Jtimes(matrix A, int p, int q, double theta){
		double c = Math.Cos(theta);
		double s = Math.Sin(theta);
			for(int j=0;j<A.size1;j++){
				double apj = A[p,j];
				double aqj = A[q,j];
				A[p,j] =  c*apj + s*aqj;
				A[q,j] = -s*apj + c*aqj;
			}
	}

	public static void cyclic(matrix A, matrix V){
		bool changed;
		do{
			changed=false;
			for(int p=0;p<A.size1-1;p++)
			 	for(int q=p+1;q<A.size1;q++){
			 		double apq=A[p,q], app=A[p,p], aqq=A[q,q];
			 		double theta=0.5*Math.Atan2(2*apq,aqq-app);
			 		double c=Math.Cos(theta),s=Math.Sin(theta);
			 		double new_app=c*c*app-2*s*c*apq+s*s*aqq;
			 		double new_aqq=s*s*app+2*s*c*apq+c*c*aqq;
			 		if(new_app!=app || new_aqq!=aqq) // do rotation
			 			{
			 			changed=true;
			 			timesJ(A,p,q, theta); // A←A*J 
			 			Jtimes(A,p,q,-theta); // A←JT*A 
			 			timesJ(V,p,q, theta); // V←V*J
			 			}
			 	}
		}while(changed);
	}
}
