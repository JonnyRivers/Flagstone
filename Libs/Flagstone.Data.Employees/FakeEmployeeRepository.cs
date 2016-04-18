using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data.Employees
{
    public class FakeEmployeeRepository : FakeRepository<Employee>, IEmployeeRepository
    {
        protected override long GetPrimaryKey(Employee entity)
        {
            return entity.Id;
        }
    }
}
