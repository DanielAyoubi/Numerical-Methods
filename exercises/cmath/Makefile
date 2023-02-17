Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs cmath.dll
	mcs main.cs -reference:cmath.dll -target:exe -out:main.exe

cmath.dll : cmath.cs
	mcs $< -target:library -out:$@

