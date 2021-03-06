﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HyoutaTools.Tales.Vesperia.T8BTEMEG {
	public class T8BTEMEG {
		public T8BTEMEG( String filename ) {
			using ( Stream stream = new System.IO.FileStream( filename, FileMode.Open ) ) {
				if ( !LoadFile( stream ) ) {
					throw new Exception( "Loading T8BTEMEG failed!" );
				}
			}
		}

		public T8BTEMEG( Stream stream ) {
			if ( !LoadFile( stream ) ) {
				throw new Exception( "Loading T8BTEMEG failed!" );
			}
		}

		public List<EncounterGroup> EncounterGroupList;
		public Dictionary<uint, EncounterGroup> EncounterGroupIdDict;

		private bool LoadFile( Stream stream ) {
			string magic = stream.ReadAscii( 8 );
			uint encounterGroupCount = stream.ReadUInt32().SwapEndian();
			uint refStringStart = stream.ReadUInt32().SwapEndian();

			EncounterGroupList = new List<EncounterGroup>( (int)encounterGroupCount );
			for ( uint i = 0; i < encounterGroupCount; ++i ) {
				EncounterGroup s = new EncounterGroup( stream, refStringStart );
				EncounterGroupList.Add( s );
			}

			EncounterGroupIdDict = new Dictionary<uint, EncounterGroup>( EncounterGroupList.Count );
			foreach ( EncounterGroup e in EncounterGroupList ) {
				EncounterGroupIdDict.Add( e.InGameID, e );
			}

			return true;
		}
	}
}
