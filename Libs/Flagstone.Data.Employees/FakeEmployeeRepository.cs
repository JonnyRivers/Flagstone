using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data.Employees
{
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        private Dictionary<long, Employee> m_entityMap;

        public FakeEmployeeRepository()
        {
            m_entityMap = new Dictionary<long, Employee>();
        }

        public void Add(Employee entity)
        {
            m_entityMap.Add(entity.Id, entity);
        }

        public Employee Get(long id)
        {
            return m_entityMap[id];
        }

        public IEnumerable<Employee> GetAll()
        {
            return m_entityMap.Values;
        }

        public void Remove(Employee entity)
        {
            m_entityMap.Remove(entity.Id);
        }

        public IEnumerable<Employee> Where(Expression<Func<Employee, bool>> predicate)
        {
            return m_entityMap.Values.Where(predicate.Compile());
        }
    }
}
