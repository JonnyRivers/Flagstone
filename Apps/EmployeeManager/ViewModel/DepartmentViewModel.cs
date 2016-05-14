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

        private const long c_invalidId = -1;

        private long m_id;
        private string m_name;

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
        public string Name
        {
            get { return m_name; }
            set
            {
                if (value != m_name)
                {
                    m_name = value;
                    OnPropertyChanged("Name");
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
                // TODO - this seems heavy handed
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public DepartmentViewModel(IUnitOfWorkFactory unitOfWorkFactory)
            : this(unitOfWorkFactory, c_invalidId, "New Department")
        {
            m_isDirty = true;
        }

        public DepartmentViewModel(IUnitOfWorkFactory unitOfWorkFactory, long id, string name)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException("unitOfWorkFactory");

            m_unitOfWorkFactory = unitOfWorkFactory;

            Id = id;
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
            if (this.Id == c_invalidId)
            {
                Department newDepartment = new Department
                {
                    Name = this.Name
                };
                using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
                {
                    unitOfWork.Departments.Add(newDepartment);
                    this.Id = newDepartment.DepartmentId;
                    unitOfWork.Complete();
                }
            }
            else
            {
                using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
                {
                    Department storedDepartment = unitOfWork.Departments.Get(this.Id);
                    storedDepartment.Name = this.Name;
                    unitOfWork.Complete();
                }
            }

            IsDirty = false;
        }
    }
}
