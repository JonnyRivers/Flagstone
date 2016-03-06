using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Employees;

namespace EmployeeManager.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ViewModelBase employeeListViewModel)
        {
            EmployeeListViewModel = employeeListViewModel;
        }

        public ViewModelBase EmployeeListViewModel
        {
            get;
            private set;
        }
    }
}
