################################################################################
#   Makefile.am: Automake source file for building Questar.Helpers.dll.
#
#   Copyright (C) 2007
#   Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
################################################################################

ASSEMBLY = Questar.Helpers.dll

SOURCES =                              \
	$(srcdir)/ColorHelper.cs           \
	$(srcdir)/EventHelper.cs

ASSEMBLIES =                           \
	-pkg:gtk-sharp-2.0                 \
	-r:Mono.Cairo

$(ASSEMBLY): $(SOURCES)
	$(MCS) $(SOURCES) $(ASSEMBLIES) -target:library -out:$@

all: $(ASSEMBLY)

EXTRA_DIST =                           \
	$(SOURCES)

CLEANFILES =                           \
	$(ASSEMBLY)                        \
	$(ASSEMBLY).mdb

MAINTAINERCLEANFILES =                 \
	Makefile.in

