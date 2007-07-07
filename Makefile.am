################################################################################
#   Makefile.am: Main automake source file for building Questar.
#
#   Copyright (C) 2007
#   Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
################################################################################

run:
	pushd $(top_builddir)/src; \
	make run; \
	popd;

check:
	pushd $(top_builddir)/src && \
	make && \
	popd && \
	pushd $(top_builddir)/tests && \
	make run-tests && \
	popd;

SUBDIRS = \
	content \
	resources \
	src \
	tests \
	tilesets

EXTRA_DIST = \
	autogen.sh \
	AUTHORS \
	ChangeLog \
	COPYING \
	NEWS \
	README

MAINTAINERCLEANFILES = \
	INSTALL \
	Makefile.in \
	aclocal.m4 \
	config.guess \
	config.sub \
	configure \
	install-sh \
	missing \
	mkinstalldirs

