################################################################################
#   Makefile.am: Automake source file for building Questar.exe.
#
#   Copyright (C) 2007
#   Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
################################################################################

ASSEMBLY = Questar.exe

SOURCES_IN =                           \
	$(srcdir)/AssemblyInfo.cs.in

SOURCES_GEN =                          \
	$(SOURCES_IN:.in=)

SOURCES =                              \
	$(srcdir)/AboutDialog.cs           \
	$(srcdir)/Actor.cs                 \
	$(srcdir)/CairoColorExtensions.cs  \
	$(srcdir)/ConfigurationClient.cs   \
	$(srcdir)/Direction.cs             \
	$(srcdir)/DoNothingAction.cs       \
	$(srcdir)/EntryPoint.cs            \
	$(srcdir)/Events.cs                \
	$(srcdir)/Game.cs                  \
	$(srcdir)/GladeDialog.cs           \
	$(srcdir)/GladeWindow.cs           \
	$(srcdir)/Grid.cs                  \
	$(srcdir)/GridInformation.cs       \
	$(srcdir)/Hero.cs                  \
	$(srcdir)/HitPoints.cs             \
	$(srcdir)/IActionable.cs           \
	$(srcdir)/IAction.cs               \
	$(srcdir)/IActor.cs                \
	$(srcdir)/IEntity.cs               \
	$(srcdir)/MainWindow.cs            \
	$(srcdir)/Map.cs                   \
	$(srcdir)/Messages.cs              \
	$(srcdir)/MessageView.cs           \
	$(srcdir)/Monster.cs               \
	$(srcdir)/MoveAction.cs            \
	$(srcdir)/NativeMethods.cs         \
	$(srcdir)/Point.cs                 \
	$(srcdir)/PreferenceDialog.cs      \
	$(srcdir)/ProgramInformation.cs    \
	$(srcdir)/Rectangle.cs             \
	$(srcdir)/SchemaEntry.cs           \
	$(srcdir)/Singleton.cs             \
	$(srcdir)/StartDialog.cs           \
	$(srcdir)/Terrain.cs               \
	$(srcdir)/TerrainManager.cs        \
	$(srcdir)/Tile.cs                  \
	$(srcdir)/TileSet.cs               \
	$(srcdir)/UIActions.cs             \
	$(srcdir)/UISchema.cs              \
	$(srcdir)/World.cs                 \
	$(srcdir)/WorldView.cs

ASSEMBLIES =                           \
	-pkg:gconf-sharp-2.0               \
	-pkg:glade-sharp-2.0               \
	-pkg:gnome-sharp-2.0               \
	-pkg:gtk-sharp-2.0                 \
	-pkg:rsvg-sharp-2.0                \
	-r:Mono.Cairo                      \
	-r:Mono.Posix

RESOURCES = \
	$(top_srcdir)/resources/questar-ui.xml \
	$(top_srcdir)/resources/questar.glade  \
	$(top_srcdir)/resources/terrains.xml

RESOURCES_EMBEDDED = \
	$(foreach res, $(RESOURCES), $(addprefix -resource:, $(res)))

$(ASSEMBLY): $(SOURCES) $(SOURCES_GEN) $(RESOURCES)
	$(MCS) $(SOURCES) $(SOURCES_GEN) $(RESOURCES_EMBEDDED) $(ASSEMBLIES) -target:exe -out:$@ 

all: $(ASSEMBLY)

run: $(ASSEMBLY)
	$(MONO) $(ASSEMBLY)

EXTRA_DIST =                           \
	$(SOURCES)                         \
	$(SOURCES_IN)

CLEANFILES =                           \
	$(ASSEMBLY)                        \
	$(ASSEMBLY).mdb

MAINTAINERCLEANFILES =                 \
	Makefile.in

