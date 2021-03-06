﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public class EntityFrameworkEmployeeRepository : IEmployeeRepository, IDisposable
    {
        private Entities m_dbContext;

        public EntityFrameworkEmployeeRepository()
        {
            m_dbContext = new Entities();
        }

        public IEnumerable<Employee> GetAll()
        {
            return m_dbContext.Employees.ToArray();
        }

        public IEnumerable<Employee> GetAllByDepartmentName(string departmentName)
        {
            return m_dbContext.Employees.Where(e => e.Department.Name == departmentName).ToArray();
        }

        public Employee Get(long employeeId)
        {
            Employee storedEmployee = m_dbContext.Employees.First(e => e.Id == employeeId);

            return storedEmployee;
        }

        public long AddEmployee(Employee employee)
        {
            m_dbContext.Employees.Add(employee);
            m_dbContext.SaveChanges();

            return employee.Id;
        }

        public void DeleteEmployee(long employeeId)
        {
            Employee storedEmployee = m_dbContext.Employees.First(e => e.Id == employeeId);
            m_dbContext.Employees.Remove(storedEmployee);

            m_dbContext.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            Employee storedEmployee = m_dbContext.Employees.First(e => e.Id == employee.Id);
            storedEmployee.FirstName = employee.FirstName;
            storedEmployee.LastName = employee.LastName;
            storedEmployee.DateOfBirth = employee.DateOfBirth;
            storedEmployee.DepartmentId = employee.DepartmentId;

            m_dbContext.SaveChanges();
        }

        public void Dispose()
        {
            m_dbContext.Dispose();
        }
    }
}
