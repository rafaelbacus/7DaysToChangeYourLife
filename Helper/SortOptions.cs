using System;

namespace Helper
{
    public class SortOptions
    {
        public string SortBy { get; set; }
        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
    }

    public enum SortOrder
    {
        Ascending = 1,
        Descending = 2
    }
}
