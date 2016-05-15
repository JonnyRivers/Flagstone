using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Flagstone.Data.Employees;

namespace EmployeeManagerWebApi.Controllers
{
    public class DepartmentController : ApiController
    {
        private IUnitOfWorkFactory m_unitOfWorkFactory;

        public DepartmentController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));

            m_unitOfWorkFactory = unitOfWorkFactory;
        }

        public IEnumerable<Models.Department> Get()
        {
            using (IUnitOfWork unitofWork = m_unitOfWorkFactory.Create()) {
                return unitofWork.Departments.GetAll().Select(DbEntityToApiEntity);
            }
        }

        public Models.Department Get(long id)
        {
            using (IUnitOfWork unitofWork = m_unitOfWorkFactory.Create()) {
                return DbEntityToApiEntity(unitofWork.Departments.Get(id));
            }
        }

        private static Models.Department DbEntityToApiEntity(Flagstone.Data.Employees.Department dbEntity)
        {
            return new Models.Department
            {
                DepartmentId = dbEntity.DepartmentId,
                Name = dbEntity.Name
            };
        }
    }
}
