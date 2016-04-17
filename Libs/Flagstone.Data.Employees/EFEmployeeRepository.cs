using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data.Employees
{
    public class EFEmployeeRepository : EFRepository<Employee>, IEmployeeRepository
    {
        public EFEmployeeRepository(EmployeesDbContext dbContext) : base(dbContext)
        {

        }
    }
}
