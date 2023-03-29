DIR=../matlib

Out.txt: main.exe
	mono main.exe > Out.txt
	@cat Out.txt

main.exe: main.cs matrix.dll 
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs jacobi.cs hydrogen_atom.cs
	mcs $^ -target:library -out:matrix.dll

hydrogen_atom.exe : hydrogen_atom.cs matrix.dll
	mcs hydrogen_atom.cs -out:$<

clean:
	rm *.dll *.exe Out*
