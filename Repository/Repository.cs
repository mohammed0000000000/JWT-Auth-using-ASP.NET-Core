using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace WebAPIDemo.Repository
{
    public class Repository<T, K> : IRepository<T, K> where T : class
    {
        protected DbContext context;
        private DbSet<T> entitySet;
        public Repository(DbContext context) { 
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            entitySet = context.Set<T>();   
        }
        public async Task<T> Create(T item) {
            await entitySet.AddAsync(item);
            await context.SaveChangesAsync();
            //context.Entry(item).State = EntityState.Detached;   
            return item;
        }

        public async Task AddRange(List<T> items) {
            await entitySet.AddRangeAsync(items);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(T item) {
            entitySet.Attach(item);
            entitySet.Remove(item);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteById(int id) {
            T item = await entitySet.FindAsync(id);
            if (item is null)
                return false;
            entitySet.Remove(item);
            await context.SaveChangesAsync();
            return true;
        }

        //public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool includeSoftDeleted = false, params Expression<Func<T, object>>[] includes) {
        //    var items = entitySet.AsNoTracking().AsQueryable<T>();
        //    if (includes is not null) {
        //        foreach (var include in includes) {
        //            items = items.Include(include);
        //        }
        //    }
        //    return includeSoftDeleted ? items.IgnoreQueryFilters().Where(predicate):items.Where(predicate);
        //}

        //public Task<List<T>> FindList() {
        //    List<T> items = new List<T>();
        //    items = entitySet.AsNoTracking().ToList();
        //    return  Task.FromResult(items);
        //}

        public async Task<IQueryable<T>> GetAll(bool includeSoftDeleted = false) {
            return includeSoftDeleted ? entitySet.AsNoTracking().IgnoreQueryFilters() : entitySet.AsNoTracking();
        }

        public async Task<T >getById(int id) {
            var item  = await entitySet.FindAsync(id);
            if (entitySet is null) return null;
            context.Entry(item).State = EntityState.Detached;
            return item;
        }

        public async Task<bool> Update(T item) {
            entitySet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
            context.Entry(item).State = EntityState.Detached;
            return true;
        }
        //public T GetById2(Expression<Func<T, bool>> predicate, bool includeSoftDeleted = false, params Expression<Func<T, object>>[] includes) {
        //    var items = entitySet.AsNoTracking().AsQueryable();

        //    if (includes != null) {
        //        foreach (var include in includes) {
        //            items = items.Include(include);
        //        }
        //    }
        //    return includeSoftDeleted ? items.AsNoTracking().IgnoreQueryFilters().FirstOrDefault(predicate) : items.AsNoTracking().FirstOrDefault(predicate);
        //}
        //public IQueryable<T> GetByNameAndHMP(bool includeSoftDeleted = false) {


        //    return includeSoftDeleted ? entitySet.AsNoTracking().IgnoreQueryFilters() : entitySet.AsNoTracking();
        //}

    }
}
