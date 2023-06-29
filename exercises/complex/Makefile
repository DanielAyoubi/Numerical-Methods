DIR = $(HOME)/Repos/PPNM/exercises/complex/
REFERENCES=/reference:System.Numerics.dll /reference:cmath.dll

Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs cmath.dll
	mcs $(REFERENCES) main.cs

cmath.dll :$(DIR)cmath.cs $(DIR)complex.cs
	mcs -target:library -out:./cmath.dll $^

clean : 
	rm -f *.data *.txt *.dll *.exe
