using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(long id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        // TODO AddRange could go here, for example

        void Remove(TEntity entity);
        // TODO RemoveRange could go here, for example
    }
}
