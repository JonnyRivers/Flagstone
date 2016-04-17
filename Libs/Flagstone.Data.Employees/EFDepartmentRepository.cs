using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Data;

namespace Flagstone.Data.Employees
{
    public class EFDepartmentRepository : EFRepository<Department>, IDepartmentRepository
    {
        public EFDepartmentRepository(EmployeesDbContext dbContext) : base(dbContext)
        {

        }
    }
}
