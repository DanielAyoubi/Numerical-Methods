DIR=../matlib

all: Out.txt decay_plot.svg

Out.txt: main.exe
	mono $< > $@

decay_plot.svg: plot.gpi values.data
	gnuplot $<

values.data : main.exe
	mono $< > $@

main.exe: main.cs matrix.dll QRGS.dll 
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

QRGS.dll: QRGS.cs matrix.dll
	mcs QRGS.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:QRGS.dll

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs $^ -target:library -out:matrix.dll


clean:
	rm -f *.dll *.exe Out* *.data *.svg *.txt
