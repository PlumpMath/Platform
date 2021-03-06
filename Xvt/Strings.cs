﻿/*
 * Idmr.Platform.dll, X-wing series mission library file, TIE95-XWA
 * Copyright (C) 2009-2017 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the MPL v2.0 or later
 * 
 * Full notice in ../help/Idmr.Platform.chm
 * Version: 2.6
 */

/* CHANGELOG
 * v2.6, 151017
 * [NEW YOGEME #10] ability to replace craft list
 * v2.5, 170107
 * [UPD] Additional CraftWhen blanks [JB]
 * [NEW] "each special craft" [JB]
 * v2.1, 141214
 * [UPD] change to MPL
 * v2.0, 120525
 * [DEL] removed MissType
 */

using System;

namespace Idmr.Platform.Xvt
{
	/// <summary>Object for string lists used in XvT and BoP</summary>
	/// <remarks>All arrays return Clones to prevent editing</remarks>
	public abstract class Strings : BaseStrings
	{
		#region array declarations
		static string[] _roles = { "None",
									"Base",
									"Command Ship",
									"Convoy Craft",
									"Manufacturing Fac.",
									"Mission Critical Craft",
									"Primary Target",
									"Reload Craft",
									"Resource Center",
									"Secondary Target",
									"Station",
									"Strike Craft",
									"Tertiary Target"
								};
		static string[] _radio = { "None",
										"Team 1 (Imperial)",
										"Team 2 (Rebel)",
										"Team 3",
										"Team 4",
										"Team 5",
										"Team 6",
										"Team 7",
										"Team 8",
										"Player 1",
										"Player 2",
										"Player 3",
										"Player 4",
										"Player 5",
										"Player 6",
										"Player 7",
										"Player 8"
									};
		static string[] _iff = { "Rebel",
										"Imperial",
										"Blue",
										"Yellow",
										"Red",
										"Purple"
								  };
		static string[] _beam = { "None",
									"Tractor beam",
									"Jamming beam",
									"Decoy Beam"
							   };
		static string[] _craftType = { "None",
										"X-Wing",
										"Y-Wing",
										"A-Wing",
										"B-Wing",
										"TIE Fighter",
										"TIE Interceptor",
										"TIE Bomber",
										"TIE Advanced",
										"*TIE Defender",
										"10",
										"11",
										"*Missile Boat",
										"T-Wing",
										"Z-95 Headhunter",
										"R-41 Starchaser",
										"Assault Gunboat",
										"Lambda Shuttle",
										"Delta Escort Shuttle",
										"IPV Patrol Craft",
										"*Scout Craft",
										"Delta Transport",
										"Gamma Assault Transport",
										"Beta Escort Transport",
										"Tug",
										"Combat Utility Vehicle",
										"Container A",
										"Container B",
										"Container C",
										"Container D",
										"Heavy Lifter",
										"31",
										"Bulk Freighter",
										"Cargo Ferry",
										"Modular Conveyor",
										"*Container Transport",
										"Medium Transport",
										"Murrian Transport",
										"Corellian Transport",
										"39",
										"Corellian Corvette",
										"Modified Corvette",
										"Nebulon-B Frigate",
										"Modified Frigate",
										"*C-3 Passenger Liner",
										"*Carrack Cruiser",
										"Strike Cruiser",
										"Escort Carrier",
										"Dreadnaught",
										"MC80a Cruiser",
										"MC40a Light Cruiser",
										"Interdictor",
										"Victory Star Destroyer",
										"Imperator Star Destroyer",
										"Executor Star Destroyer",
										"Container E",
										"Container F",
										"Container G",
										"Container H",
										"Container I",
										"Platform A",
										"Platform B",
										"Platform C",
										"Platform D",
										"Platform E",
										"Platform F",
										"Asteroid R&D Facility",
										"Ast. Laser Battery",
										"Ast. Warhead Battery",
										"X/7 Factory",
										"Satellite 1",
										"Satellite 2",
										"71",
										"72",
										"73",
										"Mine A",
										"Mine B",
										"Mine C",
										"Gun Emplacement",
										"78",
										"Probe A",
										"Probe B",
										"81",
										"Nav Bouy 1",
										"Nav Bouy 2",
										"84",
										"Asteroid Field",
										"Planet",
										"87",
										"88",
										"Shipyard",
										"Repair Yard",
										"Modified Strike Cruiser"
									};
		static string[] _craftAbbrv = { "",
										"X-W",
										"Y-W",
										"A-W",
										"B-W",
										"TIE",
										"T/I",
										"T/B",
										"T/A",
										"*T/D",
										"",
										"",
										"*MIS",
										"T-W",
										"Z-95",
										"R-41",
										"GUN",
										"SHU",
										"E/S",
										"SPC",
										"*SCT",
										"TRN",
										"ATR",
										"ETR",
										"TUG",
										"CUV",
										"CN/A",
										"CN/B",
										"CN/C",
										"CN/D",
										"HLF",
										"",
										"FRT",
										"CARG",
										"CNVYR",
										"*CTRNS",
										"MDTRN",
										"MUTR",
										"CORT",
										"",
										"CRV",
										"M/CRV",
										"FRG",
										"M/FRG",
										"*LINER",
										"*CRCK",
										"STRKC",
										"ESC",
										"DREAD",
										"CRS",
										"CRL",
										"INT",
										"VSD",
										"ISD",
										"ESD",
										"CN/E",
										"CN/F",
										"CN/G",
										"CN/H",
										"CN/I",
										"PLT/1",
										"PLT/2",
										"PLT/3",
										"PLT/4",
										"PLT/5",
										"PLT/6",
										"R&D FC",
										"LAS BAT",
										"W LNCHR",
										"FAC/1",
										"SAT 1",
										"SAT/2",
										"",
										"",
										"",
										"MINE A",
										"MINE B",
										"MINE C",
										"GUNPLT",
										"",
										"PROBE A",
										"PROBE B",
										"",
										"NAV 1",
										"NAV 2",
										"",
										"Asteroid",
										"Planet",
										"",
										"",
										"SHPYD",
										"REPYD",
										"M/SC"
									 };
		static string[] _rating = { "Rookie",
									"Officer",
									"Veteran",
									"Ace",
									"Top Ace",
									"Jedi"
								 };
		static string[] _status = { "Normal",
									"2x Warheads",
									"1/2 Warheads",
									"Disabled",
									"1/2 Shields",
									"No Lasers",
									"No Hyperdrive",
									"Shields 0%, charging",
									"Shields added (200%)",
									"Hyperdrive added",
									"",
									"",
									"(200% Shields)",
									"Shields 50%, charging",
									"(No Lasers)",
									"",
									"Shields + Hyperdrive added",
									"",
									"200% Shields",
									"(50% Shields)",
									"Invincible",
									"Infinite Warheads"
								 };
		static string[] _trigger = { "always (TRUE)",
										"be created",
										"be destroyed",
										"be attacked",
										"be captured",
										"be inspected",
										"be boarded",
										"be docked",
										"be disabled",
										"have survived (exist)",
										"none (FALSE)",
										"---",
										"complete mission",
										"complete primary mission",
										"fail primary mission",
										"complete secondary mission",
										"fail secondary mission",
										"complete bonus mission",
										"fail bonus mission",
										"be dropped off",
										"be reinforced",
										"have 0% shields",
										"have 50% hull",
										"run out of missiles",
										"Unknown (arrive?)",
										"be dropped off",
										"be destroyed in one hit",
										"NOT be disabled",
										"NOT be picked up",
										"be destroyed, not inspected",
										"be docked with",
										"NOT be docked with",
										"begin boarding",
										"NOT begin boarding",
										"have 50% shields",
										"have 25% shields",
										"have 75% hull",
										"have 25% hull",
										"Unknown",
										"Unknown",
										"Unknown",
										"be all Player Craft",
										"reinforced by AI?",
										"come and go",
										"be picked up",
										"withdraw",
										"be carried away"
								  };
		static string[] _triggerType = { "none",
											"Flight Group",
											"Ship type",
											"Ship class",
											"Object type",
											"IFF",
											"Ship orders",
											"Craft when",
											"Global Group",
											"",
											"",
											"",
											"Team",
											"",
											"",
											"",
											"",
											"",
											"",
											"",
											"",
											"All teams except",
											"",
											"Global Unit"
									  };
		static string[] _amount = { "100%",
									"75%",
									"50%",
									"25%",
									"at least 1 of",
									"all but 1 of",
									"all special craft in",
									"all non-special craft in",
									"all non-player craft in",
									"player's craft in",
									"100% of first wave",
									"75% of first wave",
									"50% of first wave",
									"25% of first wave",
									"at least 1 of first wave",
									"all but 1 of first wave",
									"66%",
									"33%",
									"each craft",
									"each special craft"
								 };
		static string[] _orders = { "Hold Steady",
									"Go Home",
									"Circle",
									"Circle and Evade",
									"Rendezvous",
									"Disabled",
									"Awaiting Boarding",
									"Attack targets",
									"Attack escorts",
									"Protect",
									"Escort",
									"Disable targets",
									"Board to Give cargo",
									"Board to Take cargo",
									"Board to Exchange cargo",
									"Board to Capture",
									"Board to Destroy cargo",
									"Pick up / Bag",
									"Drop off",
									"Wait",
									"SS Wait",
									"SS Patrol waypoints",
									"SS Await Return",
									"SS Launch",
									"SS Protect",
									"SS Wait / Protect",
									"SS Patrol and Attack",
									"SS Patrol and Disable",
									"SS Hold Station",
									"SS Go Home",
									"SS Wait",
									"SS Board",
									"Board to Repair",
									"Hold Station",
									"Hold Steady",
									"SS Hold Station",
									"Self Destruct",
									"Kamikaze",
									"SS Disabled",
									"SS Await boarding"
								 };
		static string[] _craftWhen = { "",
										"Inspected",
										"Boarded",
										"",
										"Disabled",
										"Attacked",
										"0% shields?",
										"Special craft",
										"Non-special craft",
										"Player's craft",
										"Non-player craft",
										"",
										"NOT Disabled",
										"",
	  									"",
	  									"",
										"",
										"",
										"",
										"",
	  									"",
	  									"",
	  									"" //[JB] Need to load file: Combat\8b02w09.tie
									};
		static string[] _abort = { "never",
									"0% shields",
									"",
									"out of warheads",
									"50% hull",
									"attacked",
									"50% shields",
									"25% shields",
									"75% hull",
									"25% hull"
								};
		static string[] _orderDesc = { "Stationary, 100% Systems, does not return fire. If not first order, craft flies home|Meaningless|Meaningless",
										"Fly to Mothership, or Hyperspace|Meaningless|Meaningless",
										"Circle through Waypoints.  Ignores targets, returns fire|# of loops|Meaningless",
										"Circles through Waypoints, evading attackers.  Ignores targets, returns fire|# of loops|Meaningless",
										"Fly to Rendezvous point and await docking. Ignores targets, returns fire|# of dockings|Meaningless",
										"Disabled|Meaningless|Meaningless",
										"Disabled, awaiting boarding|# of dockings|Meaningless",
										"Attacks targets (not for starships)|Component?|Meaningless",
										"Attacks escorts of targets|Meaningless|Meaningless",
										"Attacks craft that attack targets, ignores boarding craft|Meaningless|Meaningless",
										"Attacks craft that attack targets, including boarding craft|Meaningless|Attack Player",
										"Attacks to disable.  Warheads used to lower shields|Meaningless|Meaningless",
										"Boards targets (if stationary) to give cargo|Docking time (x5 sec)|# of dockings",
										"Boards targets (if stationary) to take cargo|Docking time (x5 sec)|# of dockings",
										"Boards targets (if stationary) to exchange cargo|Docking time (x5 sec)|# of dockings",
										"Boards targets (if stationary) to capture|Dockings time (x5 sec)|# of dockings",
										"Boards targets (if stationary) to plant explosives. Target will explode when complete|Docking time (x5 sec)|# of dockings",
										"Dock or pickup target, carry for remainder of mission or until dropped|Docking time (x5 sec)|Meaningless",
										"Drops off designated Flight Group (disregards targets)|Deploy time? (x5 sec)|Flight Group #",
										"Waits for designated time before continuing. Returns fire|Wait time (x5 sec)|Meaningless",
										"Waits for designated time before continuing. Returns fire|Wait time (x5 sec)|Meaningless",
										"Circles through Waypoints. Attacks targets, returns fire|# of loops|Meaningless",
										"Wait for the return of all FGs with it as their Mothership. Attacks targets, returns fire|Meaningless|Meaningless",
										"Waits for the launch of all FGs with it as their Mothership. Attacks targets, returns fire|Meaningless|Meaningless",
										"Circles through Waypoints attacking craft that attack targets. Returns fire|Meaningless|Meaningless",
										"Circles through Waypoints attacking craft that attack targets. Returns fire|Meaningless|Meaningless",
										"Circles through Waypoints attacking targets. Returns fire|Meaningless|Meaningless",
										"Circles through Waypoints attacking targets to disable. Returns fire|Meaningless|Meaningless",
										"Waits for designated time before continuing. Does not return fire|Meaningless|Meaningless",
										"Fly to Mothership, or Hyperspace. Attacks targets, returns fire|Meaningless|Meaningless",
										"Waits for designated time before continuing. Does not return fire|Meaningless|Meaningless",
										"Boards targets (if stationary)|Docking time (x5 sec)|# of dockings",
										"Boards targets (if stationary) to repair systems|Docking time (x5 sec)|# of dockings",
										"Stationary, 100% Systems, returns fire|Meaningless|Meaningless",
										"Stationary, 100% Systems, returns fire|Meaningless|Meaningless",
										"Stationary, 100% Systems, returns fire|Meaningless|Meaningless",
										"Craft destroys self|Delay time (x5 sec)|Meaningless",
										"Craft rams target|Meaningless|Meaningless",
										"Stationary, 100% Systems, returns fire|Meaningless|Meaningless",
										"Stationary, 100% Systems, returns fire|Meaningless|Meaningless"
									};
		#endregion

		/// <summary>Replaces <see cref="CraftType"/> and <see cref="CraftAbbrv"/> with custom arrays.</summary>
		/// <param name="craftTypes">Array of new craft types.</param>
		/// <param name="craftAbbrv">Array of new craft abbreviations.</param>
		/// <exception cref="ArgumentException">The <see cref="Array.Length"/> of the arrays do match the originals.</exception>
		/// <exception cref="ArgumentNullException">Either or both of the input arrays are <b>null</b>.</exception>
		public static void OverrideShipList(string[] craftTypes, string[] craftAbbrv)
		{
			if (craftAbbrv == null || craftTypes == null)
				throw new ArgumentNullException("At least one of the arrays is null, check for valid inputs.");
			if (craftTypes.Length != _craftType.Length || craftAbbrv.Length != _craftAbbrv.Length)
				throw new ArgumentException("New arrays must match original length.");
			_craftType = craftTypes;
			_craftAbbrv = craftAbbrv;
		}

		/// <summary>Gets of copy of the craft roles used for specialized in-flight messages</summary>
		/// <remarks>Array is Length = 13</remarks>
		public static string[] Roles { get { return (string[])_roles.Clone(); } }
		/// <summary>Gets of copy of the radio channels the craft uses</summary>
		/// <remarks>Array is length = 17</remarks>
		public static string[] Radio { get { return (string[])_radio.Clone(); } }
		/// <summary>Gets of copy of the default IFF Names</summary>
		/// <remarks>Array is Length = 6</remarks>
		public static string[] IFF { get { return (string[])_iff.Clone(); } }
		/// <summary>Gets of copy of the beam weapons for craft use</summary>
		/// <remarks>Array is Length = 4</remarks>
		public static string[] Beam { get { return (string[])_beam.Clone(); } }
		/// <summary>Gets of copy of the long name for ship type</summary>
		/// <remarks>Array is Length = 93</remarks>
		public static string[] CraftType { get { return (string[])_craftType.Clone(); } }
		/// <summary>Gets of copy of the short name for ship type</summary>
		/// <remarks>Array is Length = 93</remarks>
		public static string[] CraftAbbrv { get { return (string[])_craftAbbrv.Clone(); } }
		/// <summary>Gets of copy of the AI craft settings</summary>
		/// <remarks>Array is Length = 6</remarks>
		public static string[] Rating { get { return (string[])_rating.Clone(); } }
		/// <summary>Gets of copy of the FlightGroup initial state parameter</summary>
		/// <remarks>Array is Length = 22</remarks>
		public static string[] Status { get { return (string[])_status.Clone(); } }
		/// <summary>Gets of copy of the conditions required to complete trigger</summary>
		/// <remarks>Array is Length = 47</remarks>
		public static string[] Trigger { get { return (string[])_trigger.Clone(); } }
		/// <summary>Gets of copy of the category that the Trigger Parameter belongs to</summary>
		/// <remarks>Array is Length = 24</remarks>
		public static string[] VariableType { get { return (string[])_triggerType.Clone(); } }
		/// <summary>Gets of copy of the quantities of applicable conditions that must be met</summary>
		/// <remarks>Array is Length = 20</remarks>
		public static string[] Amount { get { return (string[])_amount.Clone(); } }
		/// <summary>Gets of copy of the FlightGroup orders</summary>
		/// <remarks>Array is Length = 40</remarks>
		public static string[] Orders { get { return (string[])_orders.Clone(); } }
		/// <summary>Gets of copy of the craft behaviours to be used in triggers</summary>
		/// <remarks>Array is Length = 22</remarks>
		public static string[] CraftWhen { get { return (string[])_craftWhen.Clone(); } }
		/// <summary>Gets of copy of the individual craft abort conditions</summary>
		/// <remarks>Array is Length = 10</remarks>
		public static string[] Abort { get { return (string[])_abort.Clone(); } }
		/// <summary>Gets of copy of the descriptions of orders and variables</summary>
		/// <remarks>Array is Length = 40</remarks>
		public static string[] OrderDesc { get { return (string[])_orderDesc.Clone(); } }
	}
}