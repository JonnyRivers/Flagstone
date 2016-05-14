using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Flagstone.Data.Employees;
using Flagstone.WPF;


namespace EmployeeManager.ViewModel
{
    public class DepartmentViewModel : ViewModelBase
    {
        private readonly IUnitOfWorkFactory m_unitOfWorkFactory;

        public const long InvalidDepartmentId = -1;

        private long m_departmentId;
        private string m_name;

        private bool m_isDirty;

        public long DepartmentId
        {
            get { return m_departmentId; }
            set
            {
                if (value != m_departmentId)
                {
                    m_departmentId = value;
                    OnPropertyChanged(nameof(DepartmentId));
                    IsDirty = true;
                }
            }
        }
        public string Name
        {
            get { return m_name; }
            set
            {
                if (value != m_name)
                {
                    m_name = value;
                    OnPropertyChanged(nameof(Name));
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

        public DepartmentViewModel(IUnitOfWorkFactory unitOfWorkFactory)
            : this(unitOfWorkFactory, InvalidDepartmentId, "New Department")
        {
            m_isDirty = true;
        }

        public DepartmentViewModel(IUnitOfWorkFactory unitOfWorkFactory, long departmentId, string name)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));

            m_unitOfWorkFactory = unitOfWorkFactory;

            DepartmentId = departmentId;
            Name = name;
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
            if (this.DepartmentId == InvalidDepartmentId)
            {
                Department newDepartment = new Department
                {
                    Name = this.Name
                };
                using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
                {
                    unitOfWork.Departments.Add(newDepartment);
                    this.DepartmentId = newDepartment.DepartmentId;
                    unitOfWork.Complete();
                }
            }
            else
            {
                using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
                {
                    Department storedDepartment = unitOfWork.Departments.Get(this.DepartmentId);
                    storedDepartment.Name = this.Name;
                    unitOfWork.Complete();
                }
            }

            IsDirty = false;
        }
    }
}
