using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data.Models;

namespace Venom.Repositories
{
    public interface IPlayerRepository
    {
        Task<IReadOnlyList<Player>> GetPlayersAsync();

        Task<List<string>> GetPlayerNamesAsync();

        Task<IReadOnlyList<Player>> GetPlayersByAllyAsync( int ally );
    }
}
