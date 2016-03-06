using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        private Department[] m_departments;
        private Employee[] m_employees;

        public FakeEmployeeRepository()
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

            m_employees = new Employee[]
            {
                jonnyRivers,
                brendenBooth,
                alanWolfe
            };
        }

        public IEnumerable<Employee> GetAll()
        {
            return m_employees.ToArray();
        }

        public IEnumerable<Employee> GetByDepartmentName(string departmentName)
        {
            return m_employees.Where(e => e.Department.Name == departmentName).ToArray();
        }
    }
}
