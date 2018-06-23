using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model
{
    public class Blog : IAudit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; set; }

        public int RowCreatedBy { get; set; }
        public DateTime RowCreatedDateTime { get; set; }
        public int RowModifiedBy { get; set; }
        public DateTime RowModifiedDateTime { get; set; }
    }

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Url).IsRequired();
        }
    }
}