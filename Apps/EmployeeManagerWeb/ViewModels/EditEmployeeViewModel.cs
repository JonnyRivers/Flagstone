﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Flagstone.Employees;


namespace EmployeeManagerWeb.ViewModels
{
    public class EditEmployeeViewModel
    {
        // Used to reconstruct view model from HTML (V->VM)
        public EditEmployeeViewModel()
        {
        }

        // Used to construct view model from model (M->VM).  Should this be here?
        public EditEmployeeViewModel(Employee employee, IEnumerable<Department> departments)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;

            Departments = departments.Select(
                x => new SelectListItem {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }
            );
        }

        [HiddenInput(DisplayValue=false)]
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        

        public IEnumerable<SelectListItem> Departments { get; set; }
    }
}