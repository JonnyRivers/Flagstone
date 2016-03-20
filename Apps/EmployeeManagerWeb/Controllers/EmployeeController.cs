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
        private IEmployeeRepository m_empoyeeRepository;

        public EmployeeController()
        {
            m_empoyeeRepository = new FakeEmployeeRepository();
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View(m_empoyeeRepository.GetAll());
        }
    }
}