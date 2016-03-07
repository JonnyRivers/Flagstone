using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Employees;

namespace EmployeeManager.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        // TODO: is there much value in this layer?
        // It may allow us to keep logic out of Employee.
        public static EmployeeViewModel FromEmployee(Employee employee)
        {
            var employeeViewModel = new EmployeeViewModel()
            {
                Name = String.Format("{0} {1}", employee.FirstName, employee.LastName),
                DepartmentName = employee.Department.Name
            };

            return employeeViewModel;
        }

        public string Name { get; private set; }
        public string DepartmentName { get; private set; }
    }
}
