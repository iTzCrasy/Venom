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
using Core;

namespace Venom.Core
{
	internal class ResourceManager : Singleton<ResourceManager>
	{
		private readonly Dictionary<string, BitmapImage> _mapImages = new Dictionary<string, BitmapImage>( );

		private readonly Dictionary<string, BitmapImage> _villageImages = new Dictionary<string, BitmapImage>( );

		private readonly string _archivePath = Path.Combine( "./", "resources.zip" );



		private ResourceManager( )
		{
			Debug.Assert( File.Exists( _archivePath ) );
		}

		public void LoadArchive( )
		{
			using( var zipFile = new FileStream( _archivePath, FileMode.Open ) )
			{
				using( var archive = new ZipArchive( zipFile, ZipArchiveMode.Read ) )
				{
					LoadMapImages( archive );




					Debugger.Break( );

					//var entry = archive.GetEntry( "" );

					//using( var reader = new StreamReader( entry.Open( ) ) )
					//{
					//	// reader.ReadToEndAsync()
					//}
				}
			}
		}

		private void LoadMapImages( ZipArchive archive )
		{
			var pathPrefix = "Images/Map/Default/";

			var images = archive.Entries
				.Where( ( _ ) => _.FullName.Contains( pathPrefix ) && !_.FullName.Equals( pathPrefix ) )
				.Select( ( _ ) => new { _.FullName, Name = _.FullName.Replace( pathPrefix, string.Empty ) } );

			foreach( var i in images )
			{
				var entry = archive.GetEntry( i.FullName );

				using( var stream = entry.Open( ) )
				{
					var memoryStream = new MemoryStream( );
					stream.CopyTo( memoryStream );

					var bitmap = new BitmapImage( );
					bitmap.BeginInit( );
					bitmap.StreamSource = memoryStream;
					bitmap.CacheOption = BitmapCacheOption.OnLoad;
					bitmap.EndInit( );
					bitmap.Freeze( );

					_mapImages.Add( i.Name, bitmap );
				}
			}
		}


		public async Task ReadFile( string entryName )
		{
			using( var zipFile = new FileStream( "./resource.zip", FileMode.Open ) )
			{
				using( var archive = new ZipArchive( zipFile, ZipArchiveMode.Read ) )
				{
					var entry = archive.GetEntry( entryName );

					using( var reader = new StreamReader( entry.Open( ) ) )
					{
						// reader.ReadToEndAsync()
					}
				}
			}
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
