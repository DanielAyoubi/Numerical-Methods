DIR=../matlib

all : Out.txt errors.svg

Out.txt: main.exe
	mono main.exe > Out.txt

errors.svg : error.gpi errors.data
	gnuplot $<

main.exe: main.cs matrix.dll MC.dll QR.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

MC.dll: monte_carlo.cs matrix.dll QR.dll
	mcs $< $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:MC.dll

QR.dll : quasi_random.cs
	mcs $< $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:QR.dll

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs $^ -target:library -out:matrix.dll

clean:
	rm -f *.dll *.exe Out* *.svg *.txt *.data


