using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model
{
    public class Post : IAudit
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public bool IsActive { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int RowCreatedBy { get; set; }
        public DateTime RowCreatedDateTime { get; set; }
        public int RowModifiedBy { get; set; }
        public DateTime RowModifiedDateTime { get; set; }
    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasOne(p => p.Blog)
                   .WithMany(b => b.Posts);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Content).IsRequired().HasMaxLength(4000);
            builder.Property(p => p.IsActive).IsRequired()
                                             .HasDefaultValue(true);
        }
    }
}