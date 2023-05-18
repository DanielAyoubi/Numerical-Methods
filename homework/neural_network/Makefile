DIR=../matlib

Out.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll QRGS.dll 
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

QRGS.dll: QRGS.cs matlib.dll
	mcs $< $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:$@

matlib.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe Out* *.svg *.txt *.data
