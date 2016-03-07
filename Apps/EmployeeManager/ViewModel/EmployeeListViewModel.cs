using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Employees;

namespace EmployeeManager.ViewModel
{
    public class EmployeeListViewModel : ViewModelBase
    {
        private readonly IEmployeeRepository m_employeeRepository;

        public ObservableCollection<ViewModelBase> AllEmployees
        {
            get;
            private set;
        }

        public EmployeeListViewModel()
        {
            m_employeeRepository = new EntityFrameworkEmployeeRepository();

            AllEmployees = new ObservableCollection<ViewModelBase>(
                m_employeeRepository.GetAll().Select(entity => EmployeeViewModel.FromEmployee(entity))
            );
        }
    }
}
