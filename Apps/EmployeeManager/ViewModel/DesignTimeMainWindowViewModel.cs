using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Employees;
using Flagstone.WPF;

namespace EmployeeManager.ViewModel
{
    public class DesignTimeMainWindowViewModel : ViewModelBase
    {
        public DesignTimeMainWindowViewModel()
        {
            EmployeeListViewModel = new DesignTimeEmployeeListViewModel();
        }

        public ViewModelBase EmployeeListViewModel
        {
            get;
            private set;
        }
    }
}
