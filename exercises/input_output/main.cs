using System;
using static System.Console;
using static System.Math;
class program{
    public static void Main(string[] args){
        /* foreach(var arg in args){ */
        /*     var words = arg.Split(":"); */
        /*     if(words[0] == "-numbers"){ */
        /*         var numbers = words[1].Split(","); */
        /*         foreach(var number in numbers){ */
        /*             double x = double.Parse(number); */
        /*             int i = 1; */
        /*             WriteLine($"x{i}={x}    sin(x{i})={Math.Sin(x)}     cos(x{i})={Math.Cos(x)}"); */
        /*             i++; */
        /*         } */
        /*     } */

        /* } */

        /* char[] split_delimeters = {' ', '\t', '\n'}; */
        /* var split_options = StringSplitOptions.RemoveEmptyEntries; */
        /* for(string line=ReadLine(); line != null; line=ReadLine() ){ */
        /*     var numbers = line.Split(split_delimeters, split_options); */
        /*     foreach(var number in numbers){ */
        /*         double x = double.Parse(number); */
        /*         int i = 1; */
        /*         WriteLine($"x{i}={x}    sin(x{i})={Math.Sin(x)}     cos(x{i})={Math.Cos(x)}"); */
        /*         i++; */

        /*     } */
        /* } */

        string infile=null,outfile=null;
        foreach(var arg in args){
	        var words=arg.Split(':');
	        if(words[0]=="-input")infile=words[1];
	        if(words[0]=="-output")outfile=words[1];
	        }
        if( infile==null || outfile==null) {
	    Error.WriteLine("wrong filename argument");
        return;
	    
	    }
        var instream = new System.IO.StreamReader(infile);
        var outstream = new System.IO.StreamWriter(outfile,append:false);
        for(string line=instream.ReadLine();line!=null;line=instream.ReadLine()){
	        double x=double.Parse(line);
            int i = 1;
            outstream.WriteLine($"x{i}={x}    sin(x{i})={Math.Sin(x)}     cos(x{i})={Math.Cos(x)}");
            i++;
            }
        instream.Close();
        outstream.Close();
    }

}
