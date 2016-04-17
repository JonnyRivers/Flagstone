using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext m_dbContext;

        public EFRepository(DbContext dbContext)
        {
            m_dbContext = dbContext;
        }

        public TEntity Get(int id)
        {
            return m_dbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return m_dbContext.Set<TEntity>().ToArray();
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return m_dbContext.Set<TEntity>().Where(predicate).ToArray();
        }

        public void Add(TEntity entity)
        {
            m_dbContext.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            m_dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
