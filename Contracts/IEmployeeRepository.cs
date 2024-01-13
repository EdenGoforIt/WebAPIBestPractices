using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<PagedList<Employee>> GetEmployees(long companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<Employee> GetEmployee(long companyId, long id, bool trackChanges);
        void CreateEmploye(long companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
