using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venom.API.Models;
using Venom.Data.Models;

namespace Venom.API
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions<DataContext> options ) : base( options )
        {

        }

        public DbSet<Account> _Accounts { get; set; }
        //public DbSet<Models.Game.GameAccounts> _Players { get; set; }
        public DbSet<GameData> _GameData { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            //modelBuilder.ApplyConfiguration( new Configurations.PlayerConfiguration() );
            modelBuilder.ApplyConfiguration( new Configurations.AccountConfiguration( ) );
            modelBuilder.ApplyConfiguration( new Configurations.GameDataConfiguration( ) );

            base.OnModelCreating( modelBuilder );
        }

        public void Initialize()
        { 
            Database.EnsureCreated( );

            //=> Check Accounts
            //if( _Accounts.Any() )
            //{
            //    return;
            //}

            _Accounts.Add( new Account { Username = "Sentinel1", Password = "apfelbrot1234" } );
            _Accounts.Add( new Account { Username = "Sentinel2", Password = "apfelbrot1234" } );
            _Accounts.Add( new Account { Username = "Sentinel3", Password = "apfelbrot1234" } );
            _Accounts.Add( new Account { Username = "Sentinel4", Password = "apfelbrot1234" } );
            _Accounts.Add( new Account { Username = "Sentinel5", Password = "apfelbrot1234" } );
            _Accounts.Add( new Account { Username = "Sentinel6", Password = "apfelbrot1234" } );
            SaveChanges( );

            _GameData.Add( new GameData { Server = "DE172", Ally = "Empty", Player = "Empty", Village = "Empty" } );
            _GameData.Add( new GameData { Server = "DE173", Ally = "Empty", Player = "Empty", Village = "Empty" } );
            _GameData.Add( new GameData { Server = "DE174", Ally = "Empty", Player = "Empty", Village = "Empty" } );
            _GameData.Add( new GameData { Server = "DE175", Ally = "Empty", Player = "Empty", Village = "Empty" } );
            _GameData.Add( new GameData { Server = "DE176", Ally = "Empty", Player = "Empty", Village = "Empty" } );
            SaveChanges( );
        }
    }
}
