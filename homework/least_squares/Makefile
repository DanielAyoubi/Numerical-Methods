SDKDIR=$(HOME)/.dotnet/sdk/6.0.102# or whatever your sdk is
CSCPATH=$(SDKDIR)/Roslyn/bincore/csc.dll
NETSTANDARD=$(SDKDIR)/ref/netstandard.dll
CONFIG=$(SDKDIR)/vstest.console.runtimeconfig.json
DOTNET=DOTNET_CLI_TELEMETRY_OPTOUT=1; dotnet
CSC=$(DOTNET) $(CSCPATH) -reference:$(NETSTANDARD)
RUN=$(DOTNET) exec --runtimeconfig $(CONFIG)
CSC = mcs
RUN = mono

Fig.pdf: out.data.txt Makefile
	echo '\
	set term pdf;\
	set out "$@";\
	set key left;\
	set xlabel "$$x$$";\
	set ylabel "$$y$$";\
	set title "Least-squares fit";\
	plot \
	 "$<" index 0 with errorbars title "data"\
	,"$<" index 1 with lines title "$$F_{\\vec c}(x)\\doteq c_0+c_1x+c_2x^2$$"\
	,"$<" index 2 with lines title "$$F_{\\vec{c}+\\Delta\\vec{c}}(x)$$"\
	,"$<" index 3 with lines title "$$F_{\\vec{c}-\\Delta\\vec{c}}(x)$$"\
	'| tee log.gpi | pyxplot

out.data.txt:main.exe
	$(RUN) $< > $@ 2>log

main.exe: main.cs matlib.dll
	$(CSC) $< $(addprefix -reference:,$(filter %.dll,$^))
matlib.dll: \
	../matrix/matrix.cs\
	../matrix/vector.cs\
	../QR/givensqr.cs\
	../QR/gsqr.cs\
	./lsfit.cs
	$(CSC) $^ -target:library -out:$@

clean:
	$(RM) Out* *.dll *.exe log* out* *.pdf
