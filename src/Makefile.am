################################################################################
#   Makefile.am: Automake source file for building Questar.exe.
#
#   Copyright (C) 2007
#   Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
################################################################################

ASSEMBLY = Questar.exe

SOURCES_IN = \
	$(srcdir)/AssemblyInfo.cs.in

SOURCES_GEN = \
	$(SOURCES_IN:.in=)

SOURCES = \
	Questar/EntryPoint.cs \
	\
	Questar.Actors/AbstractActor.cs \
	Questar.Actors/Actor.cs \
	Questar.Actors/ActorEventArgs.cs \
	Questar.Actors/Hero.cs \
	Questar.Actors/HitPoints.cs \
	Questar.Actors/Monster.cs \
	Questar.Actors/MonsterDefinition.cs \
	Questar.Actors/MonsterFactory.cs \
	\
	Questar.Actors.AI/ArtificialIntelligence.cs \
	Questar.Actors.AI/StupidAI.cs \
	\
	Questar.Actors.Actions/AbstractAction.cs \
	Questar.Actors.Actions/Action.cs \
	Questar.Actors.Actions/AttackAction.cs \
	Questar.Actors.Actions/DoNothingAction.cs \
	Questar.Actors.Actions/DrinkAction.cs \
	Questar.Actors.Actions/DropAction.cs \
	Questar.Actors.Actions/ImpossibleActionException.cs \
	Questar.Actors.Actions/MoveAction.cs \
	Questar.Actors.Actions/MoveTowardsTargetAction.cs \
	Questar.Actors.Actions/PickUpAction.cs \
	Questar.Actors.Actions/RandomMoveAction.cs \
	\
	Questar.Base/Messages.cs \
	Questar.Base/NativeMethods.cs \
	Questar.Base/ProgramInformation.cs \
	Questar.Base/Singleton.cs \
	\
	Questar.Configuration/ConfigurationClient.cs \
	Questar.Configuration/SchemaEntry.cs \
	Questar.Configuration/UISchema.cs \
	\
	Questar.Core/Game.cs \
	Questar.Core/ITurnLoopDriver.cs \
	Questar.Core/IdleTurnLoopDriver.cs \
	Questar.Core/TurnLoop.cs \
	\
	Questar.Extensions/ExtensionMethods.cs \
	\
	Questar.Gui/Animation.cs \
	Questar.Gui/AboutDialog.cs \
	Questar.Gui/GladeDialog.cs \
	Questar.Gui/GladeWindow.cs \
	Questar.Gui/InventoryView.cs \
	Questar.Gui/MainWindow.cs \
	Questar.Gui/MessageView.cs \
	Questar.Gui/PickupMessageWidget.cs \
	Questar.Gui/PreferenceDialog.cs \
	Questar.Gui/StartDialog.cs \
	Questar.Gui/TangoColors.cs \
	Questar.Gui/ThemeIcons.cs \
	Questar.Gui/Tile.cs \
	Questar.Gui/TileSet.cs \
	Questar.Gui/UIActions.cs \
	Questar.Gui/WorldView.cs \
	\
	Questar.Gui.Widgets/HitPointsChart.cs \
	\
	Questar.Helpers/ColorHelper.cs \
	Questar.Helpers/EventHelper.cs \
	Questar.Helpers/RaisedEventException.cs \
	Questar.Helpers/StringHelper.cs \
	\
	Questar.Items/AbstractItem.cs \
	Questar.Items/IDrinkable.cs \
	Questar.Items/Inventory.cs \
	Questar.Items/ItemBuilder.cs \
	Questar.Items/Item.cs \
	Questar.Items/ItemDefinition.cs \
	Questar.Items/ItemFactory.cs \
	\
	Questar.Items.Concrete/HealthPotion.cs \
	\
	Questar.Maps/Grid.cs \
	Questar.Maps/GridInformation.cs \
	Questar.Maps/Map.cs \
	Questar.Maps/Terrain.cs \
	Questar.Maps/TerrainManager.cs \
	\
	Questar.Maps.Generation/IMapGenerator.cs \
	Questar.Maps.Generation/StupidGenerator.cs \
	\
	Questar.Primitives/ActorLocation.cs \
	Questar.Primitives/AbstractEntity.cs \
	Questar.Primitives/AbstractEntityFactory.cs \
	Questar.Primitives/Direction.cs \
	Questar.Primitives/Entity.cs \
	Questar.Primitives/IEntityFactory.cs \
	Questar.Primitives/Location.cs \
	Questar.Primitives/LocationFactory.cs \
	Questar.Primitives/MapLocation.cs \
	Questar.Primitives/NullLocation.cs \
	Questar.Primitives/Point.cs \
	Questar.Primitives/Rectangle.cs

ASSEMBLIES = \
	-pkg:gconf-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-pkg:gnome-sharp-2.0 \
	-pkg:gtk-sharp-2.0 \
	-pkg:rsvg-sharp-2.0 \
	-r:Mono.Cairo \
	-r:Mono.Posix \
	-r:System.Core

RESOURCES = \
	$(top_srcdir)/resources/items.xml \
	$(top_srcdir)/resources/monsters.xml \
	$(top_srcdir)/resources/questar-ui.xml \
	$(top_srcdir)/resources/questar.glade \
	$(top_srcdir)/resources/terrains.xml

RESOURCES_EMBEDDED = \
	$(foreach res, $(RESOURCES), $(addprefix -resource:, $(res)))

$(ASSEMBLY): $(SOURCES) $(SOURCES_GEN) $(RESOURCES)
	$(MCS) -langversion:linq $(SOURCES) $(SOURCES_GEN) $(RESOURCES_EMBEDDED) $(ASSEMBLIES) -debug -target:winexe -out:$@

all: $(ASSEMBLY)

run: $(ASSEMBLY)
	$(MONO) --debug $(ASSEMBLY)

EXTRA_DIST = \
	$(SOURCES) \
	$(SOURCES_IN)

CLEANFILES = \
	$(ASSEMBLY) \
	$(ASSEMBLY).mdb

MAINTAINERCLEANFILES = \
	Makefile.in

