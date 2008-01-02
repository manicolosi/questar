################################################################################
#   Makefile.am: Automake source file for building unit tests.
#
#   Copyright (C) 2007
#   Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
################################################################################

ASSEMBLY = Questar.UnitTests.dll
NUNIT_PATH = /home/mark/applications/nunit/bin
NUNIT_DLL = $(NUNIT_PATH)/nunit.framework.dll
NUNIT_CONSOLE = $(NUNIT_PATH)/nunit-console.exe

SOURCES = \
	$(srcdir)/Questar.UnitTests.Primitives/DirectionFixture.cs \
	$(srcdir)/Questar.UnitTests.Primitives/PointFixture.cs \
	$(srcdir)/Questar.UnitTests.Items/ItemFactoryFixture.cs \
	$(srcdir)/Questar.UnitTests.Extensions/CheckExtensionsFixture.cs \
	$(srcdir)/Questar.UnitTests.Extensions/ExtensionMethodsFixture.cs \
	$(srcdir)/../src/Questar.Extensions/ExtensionMethods.cs \
	$(srcdir)/../src/Questar.Extensions/CheckExtensions.cs \
	#$(srcdir)/Questar.UnitTests.Primitives/LocationFixture.cs \
	#$(srcdir)/TestActor.cs \
	#$(srcdir)/TestGame.cs \
	#$(srcdir)/TestHitPoints.cs \
	#$(srcdir)/TestMap.cs \
	#$(srcdir)/TestPoint.cs \
	#$(srcdir)/TestRectangle.cs \
	#$(srcdir)/TestSchemaEntry.cs \
	#$(srcdir)/TestSingleton.cs \
	#\
	#$(srcdir)/Questar.MockObjects/MockItem.cs \
	#\
	#$(srcdir)/Questar.UnitTests.Actors/ActorFixture.cs \
	#\
	#$(srcdir)/Questar.UnitTests.Actors.Actions/PickUpActionFixture.cs \
	#\
	#$(srcdir)/Questar.UnitTests.Items/InventoryFixture.cs \
	#$(srcdir)/Questar.UnitTests.Items/ItemFixture.cs


ASSEMBLIES = \
	-pkg:glib-sharp-2.0 \
	-pkg:gtk-sharp-2.0 \
	-r:$(NUNIT_DLL) \
	-r:../src/Questar.exe \
	-r:System.Core

$(ASSEMBLY): $(SOURCES) 
	$(MCS) -langversion:linq -target:library -out:$@ $(SOURCES) $(ASSEMBLIES) 

all: $(ASSEMBLY)

run-tests: all
	MONO_PATH=../src:$(NUNIT_PATH) mono $(NUNIT_CONSOLE) -nologo -labels $(ASSEMBLY)

EXTRA_DIST = \
	$(SOURCES)

CLEANFILES = \
	$(ASSEMBLY) \
	$(ASSEMBLY).mdb

MAINTAINERCLEANFILES = \
	Makefile.in

