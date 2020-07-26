using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Venom.API.Database.Logging
{
    public class LoggingContext : DbContext
    {
        #region Entities
        public DbSet<Entities.ServerUpdates> ServerUpdates { get; set; }
        #endregion

        public LoggingContext( DbContextOptions<LoggingContext> options ) : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            #region Server Updates
            builder.Entity<Entities.ServerUpdates>( )
                .HasIndex( p => new { p.Server, p.UpdateTime } )
                .IsUnique( );
            #endregion
        }
    }
}
