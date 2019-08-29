using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data;
using Venom.Data.Models;

namespace Venom.Repositories
{
    public class AllyRepository : IAllyRepository
    {
        private readonly DataContext _context;


        public AllyRepository(
            DataContext context
            )
        {
            _context = context;
        }


        public Task<IReadOnlyList<Ally>> GetAllysAsync( GameServer server )
        {
            return _context.GetAllys( server );
        }
    }
}
