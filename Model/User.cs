using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model
{
    public class User : IdentityUser<int>, IAudit
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        public int RowCreatedBy { get; set; }
        public DateTime RowCreatedDateTime { get; set; }
        public int RowModifiedBy { get; set; }
        public DateTime RowModifiedDateTime { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired();
            builder.Property(p => p.NormalizedUserName).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.NormalizedEmail).IsRequired();
            builder.Property(p => p.PasswordHash).IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.IsActive).IsRequired()
                                             .HasDefaultValue(true);
        }
    }
}