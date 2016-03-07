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
    public class EmployeeListViewModel : ViewModelBase
    {
        private readonly IDepartmentRepository m_departmentRepository;
        private readonly IEmployeeRepository m_employeeRepository;

        public ObservableCollection<ViewModelBase> AllEmployees
        {
            get;
            private set;
        }

        public EmployeeListViewModel()
        {
            // TODO: These should probably be created elsewhere and passed through
            m_departmentRepository = new EntityFrameworkDepartmentRepository();
            m_employeeRepository = new EntityFrameworkEmployeeRepository();

            AllEmployees = new ObservableCollection<ViewModelBase>(
                m_employeeRepository.GetAll().Select(employee => new EmployeeSummaryViewModel(m_departmentRepository, m_employeeRepository, employee))
            );
        }

        protected override void OnDispose()
        {
            m_departmentRepository.Dispose();
            m_employeeRepository.Dispose();
        }
    }
}
