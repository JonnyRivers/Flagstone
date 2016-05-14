using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagerWeb.ViewModels
{
    public class EditDepartmentViewModel
    {
        public EditDepartmentViewModel()
        {
        }

        [HiddenInput(DisplayValue = false)]
        public long DepartmentId { get; set; }

        public string Name { get; set; }
    }
}