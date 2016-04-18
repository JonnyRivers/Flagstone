using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data
{
    public abstract class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Dictionary<long, TEntity> m_entityMap;

        public FakeRepository()
        {
            m_entityMap = new Dictionary<long, TEntity>();
        }

        protected abstract long GetPrimaryKey(TEntity entity);

        public void Add(TEntity entity)
        {
            m_entityMap.Add(GetPrimaryKey(entity), entity);
        }

        public TEntity Get(long id)
        {
            return m_entityMap[id];
        }

        public IEnumerable<TEntity> GetAll()
        {
            return m_entityMap.Values;
        }

        public void Remove(TEntity entity)
        {
            m_entityMap.Remove(GetPrimaryKey(entity));
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return m_entityMap.Values.Where(predicate.Compile());
        }
    }
}
