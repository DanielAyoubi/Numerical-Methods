using static System.Console;

public  class vec{
public  double x,y,z = 0;

        public void print(string s){Write(s);WriteLine($"{x} {y} {z}");}
    public  vec(){
        x=y=z=0;   

    }
    public vec(double a, double b, double c){
        x=a; y=b; z=c;
    }

    public static vec operator+(vec u, vec v){
        return new vec (u.x + v.x, u.y+v.y, u.z+v.z);
    }


}
