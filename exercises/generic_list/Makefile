Out.txt : main.exe
	mono main.exe -input:Input.txt -output:Out.txt > Out.txt
main.exe : genlist.cs 
	mcs genlist.cs -target:exe -out:main.exe 

#sfuns.dll : sfuns.cs
#	mcs $< -target:library -out:$@

clean : 
	rm -f *.exe Out.txt
