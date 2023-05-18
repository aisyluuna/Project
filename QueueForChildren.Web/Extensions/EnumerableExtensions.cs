using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueForChildren.Web.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> enumerable, bool cond, Func<T, bool> predicate)
        {
            return cond ? enumerable.Where(predicate) : enumerable;
        }
    }
}