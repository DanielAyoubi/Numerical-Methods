DIR=../matlib

all : Out.txt plot.svg 

plot.svg : OC_plot.gpi out.times.data	
	gnuplot $<

out.times.data : main.exe
	>$@
	for N in $$(seq 100 20 300); do \
		mono $< -size:$$N 1>out 2>err ;\
	done

Out.txt: main.exe
	mono $< > $@

main.exe: main.cs matrix.dll QRGS.dll 
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

QRGS.dll: QRGS.cs matrix.dll
	mcs QRGS.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:QRGS.dll

matrix.dll : $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs $^ -target:library -out:matrix.dll

clean:
	rm -f *.dll *.exe Out* *.txt *.data *.log *.svg err out

