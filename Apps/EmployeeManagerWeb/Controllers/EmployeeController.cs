using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Flagstone.Employees;

namespace EmployeeManagerWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private IDepartmentRepository m_departmentRepository;
        private IEmployeeRepository m_empoyeeRepository;

        public EmployeeController(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            m_departmentRepository = departmentRepository;
            m_empoyeeRepository = employeeRepository;
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View(m_empoyeeRepository.GetAll());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee storedEmployee = m_empoyeeRepository.Get(id);
            IEnumerable<Department> departments = m_departmentRepository.GetAll();
            ViewModels.EditEmployeeViewModel viewModel = new ViewModels.EditEmployeeViewModel(storedEmployee, departments);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.EditEmployeeViewModel viewModel)
        {
            Employee storedEmployee = m_empoyeeRepository.Get(viewModel.Id);
            storedEmployee.FirstName = viewModel.FirstName;
            storedEmployee.LastName = viewModel.LastName;
            m_empoyeeRepository.UpdateEmployee(storedEmployee);

            return RedirectToAction("Index");
        }
    }
}