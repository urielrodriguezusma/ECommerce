using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;
        private Hashtable repositories;

        public UnitOfWork(StoreContext context)
        {
            this.context = context;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Hashtable();
            }

            var entityName = typeof(TEntity).Name;
            if (!repositories.ContainsKey(entityName))
            {
                var genericType = typeof(IGenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(genericType.MakeGenericType(typeof(TEntity)), this.context);
                repositories.Add(entityName, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)repositories[entityName];
        }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
