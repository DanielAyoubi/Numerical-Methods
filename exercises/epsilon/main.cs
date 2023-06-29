using System;
using static System.Console;

class program{
    static void Main(){
        int i = 1;
        int j = 1;
        while(i+1 > i){i++;}
        WriteLine($"my max int = {i}");
        while(j-1 < i){j--;}
        WriteLine($"my min int = {j}");
        
        
        double x = 1;
        while(x+1 != 1){
        x /= 2;
        }
        x*=2;
        Console.WriteLine(x);
        Console.WriteLine(Math.Pow(2,-23));

        float y = 1F;
        while(y+1 != 1){
        y /= 2;
        }
        y*=2;
        Console.WriteLine(y);

        int n=(int)1e6;
        double epsilon=Math.Pow(2,-52);
        double tiny=epsilon/2;
        double sumA=0,sumB=0;
        
        sumA += 1;
        for(int k=0;k<n;k++){sumA+=tiny;}
        for(int k=0;k<n;k++){sumB+=tiny;} 
        sumB+=1;

        WriteLine($"sumA-1 = {sumA-1:e} should be {n*tiny:e}");
        WriteLine($"sumB-1 = {sumB-1:e} should be {n*tiny:e}");
   
        WriteLine(approx(1.0,1.0));
    }

        public static bool approx(double a, double b, double acc=1e-9, double eps=1e-9){
            if(Math.Abs(a-b) < acc){return true;}

            else if(Math.Abs(a-b) < Math.Max(Math.Abs(a), Math.Abs(b))*eps){return true;}
            else return false;

        }
}
