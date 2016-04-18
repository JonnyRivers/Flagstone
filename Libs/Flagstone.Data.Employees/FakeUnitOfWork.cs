using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data.Employees
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public FakeUnitOfWork()
        {
            Departments = new FakeDepartmentRepository();
            Employees = new FakeEmployeeRepository();

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

            Departments.Add(toolsDepartment);
            Departments.Add(engineDepartment);

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

            Employees.Add(jonnyRivers);
            Employees.Add(brendenBooth);
            Employees.Add(alanWolfe);
        }

        public IDepartmentRepository Departments { get; }
        public IEmployeeRepository Employees { get; }

        public void Complete()
        {
            // JWR - this makes the fake rather shoddy as nothing persists
            // We would need some static, thread-safe storage to fix this.
        }

        public void Dispose()
        {
            
        }
    }
}
