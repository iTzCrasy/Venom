using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Venom.API.Data.Database
{
    public class ApiContext : DbContext
    {
        #region Entities
        public DbSet<Entities.Player> Player { get; set; }
        #endregion


        public ApiContext( DbContextOptions<ApiContext> options ) : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            builder.Entity<Entities.Player>( )
                .HasIndex( p => new { p.PlayerId, p.Name, p.Server } )
                .IsUnique( );
        }
    }
}
