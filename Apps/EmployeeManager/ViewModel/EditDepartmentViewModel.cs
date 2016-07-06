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
    public class EditDepartmentViewModel : ViewModelBase
    {
        private readonly IUnitOfWorkFactory m_unitOfWorkFactory;

        public const long InvalidDepartmentId = -1;

        private long m_departmentId;
        private string m_name;

        public long DepartmentId
        {
            get { return m_departmentId; }
            set
            {
                if (value != m_departmentId)
                {
                    m_departmentId = value;
                    OnPropertyChanged(nameof(DepartmentId));
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
                }
            }
        }

        public EditDepartmentViewModel(IUnitOfWorkFactory unitOfWorkFactory)
            : this(unitOfWorkFactory, InvalidDepartmentId, "New Department")
        {
            
        }

        public EditDepartmentViewModel(IUnitOfWorkFactory unitOfWorkFactory, long departmentId, string name)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));

            m_unitOfWorkFactory = unitOfWorkFactory;

            DepartmentId = departmentId;
            Name = name;
        }
    }
}
