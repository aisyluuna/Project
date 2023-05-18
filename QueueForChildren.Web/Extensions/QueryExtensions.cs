using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueueForChildren.Web.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> query, bool cond,
            Expression<Func<TSource, bool>> predicate)
        {
            return cond ? query.Where(predicate) : query;
        }
    }
}