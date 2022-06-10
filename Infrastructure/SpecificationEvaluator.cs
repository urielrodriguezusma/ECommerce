using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> currentQuery, ISpecification<TEntity> spec)
        {
            //var currentQuery = inputQuery;

            if (spec.Criteria != null)
            {
                currentQuery = currentQuery.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                currentQuery = currentQuery.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                currentQuery = currentQuery.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagingEnabled)
            {
                currentQuery = currentQuery.Skip(spec.Skip).Take(spec.Take);
            }

            if (spec.Includes.Count > 0)
            {
                currentQuery = spec.Includes.Aggregate(currentQuery, (current, include) => current.Include(include));
            }


            return currentQuery;
        }
    }
}
