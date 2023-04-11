DIR=../matlib

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs matrix.dll QRGS.dll 
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

QRGS.dll: QRGS.cs matrix.dll
	mcs QRGS.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:QRGS.dll

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs $^ -target:library -out:matrix.dll

clean:
	rm -f *.dll *.exe Out* *.svg *.txt *.data
