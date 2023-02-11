using static System.Console;
public static class Program{
    public static void print(this double x,string s){ /* x.print("x="); */
		Write(s);WriteLine(x);
		}
   public static void Main(){
        vec v = new vec(1,2,3);
        vec u = new vec(4,5,6);
        
        (u+v).print("u+v= ");
        vec vec_1 = u+v;
        vec_1.print("u+v= ");

    }
}
