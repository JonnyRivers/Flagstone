using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Employees;

namespace EmployeeManager
{
    public class EmployeesViewModel
    {
        public EmployeesViewModel()
        {
            m_employeeRepository = new EntityFrameworkEmployeeRepository();
            Employees = new ObservableCollection<Employee>(m_employeeRepository.GetAll());
        }

        public ObservableCollection<Employee> Employees { get; private set; }

        private IEmployeeRepository m_employeeRepository;
    }
}
