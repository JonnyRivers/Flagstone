using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data.Employees
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        static FakeUnitOfWork()
        {
            // This isn't thread-safe.  At all.  Is it good enough?
            s_departmentRepository = new FakeDepartmentRepository();
            s_employeeRepository = new FakeEmployeeRepository();

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

            s_departmentRepository.Add(toolsDepartment);
            s_departmentRepository.Add(engineDepartment);

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

            s_employeeRepository.Add(jonnyRivers);
            s_employeeRepository.Add(brendenBooth);
            s_employeeRepository.Add(alanWolfe);
        }

        private static FakeDepartmentRepository s_departmentRepository;
        private static FakeEmployeeRepository s_employeeRepository;

        public FakeUnitOfWork()
        {
            Departments = s_departmentRepository;
            Employees = s_employeeRepository;
        }

        public IDepartmentRepository Departments { get; }
        public IEmployeeRepository Employees { get; }

        public void Complete()
        {
            // We aren't repsecting the pattern.  At all.  Is it good enough?
        }

        public void Dispose()
        {
            
        }
    }
}
