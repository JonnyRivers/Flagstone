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
    public class EmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeeRepository m_employeeRepository;

        private readonly long m_id;
        private string m_firstName;
        private string m_lastName;

        private bool m_isDirty;

        public long Id
        {
            get { return m_id; }
        }
        public string FirstName
        {
            get { return m_firstName; }
            set {
                if (value != m_firstName)
                {
                    m_firstName = value;
                    IsDirty = true;
                }
            }
        }
        public string LastName
        {
            get { return m_lastName; }
            set
            {
                if (value != m_lastName)
                {
                    m_lastName = value;
                    IsDirty = true;
                }
            }
        }
        public DateTime DateOfBirth { get; set; }
        public DepartmentViewModel Department { get; set; }

        public bool IsDirty 
        {
            get { return m_isDirty; }
            private set
            {
                m_isDirty = value;
                OnPropertyChanged("IsDirty");
            }
        }

        public EmployeeViewModel(IEmployeeRepository employeeRepository, long id, string firstName, string lastName, DateTime dateOfBirth, DepartmentViewModel deparment)
        {
            m_employeeRepository = employeeRepository;

            m_id = id;
            m_firstName = firstName;
            m_lastName = lastName;
            DateOfBirth = dateOfBirth;
            Department = deparment;
            m_isDirty = false;

            ApplyChangesCommand = new RelayCommand(ApplyChangesExecute, null);
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
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                DepartmentId = this.Department.Id
            };
            m_employeeRepository.UpdateEmployee(updatedEmployee);

            IsDirty = false;
        }
    }
}

