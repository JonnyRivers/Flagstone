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
    public class DesignTimeDepartmentListViewModel : ViewModelBase
    {
        private readonly IUnitOfWorkFactory m_unitOfWorkFactory;

        public ObservableCollection<DepartmentViewModel> AllDepartments
        {
            get;
            private set;
        }

        public DesignTimeDepartmentListViewModel()
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
            }
        }
    }
}
