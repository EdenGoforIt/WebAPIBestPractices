using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(long companyId, bool trackChanges);
        Employee GetEmployee(long company, long id, bool trackChanges);
        void CreateEmploye(long companyId, Employee employee);
    }
}
