using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Venom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<UpdateController> _logger;

        public UpdateController( IHttpContextAccessor accessor, ILogger<UpdateController> logger )
        {
            _accessor = accessor;
            _logger = logger;
        }

        /// <summary>
        /// GET: api/Update/<ver>
        /// Checking for updates, if version missmatch --> update
        /// </summary>
        /// <param name="ver"></param>
        /// <returns>True / False</returns>
        [HttpGet( "Test/{ver}" )]
        public string Get( int ver )
        {
            //=> Check Version, multi return
            return "value";
        }


        /// <summary>
        /// GET: api/Update/
        /// Pull the newest version 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Newest Version, binary</returns>
        [HttpGet]
        public async Task<FileStreamResult> Get()
        {
            _logger.LogInformation( "{0} Get Update", _accessor.HttpContext.Connection.RemoteIpAddress.ToString( ) );

            var session = HttpContext.Session.Get( "Test" );

            var path = "venom.exe";
            var stream = System.IO.File.OpenRead( path );
            return new FileStreamResult( stream, "application/octet-stream" );
        }
    }
}
