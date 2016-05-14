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
    public class EmployeeListViewModel : ViewModelBase
    {
        private readonly IUnitOfWorkFactory m_unitOfWorkFactory;

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
                    if (m_selectedEmployee != null)
                        m_selectedEmployee.PropertyChanged -= OnSelectedEmployeeChanged;

                    m_selectedEmployee = value;
                    if (m_selectedEmployee != null)
                        m_selectedEmployee.PropertyChanged += OnSelectedEmployeeChanged;
                    
                    OnPropertyChanged(nameof(SelectedEmployee));
                    OnPropertyChanged(nameof(IsSelectionValid));
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        private void OnSelectedEmployeeChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Id")
                CommandManager.InvalidateRequerySuggested();
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
            var newEmployee = new EmployeeViewModel(m_unitOfWorkFactory, AllDepartments.First());

            AllEmployees.Add(newEmployee);

            SelectedEmployee = newEmployee;
        }

        private void DeleteEmployeeExecute(object parameter)
        {
            // update model
            using(IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
            {
                Employee storedEmployee = unitOfWork.Employees.Get(SelectedEmployee.Id);
                unitOfWork.Employees.Remove(storedEmployee);
                unitOfWork.Complete();
            }

            // update viewmodel
            AllEmployees.Remove(SelectedEmployee);
            SelectedEmployee = null;
        }

        private bool DeleteEmployeeCanExecute(object parameter)
        {
            return (SelectedEmployee != null && SelectedEmployee.Id != -1);
        }

        public EmployeeListViewModel(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));

            m_unitOfWorkFactory = unitOfWorkFactory;

            using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
            {

                AllDepartments = new ObservableCollection<DepartmentViewModel>(
                    unitOfWork.Departments.GetAll().Select(
                        department => new DepartmentViewModel(
                            m_unitOfWorkFactory,
                            department.DepartmentId,
                            department.Name
                        )
                    )
                );

                AllEmployees = new ObservableCollection<EmployeeViewModel>(
                    unitOfWork.Employees.GetAll().Select(
                        employee => new EmployeeViewModel(
                            m_unitOfWorkFactory,
                            employee.EmployeeId,
                            employee.FirstName,
                            employee.LastName,
                            employee.DateOfBirth, 
                            AllDepartments.First(d => d.Id == employee.DepartmentId)
                        )
                    )
                );
            }

            SelectedEmployee = null;

            AddEmployeeCommand = new RelayCommand(AddEmployeeExecute, null);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployeeExecute, DeleteEmployeeCanExecute);
        }
    }
}
