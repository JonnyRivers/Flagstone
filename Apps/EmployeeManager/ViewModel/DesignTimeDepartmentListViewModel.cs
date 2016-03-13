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
    public class DesignTimeDepartmentListViewModel : ViewModelBase
    {
        private readonly IDepartmentRepository m_departmentRepository;

        public ObservableCollection<DepartmentViewModel> AllDepartments
        {
            get;
            private set;
        }

        public DesignTimeDepartmentListViewModel()
        {
            m_departmentRepository = new FakeDepartmentRepository();

            AllDepartments = new ObservableCollection<DepartmentViewModel>(
                m_departmentRepository.GetAll().Select(
                    department => new DepartmentViewModel(
                        m_departmentRepository,
                        department.Id,
                        department.Name
                    )
                )
            );
        }
    }
}
