using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public class FakeDepartmentRepository : IDepartmentRepository, IDisposable
    {
        private Dictionary<long, Department> m_departments;

        public FakeDepartmentRepository()
        {
            var toolsDepartment = new Department()
            {
                Id = 1,
                Name = "Tools"
            };
            var engineDepartment = new Department()
            {
                Id = 2,
                Name = "Engine"
            };

            var jonnyRivers = new Employee()
            {
                Id = 1,
                FirstName = "Jonny",
                LastName = "Rivers",
                DateOfBirth = new DateTime(1979, 8, 16),
                DepartmentId = toolsDepartment.Id,
                Department = toolsDepartment
            };
            var brendenBooth = new Employee()
            {
                Id = 2,
                FirstName = "Brenden",
                LastName = "Booth",
                DateOfBirth = new DateTime(1991, 4, 9),
                DepartmentId = toolsDepartment.Id,
                Department = toolsDepartment
            };
            var alanWolfe = new Employee()
            {
                Id = 3,
                FirstName = "Alan",
                LastName = "Wolfe",
                DateOfBirth = new DateTime(1983, 3, 6),
                DepartmentId = engineDepartment.Id,
                Department = engineDepartment
            };

            toolsDepartment.Employees = new Employee[]
            {
                jonnyRivers,
                brendenBooth
            };
            engineDepartment.Employees = new Employee[]
            {
                alanWolfe
            };

            m_departments = new Dictionary<long, Department>()
            {
                {toolsDepartment.Id, toolsDepartment},
                {engineDepartment.Id, engineDepartment}
            };
        }

        public IEnumerable<Department> GetAll()
        {
            return m_departments.Values.ToArray();
        }
        public long AddDepartment(Department department)
        {
            var newDepartment = new Department()
            {
                Id = m_departments.Keys.Max() + 1,
                Name = department.Name
            };

            m_departments.Add(newDepartment.Id, newDepartment);

            return newDepartment.Id;
        }

        public void DeleteDepartment(long departmentId)
        {
            m_departments.Remove(departmentId);
        }

        public void UpdateDepartment(Department department)
        {
            Department storedDepartment = m_departments[department.Id];
            storedDepartment.Name = department.Name;
        }

        public void Dispose()
        {
            
        }
    }
}
