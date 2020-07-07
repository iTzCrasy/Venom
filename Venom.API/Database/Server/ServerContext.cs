using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Venom.API.Database.Server
{
    public class ServerContext : DbContext
    {
        #region Entities
        public DbSet<Entities.Player> Player { get; set; }
        public DbSet<Entities.Ally> Ally { get; set; }
        public DbSet<Entities.Village> Village { get; set; }
        #endregion

        public ServerContext( DbContextOptions<ServerContext> options ) : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            #region Player
            builder.Entity<Entities.Player>( )
                .HasKey( p => new { p.PlayerId, p.Name, p.Server } );
            #endregion

            #region Ally
            builder.Entity<Entities.Ally>( )
                .HasKey( p => new { p.AllyId, p.Name, p.Tag, p.Server } );
            #endregion

            #region Village
            builder.Entity<Entities.Village>( )
                .HasKey( p => new { p.VillageId, p.X, p.Y, p.Server } );
            #endregion
        }
    }
}
