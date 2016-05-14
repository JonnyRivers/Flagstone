using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data.Employees
{
    public class FakeDepartmentRepository : FakeRepository<Department>, IDepartmentRepository
    {
        protected override long GetPrimaryKey(Department entity)
        {
            return entity.DepartmentId;
        }

        protected override void SetPrimaryKey(Department entity, long primaryKey)
        {
            entity.DepartmentId = primaryKey;
        }
    }
}