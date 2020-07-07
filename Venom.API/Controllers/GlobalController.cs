using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Venom.API.Utility;
using Venom.API.Database.Global;
using Venom.API.Database.Global.Entities;
using Microsoft.EntityFrameworkCore;
using Venom.Data.Api;
using Microsoft.AspNetCore.Authorization;

namespace Venom.API.Controllers
{
    [Route("api/global")]
    [ApiController]
    public class GlobalController : ControllerBase
    {
        private readonly GlobalContext _globalContext;

        public GlobalController( GlobalContext globalContext )
        {
            _globalContext = globalContext;
        }


        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password">SHA256 Hash by Client!</param>
        /// <returns>Login Type, if Blocked, License Requiered, Successfull, Wrong Data.</returns>
        [HttpGet( "Login" )]
        public async Task<Protocol.LoginTypes> Login( string Username, string Password )
        {
            if( string.IsNullOrEmpty( Username ) || string.IsNullOrEmpty( Password ) )
            {
                return Protocol.LoginTypes.WrongData;
            }

            var account = await _globalContext.Account.Where( p => p.Username == Username && p.Password == Password ).FirstOrDefaultAsync( ); 
            if( account != null )
            {
                if( account.Blocked )
                {
                    return Protocol.LoginTypes.Blocked;
                }

                if( DateTime.Now > account.LicenseDate )
                {
                    return Protocol.LoginTypes.Payment;
                }

                //////////////////////////////////////////////////////////////
                //=> TODO: Create Session, Update Account State, Logging
                //////////////////////////////////////////////////////////////

                return Protocol.LoginTypes.Success;
            }

            return Protocol.LoginTypes.WrongData;
        }

        /// <summary>
        /// Get Server List
        /// </summary>
        /// <returns>Responses the current Server List.</returns>
        [HttpGet( "ServerList" )]
        public async Task<ActionResult<IEnumerable<ServerData>>> ServerList()
        {
            //////////////////////////////////////////////////////////////
            //=> TODO: Check Session is Valid!
            //////////////////////////////////////////////////////////////
    
            //=> Only Send Valid Server Back!
            return await _globalContext.ServerList.Where( p => p.IsValid == true ).OrderBy( p => p.World ).ToListAsync();
        }
    }
}
