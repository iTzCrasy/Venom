using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Venom.Repositories
{
    public interface IResourceRepository
    {
        void Initialize();
        Task OpenEntryAsync( string entry, Func<ZipArchiveEntry, Task> action );

        BitmapImage GetMapImage( string imageName );
        BitmapImage GetVillageImage( string imageName );
    }
}
