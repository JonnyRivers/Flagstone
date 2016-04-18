using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data.Employees
{
    public class FakeDepartmentRepository : IDepartmentRepository
    {
        private Dictionary<long, Department> m_entityMap;

        public FakeDepartmentRepository()
        {
            m_entityMap = new Dictionary<long, Department>();
        }

        public void Add(Department entity)
        {
            m_entityMap.Add(entity.Id, entity);
        }

        public Department Get(long id)
        {
            return m_entityMap[id];
        }

        public IEnumerable<Department> GetAll()
        {
            return m_entityMap.Values;
        }

        public void Remove(Department entity)
        {
            m_entityMap.Remove(entity.Id);
        }

        public IEnumerable<Department> Where(Expression<Func<Department, bool>> predicate)
        {
            return m_entityMap.Values.Where(predicate.Compile());
        }
    }
}
;