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


        public Task<IReadOnlyList<Player>> GetPlayersAsync()
        {
            return _context.GetPlayers();
        }

        public async Task<List<string>> GetPlayerNamesAsync( )
        {
            var players = await GetPlayersAsync()
                .ConfigureAwait( false );


            var list = new List<string>( );
            foreach( var i in players )
            {
                list.Add( i.Name );
            }
            return list;
        }

        public async Task<IReadOnlyList<Player>> GetPlayersByAllyAsync( int ally )
        {
            var players = await GetPlayersAsync( )
                .ConfigureAwait( false );

            var list = new List<Player>( );
            foreach( var i in players )
            {
                if( i.Ally.Equals( ally ) )
                    list.Add( i );
            }
            return list;
        }
    }
}
