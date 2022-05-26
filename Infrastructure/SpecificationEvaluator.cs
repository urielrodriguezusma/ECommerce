using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (spec.Includes.Count > 0)
            {
                currentQuery = spec.Includes.Aggregate(currentQuery, (current, include) => current.Include(include));
            }

            return currentQuery;
        }
    }
}
