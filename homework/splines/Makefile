DIR=../matlib

all : Out.txt plot_linear_splines.svg plot_qspline.svg

Out.txt: main.exe
	mono main.exe > Out.txt

plot_linear_splines.svg: data_points.data plot.gpi
	gnuplot plot.gpi

plot_qspline.svg: qspline.data plot_qspline.gpi
	gnuplot plot_qspline.gpi

main.exe: main.cs splines.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

splines.dll : linspline.cs qspline.cs
	mcs $^ -target:library -out:splines.dll

data_points.data: main.exe
	mono main.exe

qspline.data: main.exe
	mono main.exe

clean:
	rm -f *.dll *.exe Out* *.data *.txt *.png *.svg
