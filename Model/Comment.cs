using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model
{
    public class Comment : IAudit
    {
        [Key]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public IEnumerable<Comment> Replies { get; set; }
        public bool IsActive { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int RowCreatedBy { get; set; }
        public DateTime RowCreatedDateTime { get; set; }
        public int RowModifiedBy { get; set; }
        public DateTime RowModifiedDateTime { get; set; }
    }

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c => c.Post)
                   .WithMany(p => p.Comments);
            builder.Property(c => c.Author).HasMaxLength(50);
            builder.Property(c => c.Content).IsRequired().HasMaxLength(300);
            builder.Property(c => c.IsActive).IsRequired()
                                             .HasDefaultValue(true);
        }
    }
}