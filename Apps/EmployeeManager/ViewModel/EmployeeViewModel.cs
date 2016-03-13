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

        private const long c_invalidId = -1;

        private long m_id;
        private string m_firstName;
        private string m_lastName;
        private DateTime m_dateOfBirth;
        private DepartmentViewModel m_department;

        private bool m_isDirty;

        public long Id
        {
            get { return m_id; }
            set
            {
                if (value != m_id)
                {
                    m_id = value;
                    OnPropertyChanged("Id");
                    IsDirty = true;
                }
            }
        }
        public string FirstName
        {
            get { return m_firstName; }
            set {
                if (value != m_firstName)
                {
                    m_firstName = value;
                    OnPropertyChanged("FirstName");
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
                    OnPropertyChanged("LastName");
                    IsDirty = true;
                }
            }
        }
        public DateTime DateOfBirth
        {
            get { return m_dateOfBirth; }
            set
            {
                if (value != m_dateOfBirth)
                {
                    m_dateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                    IsDirty = true;
                }
            }
        }
        public DepartmentViewModel Department
        {
            get { return m_department; }
            set
            {
                if (value != m_department)
                {
                    m_department = value;
                    OnPropertyChanged("Department");
                    IsDirty = true;
                }
            }
        }

        public bool IsDirty 
        {
            get { return m_isDirty; }
            private set
            {
                m_isDirty = value;
                OnPropertyChanged("IsDirty");
            }
        }

        public EmployeeViewModel(IEmployeeRepository employeeRepository, DepartmentViewModel deparment)
        {
            m_employeeRepository = employeeRepository;

            m_id = c_invalidId;
            m_firstName = String.Empty;
            m_lastName = String.Empty;
            DateOfBirth = DateTime.Today;
            m_department = deparment;
            m_isDirty = true;

            ApplyChangesCommand = new RelayCommand(ApplyChangesExecute, null);
        }

        public EmployeeViewModel(IEmployeeRepository employeeRepository, long id, string firstName, string lastName, DateTime dateOfBirth, DepartmentViewModel deparment)
        {
            m_employeeRepository = employeeRepository;

            m_id = id;
            m_firstName = firstName;
            m_lastName = lastName;
            DateOfBirth = dateOfBirth;
            m_department = deparment;
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
            if (this.Id == c_invalidId) {
                Employee newEmployee = new Employee
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    DateOfBirth = this.DateOfBirth,
                    DepartmentId = this.Department.Id
                };
                this.Id = m_employeeRepository.AddEmployee(newEmployee);
            }
            else
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
            }

            IsDirty = false;
        }
    }
}

