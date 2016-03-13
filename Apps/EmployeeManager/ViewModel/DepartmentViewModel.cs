using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flagstone.Employees;
using Flagstone.WPF;

namespace EmployeeManager.ViewModel
{
    public class DepartmentViewModel : ViewModelBase
    {
        public long Id { get; private set; }
        public string Name { get; set; }

        public DepartmentViewModel(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
