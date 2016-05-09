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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Directly using the Department like this seems wrong.
            // We need to support validation.
            Department storedDepartment = m_unitOfWork.Departments.Get(id);
            return View(storedDepartment);
        }

        [HttpPost]
        public ActionResult Edit(Department editedDepartment)
        {
            if(editedDepartment.Id == 0)
            {
                Department newDepartment = new Department
                {
                    Name = editedDepartment.Name
                };
                m_unitOfWork.Departments.Add(newDepartment);
            }
            else
            {
                Department storedDepartment = m_unitOfWork.Departments.Get(editedDepartment.Id);
                storedDepartment.Name = editedDepartment.Name;
            }

            m_unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            Department newDepartment = new Department
            {
                Name = "New Department"
            };

            return View("Edit", newDepartment);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Department storedDepartment = m_unitOfWork.Departments.Get(id);
            m_unitOfWork.Departments.Remove(storedDepartment);
            m_unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}