using System;

namespace Model
{
    public interface IAudit
    {
        int RowCreatedBy { get; set; }
        DateTime RowCreatedDateTime { get; set; }
        int RowModifiedBy { get; set; }
        DateTime RowModifiedDateTime { get; set; }
    }
}