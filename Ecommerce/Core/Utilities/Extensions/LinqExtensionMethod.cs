using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public static class LinqExtensionMethod
    {
        public static IQueryable<T> WhereAll<T, TKey>(this IQueryable<T> dbq, IEnumerable<TKey> searchTerms, Expression<Func<T, TKey, bool>> testFne)
        {
            var pred = PredicateBuilder.New<T>();
            foreach (var s in searchTerms)
                pred = pred.And(r => testFne.Invoke(r, s));
            return dbq.Where((Expression<Func<T, bool>>)pred.Expand());
        }
    }
}
