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
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public EmployeeViewModel(IUnitOfWorkFactory unitOfWorkFactory, DepartmentViewModel deparment) : 
            this(
                unitOfWorkFactory,
                c_invalidId,
                "New",
                "Employee",
                DateTime.Now,
                deparment
            )
        {
            m_isDirty = true;
        }

        public EmployeeViewModel(IUnitOfWorkFactory unitOfWorkFactory, long id, string firstName, string lastName, DateTime dateOfBirth, DepartmentViewModel deparment)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException("unitOfWorkFactory");

            m_unitOfWorkFactory = unitOfWorkFactory;

            m_id = id;
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
            if (this.Id == c_invalidId) {
                Employee newEmployee = new Employee
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    DateOfBirth = this.DateOfBirth,
                    DepartmentId = this.Department.Id
                };
                using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
                {
                    unitOfWork.Employees.Add(newEmployee);
                    this.Id = newEmployee.DepartmentId;
                    unitOfWork.Complete();
                }
            }
            else
            {
                using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
                {
                    Employee storedEmployee = unitOfWork.Employees.Get(this.Id);
                    storedEmployee.FirstName = this.FirstName;
                    storedEmployee.LastName = this.LastName;
                    storedEmployee.DateOfBirth = this.DateOfBirth;
                    storedEmployee.DepartmentId = this.Department.Id;
                    unitOfWork.Complete();
                }
            }

            IsDirty = false;
        }
    }
}

