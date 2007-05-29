ASSEMBLY = Questar.UnitTests.dll

SOURCES =                              \
	$(srcdir)/MockActor.cs             \
	$(srcdir)/TestWorld.cs

ASSEMBLIES =                           \
	-pkg:nunit-2.2                     \
	-r:../src/Questar.exe

$(ASSEMBLY): $(SOURCES) 
	$(MCS) -target:library -out:$@ $(SOURCES) $(ASSEMBLIES) 

all: $(ASSEMBLY)

tests: all

run-tests: tests
	MONO_PATH=../src nunit-console /nologo $(ASSEMBLY)

EXTRA_DIST =                           \
	$(SOURCES)

CLEANFILES =                           \
	$(ASSEMBLY)                        \
	$(ASSEMBLY).mdb

MAINTAINERCLEANFILES =                 \
	Makefile.in

