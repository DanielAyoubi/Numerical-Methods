class main{
static void Main(){
	var ma=new matrix("1 2 ; 5 6");
	ma.print();
	(ma+ma.T).print();
	(matrix.id(2)).print();
}
}
