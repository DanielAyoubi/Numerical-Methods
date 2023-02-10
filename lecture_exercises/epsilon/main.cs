using System;

class program{
    static void Main(){
        int i = 1;
        int j = 1;
        while(i+1 > i){
            i++;
        }
        while(j-1 < i){j--;}
        /* Console.WriteLine(i); */
        /* Console.WriteLine(j); */
        /* Console.WriteLine(int.MaxValue); */
        /* Console.WriteLine(int.MinValue); */

        
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

    }
    

}
