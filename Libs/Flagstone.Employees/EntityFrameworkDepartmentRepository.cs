using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public class EntityFrameworkDepartmentRepository : IDepartmentRepository, IDisposable
    {
        private Entities m_dbContext;

        public EntityFrameworkDepartmentRepository()
        {
            m_dbContext = new Entities();
        }

        public IEnumerable<Department> GetAll()
        {
            return m_dbContext.Departments.ToArray();
        }

        public long AddDepartment(Department department)
        {
            m_dbContext.Departments.Add(department);
            m_dbContext.SaveChanges();

            return department.Id;
        }

        public void DeleteDepartment(long departmentId)
        {
            Department storedDepartment = m_dbContext.Departments.First(e => e.Id == departmentId);
            m_dbContext.Departments.Remove(storedDepartment);

            m_dbContext.SaveChanges();
        }

        public void UpdateDepartment(Department department)
        {
            Department storedDepartment = m_dbContext.Departments.First(e => e.Id == department.Id);
            storedDepartment.Name = department.Name;

            m_dbContext.SaveChanges();
        }

        public void Dispose()
        {
            m_dbContext.Dispose();
        }
    }
}

