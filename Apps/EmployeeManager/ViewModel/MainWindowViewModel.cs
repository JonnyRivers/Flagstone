using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.WPF;

namespace EmployeeManager.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ViewModelBase employeeListViewModel, ViewModelBase departmentListViewModel)
        {
            if (employeeListViewModel == null)
                throw new ArgumentNullException(nameof(employeeListViewModel));

            if (departmentListViewModel == null)
                throw new ArgumentNullException(nameof(departmentListViewModel));

            EmployeeListViewModel = employeeListViewModel;
            DepartmentListViewModel = departmentListViewModel;
        }

        public ViewModelBase EmployeeListViewModel
        {
            get;
            private set;
        }

        public ViewModelBase DepartmentListViewModel
        {
            get;
            private set;
        }
    }
}
