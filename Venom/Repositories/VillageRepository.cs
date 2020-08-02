using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data;
using Venom.Data.Models;

namespace Venom.Repositories
{
    public class VillageRepository : IVillageRepository
    {
        private readonly DataContext _context;


        public VillageRepository(
            DataContext context
            )
        {
            _context = context;
        }

        public Task<IReadOnlyList<Village>> GetVillagesAsync( int Server )
        {
            return _context.GetVillages( Server );
        }
    }
}
