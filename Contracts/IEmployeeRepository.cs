using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(long companyId, bool trackChanges);
        Employee GetEmployee(long companyId, long id, bool trackChanges);
        void CreateEmploye(long companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
