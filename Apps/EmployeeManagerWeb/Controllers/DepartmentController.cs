using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Flagstone.Data.Employees;

namespace EmployeeManagerWeb.Controllers
{
    public class DepartmentController : Controller
    {
        private IUnitOfWork m_unitOfWork;

        public DepartmentController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            m_unitOfWork = unitOfWorkFactory.Create();
        }

        protected override void Dispose(bool disposing)
        {
            m_unitOfWork.Dispose();

            base.Dispose(disposing);
        }

        // GET: Department
        public ActionResult Index()
        {
            return View(m_unitOfWork.Departments.GetAll());
        }
    }
}