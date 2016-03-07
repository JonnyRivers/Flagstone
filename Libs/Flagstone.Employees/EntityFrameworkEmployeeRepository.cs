using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public class EntityFrameworkEmployeeRepository : IEmployeeRepository, IDisposable
    {
        private Entities m_dbContext;

        public EntityFrameworkEmployeeRepository()
        {
            m_dbContext = new Entities();
        }

        public IEnumerable<Employee> GetAll()
        {
            return m_dbContext.Employees.ToArray();
        }

        public IEnumerable<Employee> GetByDepartmentName(string departmentName)
        {
            return m_dbContext.Employees.Where(e => e.Department.Name == departmentName).ToArray();
        }

        public void UpdateEmployee(Employee employee)
        {
            // TODO: Do we need to make sure we aren't updating entities 'live'?
            m_dbContext.SaveChanges();
        }

        public void Dispose()
        {
            m_dbContext.Dispose();
        }
    }
}
