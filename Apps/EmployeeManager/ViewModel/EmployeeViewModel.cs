using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Flagstone.Data.Employees;
using Flagstone.WPF;

namespace EmployeeManager.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        private readonly IUnitOfWorkFactory m_unitOfWorkFactory;

        public const long InvalidEmployeeId = -1;

        private long m_employeeId;
        private string m_firstName;
        private string m_lastName;
        private DateTime m_dateOfBirth;
        private DepartmentViewModel m_department;

        private bool m_isDirty;

        public long EmployeeId
        {
            get { return m_employeeId; }
            set
            {
                if (value != m_employeeId)
                {
                    m_employeeId = value;
                    OnPropertyChanged(nameof(EmployeeId));
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
                    OnPropertyChanged(nameof(FirstName));
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
                    OnPropertyChanged(nameof(LastName));
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
                    OnPropertyChanged(nameof(DateOfBirth));
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
                    OnPropertyChanged(nameof(Department));
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
                OnPropertyChanged(nameof(IsDirty));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public EmployeeViewModel(IUnitOfWorkFactory unitOfWorkFactory, DepartmentViewModel deparment) : 
            this(
                unitOfWorkFactory,
                InvalidEmployeeId,
                "New",
                "Employee",
                DateTime.Now,
                deparment
            )
        {
            m_isDirty = true;
        }

        public EmployeeViewModel(IUnitOfWorkFactory unitOfWorkFactory, long employeeId, string firstName, string lastName, DateTime dateOfBirth, DepartmentViewModel deparment)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));

            m_unitOfWorkFactory = unitOfWorkFactory;

            m_employeeId = employeeId;
            m_firstName = firstName;
            m_lastName = lastName;
            DateOfBirth = dateOfBirth;
            m_department = deparment;
            m_isDirty = false;

            ApplyChangesCommand = new RelayCommand(ApplyChangesExecute, ApplyChangesCanExecute);
        }

        public ICommand ApplyChangesCommand
        {
            get;
            private set;
        }

        private bool ApplyChangesCanExecute(object parameter)
        {
            return IsDirty;
        }

        private void ApplyChangesExecute(object parameter)
        {
            if (this.EmployeeId == InvalidEmployeeId) {
                Employee newEmployee = new Employee
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    DateOfBirth = this.DateOfBirth,
                    DepartmentId = this.Department.DepartmentId
                };
                using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
                {
                    unitOfWork.Employees.Add(newEmployee);
                    this.EmployeeId = newEmployee.EmployeeId;
                    unitOfWork.Complete();
                }
            }
            else
            {
                using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
                {
                    Employee storedEmployee = unitOfWork.Employees.Get(this.EmployeeId);
                    storedEmployee.FirstName = this.FirstName;
                    storedEmployee.LastName = this.LastName;
                    storedEmployee.DateOfBirth = this.DateOfBirth;
                    storedEmployee.DepartmentId = this.Department.DepartmentId;
                    unitOfWork.Complete();
                }
            }

            IsDirty = false;
        }
    }
}

