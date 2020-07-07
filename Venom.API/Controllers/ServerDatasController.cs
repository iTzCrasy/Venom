using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venom.API.Database.Global;
using Venom.API.Database.Global.Entities;

namespace Venom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces( "application/json" )]
    public class ServerDatasController : ControllerBase
    {
        private readonly GlobalContext _context;

        public ServerDatasController(GlobalContext context)
        {
            _context = context;
        }

        // GET: api/ServerDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServerData>>> GetServerList()
        {
            return StatusCode( StatusCodes.Status100Continue );
            return await _context.ServerList.ToListAsync();
        }

        // GET: api/ServerDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerData>> GetServerData(int id)
        {
            var serverData = await _context.ServerList.FindAsync(id);

            if (serverData == null)
            {
                return NotFound();
            }

            return serverData;
        }

        // PUT: api/ServerDatas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServerData(int id, ServerData serverData)
        {
            if (id != serverData.Id)
            {
                return BadRequest();
            }

            _context.Entry(serverData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ServerDatas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ServerData>> PostServerData(ServerData serverData)
        {
            _context.ServerList.Add(serverData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServerData", new { id = serverData.Id }, serverData);
        }

        // DELETE: api/ServerDatas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServerData>> DeleteServerData(int id)
        {
            var serverData = await _context.ServerList.FindAsync(id);
            if (serverData == null)
            {
                return NotFound();
            }

            _context.ServerList.Remove(serverData);
            await _context.SaveChangesAsync();

            return serverData;
        }

        private bool ServerDataExists(int id)
        {
            return _context.ServerList.Any(e => e.Id == id);
        }
    }
}
