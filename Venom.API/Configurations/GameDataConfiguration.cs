using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venom.API.Models;

namespace Venom.API.Configurations
{
    public class GameDataConfiguration : IEntityTypeConfiguration<GameData>
    {
        public void Configure( EntityTypeBuilder<GameData> builder )
        {
            builder.ToTable( "GameData" );
            //=> Primary Key
            builder.HasKey( p => p.Server );

            //=> Columns
            //builder.Property( p => p.Player ).HasColumnType( "nvarchar(max)" ).IsRequired( );
            //builder.Property( p => p.Ally ).HasColumnType( "nvarchar(max)" ).IsRequired( );
            //builder.Property( p => p.Village ).HasColumnType( "nvarchar(max)" ).IsRequired( );
            //builder.Property( p => p.Conquered ).HasColumnType( "nvarchar(max)" ).IsRequired( );

            //=> Columns
            builder.Property( p => p.Date ).HasColumnType( "datetime" ).HasDefaultValueSql( "GETDATE()" );
        }
    }
}
