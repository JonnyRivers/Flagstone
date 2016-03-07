using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public interface IDepartmentRepository : IDisposable
    {
        IEnumerable<Department> GetAll();
    }
}
