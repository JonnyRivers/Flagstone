using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Flagstone.Data.Employees;
using Flagstone.WPF;

namespace EmployeeManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IUnitOfWorkFactory m_unitOfWorkFactory;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Compose DAL
#if DEBUG
            m_unitOfWorkFactory = new FakeUnitOfWorkFactory();
#else
            m_unitOfWorkFactory = new EFUnitOfWorkFactory();
#endif

            // Compose view model
            var employeeListViewModel = new ViewModel.EmployeeListViewModel(m_unitOfWorkFactory);
            var departmentListViewModel = new ViewModel.DepartmentListViewModel(m_unitOfWorkFactory);
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
        }
    }
}

