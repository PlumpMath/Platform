﻿/*
 * Idmr.Platform.dll, X-wing series mission library file, TIE95-XWA
 * Copyright (C) 2009-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in ../help/Idmr.Platform.html
 * Version: 2.0
 */

/* CHANGELOG
 * 120212 - T[] to List<T> conversion
 */

using System;
using System.Collections.Generic;

namespace Idmr.Platform.Xvt
{
	/// <summary>Object to maintain Teams</summary>
	public class TeamCollection : Idmr.Common.FixedSizeCollection<Team>
	{
		/// <summary>Creates a new Collection of Teams (10)</summary>
		public TeamCollection()
		{
			_items = new List<Team>(10);
			for (int i = 0; i < _items.Capacity; i++) _items.Add(new Team(i));
		}

		/// <summary>Resets selected Team to defaults</summary>
		/// <param name="team">Team index</param>
		public void Clear(int team) { _setItem(team, new Team(team)); }

		/// <summary>Resets all Teams to defaults</summary>
		public void ClearAll() { for (int i = 0; i < Count; i++) _setItem(i, new Team(i)); }
	}
}