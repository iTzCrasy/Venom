using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data;
using Venom.Data.Models;

namespace Venom.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DataContext _context;


        public PlayerRepository(
            DataContext context
            )
        {
            _context = context;
        }


        public Task<IReadOnlyList<Player>> GetPlayersAsync( GameServer server )
        {
            return _context.GetPlayers( server );
        }

        public async Task<List<string>> GetPlayerNamesAsync( GameServer server )
        {
            var players = await GetPlayersAsync( server )
                .ConfigureAwait( false );


            var list = new List<string>( );
            foreach( var i in players )
            {
                list.Add( i.Name );
            }

            return list;
        }
    }
}
