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

namespace Venom.Core
{
	internal class ResourceManager : Singleton<ResourceManager>
	{
		private readonly string _archivePath = Path.Combine( Directory.GetCurrentDirectory( ), "resources.zip" );

		private Dictionary<string, BitmapImage> _mapImages;

		private Dictionary<string, BitmapImage> _villageImages;


		/// <summary>
		/// initializes all resources
		/// </summary>
		public void Initialize( )
		{
			// Initialize needs to be run from the ui thread!
			Debug.Assert( Dispatcher.FromThread( Thread.CurrentThread ) != null );

			OpenArchive( ( archive ) =>
			{
				_mapImages = LoadImagesFromArchive( archive, "Images/Map/Default/" );
				_villageImages = LoadImagesFromArchive( archive, "Images/Villages/Default/" );

				// check if images were actually loaded
				Debug.Assert( _mapImages.Count > 0 || _villageImages.Count > 0 );
			} );
		}

		public void OpenArchive( Action<ZipArchive> action )
		{
			if( !File.Exists( _archivePath ) )
			{
				throw new FileNotFoundException( $"Resource file not found at: {_archivePath}" );
			}

			using( var zipFile = new FileStream( _archivePath, FileMode.Open ) )
			{
				using( var archive = new ZipArchive( zipFile, ZipArchiveMode.Read ) )
				{
					action( archive );
				}
			}
		}

		public async Task OpenArchiveAsync( Func<ZipArchive, Task> action )
		{
			if( !File.Exists( _archivePath ) )
			{
				throw new FileNotFoundException( $"Resource file not found at: {_archivePath}" );
			}

			using( var zipFile = new FileStream( _archivePath, FileMode.Open ) )
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
					// log
					Debug.Print( $"[LoadImagesFromArchive] skipping file {i.FullName}" );
					continue;
				}

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



		public BitmapImage GetMapImage( string imageName )
		{
			return _mapImages.TryGetValue( imageName, out var image ) ? image : null;
		}

		public BitmapImage GetViallgeImage( string imageName )
		{
			return _villageImages.TryGetValue( imageName, out var image ) ? image : null;
		}
	}
}
