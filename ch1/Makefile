helloWorld: helloWorld/Main.cs FORCE
	csc -out:ch1_helloWorld.exe helloWorld/Main.cs

theBasics: theBasics/Main.cs theBasics/IPerson.cs theBasics/PublicServant.cs theBasics/FireFighter.cs theBasics/PoliceOfficer.cs FORCE
	csc -out:ch1_the_basics.exe theBasics/Main.cs theBasics/IPerson.cs theBasics/PublicServant.cs theBasics/FireFighter.cs theBasics/PoliceOfficer.cs

pinvoke: pinvoke/Main.cs FORCE
	csc -out:ch1_pinvoke.exe pinvoke/Main.cs

FORCE:

ch1:
	make helloWorld
	make theBasics
	make pinvoke

clean:
	rm ch1_*.exe

help:
	@echo "helloWorld theBasics pinvoke ch1 clean"