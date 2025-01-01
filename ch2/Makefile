sqliFuzzer: sqliFuzzer/book/Main.cs FORCE
	csc -out:ch2_sqliFuzzer.exe sqliFuzzer/book/Main.cs

getFuzzer: getFuzzer/Main.cs FORCE
	csc -out:ch2_getFuzzer.exe getFuzzer/Main.cs

sqliUnion: sqliUnion/Main.cs FORCE
	csc -out:ch2_sqliUnion.exe sqliUnion/Main.cs

dev: dev/Main.cs FORCE
	csc -out:ch2_dev.exe dev/Main.cs

FORCE:

ch2:
	make sqliFuzzer
	make getFuzzer
	make sqliUnion
	make dev

clean:
	rm ch2_*.exe

help:
	@echo "sqliFuzzer ch2 clean"