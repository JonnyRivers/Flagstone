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
    public class EmployeeListViewModel : ViewModelBase
    {
        private readonly IDepartmentRepository m_departmentRepository;
        private readonly IEmployeeRepository m_employeeRepository;

        private EmployeeViewModel m_selectedEmployee;

        public ObservableCollection<DepartmentViewModel> AllDepartments
        {
            get;
            private set;
        }

        public ObservableCollection<EmployeeViewModel> AllEmployees
        {
            get;
            private set;
        }

        public EmployeeViewModel SelectedEmployee
        {
            get { return m_selectedEmployee; }
            set
            {
                if (m_selectedEmployee != value)
                {
                    m_selectedEmployee = value;
                    OnPropertyChanged("SelectedEmployee");
                    OnPropertyChanged("IsSelectionValid");
                    // TODO - this seems heavy handed
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public bool IsSelectionValid
        {
            get { return SelectedEmployee != null; }
        }

        public ICommand AddEmployeeCommand
        {
            get;
            private set;
        }

        public ICommand DeleteEmployeeCommand
        {
            get;
            private set;
        }

        private void AddEmployeeExecute(object parameter)
        {
            var newEmployee = new EmployeeViewModel(m_employeeRepository, AllDepartments.First());

            AllEmployees.Add(newEmployee);

            SelectedEmployee = newEmployee;
        }

        private void DeleteEmployeeExecute(object parameter)
        {
            // update model
            m_employeeRepository.DeleteEmployee(SelectedEmployee.Id);

            // update viewmodel
            AllEmployees.Remove(SelectedEmployee);
            SelectedEmployee = null;
        }

        private bool DeleteEmployeeCanExecute(object parameter)
        {
            return IsSelectionValid;
        }

        public EmployeeListViewModel(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            if (departmentRepository == null)
                throw new ArgumentNullException("departmentRepository");

            if (employeeRepository == null)
                throw new ArgumentNullException("employeeRepository");

            m_departmentRepository = departmentRepository;
            m_employeeRepository = employeeRepository;

            AllDepartments = new ObservableCollection<DepartmentViewModel>(
                m_departmentRepository.GetAll().Select(
                    department => new DepartmentViewModel(
                        department.Id, 
                        department.Name
                    )
                )
            );

            AllEmployees = new ObservableCollection<EmployeeViewModel>(
                m_employeeRepository.GetAll().Select(
                    employee => new EmployeeViewModel(
                        m_employeeRepository, 
                        employee.Id, 
                        employee.FirstName, 
                        employee.LastName, 
                        employee.DateOfBirth, AllDepartments.First(d => d.Id == employee.DepartmentId)
                    )
                )
            );

            SelectedEmployee = null;

            AddEmployeeCommand = new RelayCommand(AddEmployeeExecute, null);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployeeExecute, DeleteEmployeeCanExecute);
        }
    }
}
