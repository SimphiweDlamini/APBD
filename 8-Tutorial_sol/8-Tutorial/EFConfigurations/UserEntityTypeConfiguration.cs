using _8_Tutorial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.EFConfigurations
{
    public class UserEntityTypeConfiguration:IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.IdUser);
            builder.Property(e => e.IdUser).ValueGeneratedOnAdd();

            builder.Property(e => e.Login).IsRequired();
            builder.Property(e => e.HashedPassword).IsRequired();
            builder.Property(e => e.Salt).IsRequired();
            builder.Property(e => e.RefreshToken).IsRequired();

        }
    }
}
