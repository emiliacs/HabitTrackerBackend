using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TeamRedBackEnd.Database.Repositories
{
    public interface IRepositoryBase<T> where T : class, Models.IModelBase, new()
    {
        List<T> FindAll();
        Task<List<T>> FindAllAsync();
        List<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        T GetSingle(int id);
        T GetSingle(Expression<Func<T, bool>> expression);

        bool Exists(Expression<Func<T, bool>> expression);
        
        void Create(T model);
        void Update(T model);
        void Delete(T model);
        
    }
}
