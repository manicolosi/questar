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

SOURCES = \
	Questar/EntryPoint.cs \
	Questar.Actors/Actor.cs \
	Questar.Actors/Hero.cs \
	Questar.Actors/HitPoints.cs \
	Questar.Actors/Monster.cs \
	Questar.Actors/MonsterDefinition.cs \
	Questar.Actors/MonsterFactory.cs \
	\
	Questar.Actors.Actions/AttackAction.cs \
	Questar.Actors.Actions/DoNothingAction.cs \
	Questar.Actors.Actions/IAction.cs \
	Questar.Actors.Actions/MoveAction.cs \
	Questar.Actors.Actions/RandomMoveAction.cs \
	\
	Questar.Base/Game.cs \
	Questar.Base/Messages.cs \
	Questar.Base/NativeMethods.cs \
	Questar.Base/ProgramInformation.cs \
	Questar.Base/Singleton.cs \
	Questar.Base/World.cs \
	\
	Questar.Configuration/ConfigurationClient.cs \
	Questar.Configuration/SchemaEntry.cs \
	Questar.Configuration/UISchema.cs \
	\
	Questar.Gui/AboutDialog.cs \
	Questar.Gui/GladeDialog.cs \
	Questar.Gui/GladeWindow.cs \
	Questar.Gui/MainWindow.cs \
	Questar.Gui/MessageView.cs \
	Questar.Gui/PreferenceDialog.cs \
	Questar.Gui/StartDialog.cs \
	Questar.Gui/ThemeIcons.cs \
	Questar.Gui/Tile.cs \
	Questar.Gui/TileSet.cs \
	Questar.Gui/UIActions.cs \
	Questar.Gui/WorldView.cs \
	\
	Questar.Helpers/ColorHelper.cs \
	Questar.Helpers/EventHelper.cs \
	Questar.Helpers/StringHelper.cs \
	\
	Questar.Items/HealthPotion.cs \
	Questar.Items/Inventory.cs \
	Questar.Items/Item.cs \
	Questar.Items/ItemFactory.cs \
	\
	Questar.Maps/Grid.cs \
	Questar.Maps/GridInformation.cs \
	Questar.Maps/Map.cs \
	Questar.Maps/Terrain.cs \
	Questar.Maps/TerrainManager.cs \
	\
	Questar.Primitives/Direction.cs \
	Questar.Primitives/Entity.cs \
	Questar.Primitives/Point.cs \
	Questar.Primitives/Rectangle.cs

ASSEMBLIES = \
	-pkg:gconf-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-pkg:gnome-sharp-2.0 \
	-pkg:gtk-sharp-2.0 \
	-pkg:rsvg-sharp-2.0 \
	-r:Mono.Cairo \
	-r:Mono.Posix

RESOURCES = \
	$(top_srcdir)/resources/items.xml \
	$(top_srcdir)/resources/monsters.xml \
	$(top_srcdir)/resources/questar-ui.xml \
	$(top_srcdir)/resources/questar.glade \
	$(top_srcdir)/resources/terrains.xml

RESOURCES_EMBEDDED = \
	$(foreach res, $(RESOURCES), $(addprefix -resource:, $(res)))

$(ASSEMBLY): $(SOURCES) $(SOURCES_GEN) $(RESOURCES)
	$(MCS) $(SOURCES) $(SOURCES_GEN) $(RESOURCES_EMBEDDED) $(ASSEMBLIES) -target:winexe -out:$@

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

