DIR=../matlib

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs NM.dll ODE.dll genlist.cs
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))


NM.dll: newton_method.cs ODE.dll QRGS.dll
	mcs  $< $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:$@

QRGS.dll : QRGS.cs ODE.dll	
	mcs  $< $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:$@

ODE.dll: $(DIR)/vector.cs $(DIR)/matrix.cs ../ODE/RK45.cs ../ODE/genlist.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe Out* *.svg *.txt *.data
