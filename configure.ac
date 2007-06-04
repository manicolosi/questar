AC_INIT(README)
AC_CANONICAL_SYSTEM

AM_INIT_AUTOMAKE(questar, 0.0.1)

AM_MAINTAINER_MODE

AC_PROG_INSTALL

AC_PATH_PROG(MCS, gmcs, no)
if test "x$MCS" = "xno"; then
    AC_MSG_ERROR([Cannot find 'gmcs' compiler in your PATH])
fi

AC_PATH_PROG(MONO, mono, no)
if test "x$MONO" = "xno"; then
    AC_MSG_ERROR([Cannot find 'mono' runtime in your PATH])
fi

GTKSHARP_REQUIRED=2.8.0
PKG_CHECK_MODULES(GCONF, gconf-sharp-2.0 >= $GTKSHARP_REQUIRED)
PKG_CHECK_MODULES(GLADE, glade-sharp-2.0 >= $GTKSHARP_REQUIRED)
PKG_CHECK_MODULES(GNOME, gnome-sharp-2.0 >= $GTKSHARP_REQUIRED)
PKG_CHECK_MODULES(GTK, gtk-sharp-2.0 >= $GTKSHARP_REQUIRED)
PKG_CHECK_MODULES(RSVG, rsvg-sharp-2.0 >= $GTKSHARP_REQUIRED)

AC_OUTPUT([
Makefile
resources/Makefile
src/Makefile
src/AssemblyInfo.cs
tests/Makefile
tilesets/Makefile
tilesets/ascii/Makefile
tilesets/default/Makefile
])

