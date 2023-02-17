using System;
using static System.Console;
using static System.Math;
namespace vector{
public  class vec{
	public  double x,y,z;
	public  vec (double a,double b,double c){ x=a; y=b; z=c; }
	public void print(string s){Write(s);WriteLine($"{x} {y} {z}");}

	public static vec operator+(vec u,vec v){
		return new vec(u.x+v.x,u.y+v.y,u.z+v.z);
			}

	public static vec operator-(vec u,vec v){return new vec(u.x-v.x,u.y-v.y,u.z-v.z);
			}

	public static vec operator-(vec v){
		return new vec(-v.x,-v.y,-v.z);
			}

	public static vec operator*(vec u,double c){
		return new vec(u.x*c,u.y*c,u.z*c);
	}

	public static vec operator*(double c,vec u){
		return u*c;
	}

	public static double operator% (vec u,vec v){ 
        return u.x*v.x+u.y*v.y+u.z*v.z;
	}

	public double dot(vec u){
        return this.x * u.x + this.y * u.y + this.z * u.z; 
    }

    public static double dot(vec v, vec u){
        return u.x*v.x+u.y*v.y+u.z*v.z;
    }
    public bool approx(double a, double b ,double acc=1e-9, double eps=1e-9){
        if(Math.Abs(a-b) < acc) return true;
        if(Math.Abs(a-b) < (Math.Abs(a) - Math.Abs(b))*eps) return true;
        return false;
    }

    public bool approx(vec v){
        if(!approx(this.x, v.x)) return false;
        if(!approx(this.y, v.y)) return false;
        if(!approx(this.z, v.z)) return false;
        return true;
    }


    public static bool approx(vec u, vec v) => u.approx(v);

    public override string ToString(){
        return $"{x} {y} {z}";
    }

}
}
