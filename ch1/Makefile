helloWorld: helloWorld/Main.cs FORCE
	mcs -lib: helloWorld/Main.cs -out:ch1_helloWorld.exe

theBasics: theBasics/Main.cs theBasics/IPerson.cs theBasics/PublicServant.cs theBasics/FireFighter.cs theBasics/PoliceOfficer.cs FORCE
	mcs -lib: theBasics/Main.cs theBasics/IPerson.cs theBasics/PublicServant.cs theBasics/FireFighter.cs theBasics/PoliceOfficer.cs -out:ch1_the_basics.exe

FORCE:

ch1:
	make helloWorld
	make theBasics

clean:
	rm ch1_*.exe

help:
	@echo "helloWorld theBasics ch1 clean"