using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model
{
    public class Role : IdentityRole<int>, IAudit
    {
        public int RowCreatedBy { get ; set ; }
        public DateTime RowCreatedDateTime { get ; set ; }
        public int RowModifiedBy { get ; set ; }
        public DateTime RowModifiedDateTime { get ; set ; }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            
        }
    }
}