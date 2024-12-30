sqliFuzzer: sqliFuzzer/Main.cs FORCE
	csc -out:ch2_sqliFuzzer.exe sqliFuzzer/Main.cs

simpleSqliXssFuzzer: simpleSqliXssFuzzer/Main.cs FORCE
	csc -out:ch2_simpleSqliXssFuzzer.exe simpleSqliXssFuzzer/Main.cs

FORCE:

ch2:
	make sqliFuzzer
	make simpleSqliXssFuzzer

clean:
	rm ch2_*.exe

help:
	@echo "sqliFuzzer ch2 clean"