using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Data;
using Venom.Data.Models;

namespace Venom.Repositories
{
    public interface IAllyRepository
    {
        Task<IReadOnlyList<Ally>> GetAllysAsync( GameServer server );
    }
}
