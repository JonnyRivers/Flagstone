using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Employees;
using Flagstone.WPF;

namespace EmployeeManager.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ViewModelBase employeeListViewModel)
        {
            if (employeeListViewModel == null)
                throw new ArgumentNullException("employeeListViewModel");

            EmployeeListViewModel = employeeListViewModel;
        }

        public ViewModelBase EmployeeListViewModel
        {
            get;
            private set;
        }
    }
}
