SOURCES =                              \
	$(srcdir)/MockActor.cs             \
	$(srcdir)/TestWorld.cs

ASSEMBLIES =                           \
	-pkg:nunit-2.2                     \
	-r:../src/Questar.exe

QuestarTests.dll: $(SOURCES) 
	$(MCS) -target:library -out:$@ $(SOURCES) $(ASSEMBLIES) 

all: QuestarTests.dll

tests: all

run-tests: tests
	MONO_PATH=../src nunit-console /nologo QuestarTests.dll

EXTRA_DIST =                           \
	$(SOURCES)

CLEANFILES =                           \
	QuestarTests.dll                   \
	QuestarTests.dll.mdb

MAINTAINERCLEANFILES =                 \
	Makefile.in

