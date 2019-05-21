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
        Task<IReadOnlyList<Player>> GetPlayersAsync( GameServer server );

        Task<List<string>> GetPlayerNamesAsync( GameServer server );
    }
}
