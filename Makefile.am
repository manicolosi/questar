run:
	pushd $(top_builddir)/src;         \
	make run;                          \
	popd;

test:
	pushd $(top_builddir)/tests;       \
	make test;                         \
	popd;

SUBDIRS =                              \
	src                                \
	tests

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

