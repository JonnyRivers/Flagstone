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

        // GET: Employee
        public ActionResult Index()
        {
            return View(m_unitOfWork.Employees.GetAll());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee storedEmployee = m_unitOfWork.Employees.Get(id);
            IEnumerable<Department> departments = m_unitOfWork.Departments.GetAll();
            ViewModels.EditEmployeeViewModel viewModel = new ViewModels.EditEmployeeViewModel(storedEmployee, departments);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.EditEmployeeViewModel viewModel)
        {
            if(viewModel.Id == 0)
            {
                int firstDepartmentId = Int32.Parse(viewModel.Departments.First().Value);

                Employee newEmployee = new Employee {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    DateOfBirth = new DateTime(1970, 1, 1),
                    DepartmentId = firstDepartmentId
                };
                m_unitOfWork.Employees.Add(newEmployee);
            }
            else
            {
                Employee storedEmployee = m_unitOfWork.Employees.Get(viewModel.Id);
                storedEmployee.FirstName = viewModel.FirstName;
                storedEmployee.LastName = viewModel.LastName;
                storedEmployee.DateOfBirth = viewModel.DateOfBirth;
                // TODO fix crash here
                //storedEmployee.DepartmentId = viewModel.Department.Id;
            }
            
            m_unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Employee storedEmployee = m_unitOfWork.Employees.Get(id);
            m_unitOfWork.Employees.Remove(storedEmployee);
            m_unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            Employee newEmployee = new Employee();
            IEnumerable<Department> departments = m_unitOfWork.Departments.GetAll();
            ViewModels.EditEmployeeViewModel viewModel = new ViewModels.EditEmployeeViewModel(newEmployee, departments);
            return View("Edit", viewModel);
        }
    }
}