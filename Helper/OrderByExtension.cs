using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Helper
{
    public static class OrderByExtension
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, SortOptions options)
        {
            if (!source.Any() || String.IsNullOrWhiteSpace(options.SortBy))
            {
                return source;
            }

            var propertyInfo = source.First().GetType().GetProperty(options.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (options.SortOrder == SortOrder.Descending)
            {
                return source.OrderByDescending(s => propertyInfo.GetValue(s));
            }

            return source.OrderBy(s => propertyInfo.GetValue(s));   
        }
    }
}
