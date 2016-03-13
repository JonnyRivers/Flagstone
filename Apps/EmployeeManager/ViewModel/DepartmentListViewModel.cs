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
    public class DepartmentListViewModel : ViewModelBase
    {
        private readonly IDepartmentRepository m_departmentRepository;

        private DepartmentViewModel m_selectedDepartment;

        public ObservableCollection<DepartmentViewModel> AllDepartments
        {
            get;
            private set;
        }

        public DepartmentViewModel SelectedDepartment
        {
            get { return m_selectedDepartment; }
            set
            {
                if (m_selectedDepartment != value)
                {
                    m_selectedDepartment = value;
                    OnPropertyChanged("SelectedDepartment");
                    OnPropertyChanged("IsSelectionValid");
                    // TODO - this seems heavy handed
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public bool IsSelectionValid
        {
            get { return SelectedDepartment != null; }
        }

        public ICommand AddDepartmentCommand
        {
            get;
            private set;
        }

        public ICommand DeleteDepartmentCommand
        {
            get;
            private set;
        }

        private void AddDepartmentExecute(object parameter)
        {
            var newDepartment = new DepartmentViewModel(m_departmentRepository);

            AllDepartments.Add(newDepartment);

            SelectedDepartment = newDepartment;
        }

        private void DeleteDepartmentExecute(object parameter)
        {
            // update model
            m_departmentRepository.DeleteDepartment(SelectedDepartment.Id);

            // update viewmodel
            AllDepartments.Remove(SelectedDepartment);
            SelectedDepartment = null;
        }

        private bool DeleteDepartmentCanExecute(object parameter)
        {
            return IsSelectionValid;
        }

        public DepartmentListViewModel(IDepartmentRepository departmentRepository)
        {
            m_departmentRepository = departmentRepository;

            AllDepartments = new ObservableCollection<DepartmentViewModel>(
                m_departmentRepository.GetAll().Select(
                    department => new DepartmentViewModel(
                        m_departmentRepository,
                        department.Id,
                        department.Name
                    )
                )
            );

            SelectedDepartment = null;

            AddDepartmentCommand = new RelayCommand(AddDepartmentExecute, null);
            DeleteDepartmentCommand = new RelayCommand(DeleteDepartmentExecute, DeleteDepartmentCanExecute);
        }
    }
}
