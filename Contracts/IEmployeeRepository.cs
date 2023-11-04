using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
    }
}
