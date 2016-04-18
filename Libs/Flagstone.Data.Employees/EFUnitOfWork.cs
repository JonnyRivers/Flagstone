using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data.Employees
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EmployeesDbContext m_dbContext;

        // JWR - this could be internal.  Should it be?
        public EFUnitOfWork(EmployeesDbContext dbContext)
        {
            m_dbContext = dbContext;

            Departments = new EFDepartmentRepository(dbContext);
            Employees = new EFEmployeeRepository(dbContext);
        }

        public IDepartmentRepository Departments { get; }
        public IEmployeeRepository Employees { get; }

        public void Complete()
        {
            m_dbContext.SaveChanges();
        }

        public void Dispose()
        {
            m_dbContext.Dispose();
        }
    }
}
