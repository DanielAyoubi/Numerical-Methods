DIR=../matlib

Out.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll NN.dll 
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

NN.dll: neural_network.cs matlib.dll
	mcs $< $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:$@

matlib.dll : $(DIR)/vector.cs $(DIR)/matrix.cs $(DIR)/genlist.cs minimize.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe Out* *.svg *.txt *.data
