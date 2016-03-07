using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public class EntityFrameworkDepartmentRepository : IDepartmentRepository, IDisposable
    {
        private Entities m_dbContext;

        public EntityFrameworkDepartmentRepository()
        {
            m_dbContext = new Entities();
        }

        public IEnumerable<Department> GetAll()
        {
            return m_dbContext.Departments.ToArray();
        }

        public void Dispose()
        {
            m_dbContext.Dispose();
        }
    }
}

