Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs sfuns.dll
	mcs main.cs -reference:sfuns.dll -target:exe -out:main.exe

sfuns.dll : sfuns.cs
	mcs $< -target:library -out:$@

print : main.exe
	@cat Out.txt
