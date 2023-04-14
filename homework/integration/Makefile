DIR=../matlib

all : Out.txt erf_plot.svg

Out.txt: main.exe
	mono main.exe > Out.txt

erf_plot.svg : plot.gpi erf.data
	gnuplot $<

main.exe: main.cs quadrature.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

quadrature.dll : quadrature.cs $(DIR)/vector.cs $(DIR)/matrix.cs 
	mcs $^ -target:library -out:quadrature.dll

clean:
	rm -f *.dll *.exe Out* *.svg *.txt *.data
