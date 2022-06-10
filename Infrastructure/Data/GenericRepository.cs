using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }
        public async Task<int> AddNewEntityAsync(T entity)
        {
            this.context.Add(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteEntityAsync(T entity)
        {
            this.context.Remove(entity);
            return await this.context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await this.context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T> UpdateEntityAsync(T entity)
        {
            this.context.Update(entity);
            await this.context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ImplementEvaluator(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ImplementEvaluator(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ImplementEvaluator(spec).CountAsync();
        }

        private IQueryable<T> ImplementEvaluator(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
        }
    }
}
