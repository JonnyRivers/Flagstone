﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeManager.View
{
    /// <summary>
    /// Interaction logic for EditDepartmentView.xaml
    /// </summary>
    public partial class EditDepartmentView : Window
    {
        public EditDepartmentView()
        {
            InitializeComponent();
        }

        // This is unfortunate
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}