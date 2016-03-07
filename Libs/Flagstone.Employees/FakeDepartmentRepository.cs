using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public class FakeDepartmentRepository : IDepartmentRepository, IDisposable
    {
        private Department[] m_departments;

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
                FirstName = "Jonny",
                LastName = "Rivers",
                DateOfBirth = new DateTime(1979, 8, 16),
                DepartmentId = toolsDepartment.Id,
                Department = toolsDepartment
            };
            var brendenBooth = new Employee()
            {
                FirstName = "Brenden",
                LastName = "Booth",
                DateOfBirth = new DateTime(1991, 4, 9),
                DepartmentId = toolsDepartment.Id,
                Department = toolsDepartment
            };
            var alanWolfe = new Employee()
            {
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

            m_departments = new Department[]
            {
                toolsDepartment,
                engineDepartment
            };
        }

        public IEnumerable<Department> GetAll()
        {
            return m_departments.ToArray();
        }

        public void Dispose()
        {
            
        }
    }
}
