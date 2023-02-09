Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs
	mcs main.cs  -target:exe -out:main.exe

main.dll :  main.cs
	mcs $^ -target:library -out:main.dll
	#mcs static_hello.cs static_world.cs -target:library -out:hello.dll

main.exe : main.cs main.dll
	mcs main.cs -reference:main.dll -target:exe -out:main.exe

clear : main.exe
	rm main.exe

print : Out.txt
	cat Out.txt
