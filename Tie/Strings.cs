﻿using System;

namespace Idmr.Platform.Tie
{
	/// <summary>Object for string lists used in TIE</summary>
	/// <remarks>All arrays return Clones to prevent editing</remarks>
	public abstract class Strings : BaseStrings
	{
		#region array definitions
		private static string[] _iff = { "Rebel",
								  "Imperial",
								  "IFF3-Blue",
								  "IFF4-Purple",
								  "IFF5-Red",
								  "IFF6-Purple"
							  };
		private static string[] _beam = { "None",
								   "Tractor beam",
								   "Jamming beam"
							   };
		private static string[] _craftType = { "None",
										"X-Wing",
										"Y-Wing",
										"A-Wing",
										"B-Wing",
										"TIE Fighter",
										"TIE Interceptor",
										"TIE Bomber",
										"TIE Advanced",
										"TIE Defender",
										"*Shipyard",
										"*Repair Yard",
										"Missile Boat",
										"T-Wing",
										"Z-95 Headhunter",
										"R-41 Starchaser",
										"Assault Gunboat",
										"Lambda Shuttle",
										"Delta Escort Shuttle",
										"IPV Patrol Craft",
										"Scout Craft",
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
										"*Gun Emplacement",
										"Bulk Freighter",
										"Cargo Ferry",
										"Modular Conveyor",
										"Container Transport",
										"*Medium Transport",
										"Murrian Transport",
										"Corellian Transport",
										"*Modified Strike Cruiser",
										"Corellian Corvette",
										"Modified Corvette",
										"Nebulon-B Frigate",
										"Modified Frigate",
										"C-3 Passenger Liner",
										"Carrack Cruiser",
										"Strike Cruiser",
										"Escort Carrier",
										"Dreadnaught",
										"MC80a Cruiser",
										"MC40a Light Cruiser",
										"Interdictor",
										"Victory Star Destroyer",
										"Imperator Star Destroyer",
										"*Executor Star Destroyer",
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
										"77",
										"78",
										"Probe A",
										"Probe B",
										"81",
										"Nav Buoy 1",
										"Nav Buoy 2",
										"Asteroid Field 1",
										"Asteroid Field 2",
										"Planet"
									};
		private static string[] _craftAbbrv = { "",
										 "X-W",
										 "Y-W",
										 "A-W",
										 "B-W",
										 "TIE",
										 "T/I",
										 "T/B",
										 "T/A",
										 "T/D",
										 "*SHPYD",
										 "*REPYD",
										 "MIS",
										 "T-W",
										 "Z-95",
										 "R-41",
										 "GUN",
										 "SHU",
										 "E/S",
										 "SPC",
										 "SCT",
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
										 "*GPLT",
										 "FRT",
										 "CARG",
										 "CNVYR",
										 "CTRNS",
										 "MDTRN",
										 "MUTR",
										 "CORT",
										 "*M/SC",
										 "CRV",
										 "M/CRV",
										 "FRG",
										 "M/FRG",
										 "LINER",
										 "CRCK",
										 "STRKC",
										 "ESC",
										 "DREAD",
										 "CRS",
										 "CRL",
										 "INT",
										 "VSD",
										 "ISD",
										 "*SSD",
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
										 "",
										 "",
										 "PROBE A",
										 "PROBE B",
										 "",
										 "NAV 1",
										 "NAV 2",
										 "Asteroid",
										 "Asteroid",
										 "Planet"
									 };
		private static string[] _rating = { "Rookie",
									 "Novice",
									 "Veteran",
									 "Officer",
									 "Ace",
									 "Top Ace (invul)"
								 };
		private static string[] _status = { "Normal",
									 "2x Warheads",
									 "1/2 Warheads",
									 "Disabled",
									 "1/2 Shields",
									 "No Lasers",
									 "No Hyperdrive",
									 "Shields 0%, charging",
									 "Shields added",
									 "Hyperdrive added",
									 "",
									 "",
									 "",
									 "",
									 "",
									 "",
									 "",
									 "",
									 "",
									 "",
									 "Invincible"
								 };
		private static string[] _trigger = { "always (TRUE)",
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
									  "Unknown (arrive?)"
								  };
		private static string[] _triggerType = { "none",
										  "Flight Group",
										  "Ship type",
										  "Ship class",
										  "Object type",
										  "IFF",
										  "Ship orders",
										  "Craft when",
										  "Global Group",
										  "Misc"
									  };
		private static string[] _amount = { "100%",
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
									 "all but 1 of first wave"
								 };
		private static string[] _goalAmount = { "100%",
									  "50%",
									  "at least 1 of",
									  "all but 1 of",
									  "all special craft in"
								  };
		private static string[] _orders = { "Hold Steady",
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
									 "Go Home",
									 "Evade Waypoint 1",
									 "Evade Waypoint 1",
									 "Rendezvous II",
									 "SS Disabled"
								 };
		private static string[] _craftWhen = { "",
										"Boarding",
										"Boarded",
										"Defending",
										"Disabled",
										"",
										"",
										"Special craft",
										"Non-special craft",
										"Player's craft",
										"Non-player craft",
										""
									};
		private static string[] _misc = { "Rookie craft",
								   "Novice craft",
								   "Officer craft",
								   "Veteran craft",
								   "Ace craft",
								   "Top Ace craft",
								   "Stationary craft",
								   "Craft returning to base",
								   "Non-evading craft",
								   "Craft in formation",
								   "Rendezvousing craft",
								   "Disabled craft",
								   "Craft awaiting orders",
								   "Attacking craft",
								   "Craft attacking escorts",
								   "Protecting craft",
								   "Escorting craft",
								   "Disabling craft",
								   "Delivering craft",
								   "Seizing craft",
								   "Exchanging craft",
								   "Capturing craft",
								   "Craft destroying cargo",
								   "Bagging craft",
								   "Drop Off craft",
								   "Waiting fighters",
								   "Waiting starships",
								   "Patrolling SS",
								   "SS waiting for returns",
								   "SS waiting to launch",
								   "SS waiting for boarding craft",
								   "SS waiting for boarding craft",
								   "SS attacking",
								   "SS disabling",
								   "SS disabling?",
								   "SS flying home",
								   "Rebels",
								   "Imperials",
								   "",
								   "Spacecraft",
								   "Weapons",
								   "Satellites/Mines",
								   "",
								   "",
								   "",
								   "",
								   "Fighters",
								   "Transports",
								   "Freighters",
								   "Utility craft",
								   "Starships",
								   "Platforms",
								   "",
								   "",
								   "Mines"
							   };
		private static string[] _abort = { "never",
									"0% shields",
									"75% systems (not SS)",
									"out of warheads",
									"50% hull",
									"attacked"
								};
		private static string[] _orderDesc = { "Stationary, 100% Systems, does not return fire. If not first order, craft flies home|Meaningless|Meaningless",
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
										"Waits for designated time before continuing. Does not return fire|Wait time (x5 sec)|Meaningless",
										"Boards targets (if stationary)|Docking time (x5 sec)|# of dockings",
										"Boards targets (if stationary) to repair systems|Docking time (x5 sec)|# of dockings",
										"Stationary, 100% Systems, returns fire|Meaningless|Meaningless",
										"Stationary, 100% Systems, returns fire|Meaningless|Meaningless",
										"Enters Hyperspace|Meaningless|Meaningless",
										"Circles Waypoint 1|Meaningless|Meaningless",
										"Circles Waypoint 1|Meaningless|Meaningless",
										"Flies to Waypoint 1 and rendezvouses with other craft|Meaningless|Meaningless",
										"Disabled|Meaningless|Meaningless"
									};
		private static string[] _formation = { "Vic",
										"Finger Four",
										"Line Astern",
										"Line Abreast",
										"Echelon Right",
										"Echelon Left",
										"Double Astern",
										"Diamond",
										"Stack",
										"High X",
										"Vic Abreast",
										"High Vic",
										"Reverse High Vic"
									};
		#endregion
		/// <summary>Default IFF Names</summary>
		/// <remarks>Array is Length = 6</remarks>
		public static string[] IFF	{ get { return (string[])_iff.Clone(); } }
		/// <summary>Beam weapons for craft use</summary>
		/// <remarks>Array is Length = 3</remarks>
		public static string[] Beam { get { return (string[])_beam.Clone(); } }
		/// <summary>Long name for ship type</summary>
		/// <remarks>Array is Length = 88</remarks>
		public static string[] CraftType { get { return (string[])_craftType.Clone(); } }
		/// <summary>Short name for ship type</summary>
		/// <remarks>Array is Length = 88</remarks>
		public static string[] CraftAbbrv { get { return (string[])_craftAbbrv.Clone(); } }
		/// <summary>AI setting for craft</summary>
		/// <remarks>Array is Length = 6</remarks>
		public static string[] Rating { get { return (string[])_rating.Clone(); } }
		/// <summary>Flight Group initial state parameter</summary>
		/// <remarks>Array is Length = 21</remarks>
		public static string[] Status { get { return (string[])_status.Clone(); } }
		/// <summary>Condition required to complete trigger</summary>
		/// <remarks>Array is Length = 25</remarks>
		public static string[] Trigger { get { return (string[])_trigger.Clone(); } }
		/// <summary>Category that the Trigger Parameter belongs to</summary>
		/// <remarks>Array is Length = 10</remarks>
		public static string[] TriggerType { get { return (string[])_triggerType.Clone(); } }
		/// <summary>Quantity of applicable conditions that must be met</summary>
		/// <remarks>Array is Length = 16</remarks>
		public static string[] Amount { get { return (string[])_amount.Clone(); } }
		/// <summary>Quantity of applicable conditions that must be met for FlightGroup Goals</summary>
		/// <remarks>Array is Length = 5</remarks>
		public static string[] GoalAmount { get { return (string[])_goalAmount.Clone(); } }
		/// <summary>FlightGroup orders</summary>
		/// <remarks>Array is Length = 40</remarks>
		public static string[] Orders { get { return (string[])_orders.Clone(); } }
		/// <summary>Craft behaviour to be used in triggers</summary>
		/// <remarks>Array is Length = 12</remarks>
		public static string[] CraftWhen { get { return (string[])_craftWhen.Clone(); } }
		/// <summary>Miscellaneous triggers</summary>
		/// <remarks>Array is Length = 55</remarks>
		public static string[] Misc { get { return (string[])_misc.Clone(); } }
		/// <summary>Individual craft abort conditions</summary>
		/// <remarks>Array is Length = 6</remarks>
		public static string[] Abort { get { return (string[])_abort.Clone(); } }
		/// <summary>Description of orders and variables</summary>
		/// <remarks>Array is Length = 40</remarks>
		public static string[] OrderDesc { get { return (string[])_orderDesc.Clone(); } }
		/// <summary>FlightGroup formation</summary>
		/// <remarks>Array is Length = 13, replaces BaseStrings.Formation</remarks>
		new public static string[] Formation { get { return (string[])_formation.Clone(); } }
	}
}