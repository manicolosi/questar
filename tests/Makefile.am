################################################################################
#   Makefile.am: Automake source file for building unit tests.
#
#   Copyright (C) 2007
#   Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
################################################################################

ASSEMBLY = Questar.UnitTests.dll
NUNIT_PATH = /home/mark/source/nunit-bin/bin

SOURCES =                              \
	$(srcdir)/MockActor.cs             \
	$(srcdir)/TestIActor.cs            \
	$(srcdir)/TestHitPoints.cs         \
	$(srcdir)/TestSchemaEntry.cs       \
	$(srcdir)/TestWorld.cs

ASSEMBLIES =                                \
	-pkg:glib-sharp-2.0                     \
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

