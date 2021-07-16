using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TeamRedBackEnd.Database.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, Models.IModelBase, new()
    {
        protected DatabaseContext DatabaseContext { get; set; }
        public RepositoryBase(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public List<T> FindAll()
        {
            return this.DatabaseContext.Set<T>().ToList();
        }

        public List<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DatabaseContext.Set<T>().Where(expression).AsNoTracking().ToList();
        }

        public async Task<List<T>> FindAllAsync()
        {
            return await this.DatabaseContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.DatabaseContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }

        public T GetSingle(int id)
        {
            return this.DatabaseContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> expression)
        {
            return this.DatabaseContext.Set<T>().FirstOrDefault(expression);
        }
        public bool Exists(Expression<Func<T, bool>> expression)
        {
            var model = this.DatabaseContext.Set<T>().FirstOrDefault(expression);
            return model != null;
        }

        public void Create(T model)
        {
            this.DatabaseContext.Set<T>().Add(model);
        }

        public void Update(T model)
        {
            this.DatabaseContext.Set<T>().Update(model);
        }

        public void Delete(T model)
        {
            this.DatabaseContext.Set<T>().Remove(model);
        }

        
    }
}
