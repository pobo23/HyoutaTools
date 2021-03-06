﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HyoutaTools.Tales.Vesperia.TSS;

namespace HyoutaTools.Tales.Vesperia.ItemDat {
	class RunItemViewer {
		public static int Execute( List<string> args ) {
			if ( args.Count < 7 ) {
				Console.WriteLine( "Usage: [360/PS3] ITEM.DAT STRING_DIC.SO T8BTSK T8BTEMST COOKDAT WRLDDAT" );
				return -1;
			}

			GameVersion version = GameVersion.None;
			switch ( args[0].ToUpperInvariant() ) {
				case "360":
					version = GameVersion.X360;
					break;
				case "PS3":
					version = GameVersion.PS3;
					break;
			}

			if ( version == GameVersion.None ) {
				Console.WriteLine( "First parameter must indicate game version!" );
				return -1;
			}

			ItemDat items = new ItemDat( args[1] );

			TSSFile TSS;
			try {
				TSS = new TSSFile( System.IO.File.ReadAllBytes( args[2] ) );
			} catch ( System.IO.FileNotFoundException ) {
				Console.WriteLine( "Could not open STRING_DIC.SO, exiting." );
				return -1;
			}

			T8BTSK.T8BTSK skills = new T8BTSK.T8BTSK( args[3] );
			T8BTEMST.T8BTEMST enemies = new T8BTEMST.T8BTEMST( args[4] );
			COOKDAT.COOKDAT cookdat = new COOKDAT.COOKDAT( args[5] );
			WRLDDAT.WRLDDAT locations = new WRLDDAT.WRLDDAT( args[6] );

			Console.WriteLine( "Initializing GUI..." );
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			ItemForm itemForm = new ItemForm( version, items, TSS, skills, enemies, cookdat, locations );
			Application.Run( itemForm );
			return 0;
		}
	}
}
