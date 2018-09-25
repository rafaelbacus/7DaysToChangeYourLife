using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model
{
    public class SysException : IAudit
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string StackTrace { get; set; }
        public string Url { get; set; }

        public int RowCreatedBy { get; set; } = 1;
        public DateTime RowCreatedDateTime { get; set; }
        public int RowModifiedBy { get; set; } = 1;
        public DateTime RowModifiedDateTime { get; set; }

        public SysException()
        {
            
        }

        public SysException(Exception e)
        {
            Message = e.Message;
            Type = e.GetBaseException().GetType().ToString();
            StackTrace = e.StackTrace;
            RowCreatedBy = RowModifiedBy = 1;
            RowCreatedDateTime = RowModifiedDateTime = DateTime.Now;
        }
    }

    public class SysExceptionConfiguration : IEntityTypeConfiguration<SysException>
    {
        public void Configure(EntityTypeBuilder<SysException> builder)
        {
            builder.Property(s => s.Message).IsRequired()
                                            .HasMaxLength(256);
            builder.Property(s => s.Type).IsRequired()
                                         .HasMaxLength(256);
            builder.Property(s => s.StackTrace).IsRequired()
                                               .HasMaxLength(8192);
            builder.Property(s => s.Url).IsRequired()
                                        .HasMaxLength(256);
            builder.Property(s => s.RowCreatedBy).IsRequired();
            builder.Property(s => s.RowCreatedDateTime).IsRequired();
            builder.Property(s => s.RowModifiedBy).IsRequired();
            builder.Property(s => s.RowModifiedDateTime).IsRequired();
        }
    }
}