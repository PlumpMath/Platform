﻿/*
 * Idmr.Platform.dll, X-wing series mission library file, TIE95-XWA
 * Copyright (C) 2009-2012 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in ../help/Idmr.Platform.chm
 * Version: 2.0.1
 */

/* CHANGELOG 
 */

using System;
using Idmr.Common;

namespace Idmr.Platform.Xwa
{
	public partial class Team
	{
		/// <summary>Object to provide array access to the EoM Message values</summary>
		/// <remarks>Six EoM Messages, use <see cref="EomMessageIndex"/> for indexes</remarks>
		public class EomMessageIndexer : Indexer<string>
		{
			/// <summary>Initializes the indexer</summary>
			/// <remarks>Character limit set to 64</remarks>
			/// <param name="eomMessages">The EoM Message array</param>
			public EomMessageIndexer(string[] eomMessages) : base(eomMessages, 64) { }
			
			/// <summary>Gets or sets the EoM Message</summary>
			/// <remarks>64 character limit</remarks>
			/// <param name="index">EomMessageIndex</param>
			public string this[EomMessageIndex index]
			{
				get { return _items[(int)index]; }
				set { this[(int)index] = value; }
			}
		}
	}
}