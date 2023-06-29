Out.txt : main.exe
	mono main.exe -input:Input.txt -output:Out.txt
main.exe : main.cs 
	mcs main.cs -target:exe -out:main.exe

#sfuns.dll : sfuns.cs
#	mcs $< -target:library -out:$@

print : main.exe
	@cat Out.txt

clean :
	rm -f Out.txt *.exe
