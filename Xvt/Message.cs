﻿/*
 * Idmr.Platform.dll, X-wing series mission library file, TIE95-XWA
 * Copyright (C) 2009-2014 Michael Gaisser (mjgaisser@gmail.com)
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

namespace Idmr.Platform.Xvt
{
	/// <summary>Object for individual in-flight messages</summary>
	/// <remarks>Class is serializable to allow copy and paste functionality</remarks>
	[Serializable]
	public class Message : BaseMessage
	{
		Mission.Trigger[] _triggers = new Mission.Trigger[4];
		bool[] _sentToTeam = new bool[10];
		string _note = "";

		/// <summary>Creates a new Message object</summary>
		public Message()
		{
			for (int i = 0; i < 4; i++) _triggers[i] = new Mission.Trigger();
			_sentToTeam[0] = true;
		}

		/// <summary>Gets the Triggers that control the Message behaviour</summary>
		/// <remarks>Array length is 4</remarks>
		public Mission.Trigger[] Triggers { get { return _triggers; } }
		/// <summary>Gets the array that control which teams can receive the message</summary>
		/// <remarks>Array length is 10</remarks>
		public bool[] SentToTeam { get { return _sentToTeam; } }
		/// <summary>Gets or sets if both triggers must be completed</summary>
		/// <remarks><b>false</b> is "And", <b>true</b> is "Or", defaults to <b>false</b></remarks>
		public bool T1AndOrT2 { get; set; }
		/// <summary>Gets or sets if both triggers must be completed</summary>
		/// <remarks><b>false</b> is "And", <b>true</b> is "Or", defaults to <b>false</b></remarks>
		public bool T3AndOrT4 { get; set; }
		/// <summary>Gets or sets if both trigger pairs must be completed</summary>
		/// <remarks><b>false</b> is "And", <b>true</b> is "Or", defaults to <b>false</b></remarks>
		public bool T12AndOrT34 { get; set; }
		/// <summary>Gets or sets the string used as editor notes</summary>
		/// <remarks>Value is restricted to 15 characters</remarks>
		public string Note
		{
			get { return _note; }
			set { _note = Idmr.Common.StringFunctions.GetTrimmed(value, 0xF); }
		}
		/// <summary>Gets or sets the seconds after trigger is fired divded by five</summary>
		/// <remarks>Default is <b>zero</b>. Value of <b>1</b> is 5s, <b>2</b> is 10s, etc.</remarks>
		public byte Delay { get; set; }
	}
}
