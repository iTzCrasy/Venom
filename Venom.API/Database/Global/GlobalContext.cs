using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venom.API.Database.Server.Entities;

namespace Venom.API.Database.Global
{
    public class GlobalContext : DbContext
    {
        #region Entities
        public DbSet<Entities.Account> Account { get; set; }
        public DbSet<Entities.ServerData> ServerList { get; set; }
        #endregion


        public GlobalContext( DbContextOptions<GlobalContext> options ) : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            #region Account
            builder.Entity<Entities.Account>( )
                .HasKey( p => p.Id );
            #endregion

            #region ServerData
            builder.Entity<Entities.ServerData>( )
                .HasIndex( p => new { p.World, p.Url } )
                .IsUnique( );
            #endregion
        }
    }
}
