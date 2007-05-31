ASSEMBLY = Questar.UnitTests.dll
NUNIT_CONSOLE_PATH = ~/source/nunit-bin/bin

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

test: tests
	MONO_PATH=../src:$(NUNIT_CONSOLE_PATH) $(NUNIT_CONSOLE_PATH)/nunit-console.exe -nologo -labels $(ASSEMBLY)

EXTRA_DIST =                           \
	$(SOURCES)

CLEANFILES =                           \
	$(ASSEMBLY)                        \
	$(ASSEMBLY).mdb

MAINTAINERCLEANFILES =                 \
	Makefile.in

