DIR=../homework/matlib

all: Out.txt computation_times.svg

Out.txt times.data : main.exe
	mono $< > $@

computation_times.svg : plot.gpi times.data
	gnuplot $<

main.exe: main.cs matlib.dll 
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

matlib.dll : $(DIR)/vector.cs $(DIR)/matrix.cs QRGS.cs newton_method.cs RankOneUpdate.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe Out* *.svg *.txt *.data
