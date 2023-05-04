DIR=../matlib

all : errors.svg

errors.data : main.exe
	mono $<

errors.svg : error.gpi errors.data
	gnuplot $<

main.exe: main.cs matlib.dll MC.dll QR.dll SS.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

SS.dll : stratified.cs matlib.dll QR.dll
	mcs $< $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:$@

MC.dll: monte_carlo.cs matlib.dll QR.dll
	mcs $< $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:$@

QR.dll : quasi_random.cs
	mcs $< $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:$@

matlib.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe Out* *.svg *.txt *.data


