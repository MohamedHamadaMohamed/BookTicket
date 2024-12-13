using BookTicket.Data;
using BookTicket.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace BookTicket.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;// = new ApplicationDbContext();
        DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public void Alter(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null) 
            {
                query = query.Where(filter);
            }
            if (includeProps != null)
            {
                    foreach (var prop in includeProps)
                    {
                       
                        query = query.Include(prop);
                    }
            }
            
            if (!tracked)
            {
                    query = query.AsNoTracking();
            }
            return query;
        }

        public T? GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool trancked = true)
        {
                return Get(filter: filter, includeProps: includeProps , tracked: trancked).FirstOrDefault();
            }
    }
}

