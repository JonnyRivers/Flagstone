using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Flagstone.Employees;
using Flagstone.WPF;

namespace EmployeeManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDepartmentRepository m_departmentRepository;
        private IEmployeeRepository m_employeeRepository;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Compose DAL
            m_departmentRepository = new EntityFrameworkDepartmentRepository();
            m_employeeRepository = new EntityFrameworkEmployeeRepository();

            // Compose view model
            var employeeListViewModel = new ViewModel.EmployeeListViewModel(m_departmentRepository, m_employeeRepository);
            var departmentListViewModel = new ViewModel.DepartmentListViewModel(m_departmentRepository);
            var mainWindowViewModel = new ViewModel.MainWindowViewModel(employeeListViewModel, departmentListViewModel);

            // Compose view
            var mainWindow = new View.MainWindow() {
                DataContext = mainWindowViewModel
            };

            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            m_employeeRepository.Dispose();
            m_departmentRepository.Dispose();
        }
    }
}

