/*
 * Idmr.Platform.dll, X-wing series mission library file, TIE95-XWA
 * Copyright (C) 2009-2016 Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the MPL v2.0 or later
 * 
 * Full notice in ../help/Idmr.Platform.chm
 * Version: 2.4+
 */

/* CHANGELOG
 * [FIX] Enforced string encodings [JB]
 * [FIX] Craft options [JB]
 * [FIX] Message read length check [JB]
 * [FIX] FG Goal Unks write [JB]
 * [FIX] FG options writing [JB]
 * [FIX] write Message.Color [JB]
 * [FIX] Team write [JB]
 * [UPD] Briefing Teams R/W [JB]
 * v2.4, 160606
 * [FIX] Invert WP.Y at read/write
 * v2.1, 141214
 * [UPD] change to MPL
 * [FIX] Team.EndOfMissionMessageColor load/save
 * v2.0.1, 120814
 * [FIX] Critical load failure (located in briefing)
 * [FIX] Save failure (located in briefing)
 * [FIX] FlightGroup.SpecialCargoCraft load/save
 * v2.0, 120525
 * [NEW] CraftCheck(), CheckTarget()
 * - removed _briefings/_globals/_teams
 * [UPD] class inherits MissionFile
 * [DEL] removed NumFlightGroups, NumMessages
 * [UPD] BoP renamed to IsBop
 */

using System;
using System.IO;
using Idmr.Common;

namespace Idmr.Platform.Xvt
{
	/// <summary>Framework for XvT and BoP</summary>
	/// <remarks>This is the primary container object for XvT and BoP mission files</remarks>
	public partial class Mission : MissionFile
	{
		string _unknown4 = "";
		string _unknown5 = "";
		string _missionDescription = "";
		string _missionFailed = "";
		string _missionSuccessful = "";

		/// <summary>The types of mission</summary>
		public enum MissionTypeEnum : byte {
			/// <summary>Standard combat mission</summary>
			Training,
			/// <summary>Unknown mission type</summary>
			Unknown,
			/// <summary>Melee (Skirmish) mission</summary>
			Melee,
			/// <summary>Multiplayer combat mission</summary>
			MPTraining,
			/// <summary>Multiplayer Melee (Skirmish) mission</summary>
			MPMelee
		}

		#region constructors
		/// <summary>Default constructor, create a blank mission</summary>
		public Mission()
		{
			MissionType = MissionTypeEnum.Training;
			_initialize();
		}

		/// <summary>Create a new mission from a file</summary>
		/// <param name="filePath">Full path to the file</param>
		/// <exception cref="System.IO.FileNotFoundException"><i>filePath</i> does not exist</exception>
		/// <exception cref="System.IO.InvalidDataException"><i>filePath</i> is not a XvT or BoP mission file</exception>
		public Mission(string filePath)
		{
			_initialize();
			LoadFromFile(filePath);
		}

		/// <summary>Create a new mission from an open FileStream</summary>
		/// <param name="stream">Opened FileStream to mission file</param>
		/// <exception cref="InvalidDataException"><i>stream</i> is not a valid XvT or BoP mission file</exception>
		public Mission(FileStream stream)
		{
			_initialize();
			LoadFromStream(stream);
		}
		
		void _initialize()
		{
			_invalidError = _invalidError.Replace("{0}", "XvT or BoP");
			FlightGroups = new FlightGroupCollection();
			Messages = new MessageCollection();
			Globals = new GlobalsCollection();
			Teams = new TeamCollection();
			Briefings = new BriefingCollection();
		}
		#endregion

		#region public methods
		/// <summary>Load a mission from a file</summary>
		/// <param name="filePath">Full path to the file</param>
		/// <exception cref="System.IO.FileNotFoundException"><i>filePath</i> does not exist</exception>
		/// <exception cref="System.IO.InvalidDataException"><i>filePath</i> is not a XvT or BoP mission file</exception>
		public void LoadFromFile(string filePath)
		{
			if (!File.Exists(filePath)) throw new FileNotFoundException();
			FileStream fs = File.OpenRead(filePath);
			LoadFromStream(fs);
			fs.Close();
		}

		/// <summary>Load a mission from an open FileStream</summary>
		/// <param name="stream">Opened FileStream to mission file</param>
		/// <exception cref="InvalidDataException"><i>stream</i> is not a valid XvT or BoP mission file</exception>
		public void LoadFromStream(FileStream stream)
		{
			MissionFile.Platform p = MissionFile.GetPlatform(stream);
			if (p != MissionFile.Platform.XvT && p != MissionFile.Platform.BoP) throw new InvalidDataException(_invalidError);
			IsBop = (p == MissionFile.Platform.BoP);
			BinaryReader br = new BinaryReader(stream, System.Text.Encoding.Default); //[JB] Added encoding. BoP\Train\8xrcb06 fails otherwise
			int i, j;
			stream.Position = 2;
			short numFlightGroups = br.ReadInt16();
			short numMessages = br.ReadInt16();
			#region Platform
			Unknown1 = br.ReadByte();
			stream.Position++;
			Unknown2 = br.ReadByte();
			stream.Position = 0xB;
			Unknown3 = Convert.ToBoolean(br.ReadByte());
			stream.Position = 0x28;
			Unknown4 = new string(br.ReadChars(0x10)).Trim('\0');
			stream.Position = 0x50;
			Unknown5 = new string(br.ReadChars(0x10)).Trim('\0');
			stream.Position = 0x64;
			MissionType = (MissionTypeEnum)br.ReadByte();
			Unknown6 = Convert.ToBoolean(br.ReadByte());
			TimeLimitMin = br.ReadByte();
			TimeLimitSec = br.ReadByte();
			stream.Position = 0xA4;
			#endregion
			#region FlightGroups
			FlightGroups = new FlightGroupCollection(numFlightGroups);
			byte[] buffer = new byte[64];
			for (i=0;i<numFlightGroups;i++)
			{
				#region Craft
				FlightGroups[i].Name = new string(br.ReadChars(0x14));	// null-termed
				for (j=0;j<4;j++) FlightGroups[i].Roles[j] = new string(br.ReadChars(4));
				stream.Position += 4;
				FlightGroups[i].Cargo = new string(br.ReadChars(0x14));	// null-termed
				FlightGroups[i].SpecialCargo = new string(br.ReadChars(0x14));	// null-termed
				stream.Read(buffer, 0, 0x1A);
				FlightGroups[i].SpecialCargoCraft = buffer[0];
				FlightGroups[i].RandSpecCargo = Convert.ToBoolean(buffer[1]);
				FlightGroups[i].CraftType = buffer[2];
				FlightGroups[i].NumberOfCraft = buffer[3];
				if (!FlightGroups[i].RandSpecCargo)
				{
					if (FlightGroups[i].SpecialCargoCraft >= FlightGroups[i].NumberOfCraft) FlightGroups[i].SpecialCargoCraft = 0;
					else FlightGroups[i].SpecialCargoCraft++;
				}
				FlightGroups[i].Status1 = buffer[4];
				FlightGroups[i].Missile = buffer[5];
				FlightGroups[i].Beam = buffer[6];
				FlightGroups[i].IFF = buffer[7];
				FlightGroups[i].Team = buffer[8];
				FlightGroups[i].AI = buffer[9];
				FlightGroups[i].Markings = buffer[0xA];
				FlightGroups[i].Radio = buffer[0xB];
				FlightGroups[i].Formation = buffer[0xD];
				FlightGroups[i].FormDistance = buffer[0xE];
				FlightGroups[i].GlobalGroup = buffer[0xF];
				FlightGroups[i].FormLeaderDist = buffer[0x10];
				FlightGroups[i].NumberOfWaves = (byte)(buffer[0x11]+1);
				FlightGroups[i].Unknowns.Unknown1 = buffer[0x12];
				FlightGroups[i].Unknowns.Unknown2 = Convert.ToBoolean(buffer[0x13]);
				FlightGroups[i].PlayerNumber = buffer[0x14];
				FlightGroups[i].ArriveOnlyIfHuman = Convert.ToBoolean(buffer[0x15]);
				FlightGroups[i].PlayerCraft = buffer[0x16];
				FlightGroups[i].Yaw = (short)Math.Round((double)(sbyte)buffer[0x17] * 360 / 0x100);
				FlightGroups[i].Pitch = (short)Math.Round((double)(sbyte)buffer[0x18] * 360 / 0x100);
				FlightGroups[i].Pitch += (short)(FlightGroups[i].Pitch < -90 ? 270 : -90);
				FlightGroups[i].Roll = (short)Math.Round((double)(sbyte)buffer[0x19] * 360 / 0x100);
				stream.Position += 3;
				#endregion
				#region Arr/Dep
				stream.Read(buffer, 0, 0x35);
				FlightGroups[i].Difficulty = buffer[0];
				for (j=0;j<4;j++)
				{
					FlightGroups[i].ArrDepTriggers[0][j] = buffer[1+j];	// Arr1...
					FlightGroups[i].ArrDepTriggers[1][j] = buffer[5+j];
					FlightGroups[i].ArrDepTriggers[2][j] = buffer[0xC+j];
					FlightGroups[i].ArrDepTriggers[3][j] = buffer[0x10+j];
					FlightGroups[i].ArrDepTriggers[4][j] = buffer[0x1B+j];	// Dep1...
					FlightGroups[i].ArrDepTriggers[5][j] = buffer[0x1F+j];
				}
				FlightGroups[i].ArrDepAO[0] = Convert.ToBoolean(buffer[0xB]);
				FlightGroups[i].ArrDepAO[1] = Convert.ToBoolean(buffer[0x16]);
				FlightGroups[i].ArrDepAO[2] = Convert.ToBoolean(buffer[0x17]);
				FlightGroups[i].Unknowns.Unknown3 = buffer[0x18];
				FlightGroups[i].ArrivalDelayMinutes = buffer[0x19];
				FlightGroups[i].ArrivalDelaySeconds = buffer[0x1A];
				FlightGroups[i].ArrDepAO[3] = Convert.ToBoolean(buffer[0x25]);
				FlightGroups[i].DepartureTimerMinutes = buffer[0x26];
				FlightGroups[i].DepartureTimerSeconds = buffer[0x27];
				FlightGroups[i].AbortTrigger = buffer[0x28];
				FlightGroups[i].Unknowns.Unknown4 = buffer[0x29];
				FlightGroups[i].Unknowns.Unknown5 = buffer[0x2B];
				FlightGroups[i].ArrivalCraft1 = buffer[0x2D];
				FlightGroups[i].ArrivalMethod1 = Convert.ToBoolean(buffer[0x2E]);	// false = hyper, true = mothership
				FlightGroups[i].ArrivalCraft2 = buffer[0x2F];
				FlightGroups[i].ArrivalMethod2 = Convert.ToBoolean(buffer[0x30]);
				FlightGroups[i].DepartureCraft1 = buffer[0x31];
				FlightGroups[i].DepartureMethod1 = Convert.ToBoolean(buffer[0x32]);
				FlightGroups[i].DepartureCraft2 = buffer[0x33];
				FlightGroups[i].DepartureMethod2 = Convert.ToBoolean(buffer[0x34]);
				#endregion
				#region Orders
				for (j=0;j<4;j++)
				{
					stream.Read(buffer, 0, 0x13);
					for (int h=0;h<0x13;h++) FlightGroups[i].Orders[j][h] = buffer[h];
					FlightGroups[i].Orders[j].Designation = new string(br.ReadChars(16));	// null-termed
					stream.Position += 0x2F;
				}
				stream.Read(buffer, 0, 0xB);
				for (j=0;j<4;j++)
				{
					FlightGroups[i].SkipToOrder4Trigger[0][j] = buffer[j];
					FlightGroups[i].SkipToOrder4Trigger[1][j] = buffer[4+j];
				}
				FlightGroups[i].SkipToO4T1AndOrT2 = Convert.ToBoolean(buffer[0xA]);
				#endregion
				#region Goals
				for (j=0;j<8;j++)
				{
					FlightGroups[i].Goals[j] = new FlightGroup.Goal(br.ReadBytes(0xF));
					stream.Position += 0x3F;
				}
				stream.Position++;
				#endregion
				for (j = 0; j < 4; j++) for (int k = 0; k < 22; k++) FlightGroups[i].Waypoints[k][j] = (short)(br.ReadInt16() * (j == 1 ? -1 : 1));
				#region Options/Other
				FlightGroups[i].Unknowns.Unknown17 = br.ReadBoolean();
				stream.Position++;
				FlightGroups[i].Unknowns.Unknown18 = br.ReadBoolean();
				stream.Position += 7;
				stream.Read(buffer, 0, 0xF);
				FlightGroups[i].Unknowns.Unknown19 = Convert.ToBoolean(buffer[0]);
				FlightGroups[i].Unknowns.Unknown20 = buffer[1];
				FlightGroups[i].Unknowns.Unknown21 = buffer[2];
				FlightGroups[i].Countermeasures = buffer[3];
				FlightGroups[i].ExplosionTime = buffer[4];
				FlightGroups[i].Status2 = buffer[5];
				FlightGroups[i].GlobalUnit = buffer[6];
				FlightGroups[i].Unknowns.Unknown22 = Convert.ToBoolean(buffer[7]);
				FlightGroups[i].Unknowns.Unknown23 = Convert.ToBoolean(buffer[8]);
				FlightGroups[i].Unknowns.Unknown24 = Convert.ToBoolean(buffer[9]);
				FlightGroups[i].Unknowns.Unknown25 = Convert.ToBoolean(buffer[0xA]);
				FlightGroups[i].Unknowns.Unknown26 = Convert.ToBoolean(buffer[0xB]);
				FlightGroups[i].Unknowns.Unknown27 = Convert.ToBoolean(buffer[0xC]);
				FlightGroups[i].Unknowns.Unknown28 = Convert.ToBoolean(buffer[0xD]);
				FlightGroups[i].Unknowns.Unknown29 = Convert.ToBoolean(buffer[0xE]);
				stream.Position++;
				for (j=0;j<8;j++)
				{
					byte x = br.ReadByte();
					if (x != 0 && x < 8) FlightGroups[i].OptLoadout[x] = true;
				}
				for (j=8;j<12;j++)
				{
					byte x = br.ReadByte();
					if (x != 0 && x < 4) FlightGroups[i].OptLoadout[8 + x] = true;	//[JB] forgot the offset
				}
				stream.Position += 2;
				for (j=12;j<15;j++)
				{
					byte x = br.ReadByte();
					if (x != 0 && x < 3) FlightGroups[i].OptLoadout[12 + x] = true; //[JB] forgot the offset
				}
				stream.Position++;
				FlightGroups[i].OptCraftCategory = (FlightGroup.OptionalCraftCategory)br.ReadByte();
				stream.Read(buffer, 0, 0x1E);
				for (int k=0;k<10;k++)
				{
					FlightGroups[i].OptCraft[k].CraftType = buffer[k];
					FlightGroups[i].OptCraft[k].NumberOfCraft = buffer[k+10];
					FlightGroups[i].OptCraft[k].NumberOfWaves = buffer[k+20];
				}
				stream.Position++;
				#endregion
			}
			#endregion
			#region Messages
			if (numMessages != 0)
			{
				Messages = new MessageCollection(numMessages);
				for (i=0;i<numMessages;i++)
				{
					stream.Position += 2;
					Messages[i].MessageString = new string(br.ReadChars(64)).Trim('\0');		// null-termed
					Messages[i].Color = 0;
					if (Messages[i].MessageString.Length > 0)  //[JB]
					{
						char c = Messages[i].MessageString[0];
						if (c >= '1' && c <= '3')
						{
							Messages[i].Color = (byte)(c - '0');
							Messages[i].MessageString = Messages[i].MessageString.Substring(1);
						}
					}
					stream.Read(buffer, 0, 0x20);
					for (j=0;j<10;j++) Messages[i].SentToTeam[j] = Convert.ToBoolean(buffer[j]);
					for (j=0;j<4;j++)
					{
						Messages[i].Triggers[0][j] = buffer[0xA+j];
						Messages[i].Triggers[1][j] = buffer[0xE+j];
						Messages[i].Triggers[2][j] = buffer[0x15+j];
						Messages[i].Triggers[3][j] = buffer[0x19+j];
					}
					Messages[i].T1AndOrT2 = Convert.ToBoolean(buffer[0x14]);
					Messages[i].T3AndOrT4 = Convert.ToBoolean(buffer[0x1F]);
					Messages[i].Note = new string(br.ReadChars(16)).Trim('\0');	// null-termed
					Messages[i].Delay = br.ReadByte();
					Messages[i].T12AndOrT34 = Convert.ToBoolean(br.ReadByte());
				}
			}
			else Messages.Clear();
			#endregion
			#region Globals
			Globals.ClearAll();
			for (i=0;i<10;i++)
			{
				stream.Position += 2;
				for (int k=0;k<3;k++)
				{
					stream.Read(buffer, 0, 8);
					for (j=0;j<4;j++)
					{
						Globals[i].Goals[k].Triggers[0][j] = buffer[j];
						Globals[i].Goals[k].Triggers[1][j] = buffer[j+4];
					}
					stream.Position += 2;
					Globals[i].Goals[k].T1AndOrT2 = br.ReadBoolean();
					stream.Read(buffer, 0, 8);
					for (j=0;j<4;j++)
					{
						Globals[i].Goals[k].Triggers[2][j] = buffer[j];
						Globals[i].Goals[k].Triggers[3][j] = buffer[j+4];
					}
					stream.Position += 2;
					Globals[i].Goals[k].T3AndOrT4 = br.ReadBoolean();
					stream.Position += 0x11;
					Globals[i].Goals[k].T12AndOrT34 = br.ReadBoolean();
					stream.Position++;
					Globals[i].Goals[k].RawPoints = br.ReadSByte();
				}
			}
			#endregion
			#region Teams
			Teams.ClearAll();
			for (i=0;i<10;i++)
			{
				stream.Position += 2;
				Teams[i].Name = new string(br.ReadChars(0x10));	// null-termed
				stream.Position += 8;
				for (j=0;j<10;j++) Teams[i].AlliedWithTeam[j] = br.ReadBoolean();
				for (j=0;j<6;j++)
				{
					Teams[i].EndOfMissionMessages[j] = new string(br.ReadChars(0x40));
					if (Teams[i].EndOfMissionMessages[j] != "")
					{
						char c = Teams[i].EndOfMissionMessages[j][0];
						if (c == '1' || c == '2' || c == '3')
						{
							Teams[i].EndOfMissionMessageColor[j] = Byte.Parse(c.ToString());
							Teams[i].EndOfMissionMessages[j] = Teams[i].EndOfMissionMessages[j].Substring(1);
						}
					}
				}
				stream.Position += 0x43;
			}
			#endregion
			#region Briefing
			Briefings.ClearAll();
			for (i=0;i<8;i++)
			{
				Briefings[i].Length = br.ReadInt16();
				Briefings[i].Unknown1 = br.ReadInt16();
				stream.Position += 4;	// StartLength, EventsLength
				Briefings[i].Unknown3 = br.ReadInt16();
				for (j=0;j<12;j++)
				{
					stream.Read(buffer, 0, 0x40);
					Buffer.BlockCopy(buffer, 0, Briefings[i].Events, 0x40 * j, 0x40);
				}
				stream.Read(buffer, 0, 0x20);
				Buffer.BlockCopy(buffer, 0, Briefings[i].Events, 0x300, 0x20);
				stream.Read(buffer, 0, 0xA);
				Buffer.BlockCopy(buffer, 0, Briefings[i].Team, 0, 0xA);
				for (j=0;j<32;j++)
				{
					int k = br.ReadInt16();
					Briefings[i].BriefingTag[j] = "";
					if (k > 0) Briefings[i].BriefingTag[j] = new string(br.ReadChars(k)).Trim('\0');	// shouldn't need the trim
				}
				for (j=0;j<32;j++)
				{
					int k = br.ReadInt16();
					Briefings[i].BriefingString[j] = "";
					if (k > 0) Briefings[i].BriefingString[j] = new string(br.ReadChars(k)).Trim('\0');
				}
			}
			#endregion
			#region FG goal strings
			for (i=0;i<FlightGroups.Count;i++)
				for (j=0;j<8;j++)
				{
					FlightGroups[i].Goals[j].IncompleteText = new string(br.ReadChars(0x40));
					FlightGroups[i].Goals[j].CompleteText = new string(br.ReadChars(0x40));
					FlightGroups[i].Goals[j].FailedText = new string(br.ReadChars(0x40));
				}
			#endregion
			#region Globals strings
			for (i = 0; i < 10; i++)	// Team
			{
				for (j = 0; j < 12; j++)	// Goal * Trigger
				{
					for (int k = 0; k < 3; k++)	// State
					{
							if (j >= 8 && k == 0) { stream.Position += 0x40; continue; }	// skip Sec Inc
							if (j >= 4 && k == 2) { stream.Position += 0x40; continue; }	// skip Prev & Sec Fail
							Globals[i].Goals[j / 4].Triggers[j % 4].GoalStrings[k] = new string(br.ReadChars(0x40));
					}
				}
				stream.Position += 0xC00;
			}
			#endregion
			#region Debriefs
			if (IsBop)
			{
				_missionSuccessful = new string(br.ReadChars(0x1000)).Trim('\0');
				_missionFailed = new string(br.ReadChars(0x1000)).Trim('\0');
				_missionDescription = new string(br.ReadChars(0x1000)).Trim('\0');
			}
			else _missionDescription = new string(br.ReadChars(0x400)).Trim('\0');
			#endregion
			MissionPath = stream.Name;
		}

		/// <summary>Save the mission with the default path</summary>
		/// <exception cref="System.UnauthorizedAccessException">Write permissions for <see cref="MissionFile.MissionPath"/> are denied</exception>
		public void Save()
		{
			FileStream fs = null;
			try
			{
				if (File.Exists(MissionPath)) File.Delete(MissionPath);
				fs = File.OpenWrite(MissionPath);
				BinaryWriter bw = new BinaryWriter(fs, System.Text.Encoding.Default);	//[JB]
				int i;
				long p;
				#region Platform
				if (IsBop) bw.Write((short)14);
				else bw.Write((short)12);
				bw.Write((short)FlightGroups.Count);
				bw.Write((short)Messages.Count);
				bw.Write((short)Unknown1);
				bw.Write((short)Unknown2);
				fs.Position++;
				bw.Write(Unknown3);
				fs.Position = 0x28;
				bw.Write(Unknown4.ToCharArray());
				fs.WriteByte(0);	// just to ensure termination
				fs.Position = 0x50;
				bw.Write(Unknown5.ToCharArray());
				fs.WriteByte(0);	// just to ensure termination
				fs.Position = 0x64;
				bw.Write((byte)MissionType);
				bw.Write(Unknown6);
				bw.Write(TimeLimitMin);
				bw.Write(TimeLimitSec);
				fs.Position = 0xA4;
				#endregion
				#region FlightGroups
				for (i = 0;i<FlightGroups.Count;i++)
				{
					p = fs.Position;
					int j;
					#region Craft
					bw.Write(FlightGroups[i].Name.ToCharArray());
					fs.WriteByte(0);
					fs.Position = p + 0x14;
					for (j=0;j<4;j++)
					{
						string s = FlightGroups[i].Roles[j];
						if (FlightGroups[i].Roles[j] != "") bw.Write((s.Length > 4 ? s.Substring(0, 4).ToCharArray() : s.ToCharArray()));
						else bw.Write((Int32)0);
					}
					fs.Position = p + 0x28;
					bw.Write(FlightGroups[i].Cargo.ToCharArray());
					fs.WriteByte(0);
					fs.Position = p + 0x3C;
					bw.Write(FlightGroups[i].SpecialCargo.ToCharArray());
					fs.WriteByte(0);
					fs.Position = p + 0x50;
					if (FlightGroups[i].SpecialCargoCraft == 0) fs.WriteByte(FlightGroups[i].NumberOfCraft);
					else fs.WriteByte((byte)(FlightGroups[i].SpecialCargoCraft - 1));
					bw.Write(FlightGroups[i].RandSpecCargo);
					fs.WriteByte(FlightGroups[i].CraftType);
					fs.WriteByte(FlightGroups[i].NumberOfCraft);
					fs.WriteByte(FlightGroups[i].Status1);
					fs.WriteByte(FlightGroups[i].Missile);
					fs.WriteByte(FlightGroups[i].Beam);
					fs.WriteByte(FlightGroups[i].IFF);
					fs.WriteByte(FlightGroups[i].Team);
					fs.WriteByte(FlightGroups[i].AI);
					fs.WriteByte(FlightGroups[i].Markings);
					fs.WriteByte(FlightGroups[i].Radio);
					fs.Position++;
					fs.WriteByte(FlightGroups[i].Formation);
					fs.WriteByte(FlightGroups[i].FormDistance);
					fs.WriteByte(FlightGroups[i].GlobalGroup);
					fs.WriteByte(FlightGroups[i].FormLeaderDist);
					fs.WriteByte((byte)(FlightGroups[i].NumberOfWaves-1));
					fs.WriteByte(FlightGroups[i].Unknowns.Unknown1);
					bw.Write(FlightGroups[i].Unknowns.Unknown2);
					fs.WriteByte(FlightGroups[i].PlayerNumber);
					bw.Write(FlightGroups[i].ArriveOnlyIfHuman);
					fs.WriteByte(FlightGroups[i].PlayerCraft);
					fs.WriteByte((byte)(FlightGroups[i].Yaw * 0x100 / 360));
					fs.WriteByte((byte)((FlightGroups[i].Pitch >= 64 ? FlightGroups[i].Pitch - 270 : FlightGroups[i].Pitch + 90) * 0x100 / 360));
					fs.WriteByte((byte)(FlightGroups[i].Roll * 0x100 / 360));
					fs.Position = p + 0x6D;
					#endregion
					#region Arr/Dep
					fs.WriteByte(FlightGroups[i].Difficulty);
					for (j = 0; j < 4; j++) fs.WriteByte(FlightGroups[i].ArrDepTriggers[0][j]);
					for (j = 0; j < 4; j++) fs.WriteByte(FlightGroups[i].ArrDepTriggers[1][j]);
					fs.Position += 2;
					bw.Write(FlightGroups[i].ArrDepAO[0]);
					for (j = 0; j < 4; j++) fs.WriteByte(FlightGroups[i].ArrDepTriggers[2][j]);
					for (j = 0; j < 4; j++) fs.WriteByte(FlightGroups[i].ArrDepTriggers[3][j]);
					fs.Position += 2;
					bw.Write(FlightGroups[i].ArrDepAO[1]);
					bw.Write(FlightGroups[i].ArrDepAO[2]);
					fs.WriteByte(FlightGroups[i].Unknowns.Unknown3);
					fs.WriteByte(FlightGroups[i].ArrivalDelayMinutes);
					fs.WriteByte(FlightGroups[i].ArrivalDelaySeconds);
					for (j = 0; j < 4; j++) fs.WriteByte(FlightGroups[i].ArrDepTriggers[4][j]);
					for (j = 0; j < 4; j++) fs.WriteByte(FlightGroups[i].ArrDepTriggers[5][j]);
					fs.Position += 2;
					bw.Write(FlightGroups[i].ArrDepAO[3]);
					fs.WriteByte(FlightGroups[i].DepartureTimerMinutes);
					fs.WriteByte(FlightGroups[i].DepartureTimerSeconds);
					fs.WriteByte(FlightGroups[i].AbortTrigger);
					fs.WriteByte(FlightGroups[i].Unknowns.Unknown4);
					fs.Position++;
					fs.WriteByte(FlightGroups[i].Unknowns.Unknown5);
					fs.Position++;
					fs.WriteByte(FlightGroups[i].ArrivalCraft1);
					bw.Write(FlightGroups[i].ArrivalMethod1);
					fs.WriteByte(FlightGroups[i].ArrivalCraft2);
					bw.Write(FlightGroups[i].ArrivalMethod2);
					fs.WriteByte(FlightGroups[i].DepartureCraft1);
					bw.Write(FlightGroups[i].DepartureMethod1);
					fs.WriteByte(FlightGroups[i].DepartureCraft2);
					bw.Write(FlightGroups[i].DepartureMethod2);
					#endregion
					#region Orders
					for (j=0;j<4;j++)
					{
						for (int k=0;k<0x13;k++) fs.WriteByte(FlightGroups[i].Orders[j][k]);
						bw.Write(FlightGroups[i].Orders[j].Designation.ToCharArray());
						fs.WriteByte(0);
						fs.Position = p + 0xF4 + (j*0x52);
					}
					for (j = 0; j < 4; j++) fs.WriteByte(FlightGroups[i].SkipToOrder4Trigger[0][j]);
					for (j = 0; j < 4; j++) fs.WriteByte(FlightGroups[i].SkipToOrder4Trigger[1][j]);
					fs.Position += 2;
					bw.Write(FlightGroups[i].SkipToO4T1AndOrT2);
					#endregion
					#region Goals
					for (j=0;j<8;j++)
					{
						fs.WriteByte(FlightGroups[i].Goals[j].Argument);
						fs.WriteByte(FlightGroups[i].Goals[j].Condition);
						fs.WriteByte(FlightGroups[i].Goals[j].Amount);
						bw.Write(FlightGroups[i].Goals[j].RawPoints);
						bw.Write(FlightGroups[i].Goals[j].Enabled);
						fs.WriteByte(FlightGroups[i].Goals[j].Team);
						bw.Write(FlightGroups[i].Goals[j].Unknown10);
						bw.Write(FlightGroups[i].Goals[j].Unknown11);
						bw.Write(FlightGroups[i].Goals[j].Unknown12);
						fs.Position += 2;	//[JB]
						fs.WriteByte(FlightGroups[i].Goals[j].Unknown13);
						bw.Write(FlightGroups[i].Goals[j].Unknown14);
						fs.Position++;
						fs.WriteByte(FlightGroups[i].Goals[j].Unknown16);
						fs.Position = p + 0x243 + (j*0x4E);
					}
					fs.Position++;
					#endregion
					for (j = 0; j < 4; j++) for (int k = 0; k < 22; k++) bw.Write((short)(FlightGroups[i].Waypoints[k][j] * (j == 1 ? -1 : 1)));
					#region Options/Other
					bw.Write(FlightGroups[i].Unknowns.Unknown17);
					fs.Position++;
					bw.Write(FlightGroups[i].Unknowns.Unknown18);
					fs.Position += 7;
					bw.Write(FlightGroups[i].Unknowns.Unknown19);
					fs.WriteByte(FlightGroups[i].Unknowns.Unknown20);
					fs.WriteByte(FlightGroups[i].Unknowns.Unknown21);
					fs.WriteByte(FlightGroups[i].Countermeasures);
					fs.WriteByte(FlightGroups[i].ExplosionTime);
					fs.WriteByte(FlightGroups[i].Status2);
					fs.WriteByte(FlightGroups[i].GlobalUnit);
					bw.Write(FlightGroups[i].Unknowns.Unknown22);
					bw.Write(FlightGroups[i].Unknowns.Unknown23);
					bw.Write(FlightGroups[i].Unknowns.Unknown24);
					bw.Write(FlightGroups[i].Unknowns.Unknown25);
					bw.Write(FlightGroups[i].Unknowns.Unknown26);
					bw.Write(FlightGroups[i].Unknowns.Unknown27);
					bw.Write(FlightGroups[i].Unknowns.Unknown28);
					bw.Write(FlightGroups[i].Unknowns.Unknown29);
					fs.Position++;
					//[JB] The old code iterated through the array and wrote the values, but it didn't work properly.  
					//The list of options must not contain any gaps of 00.  
					byte[] optBuff = new byte[15]; //One array to store everything so we don't need to zero anything between use  
					int oi = 0;
					for (j = 1; j < 8; j++) if (FlightGroups[i].OptLoadout[j]) optBuff[oi++] = (byte)j;
					bw.Write(optBuff, 0, 8);  //Warheads  
					oi = 8;
					for (j = 1; j < 4; j++) if (FlightGroups[i].OptLoadout[j + 8]) optBuff[oi++] = (byte)j;
					bw.Write(optBuff, 8, 4);  //Beams  
					fs.Position += 2; //Empty space  
					oi = 12;
					for (j = 1; j < 3; j++) if (FlightGroups[i].OptLoadout[j + 12]) optBuff[oi++] = (byte)j;
					bw.Write(optBuff, 12, 3); //Countermeasures  
					fs.Position++;    //Empty space 
					fs.WriteByte((byte)FlightGroups[i].OptCraftCategory);
					for (int k=0;k<10;k++) fs.WriteByte(FlightGroups[i].OptCraft[k].CraftType);
					for (int k=0;k<10;k++) fs.WriteByte(FlightGroups[i].OptCraft[k].NumberOfCraft);
					for (int k=0;k<10;k++) fs.WriteByte(FlightGroups[i].OptCraft[k].NumberOfWaves);
					fs.Position++;
					#endregion
				}
				#endregion
				#region Messages
				for (i=0;i<Messages.Count;i++)
				{
					p = fs.Position;
					bw.Write((short)i);
					if (Messages[i].Color != 0) bw.Write((byte)(Messages[i].Color + 0x30));  //[JB] forgot color
					bw.Write(Messages[i].MessageString.ToCharArray());
					fs.WriteByte(0);
					fs.Position = p + 0x42;
					for (int j=0;j<Messages[i].SentToTeam.Length;j++) bw.Write(Messages[i].SentToTeam[j]);
					for (int j = 0; j < 4; j++) fs.WriteByte(Messages[i].Triggers[0][j]);
					for (int j = 0; j < 4; j++) fs.WriteByte(Messages[i].Triggers[1][j]);
					fs.Position += 2;
					bw.Write(Messages[i].T1AndOrT2);
					for (int j = 0; j < 4; j++) fs.WriteByte(Messages[i].Triggers[2][j]);
					for (int j = 0; j < 4; j++) fs.WriteByte(Messages[i].Triggers[3][j]);
					fs.Position += 2;
					bw.Write(Messages[i].T3AndOrT4);
					bw.Write(Messages[i].Note.ToCharArray());
					fs.WriteByte(0);
					fs.Position = p + 0x72;
					fs.WriteByte(Messages[i].Delay);
					bw.Write(Messages[i].T12AndOrT34);
				}
				#endregion
				#region Globals
				for (i=0;i<10;i++)
				{
					bw.Write((short)3);
					for (int j=0;j<3;j++)
					{
						for (int k = 0; k < 4; k++) fs.WriteByte(Globals[i].Goals[j].Triggers[0][k]);
						for (int k = 0; k < 4; k++) fs.WriteByte(Globals[i].Goals[j].Triggers[1][k]);
						fs.Position += 2;
						bw.Write(Globals[i].Goals[j].T1AndOrT2);
						for (int k = 0; k < 4; k++) fs.WriteByte(Globals[i].Goals[j].Triggers[2][k]);
						for (int k = 0; k < 4; k++) fs.WriteByte(Globals[i].Goals[j].Triggers[3][k]);
						fs.Position += 2;
						bw.Write(Globals[i].Goals[j].T3AndOrT4);
						fs.Position += 0x11;
						bw.Write(Globals[i].Goals[j].T12AndOrT34);
						fs.Position++;
						fs.WriteByte((byte)Globals[i].Goals[j].RawPoints);
					}
				}
				#endregion
				#region Teams
				for (i=0;i<10;i++)
				{
					p = fs.Position;
					bw.Write((short)1);
					bw.Write(Teams[i].Name.ToCharArray());
					fs.WriteByte(0);
					fs.Position = p + 0x1A;
					for (int j=0;j<10;j++) bw.Write(Teams[i].AlliedWithTeam[j]);
					for (int j=0;j<6;j++)
					{
						if (Teams[i].EndOfMissionMessageColor[j] != 0) bw.Write(Convert.ToByte(Teams[i].EndOfMissionMessageColor[j] + 48));
						bw.Write(Teams[i].EndOfMissionMessages[j].ToCharArray());
						fs.WriteByte(0);
						fs.Position = p + 0x24 + (j + 1) * 0x40;	//[JB] was missing the +1
					}
					fs.Position = p + 0x1E7;
				}
				#endregion
				#region Briefing
				for (i=0;i<8;i++)
				{
					bw.Write(Briefings[i].Length);
					bw.Write(Briefings[i].Unknown1);
					bw.Write(Briefings[i].StartLength);
					bw.Write(Briefings[i].EventsLength);
					bw.Write(Briefings[i].Unknown3);
					byte[] briefBuffer = new byte[Briefings[i].Events.Length * 2];
					Buffer.BlockCopy(Briefings[i].Events, 0, briefBuffer, 0, briefBuffer.Length);
					bw.Write(briefBuffer);
					for(int j=0;j<10;j++) bw.Write(Briefings[i].Team[j]);	//[JB]
					for (int j=0;j<32;j++)
					{
						bw.Write((short)Briefings[i].BriefingTag[j].Length);
						if (Briefings[i].BriefingTag[j].Length != 0) bw.Write(Briefings[i].BriefingTag[j].ToCharArray());
					}
					for (int j=0;j<32;j++)
					{
						bw.Write((short)Briefings[i].BriefingString[j].Length);
						if (Briefings[i].BriefingString[j].Length != 0) bw.Write(Briefings[i].BriefingString[j].ToCharArray());
					}
				}
				#endregion
				#region FG Goal Strings
				for (i=0;i<FlightGroups.Count;i++)
				{
					for (int j=0;j<8;j++)
					{
						for (int k=0;k<3;k++)
						{
							p = fs.Position;
							if (k == 0) bw.Write(FlightGroups[i].Goals[j].IncompleteText.ToCharArray());
							else if (k == 1) bw.Write(FlightGroups[i].Goals[j].CompleteText.ToCharArray());
							else bw.Write(FlightGroups[i].Goals[j].FailedText.ToCharArray());
							fs.WriteByte(0);
							fs.Position = p + 0x40;
						}
					}
				}
				#endregion
				#region Global Goal Strings
				for (i = 0; i < 10; i++)
				{
					for (int j = 0; j < 12; j++)
					{
						for (int k = 0; k < 3; k++)
						{
							if (j >= 8 && k==0) { fs.Position += 0x40; continue; }	// skip Sec Inc
							if (j >= 4 && k==2) { fs.Position += 0x40; continue; }	// skip Prev & Sec Fail
							p = fs.Position;
							bw.Write(Globals[i].Goals[j / 4].Triggers[j % 4].GoalStrings[k].ToCharArray());
							fs.WriteByte(0);
							fs.Position = p + 0x40;
						}
					}
					fs.Position += 0xC00;
				}
				#endregion
				#region Debriefs
				p = fs.Position;
				if (IsBop)
				{
					bw.Write(_missionSuccessful.ToCharArray());
					fs.WriteByte(0);
					fs.Position = p + 0x1000;
					bw.Write(_missionFailed.ToCharArray());
					fs.WriteByte(0);
					fs.Position = p + 0x2000;
					bw.Write(_missionDescription.ToCharArray());
					fs.WriteByte(0);
					fs.Position = p + 0x3000;
				}
				else
				{
					bw.Write(_missionDescription.ToCharArray());
					fs.WriteByte(0);
					fs.Position = p + 0x400;
				}
				bw.Write((short)0x2106);	//TODO: might need to remove this
				fs.SetLength(fs.Position);
				#endregion
				fs.Close();
			}
			catch
			{
				fs.Close();
				throw;
			}
		}

		/// <summary>Save the mission to a new location</summary>
		/// <param name="filePath">Full path to the new file location</param>
		/// <exception cref="System.UnauthorizedAccessException">Write permissions for <i>filePath</i> are denied</exception>
		public void Save(string filePath)
		{
			MissionPath = filePath;
			Save();
		}
		
		/// <summary>Checks a CraftType for valid values and adjusts if necessary</summary>
		/// <param name="craftType">The craft index to check</param>
		/// <returns>Validated craft type, otherwise <b>255</b></returns>
		public static byte CraftCheck(byte craftType)
		{
			if (craftType > 91) return 255;
			else return craftType;
		}
		
		/// <summary>Checks <see cref="Trigger"/>.Type/Variable or <see cref="FlightGroup.Order"/>.TargetType/Target pairs for values compatible with TIE</summary>
		/// <remarks>First checks for invalid Types, then runs through allows values for each Type. Does not verify FlightGroup, CraftWhen, GlobalGroup or GlobalUnit</remarks>
		/// <param name="type">Trigger.Type or Order.TargetType</param>
		/// <param name="variable">Trigger.Variable or Order.Target, may be updated</param>
		/// <param name="errorMessage">Error description if found, otherwise ""</param>
		public static void CheckTarget(byte type, ref byte variable, out string errorMessage)
		{
			errorMessage = "";
			if (type > 9)
			{
				errorMessage = "Type (" + type + ")";
				return;
			}
			// can't check FG
			else if (type == 2)
			{
				byte newCraft = CraftCheck(variable);
				if (newCraft == 255) errorMessage = "CraftType";
				else variable = newCraft;
			}
			else if (type == 3) if (variable > 6) errorMessage = "CraftCategory";
			else if (type == 4) if (variable > 2) errorMessage = "ObjectCategory";
			else if (type == 5) if (variable > 5) errorMessage = "IFF";
			else if (type == 6) if (variable > 39) errorMessage = "Order";
			// don't want to check CraftWhen
			// can't check GG
			else if (type == 12 || type == 21) if (variable > 9) errorMessage = "Team";
			// can't check GU
			if (errorMessage != "") errorMessage += " (" + variable + ")";
		}
		#endregion public methods

		#region public properties
		/// <summary>Maximum number of craft that can exist at one time in-game</summary>
		/// <remarks>Value is <b>32</b></remarks>
		public const int CraftLimit = 32;
		/// <summary>Maximum number of FlightGroups that can exist in the mission file</summary>
		/// <remarks>Value is <b>48</b></remarks>
		public const int FlightGroupLimit = 48;
		/// <summary>Maximum number of In-Flight Messages that can exist in the mission file</summary>
		/// <remarks>Value is <b>64</b></remarks>
		public const int MessageLimit = 64;
		
		/// <summary>Gets or sets the mission platform</summary>
		/// <remarks><b>true</b> for Balance of Power</remarks>
		public bool IsBop { get; set; }
		/// <summary>Unknown FileHeader value</summary>
		/// <remarks>Offset = 0x06</remarks>
		public byte Unknown1 { get; set; }
		/// <summary>Unknown FileHeader value</summary>
		/// <remarks>Offset = 0x08</remarks>
		public byte Unknown2 { get; set; }
		/// <summary>Unknown FileHeader value</summary>
		/// <remarks>Offset = 0x0B</remarks>
		public bool Unknown3 { get; set; }
		/// <summary>Unknown FileHeader value (BoP only?)</summary>
		/// <remarks>Offset = 0x28, 16 char</remarks>
		public string Unknown4
		{
			get { return _unknown4; }
			set { _unknown4 = StringFunctions.GetTrimmed(value, 16); }
		}
		/// <summary>Unknown FileHeader value (BoP only?)</summary>
		/// <remarks>Offset = 0x50, 16 char</remarks>
		public string Unknown5
		{
			get { return _unknown5; }
			set { _unknown5 = StringFunctions.GetTrimmed(value, 16); }
		}
		/// <summary>Gets or sets the category the mission belongs to</summary>
		public MissionTypeEnum MissionType { get; set; }
		/// <summary>Gets or sets an unknown FileHeader value</summary>
		/// <remarks>Offset = 0x65</remarks>
		public bool Unknown6 { get; set; }
		/// <summary>Gets or sets the minutes value of the time limit</summary>
		/// <remarks>Can be used in conjunction with <see cref="TimeLimitSec"/></remarks>
		public byte TimeLimitMin { get; set; }
		/// <summary>Gets or sets the seconds value of the time limit</summary>
		/// <remarks>Can be used in conjunction with <see cref="TimeLimitMin"/></remarks>
		public byte TimeLimitSec { get; set; }
		/// <summary>Gets or sets the FlightGroups for the mission</summary>
		/// <remarks>Defaults to one FlightGroup</remarks>
		public FlightGroupCollection FlightGroups { get; set; }
		/// <summary>Gets or sets the In-Flight Messages for the mission</summary>
		/// <remarks>Defaults to zero messages</remarks>
		public MessageCollection Messages { get; set; }
		/// <summary>Gets or sets the Global Goals for the mission</summary>
		public GlobalsCollection Globals { get; set; }
		/// <summary>Gets or sets the Teams for the mission</summary>
		public TeamCollection Teams { get; set; }
		/// <summary>Gets or sets the Briefings for the mission</summary>
		public BriefingCollection Briefings { get; set; }
		/// <summary>Gets or sets the summary of the mission</summary>
		/// <remarks>1023 char limit for XvT, 4095 char limit for BoP</remarks>
		public string MissionDescription
		{
			get { return _missionDescription.Replace("$","\r\n"); }
			set
			{
				string s = value.Replace("\r\n", "$");
				_missionDescription = StringFunctions.GetTrimmed(s, (IsBop ? 0x0FFF : 0x3FF));
			}
		}
		/// <summary>Gets or sets the BoP Debriefing text</summary>
		/// <remarks>4095 char limit</remarks>
		public string MissionFailed
		{
			get { return _missionFailed.Replace("$", "\r\n"); }
			set
			{
				string s = value.Replace("\r\n", "$");
				_missionFailed = StringFunctions.GetTrimmed(s, 0x0FFF);
			}
		}
		/// <summary>Gets or sets the BoP Debriefing text</summary>
		/// <remarks>4095 char limit</remarks>
		public string MissionSuccessful
		{
			get { return _missionSuccessful.Replace("$", "\r\n"); }
			set
			{
				string s = value.Replace("\r\n", "$");
				_missionSuccessful = StringFunctions.GetTrimmed(s, 0x0FFF);
			}
		}
		#endregion public properties
	}
}
