DIR=../matlib

all : Out.txt plot.gpi higgs.svg	

Out.txt: main.exe
	mono main.exe < higgs.data.txt 1> Out.txt 2> log

higgs.svg: plot.gpi higgs.data.txt fitted.data
	gnuplot $<

main.exe: main.cs matlib.dll minimize.dll 
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

minimize.dll: minimize.cs matlib.dll
	mcs $< $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:$@

matlib.dll : $(DIR)/vector.cs $(DIR)/matrix.cs $(DIR)/genlist.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe Out* *.svg
