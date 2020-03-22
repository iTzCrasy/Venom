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

        public Task<IReadOnlyList<Village>> GetVillagesAsync()
        {
            return null;
            //return _context.GetVillages( );
        }

        public Task<IReadOnlyList<Player>> GetPlayersAsync( )
        {
            return _context.GetPlayers( );
        }
    }
}
