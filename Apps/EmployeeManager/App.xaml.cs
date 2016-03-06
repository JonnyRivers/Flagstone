using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = new View.MainWindow();
            var employeeListViewModel = new ViewModel.EmployeeListViewModel();
            var mainWindowViewModel = new ViewModel.MainWindowViewModel(employeeListViewModel);
            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }
    }
}
