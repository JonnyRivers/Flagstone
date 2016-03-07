using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Flagstone.Employees;
using Flagstone.WPF;

namespace EmployeeManager.ViewModel
{
    public class EmployeeSummaryViewModel : ViewModelBase
    {
        private readonly IDepartmentRepository m_departmentRepository;
        private readonly IEmployeeRepository m_employeeRepository;
        private readonly Employee m_employee;

        public EmployeeSummaryViewModel(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, Employee employee)
        {
            m_departmentRepository = departmentRepository;
            m_employeeRepository = employeeRepository;

            m_employee = employee;

            EditCommand = new RelayCommand(EditCommandExecute, null);
        }

        public string FirstName 
        { 
            get { return m_employee.FirstName; }
        }
        public string LastName
        {
            get { return m_employee.LastName; }
        }
        public string DepartmentName
        {
            get { return m_employee.Department.Name; }
        }

        public ICommand EditCommand
        {
            get;
            private set;
        }

        private void EditCommandExecute(object parameter)
        {
            var editEmployeeWindow = new View.EditEmployeeWindow();
            editEmployeeWindow.DataContext = new EmployeeEditViewModel(m_departmentRepository, m_employeeRepository, m_employee);
            editEmployeeWindow.Show();
        }
    }
}

