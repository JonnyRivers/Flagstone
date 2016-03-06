﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Employees
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetByDepartmentName(string departmentName);
    }
}