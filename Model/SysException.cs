using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model
{
    public class SysException
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string StackTrace { get; set; }
        public string Url { get; set; }
        public DateTime @DateTime { get; set; }
    }

    public class SysExceptionConfiguration : IEntityTypeConfiguration<SysException>
    {
        public void Configure(EntityTypeBuilder<SysException> builder)
        {
            builder.Property(s => s.Message).IsRequired();
            builder.Property(s => s.Type).IsRequired();
            builder.Property(s => s.StackTrace).IsRequired();
            builder.Property(s => s.Url).IsRequired();
            builder.Property(s => s.DateTime).IsRequired();
        }
    }
}