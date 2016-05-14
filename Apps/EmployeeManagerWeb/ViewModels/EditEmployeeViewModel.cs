using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel.DataAnnotations;

using Flagstone.Data.Employees;


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
            EmployeeId = employee.EmployeeId;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            DateOfBirth = employee.DateOfBirth;
            SelectedDepartmentId = employee.DepartmentId;

            DepartmentItems = departments.Select(
                x => new SelectListItem {
                    Value = x.DepartmentId.ToString(),
                    Text = x.Name
                }
            );
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