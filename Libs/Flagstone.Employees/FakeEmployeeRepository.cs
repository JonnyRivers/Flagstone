using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public class FakeEmployeeRepository : IEmployeeRepository, IDisposable
    {
        private Department[] m_departments;
        private Dictionary<long, Employee> m_employees;

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

            m_departments = new Department[]
            {
                toolsDepartment,
                engineDepartment
            };

            m_employees = new Dictionary<long, Employee>()
            {
                {jonnyRivers.Id, jonnyRivers},
                {brendenBooth.Id, brendenBooth},
                {alanWolfe.Id, alanWolfe}
            };
        }

        public IEnumerable<Employee> GetAll()
        {
            return m_employees.Values.ToArray();
        }

        public IEnumerable<Employee> GetByDepartmentName(string departmentName)
        {
            return m_employees.Values.Where(e => e.Department.Name == departmentName).ToArray();
        }

        public long AddEmployee(Employee employee)
        {
            Employee newEmployee = new Employee()
            {
                Id = m_employees.Keys.Max() + 1,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                DepartmentId = employee.DepartmentId,
            };

            m_employees.Add(newEmployee.Id, newEmployee);

            return newEmployee.Id;
        }

        public void DeleteEmployee(long employeeId)
        {
            m_employees.Remove(employeeId);
        }

        public void UpdateEmployee(Employee employee)
        {
            Employee storedEmployee = m_employees[employee.Id];
            storedEmployee.FirstName = employee.FirstName;
            storedEmployee.LastName = employee.LastName;
            storedEmployee.DateOfBirth = employee.DateOfBirth;
            storedEmployee.DepartmentId = employee.DepartmentId;
            storedEmployee.Department = employee.Department;
        }

        public void Dispose()
        {
            
        }
    }
}
