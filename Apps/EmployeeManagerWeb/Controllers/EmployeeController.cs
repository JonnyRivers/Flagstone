using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Flagstone.Data.Employees;

namespace EmployeeManagerWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private IUnitOfWork m_unitOfWork;

        public EmployeeController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            m_unitOfWork = unitOfWorkFactory.Create();
        }

        protected override void Dispose(bool disposing)
        {
            m_unitOfWork.Dispose();

            base.Dispose(disposing);
        }

        public ActionResult Create()
        {
            Employee newEmployee = new Employee();
            IEnumerable<Department> departments = m_unitOfWork.Departments.GetAll();
            var viewModel = new ViewModels.EditEmployeeViewModel
            {
                EmployeeId = 0,
                FirstName = "New",
                LastName = "Employee",
                DateOfBirth = DateTime.Today,
                SelectedDepartmentId = departments.First().DepartmentId,
                DepartmentItems = departments.Select(
                    x => new SelectListItem
                    {
                        Value = x.DepartmentId.ToString(),
                        Text = x.Name
                    }
                )
            };
            return View("Edit", viewModel);
        }

        public ActionResult Index()
        {
            return View(m_unitOfWork.Employees.GetAll());
        }

        public ActionResult Edit(int employeeId)
        {
            Employee storedEmployee = m_unitOfWork.Employees.Get(employeeId);
            IEnumerable<Department> departments = m_unitOfWork.Departments.GetAll();
            var viewModel = new ViewModels.EditEmployeeViewModel
            {
                EmployeeId = storedEmployee.EmployeeId,
                FirstName = storedEmployee.FirstName,
                LastName = storedEmployee.LastName,
                DateOfBirth = storedEmployee.DateOfBirth,
                SelectedDepartmentId = storedEmployee.DepartmentId,
                DepartmentItems = departments.Select(
                    x => new SelectListItem
                    {
                        Value = x.DepartmentId.ToString(),
                        Text = x.Name
                    }
                )
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.EditEmployeeViewModel viewModel)
        {
            if(viewModel.EmployeeId == 0)
            {
                Employee newEmployee = new Employee {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    DateOfBirth = viewModel.DateOfBirth,
                    DepartmentId = viewModel.SelectedDepartmentId
                };
                m_unitOfWork.Employees.Add(newEmployee);
            }
            else
            {
                Employee storedEmployee = m_unitOfWork.Employees.Get(viewModel.EmployeeId);
                storedEmployee.FirstName = viewModel.FirstName;
                storedEmployee.LastName = viewModel.LastName;
                storedEmployee.DateOfBirth = viewModel.DateOfBirth;
                storedEmployee.DepartmentId = viewModel.SelectedDepartmentId;
            }
            
            m_unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int employeeId)
        {
            Employee storedEmployee = m_unitOfWork.Employees.Get(employeeId);
            m_unitOfWork.Employees.Remove(storedEmployee);
            m_unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}