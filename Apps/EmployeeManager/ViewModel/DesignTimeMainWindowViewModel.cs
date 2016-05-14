using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.WPF;

namespace EmployeeManager.ViewModel
{
    public class DesignTimeMainWindowViewModel : ViewModelBase
    {
        public DesignTimeMainWindowViewModel()
        {
            DepartmentListViewModel = new DesignTimeDepartmentListViewModel();
            EmployeeListViewModel = new DesignTimeEmployeeListViewModel();
        }

        public ViewModelBase DepartmentListViewModel
        {
            get;
            private set;
        }

        public ViewModelBase EmployeeListViewModel
        {
            get;
            private set;
        }
    }
}
