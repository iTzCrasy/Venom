using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Venom.Core;

namespace Venom.Core
{
	public struct VillageImages
	{
		public EVillageSize Size;
		public EVillageType Type;
		public BitmapImage ImageFile;
	}

	public enum EVillageType : int
	{
		DEFAULT = 0,
		DEFAULT_LEFT,
		BONUS_DEFAULT,
		BONUS_LEFFT
	}

	public enum EVillageSize : int
	{
		S1 = 0,
		S2,
		S3,
		S4,
		S5,
		S6
	}

	public struct DecorationImages
	{
		public int ID;
		public BitmapImage ImageFile;
	}


	public class Map : Singleton<Map>
	{
		protected byte[] _Decoration = new byte[1000000];
		protected List<VillageImages> _VillageImages = new List<VillageImages>( );
		protected List<DecorationImages> _DecorationImages = new List<DecorationImages>( );

		/// <summary>
		/// Get Images
		/// </summary>
		public VillageImages GetVillageImage( EVillageSize Size ) => _VillageImages.FirstOrDefault( p => p.Size.Equals( Size ) );


		public Map( )
		{

		}

		public Task Load( )
		{
			var Tasks = new List<Task>
			{
				LoadDecoration(),
				// LoadDecorationImages(),
				// LoadVillageImages()
			};

			return Task.WhenAll( Tasks );
		}

		/// <summary>
		/// Loading Map Decoration
		/// </summary>
		public Task LoadDecoration( )
		{
			return ResourceManager.GetInstance.OpenEntryAsync( "world.dat", async ( entry ) =>
			{
				using( var stream = entry.Open( ) )
				{
					var buffer = new byte[4096];

					var offset = 0;
					var read = 0;
					while( ( read = await stream.ReadAsync( buffer, 0, 4096 ) ) > 0 )
					{
						Array.Copy( buffer, 0, _Decoration, offset, read );
						offset += read;
					}
				}
			} );
		}

		/// <summary>
		/// Loading Village Icons
		/// </summary>
		public Task LoadVillageImages( )
		{
			var Tasks = new List<Task>
			{
				LoadVillageImage( EVillageSize.S1, EVillageType.DEFAULT, "v1.png" ),
				LoadVillageImage( EVillageSize.S2, EVillageType.DEFAULT, "v2.png" ),
				LoadVillageImage( EVillageSize.S3, EVillageType.DEFAULT, "v3.png" ),
				LoadVillageImage( EVillageSize.S4, EVillageType.DEFAULT, "v4.png" ),
				LoadVillageImage( EVillageSize.S5, EVillageType.DEFAULT, "v5.png" ),
				LoadVillageImage( EVillageSize.S6, EVillageType.DEFAULT, "v6.png" )
			};

			return Task.WhenAll( Tasks );
		}

		private async Task LoadVillageImage( EVillageSize villageSize, EVillageType villageType, string Filename )
		{
			var PathData = Path.GetFullPath( "Resources" ) + "//Images//Villages//Default//v2_";
			_VillageImages.Add( new VillageImages
			{
				Size = villageSize,
				Type = villageType,
				ImageFile = await Task.Run( ( ) => new BitmapImage( new Uri( PathData + Filename ) ) )
			} );
		}

		/// <summary>
		/// Loading Decoration Images
		/// </summary>
		public Task LoadDecorationImages( )
		{
			var Tasks = new List<Task>
			{
                //=> Grass
                LoadDecorationImage( 0, "gras1.png" ),
				LoadDecorationImage( 1, "gras2.png" ),
				LoadDecorationImage( 2, "gras3.png" ),
				LoadDecorationImage( 3, "gras4.png" ),
                //=> Hills
                LoadDecorationImage( 8, "berg1.png" ),
				LoadDecorationImage( 9, "berg2.png" ),
				LoadDecorationImage( 10, "berg3.png" ),
				LoadDecorationImage( 11, "berg4.png" ),
                //=> Sea
                LoadDecorationImage( 12, "see.png" ),
                //=> Forest
                LoadDecorationImage( 16, "forest0000.png" ),
				LoadDecorationImage( 17, "forest0001.png" ),
				LoadDecorationImage( 18, "forest0010.png" ),
				LoadDecorationImage( 19, "forest0011.png" ),

				LoadDecorationImage( 20, "forest0100.png" ),
				LoadDecorationImage( 21, "forest0101.png" ),
				LoadDecorationImage( 22, "forest0110.png" ),
				LoadDecorationImage( 23, "forest0111.png" ),

				LoadDecorationImage( 24, "forest1000.png" ),
				LoadDecorationImage( 25, "forest1001.png" ),
				LoadDecorationImage( 26, "forest1010.png" ),
				LoadDecorationImage( 27, "forest1011.png" ),

				LoadDecorationImage( 28, "forest1100.png" ),
				LoadDecorationImage( 29, "forest1101.png" ),
				LoadDecorationImage( 30, "forest1110.png" ),
				LoadDecorationImage( 31, "forest1111.png" )
			};

			return Task.WhenAll( Tasks );
		}

		private async Task LoadDecorationImage( int decorationId, string Filename )
		{
			var PathData = Path.GetFullPath( "Resources" ) + "//Images//Map//Default//";
			_DecorationImages.Add( new DecorationImages
			{
				ID = decorationId,
				ImageFile = await Task.Run( ( ) => new BitmapImage( new Uri( PathData + Filename ) ) )
			} );
		}


	}
}
