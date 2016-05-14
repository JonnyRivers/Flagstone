using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Data.Employees;
using Flagstone.WPF;

namespace EmployeeManager.ViewModel
{
    public class DesignTimeEmployeeListViewModel : ViewModelBase
    {
        private readonly IUnitOfWorkFactory m_unitOfWorkFactory;

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
            m_unitOfWorkFactory = new FakeUnitOfWorkFactory();

            using(IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
            {
                AllDepartments = new ObservableCollection<DepartmentViewModel>(
                    unitOfWork.Departments.GetAll().Select(
                        department => new DepartmentViewModel(
                            m_unitOfWorkFactory,
                            department.DepartmentId,
                            department.Name
                        )
                    )
                );

                AllEmployees = new ObservableCollection<EmployeeViewModel>(
                    unitOfWork.Employees.GetAll().Select(
                        employee => new EmployeeViewModel(
                            m_unitOfWorkFactory,
                            employee.EmployeeId,
                            employee.FirstName,
                            employee.LastName,
                            employee.DateOfBirth, AllDepartments.First(d => d.Id == employee.DepartmentId)
                        )
                    )
                );
            }
        }
    }
}
