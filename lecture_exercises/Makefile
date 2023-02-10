Out.txt : main.exe
	mono main.exe > Out.txt

#hello.dll : static_hello.cs static_world.cs hello.cs
#	mcs $^ -target:library -out:hello.dll
	#mcs static_hello.cs static_world.cs -target:library -out:hello.dll

main.exe : main.cs #hello.dll
	mcs main.cs -target:exe -out:main.exe
