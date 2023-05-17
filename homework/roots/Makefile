DIR=../matlib

all: Out.txt plot.gpi hydrogen.svg

Out.txt: main.exe
	mono $< > $@

hydrogen.svg : plot.gpi hydrogen.data
	gnuplot $<

main.exe: main.cs lib.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

lib.dll : $(DIR)/vector.cs $(DIR)/matrix.cs $(DIR)/RK45.cs ../ODE/genlist.cs $(DIR)/QRGS.cs newton_method.cs
	mcs  $^ $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:$@

clean:
	rm -f *.dll *.exe Out* *.svg *.txt *.data
