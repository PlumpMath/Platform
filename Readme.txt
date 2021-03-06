Idmr.Platform.dll
=================

Author: Michael Gaisser (mjgaisser@gmail.com)
Version: 2.6
Date: 2017.10.15

Library for editing LucasArts *.TIE mission files for TIE95, XvT and XWA

=========
Version History

v2.6 - 15 Oct 2017
 - (TIE.Strings) Added missing "*" from Med Trans
 - (*.Strings) Added ability to replace craft list (YOGEME#10)

v2.5 - 7 Jan 2017
 - Lots of fixes and new features by Random Starfighter (JB)
  -- (*.Mission) Enforced string encodings
  -- (TIE.FlightGroup.Order) Hack for _checkValues added
  -- (TIE.Mission) Added Message length check during load
  -- (TIE.Mission) Fixed Global Goal loading
  -- (TIE.Strings) Added Decoy Beam
  -- (XvT.Briefing) Added Team functionality
  -- (XvT.FlightGroup.Goal) Fixed points casts
  -- (XvT.FlightGroup.Goal) Added Team visiblity
  -- (XvT.Mission) Fixed craft options
  -- (XvT.Mission) Added Message length check during load
  -- (XvT.Mission) Fixed FG Unks
  -- (XvT.Mission) Fixed Message Color writing
  -- (XvT.Mission) Fixed Team writing
  -- (XvT.Mission) Fixed Briefing Team read/write
  -- (XvT.Strings) Added empty CraftWhen entries
  -- (XvT.Strings) Added "each special craft" amount
  -- (XWA.FlightGroup) Added Ion Pulse warhead
  -- (XWA.FlightGroup.Goal) Updated ToString
  -- (XWA.Mission) Fixed Unk3 initialization
  -- (XWA.Mission) Fixed craft Options
  -- (XWA.Strings) Added Unk Role values
  -- (XWA.Strings) Added Unk Amount values

v2.4 - 6 June 2016
 - (Converter) Fixed XWA to TIE player's craft conversion
 - (All) Inverted the Y axis on all waypoints during read/write so in-game map and editor appearance match, but editor is still "positive up"
 - Fixed various references in comments
 
v2.3 - 5 Apr 2015
 - (BaseTrigger) Added TriggerIndex enum
 - (XvT.Globals.Goal) set to Serializable
 - (XvT.Globals.Goal) Deleted GoalStringIndexer, strings wrapped into new subclass, Trigger
 
v2.2 - 5 Feb 2015
 - (Team.*) set classes to Serializable (YOGEME Issue #8)
 - (BaseTrigger) set to Serializable (YOGEME Issue #5)
 
v2.1 - 14 Dec 2014
 - Converted license to MPLv2.0
 - (*.FlightGroupCollection) SetCount and IsModified implementation
 - (*.MessageCollection) SetCount and IsModified implementation
 - Couple other minor fixes that have been sitting here for a while...
 
v2.0.1 - 14 Aug 2012
 - (BaseBriefing) Fixed bug regarding StartLength calculation
 - (*.FlightGroup) Fixed bug preventing proper SpecialCargoCraft handling during Load/Save
 - (Tie.FlightGroup) Added Unknowns.Unknown19, 20 and 21
 - (Tie.Officers) Fixed bug in Save() causing '[' and ']' to save as characters instead of the appropriate highlighiting codes
 - (Xvt.Briefing) Fixed bug in Save() preventing proper Event writing
 - (Xvt.Mission) Fixed critical bug in LoadMission() that resulted in unhandled exception
 - (Xwa.Mission) Fixed critical bug in Save() causing infinite loop and filesize
 
v2.0 - 25 May 2012
 - (BaseBriefing) Added EventParameterCount
 - (BaseBriefing) Events[] is now short[]
 - (BaseFlightGroup.BaseOrder) New
 - (BaseFlightGroup.BaseWaypoint) New
 - (BaseTrigger) Inherits Indexer<byte>
 - (*.FlightGroup) Added ToString() overrides
 - (*.FlightGroup.Goal) Added ToString() override, exceptions
 - (*.FlightGroup.Goal) Inherits Indexer<byte>
 - (*.FlightGroup.LoadoutIndexer) Inherits Indexer<bool>
 - (*.FlightGroup.Order) Added ToString() overrides, exceptions, conversions
 - (*.FlightGroup.Order) Inherits BaseFlightGroup.BaseOrder
 - (*.FlightGroup.Waypoint) Added conversions
 - (*.FlightGroup.Waypoint) Inherits BaseFlightGroup.BaseWaypoint
 - (*.FlightGroupCollection) Added GetList()
 - (*.Mission) Deleted NumFlightGroups, NumMessages
 - (*.Mission) Inherits MissionFile
 - (*.Mission.Trigger) Added conversions, exceptions, ToString() overrides
 - (*.TeamCollection) Added GetList()
 - (Tie.FlightGroup) Radio renamed to FollowsOrders
 - (Tie.FlightGroup.FGGoals) Added exceptions
 - (Tie.FlightGroup.FGGoals) Inherits Indexer<byte>
 - (Tie.Mission) Added CraftCheck(), CheckTarget()
 - (Tie.Mission) IffHostile inherits Indexer<bool>
 - (Tie.Mission) EndOfMissionMessages inherits Indexer<string>
 - (Tie.Mission.IffNameIndexer) Inherits Indexer<string>
 - (Xvt.FlightGroup) Roles is now Indexer<string>
 - (Xvt.Globals.Goal) Removed AndOrIndexer
 - (Xvt.Globals.Goal) AndOr is now bool[]
 - (Xvt.Mission) Added CraftCheck(), CheckTarget(), MissionTypeEnum
 - (Xvt.Mission) BoP renamed to IsBop
 - (Xvt.Strings) Removed MissType
 - (Xvt.Team) EndOfMissionMessages is now Indexer<string>
 - (Xwa.Briefing) Removed Team.set{}
 - (Xwa.Briefing) Added BriefingStringNotes
 - (Xwa.FlightGroup.Waypoint) Added ToString() override
 - (Xwa.Mission) Added LogoEnum, HangarEnum, *Notes
 - (Xwa.Mission) GlobalGroups, Regions, Iffs are now Indexer<string>
 - (Xwa.Mission) Hangar renamed to MissionType
 - (Xwa.Mission.Trigger) ctor(byte[]) now accepts Length = 4
 - (Xwa.Mission.Strings) Removed Logo, Hangar
 - Back-end updates

==========
Additional Information

Idmr.Common.dll (v1.1 or later) is a required reference

Mission*.txt files contain file structure information per platform

Programmer's reference can be found in help/Idmr.Platform.chm

==========
Copyright Information

Copyright � 2009-2017 Michael Gaisser
This library file and related files are licensed under the Mozilla Public License
v2.0 or later.  See MPL.txt for further details.

The Galactic Empire: Empire Reborn is Copyright � 2004- Tiberius Fel

"Star Wars" and related items are trademarks of LucasFilm Ltd and
LucasArts Entertainment Co.

THESE FILES HAVE BEEN TESTED AND DECLARED FUNCTIONAL BY THE IDMR, AS SUCH THE
IDMR AND THE GALACTIC EMPIRE: EMPIRE REBORN CANNOT BE HELD RESPONSIBLE OR
LIABLE FOR UNWANTED EFFECTS DUE ITS USE OR MISUSE. THIS SOFTWARE IS OFFERED AS
IS WITHOUT WARRANTY OF ANY KIND.