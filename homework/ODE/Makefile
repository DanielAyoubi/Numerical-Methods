DIR=../matlib

all: Out.txt test.svg lotka_volterra.svg

Out.txt: main.exe
	mono main.exe > Out.txt

test.svg : plot.gpi Out.txt
	gnuplot $<

lotka_volterra.svg : lotka_volterra.gpi lotka_volterra.data
	gnuplot $<

main.exe: main.cs ODE.dll
	mcs main.cs -reference:ODE.dll

ODE.dll: $(DIR)/vector.cs $(DIR)/matrix.cs RK45.cs $(DIR)/genlist.cs
	mcs $^ -target:library -out:ODE.dll

clean:
	rm -f *.dll *.exe Out* *.svg *.txt *.data

