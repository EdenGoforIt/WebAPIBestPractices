using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees(long companyId, bool trackChanges);
        Task<Employee> GetEmployee(long companyId, long id, bool trackChanges);
        void CreateEmploye(long companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
