﻿/*
 * Idmr.Platform.dll, X-wing series mission library file, TIE95-XWA
 * Copyright (C) 2009-2015 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the MPL v2.0 or later
 * 
 * Full notice in ../help/Idmr.Platform.chm
 * Version: 2.1
 */

/* CHANGELOG
 * v2.1, 141214
 * [UPD] change to MPL
 */

using System;
using Idmr.Common;

namespace Idmr.Platform.Xvt
{
	/// <summary>Object for individual Team's mission-wide goals</summary>
	[Serializable] public partial class Globals
	{
		Goal[] _goals = new Goal[3];

		/// <summary>Goal indexes</summary>
		public enum GoalIndex : byte
		{
			/// <summary>Primary goals</summary>
			Primary,
			/// <summary>Prevent goals</summary>
			Prevent,
			/// <summary>Secondary goals</summary>
			Secondary
		}
		/// <summary>Goal status</summary>
		public enum GoalState : byte
		{
			/// <summary>Goals that have not yet been completed.<br/>Does not apply to Secondary goals</summary>
			Incomplete,
			/// <summary>Goals that have been completed</summary>
			Complete,
			/// <summary>Goals that cannot be completed due to gameplay (mission design may permit inability to complete goals without direct failure).<br/>Does not apply to Prevent or Secondary goals</summary>
			Failed
		}
		
		/// <summary>Creates a new Globals object</summary>
		/// <remarks>Three <see cref="Goals"/>, each with four <see cref="Goal.Triggers"/> all set to <b>"never (FALSE)"</b></remarks>
		public Globals() { for (int i = 0; i < 3; i++) _goals[i] = new Goal(); }
		
		/// <summary>Gets the Global Goals</summary>
		/// <remarks>Use the <see cref="GoalIndex"/> enumeration for indexes</remarks>
		public Goal[] Goals { get { return _goals; } }
	}
}
