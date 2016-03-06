using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Employees;

namespace EmployeeManager.ViewModel
{
    public class EmployeeListDesignTimeViewModel : ViewModelBase
    {
        private readonly IEmployeeRepository m_employeeRepository;

        public ObservableCollection<Employee> AllEmployees
        {
            get;
            private set;
        }

        public EmployeeListDesignTimeViewModel()
        {
            m_employeeRepository = new FakeEmployeeRepository();

            AllEmployees = new ObservableCollection<Employee>(m_employeeRepository.GetAll());
        }
    }
}
