using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Venom.Utility
{
    public class Images
    {
        private readonly string _path = Path.Combine( Directory.GetCurrentDirectory( ), "Resource.zip" );

        private Dictionary<string, BitmapImage> _imagesMap;
        private Dictionary<string, BitmapImage> _imagesVillage;
        private Dictionary<string, BitmapImage> _imagesOther;
        private Dictionary<string, BitmapImage> _imagesUnits;

        public void Initialize()
        {
			OpenArchive( ( archive ) =>
			{
				_imagesMap = LoadImagesFromArchive( archive, "Images/Map/Default" );
				_imagesVillage = LoadImagesFromArchive( archive, "Images/Villages/Default" );
				_imagesOther = LoadImagesFromArchive( archive, "Images/Other" );
			} );
        }

		public void OpenArchive( Action<ZipArchive> action )
		{
			if( !File.Exists( _path ) )
			{
				throw new FileNotFoundException( $"Resource file not found at: {_path}" );
			}

			using( var zipFile = new FileStream( _path, FileMode.Open ) )
			{
				using( var archive = new ZipArchive( zipFile, ZipArchiveMode.Read ) )
				{
					action( archive );
				}
			}
		}

		public async Task OpenArchiveAsync( Func<ZipArchive, Task> action )
		{
			if( !File.Exists( _path ) )
			{
				throw new FileNotFoundException( $"Resource file not found at: {_path}" );
			}

			using( var zipFile = new FileStream( _path, FileMode.Open ) )
			{
				using( var archive = new ZipArchive( zipFile, ZipArchiveMode.Read ) )
				{
					await action( archive )
						.ConfigureAwait( false );
				}
			}
		}

		/// <summary>
		/// Helper to directly open an entry inside the zip archive async.
		/// </summary>
		/// <param name="entry">path to file inside the archive</param>
		/// <param name="action">action that is executed async</param>
		/// <returns></returns>
		public Task OpenEntryAsync( string entry, Func<ZipArchiveEntry, Task> action )
		{
			return OpenArchiveAsync( ( archive ) => action( archive.GetEntry( entry ) ) );
		}


		/// <summary>
		/// Loads all images under the specified path inside the zip archive
		/// </summary>
		/// <param name="archive"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Globalization", "CA1307:Specify StringComparison", Justification = "<Pending>" )]
		private Dictionary<string, BitmapImage> LoadImagesFromArchive( ZipArchive archive, string path )
		{
			var entries = archive.Entries
				.Where( ( _ ) => _.FullName.Contains( path ) && !_.FullName.Equals( path ) )
				.Select( ( _ ) => new { _.FullName, Name = _.FullName.Replace( path, string.Empty ) } );

			var items = new Dictionary<string, BitmapImage>( );

			foreach( var i in entries )
			{
				if( !i.FullName.EndsWith( ".png", StringComparison.OrdinalIgnoreCase ) )
				{
					Debug.Print( $"[LoadImagesFromArchive] skipping file {i.FullName}" );
					continue;
				}

				Debug.Print( $"[LoadImagesFromArchive] loaded file {i.FullName}" );

				var entry = archive.GetEntry( i.FullName );

				using( var stream = entry.Open( ) )
				{
					var bitmap = LoadBitmapFromStream( stream );
					items.Add( i.Name, bitmap );
				}
			}

			return items;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		private BitmapImage LoadBitmapFromStream( Stream stream )
		{
			var memoryStream = new MemoryStream( );
			stream.CopyTo( memoryStream );

			var bitmap = new BitmapImage( );
			bitmap.BeginInit( );
			bitmap.StreamSource = memoryStream;
			bitmap.CacheOption = BitmapCacheOption.OnLoad;
			bitmap.EndInit( );
			bitmap.Freeze( );

			return bitmap;
		}

		public BitmapImage GetMapImage( string imageName ) => _imagesMap.TryGetValue( imageName, out var image ) ? image : null;
		public BitmapImage GetVillageImage( string imageName ) => _imagesVillage.TryGetValue( imageName, out var image ) ? image : null;
		public BitmapImage GetImageOther( string imageName ) => _imagesOther.TryGetValue( imageName, out var image ) ? image : null;
	}
}
