using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venom.API.Models;
using Venom.Data.Models;

namespace Venom.API.Context
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions<DataContext> options ) : base( options )
        {

        }

        public DbSet<Models.Game.PlayerModel> Player { get; set; }
        public DbSet<Models.Game.AllyModel> Ally { get; set; }

        public DbSet<Models.ServerModel> Server { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfiguration( new Configurations.ServerConfiguration( ) );
            modelBuilder.ApplyConfiguration( new Configurations.Game.PlayerConfiguration( ) );
            modelBuilder.ApplyConfiguration( new Configurations.Game.AllyConfiguration( ) );

            base.OnModelCreating( modelBuilder );
        }

        public void Seed()
        { 
            if( Database.EnsureDeleted() )
            {
                if( Database.EnsureCreated( ) )
                {
                    //=> Seed Database
                }
            }
        }
    }
}
