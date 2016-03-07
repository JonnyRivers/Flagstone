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
    public class EmployeeEditViewModel : ViewModelBase
    {
        private readonly IDepartmentRepository m_departmentRepository;
        private readonly IEmployeeRepository m_employeeRepository;
        private readonly Employee m_employee;

        public EmployeeEditViewModel(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, Employee employee)
        {
            m_departmentRepository = departmentRepository;
            m_employeeRepository = employeeRepository;
            m_employee = employee;

            Departments = m_departmentRepository.GetAll();

            ApplyChangesCommand = new RelayCommand(ApplyChangesExecute, null);
        }

        public string FirstName
        {
            get { return m_employee.FirstName; }
            set { m_employee.FirstName = value; }
        }
        public string LastName
        {
            get { return m_employee.LastName; }
            set { m_employee.LastName = value; }
        }
        public DateTime DateOfBirth
        {
            get { return m_employee.DateOfBirth; }
        }
        public Department Department
        {
            get { return m_employee.Department; }
            set { m_employee.Department = value; }
        }

        public IEnumerable<Department> Departments
        {
            get;
            private set;
        }

        public ICommand ApplyChangesCommand
        {
            get;
            private set;
        }

        private void ApplyChangesExecute(object parameter)
        {
            m_employeeRepository.UpdateEmployee(m_employee);
        }
    }
}
