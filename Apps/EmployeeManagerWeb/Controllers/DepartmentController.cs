using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EmployeeManagerWeb.ViewModels;

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

        public ActionResult Create()
        {
            var viewModel = new EditDepartmentViewModel
            {
                DepartmentId = 0,
                Name = "New Department"
            };

            return View("Edit", viewModel);
        }

        public ActionResult Index()
        {
            return View(m_unitOfWork.Departments.GetAll());
        }

        public ActionResult Edit(int departmentId)
        {
            Department storedDepartment = m_unitOfWork.Departments.Get(departmentId);

            var viewModel = new EditDepartmentViewModel
            {
                DepartmentId = storedDepartment.DepartmentId,
                Name = storedDepartment.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditDepartmentViewModel viewModel)
        {
            if(viewModel.DepartmentId == 0)
            {
                Department newDepartment = new Department
                {
                    Name = viewModel.Name
                };
                m_unitOfWork.Departments.Add(newDepartment);
            }
            else
            {
                Department storedDepartment = m_unitOfWork.Departments.Get(viewModel.DepartmentId);
                storedDepartment.Name = viewModel.Name;
            }

            m_unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int departmentId)
        {
            Department storedDepartment = m_unitOfWork.Departments.Get(departmentId);
            m_unitOfWork.Departments.Remove(storedDepartment);
            m_unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}