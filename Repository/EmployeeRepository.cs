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

        public void CreateEmploye(long companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public Employee GetEmployee(long companyId, long id, bool trackChanges)
        {
            return FindByCondition(e => e.Id.Equals(id) && e.CompanyId.Equals(companyId), trackChanges).SingleOrDefault();
        }

        public IEnumerable<Employee> GetEmployees(long companyId, bool trackChanges)
        {
            return FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).OrderBy(e => e.Name);
        }
    }
}
