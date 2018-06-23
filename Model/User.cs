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

        public int RowCreatedBy { get; set; }
        public DateTime RowCreatedDateTime { get; set; }
        public int RowModifiedBy { get; set; }
        public DateTime RowModifiedDateTime { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
        }
    }
}