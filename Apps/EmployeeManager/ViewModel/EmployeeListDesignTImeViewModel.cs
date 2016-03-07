using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Employees;
using Flagstone.WPF;

namespace EmployeeManager.ViewModel
{
    public class EmployeeListDesignTimeViewModel : ViewModelBase
    {
        private readonly IDepartmentRepository m_departmentRepository;
        private readonly IEmployeeRepository m_employeeRepository;

        public ObservableCollection<ViewModelBase> AllEmployees
        {
            get;
            private set;
        }

        public EmployeeListDesignTimeViewModel()
        {
            m_departmentRepository = new FakeDepartmentRepository();
            m_employeeRepository = new FakeEmployeeRepository();

            AllEmployees = new ObservableCollection<ViewModelBase>(
                m_employeeRepository.GetAll().Select(employee => new EmployeeSummaryViewModel(m_departmentRepository, m_employeeRepository, employee))
            );
        }
    }
}
