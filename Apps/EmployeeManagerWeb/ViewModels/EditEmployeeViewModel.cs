using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Flagstone.Data.Employees;


namespace EmployeeManagerWeb.ViewModels
{
    public class EditEmployeeViewModel
    {
        public EditEmployeeViewModel()
        {
        }

        [HiddenInput(DisplayValue=false)]
        public long EmployeeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public long SelectedDepartmentId { get; set; }
        

        public IEnumerable<SelectListItem> DepartmentItems { get; set; }
    }
}