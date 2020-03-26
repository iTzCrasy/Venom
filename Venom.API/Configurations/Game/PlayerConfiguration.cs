using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Venom.API.Configurations.Game
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Models.Game.PlayerModel>
    {
        public void Configure( EntityTypeBuilder<Models.Game.PlayerModel> builder )
        {
            //=> Date
            builder.Property( p => p.Date )
                .ValueGeneratedOnAdd( );

            //=> Index
            builder.HasIndex( p => p.Id )
                .IncludeProperties( p => new
                {
                    p.World,
                    p.Date
                } );
        }
    }
}
