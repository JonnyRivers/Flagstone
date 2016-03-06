using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Employees;

namespace EmployeeManager.ViewModels
{
    public class EmployeeListViewModel : ViewModelBase
    {
        private readonly IEmployeeRepository m_employeeRepository;

        public ObservableCollection<Employee> AllEmployees
        {
            get;
            private set;
        }

        public EmployeeListViewModel()
        {
            m_employeeRepository = new EntityFrameworkEmployeeRepository();

            AllEmployees = new ObservableCollection<Employee>(m_employeeRepository.GetAll());
        }
    }
}
