using System;
using System.IO;
using static System.Console;
using System.Collections.Generic;

public class genlist<T>{   // define a generic class called genlist that takes a type parameter T
	public T[] data;      // declare an array field of type T called data
	public int size => data.Length;  // declare a property called size that returns the length of the data array
	public T this[int i] => data[i];  // declare an indexer that allows accessing elements of the data array using an integer index
	public genlist(){ data = new T[0]; }  // define a constructor that initializes the data array to an empty array
	public void add(T item){ /* add item to the list */
		T[] newdata = new T[size+1];  // create a new array of type T that is one element larger than the current data array
		System.Array.Copy(data,newdata,size);  // copy the elements from the current data array to the new array
		newdata[size]=item;  // add the new item to the end of the new array
		data=newdata;  // set the data array to the new array
	}

		public void remove(int index) {
        if (index < 0 || index >= size) {
            throw new IndexOutOfRangeException();
        }
        
        T[] newdata = new T[size-1];
        int j = 0;
        
        for (int i = 0; i < size; i++) {
            if (i != index) {
                newdata[j] = data[i];
                j++;
            }
        }
        data = newdata;
    	}

	}


class generic_list{
    static void Main(string[] args){
    var list1 = new genlist<int>();
	list1.add(1);
	list1.add(2);
	list1.add(3);
	list1.add(400);
	int k = 0;
	list1.remove(k); // remove item at index n
	WriteLine(list1.size);

       var list = new genlist<double[]>();
		char[] delimiters = {' ','\t'};
		var options = StringSplitOptions.RemoveEmptyEntries;

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
		
		using (var instream = new System.IO.StreamReader(infile)) { 
	        using (var outstream = new System.IO.StreamWriter(outfile,append:false)){ 

		for(string line = instream.ReadLine(); line!=null; line = instream.ReadLine()){
			var number_string = line.Split(delimiters,options);  // generate array of the numbers as strings
			int n = number_string.Length;
			var numbers = new double[n];
			for(int i=0;i<n;i++) numbers[i] = double.Parse(number_string[i]);
			list.add(numbers);
       	}

			for(int i=0;i<list.size;i++){
				var numbers = list[i];
				foreach(var number in numbers)Write($"{number : 0.00e+00;-0.00e+00} ");
				WriteLine();
        		} 
			}
		}
    }
}

