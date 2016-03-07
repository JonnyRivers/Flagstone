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
        Employee m_employeeToEdit;

        public EmployeeEditViewModel(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, Employee employeeToEdit)
        {
            m_departmentRepository = departmentRepository;
            m_employeeRepository = employeeRepository;
            m_employeeToEdit = employeeToEdit;

            FirstName = m_employeeToEdit.FirstName;
            LastName = m_employeeToEdit.LastName;
            DateOfBirth = m_employeeToEdit.DateOfBirth;
            Department = m_employeeToEdit.Department;

            Departments = m_departmentRepository.GetAll();

            ApplyChangesCommand = new RelayCommand(ApplyChangesExecute, null);
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth  { get; private set; }
        public Department Department { get; set; }

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
            Employee updatedEmployee = new Employee
            {
                Id = m_employeeToEdit.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                DepartmentId = this.Department.Id,
                Department = this.Department
            };
            m_employeeRepository.UpdateEmployee(updatedEmployee);
        }
    }
}
