using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venom.API.Models;

namespace Venom.API.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure( EntityTypeBuilder<Account> builder )
        {
            builder.ToTable( "Account" );

            //=> Primary Key
            builder.HasKey( p => p.Id );

            //=> Columns
            builder.Property( p => p.Username ).HasColumnType( "nvarchar(64)" ).IsRequired( );
            builder.Property( p => p.Password ).HasColumnType( "nvarchar(max)" ).IsRequired( );
            builder.Property( p => p.DateCreated ).HasColumnType( "datetime" ).HasDefaultValueSql( "GETDATE()" );
        }
    }
}
