################################################################################
#   Makefile.am: Main automake source file for building Questar.
#
#   Copyright (C) 2007
#   Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
################################################################################

run:
	pushd $(top_builddir)/src;         \
	make run;                          \
	popd;

test:
	pushd $(top_builddir)/src;         \
	make;                              \
	popd;                              \
	pushd $(top_builddir)/tests;       \
	make test;                         \
	popd;

SUBDIRS =                              \
	resources                          \
	src                                \
	tests                              \
	tilesets

EXTRA_DIST =                           \
	AUTHORS                            \
	ChangeLog                          \
	COPYING                            \
	NEWS                               \
	README

MAINTAINERCLEANFILES =                 \
	Makefile.in                        \
	config.guess                       \
	config.sub                         \
	configure                          \
	install-sh                         \
	INSTALL                            \
	missing                            \
	mkinstalldirs                      \
	aclocal.m4

