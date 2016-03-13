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
    // TODO - this is just a copy of the real thing with fake databases injected
    public class DesignTimeEmployeeListViewModel : ViewModelBase
    {
        private readonly IDepartmentRepository m_departmentRepository;
        private readonly IEmployeeRepository m_employeeRepository;

        public ObservableCollection<DepartmentViewModel> AllDepartments
        {
            get;
            private set;
        }

        public ObservableCollection<EmployeeViewModel> AllEmployees
        {
            get;
            private set;
        }

        public DesignTimeEmployeeListViewModel()
        {
            m_departmentRepository = new FakeDepartmentRepository();
            m_employeeRepository = new FakeEmployeeRepository();

            AllDepartments = new ObservableCollection<DepartmentViewModel>(
                m_departmentRepository.GetAll().Select(
                    department => new DepartmentViewModel(
                        department.Id,
                        department.Name
                    )
                )
            );

            AllEmployees = new ObservableCollection<EmployeeViewModel>(
                m_employeeRepository.GetAll().Select(
                    employee => new EmployeeViewModel(
                        m_employeeRepository,
                        employee.Id,
                        employee.FirstName,
                        employee.LastName,
                        employee.DateOfBirth, AllDepartments.First(d => d.Id == employee.DepartmentId)
                    )
                )
            );
        }
    }
}
