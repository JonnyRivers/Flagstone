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
        IEnumerable<Employee> GetAllByDepartmentName(string departmentName);

        Employee Get(long employeeId);

        long AddEmployee(Employee employee);

        void DeleteEmployee(long employeeId);

        void UpdateEmployee(Employee employee);
    }
}
