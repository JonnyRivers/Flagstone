using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public interface IEmployeeRepository : IDisposable
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetByDepartmentName(string departmentName);

        long AddEmployee(Employee employee);

        void UpdateEmployee(Employee employee);
    }
}
