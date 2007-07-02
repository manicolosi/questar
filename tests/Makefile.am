################################################################################
#   Makefile.am: Automake source file for building unit tests.
#
#   Copyright (C) 2007
#   Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
################################################################################

ASSEMBLY = Questar.UnitTests.dll
NUNIT_PATH = /home/mark/source/nunit-bin/bin
NUNIT_DLL = $(NUNIT_PATH)/nunit.framework.dll
NUNIT_CONSOLE = $(NUNIT_PATH)/nunit-console.exe

SOURCES =                              \
	$(srcdir)/Questar.Items/ItemFactoryFixture.cs \
	\
	$(srcdir)/MockActor.cs             \
	$(srcdir)/TestActor.cs             \
	$(srcdir)/TestGame.cs              \
	$(srcdir)/TestHitPoints.cs         \
	$(srcdir)/TestMap.cs               \
	$(srcdir)/TestPoint.cs             \
	$(srcdir)/TestRectangle.cs         \
	$(srcdir)/TestSchemaEntry.cs       \
	$(srcdir)/TestSingleton.cs         \
	$(srcdir)/TestWorld.cs

ASSEMBLIES =                           \
	-pkg:glib-sharp-2.0                \
	-pkg:gtk-sharp-2.0                 \
	-r:$(NUNIT_DLL)                    \
	-r:../src/Questar.exe

$(ASSEMBLY): $(SOURCES) 
	$(MCS) -target:library -out:$@ $(SOURCES) $(ASSEMBLIES) 

all: $(ASSEMBLY)

run-tests: all
	MONO_PATH=../src:$(NUNIT_PATH) $(NUNIT_CONSOLE) -nologo -labels $(ASSEMBLY)

EXTRA_DIST =                           \
	$(SOURCES)

CLEANFILES =                           \
	$(ASSEMBLY)                        \
	$(ASSEMBLY).mdb

MAINTAINERCLEANFILES =                 \
	Makefile.in

