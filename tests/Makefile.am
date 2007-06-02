ASSEMBLY = Questar.UnitTests.dll
NUNIT_PATH = /home/mark/source/nunit-bin/bin

SOURCES =                              \
	$(srcdir)/HitPointsTest.cs         \
	$(srcdir)/MockActor.cs             \
	$(srcdir)/TestWorld.cs

ASSEMBLIES =                           \
	-r:$(NUNIT_PATH)/nunit.framework.dll    \
	-r:../src/Questar.exe

$(ASSEMBLY): $(SOURCES) 
	$(MCS) -target:library -out:$@ $(SOURCES) $(ASSEMBLIES) 

all: $(ASSEMBLY)

tests: all

test: tests
	MONO_PATH=../src:$(NUNIT_PATH) $(NUNIT_PATH)/nunit-console.exe -nologo -labels $(ASSEMBLY)

EXTRA_DIST =                           \
	$(SOURCES)

CLEANFILES =                           \
	$(ASSEMBLY)                        \
	$(ASSEMBLY).mdb

MAINTAINERCLEANFILES =                 \
	Makefile.in

