using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Flagstone.Employees;

namespace EmployeeManagerWeb.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentRepository m_departmentRepository;

        public DepartmentController()
        {
            m_departmentRepository = new FakeDepartmentRepository();
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View(m_departmentRepository.GetAll());
        }
    }
}