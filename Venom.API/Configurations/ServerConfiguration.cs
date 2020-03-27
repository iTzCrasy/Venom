using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Venom.API.Configurations
{
    public class ServerConfiguration : IEntityTypeConfiguration<Models.ServerModel>
    {
        public void Configure( EntityTypeBuilder<Models.ServerModel> builder )
        {
            //=> First Update
            builder.Property( p => p.FirstUpdate )
                .ValueGeneratedOnAdd( );

            //=> Last Update
            builder.Property( p => p.LastUpdate )
                .ValueGeneratedOnUpdate( );

            ////=> Data Updates
            //builder.Property( p => p.NextPlayer )
            //    .ValueGeneratedOnUpdate( );

            //builder.Property( p => p.NextAlly )
            //    .ValueGeneratedOnUpdate( );

            //builder.Property( p => p.NextVillages )
            //    .ValueGeneratedOnUpdate( );
        }
    }
}
