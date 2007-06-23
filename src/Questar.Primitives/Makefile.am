################################################################################
#   Makefile.am: Automake source file for building
#   Questar.Primitives.dll
#
#   Copyright (C) 2007
#   Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
################################################################################

ASSEMBLY = Questar.Primitives.dll

SOURCES =                              \
	$(srcdir)/Direction.cs             \
	$(srcdir)/Rectangle.cs             \
	$(srcdir)/Point.cs

$(ASSEMBLY): $(SOURCES)
	$(MCS) $(SOURCES) -target:library -out:$@

all: $(ASSEMBLY)

EXTRA_DIST =                           \
	$(SOURCES)

CLEANFILES =                           \
	$(ASSEMBLY)                        \
	$(ASSEMBLY).mdb

MAINTAINERCLEANFILES =                 \
	Makefile.in

