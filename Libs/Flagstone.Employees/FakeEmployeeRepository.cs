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

            var jonnyRivers = new Employee()
            {
                FirstName = "Jonny",
                LastName = "Rivers",
                DateOfBirth = new DateTime(1979, 8, 16),
                DepartmentId = toolsDepartment.Id,
                Department = toolsDepartment
            };

            toolsDepartment.Employees = new Employee[]
            {
                jonnyRivers
            };

            m_departments = new Department[]
            {
                toolsDepartment
            };

            m_employees = new Employee[]
            {
                jonnyRivers
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
