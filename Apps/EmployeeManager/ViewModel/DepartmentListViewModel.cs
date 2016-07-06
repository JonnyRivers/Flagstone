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
    public class DepartmentListViewModel : ViewModelBase
    {
        private readonly IUnitOfWorkFactory m_unitOfWorkFactory;

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
                    if(m_selectedDepartment != null)
                        m_selectedDepartment.PropertyChanged -= OnSelectedDepartmentChanged;

                    m_selectedDepartment = value;
                    if (m_selectedDepartment != null)
                        m_selectedDepartment.PropertyChanged += OnSelectedDepartmentChanged;

                    OnPropertyChanged(nameof(SelectedDepartment));
                    OnPropertyChanged(nameof(IsSelectionValid));
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        private void OnSelectedDepartmentChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(DepartmentViewModel.DepartmentId))
                CommandManager.InvalidateRequerySuggested();
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

        public ICommand EditDepartmentCommand
        {
            get;
            private set;
        }

        private void AddDepartmentExecute(object parameter)
        {
            var editDepartmentViewModel = new EditDepartmentViewModel(m_unitOfWorkFactory);
            var editView = new View.EditDepartmentView
            {
                DataContext = editDepartmentViewModel
            };

            // This seems nuts - we are inspecting the view model
            bool? result = editView.ShowDialog();

            if (result.HasValue && result.Value)
            {
                // Update model
                Department newDepartment = new Department
                {
                    Name = editDepartmentViewModel.Name
                };
                using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
                {
                    unitOfWork.Departments.Add(newDepartment);
                    unitOfWork.Complete();
                }

                // Update view model
                var newViewModel = new DepartmentViewModel(m_unitOfWorkFactory, newDepartment.DepartmentId, newDepartment.Name);
                AllDepartments.Add(newViewModel);
                SelectedDepartment = newViewModel;
            }
        }

        private void DeleteDepartmentExecute(object parameter)
        {
            // update model
            using(IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create()) {
                Department storedDepartment = unitOfWork.Departments.Get(SelectedDepartment.DepartmentId);
                unitOfWork.Departments.Remove(storedDepartment);
                unitOfWork.Complete();
            }

            // update viewmodel
            AllDepartments.Remove(SelectedDepartment);
            SelectedDepartment = null;
        }

        private bool DeleteDepartmentCanExecute(object parameter)
        {
            return (SelectedDepartment != null && SelectedDepartment.DepartmentId != DepartmentViewModel.InvalidDepartmentId);
        }

        private void EditDepartmentExecute(object parameter)
        {
            var editDepartmentViewModel = new EditDepartmentViewModel(m_unitOfWorkFactory, SelectedDepartment.DepartmentId, SelectedDepartment.Name);
            var editView = new View.EditDepartmentView {
                DataContext = editDepartmentViewModel
            };

            // This seems nuts - we are inspecting the view model
            bool? result = editView.ShowDialog();

            if(result.HasValue && result.Value)
            {
                // Update model
                using (IUnitOfWork unitOfWork = m_unitOfWorkFactory.Create())
                {
                    Department storedDepartment = unitOfWork.Departments.Get(editDepartmentViewModel.DepartmentId);
                    storedDepartment.Name = editDepartmentViewModel.Name;
                    unitOfWork.Complete();
                }

                // Update view model
                SelectedDepartment.Name = editDepartmentViewModel.Name;
            }
        }

        private bool EditDepartmentCanExecute(object parameter)
        {
            return (SelectedDepartment != null && SelectedDepartment.DepartmentId != DepartmentViewModel.InvalidDepartmentId);
        }

        public DepartmentListViewModel(IUnitOfWorkFactory unitOfWorkFactory)
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
            }

            SelectedDepartment = null;

            AddDepartmentCommand = new RelayCommand(AddDepartmentExecute, null);
            DeleteDepartmentCommand = new RelayCommand(DeleteDepartmentExecute, DeleteDepartmentCanExecute);
            EditDepartmentCommand = new RelayCommand(EditDepartmentExecute, EditDepartmentCanExecute);
        }
    }
}
