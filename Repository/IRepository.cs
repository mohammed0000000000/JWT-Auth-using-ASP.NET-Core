using System.Linq.Expressions;

namespace WebAPIDemo.Repository
{
    public interface IRepository<T, K> where T : class
    {
        Task<T> getById(int id);
        Task<T> Create(T item);
        Task AddRange(List<T> items);
        Task<bool> Update(T item);
        Task<bool> DeleteById(int id);
        Task<bool> Delete(T item);
        Task<IQueryable<T>> GetAll(bool includeSoftDeleted = false);
        //Task<List<T>> FindList();
        //Task<IQueryable<T>> Find(Expression<Func<T, bool>> predicate, bool includeSoftDeleted = false, params Expression<Func<T, object>>[] includes);
    }
}
