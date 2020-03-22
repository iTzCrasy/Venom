using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data.Models;


namespace Venom.Repositories
{
    public interface IVillageRepository
    {
        Task<IReadOnlyList<Village>> GetVillagesAsync( );
    }
}
