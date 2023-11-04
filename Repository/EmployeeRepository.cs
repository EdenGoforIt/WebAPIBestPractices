using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities.Models;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context)
        {
        }

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
        {
            return FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).OrderBy(e => e.Name);
        }
    }
}
