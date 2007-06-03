ASSEMBLY = Questar.exe

SOURCES =                              \
	$(srcdir)/AboutDialog.cs           \
	$(srcdir)/Actor.cs                 \
	$(srcdir)/AssemblyInfo.cs          \
	$(srcdir)/CairoUtilities.cs        \
	$(srcdir)/ConfigurationClient.cs   \
	$(srcdir)/Direction.cs             \
	$(srcdir)/DoNothingAction.cs       \
	$(srcdir)/EntryPoint.cs            \
	$(srcdir)/Events.cs                \
	$(srcdir)/GladeDialog.cs           \
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
	$(srcdir)/MapView.cs               \
	$(srcdir)/Messages.cs              \
	$(srcdir)/MessageView.cs           \
	$(srcdir)/Monster.cs               \
	$(srcdir)/MoveAction.cs            \
	$(srcdir)/NativeMethods.cs         \
	$(srcdir)/Point.cs                 \
	$(srcdir)/PreferenceDialog.cs      \
	$(srcdir)/ProgramInformation.cs    \
	$(srcdir)/SchemaEntry.cs           \
	$(srcdir)/Terrain.cs               \
	$(srcdir)/TerrainManager.cs        \
	$(srcdir)/Tile.cs                  \
	$(srcdir)/TileSet.cs               \
	$(srcdir)/UIActions.cs             \
	$(srcdir)/UISchema.cs              \
	$(srcdir)/World.cs

ASSEMBLIES =                           \
	-pkg:gconf-sharp-2.0               \
	-pkg:glade-sharp-2.0               \
	-pkg:gtk-sharp-2.0                 \
	-pkg:rsvg-sharp-2.0                \
	-r:Mono.Cairo                      \
	-r:Mono.Posix

RESOURCES = \
	$(top_srcdir)/resources/questar-ui.xml \
	$(top_srcdir)/resources/questar.glade \
	$(top_srcdir)/resources/questar.svg \
	$(top_srcdir)/resources/terrains.xml

RESOURCES_EMBEDDED = \
	$(foreach res, $(RESOURCES), $(addprefix -resource:, $(res)))

$(ASSEMBLY): $(SOURCES) $(RESOURCES)
	$(MCS) -target:exe -out:$@ $(SOURCES) $(ASSEMBLIES) $(RESOURCES_EMBEDDED)

all: $(ASSEMBLY)

run: $(ASSEMBLY)
	$(MONO) $(ASSEMBLY)

EXTRA_DIST =                           \
	$(SOURCES)

CLEANFILES =                           \
	$(ASSEMBLY)                        \
	*.mdb

MAINTAINERCLEANFILES =                 \
	Makefile.in

