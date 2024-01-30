using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

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

        public async Task<Employee> GetEmployee(long companyId, long id, bool trackChanges)
        {
            return await FindByCondition(e => e.Id.Equals(id) && e.CompanyId.Equals(companyId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<PagedList<Employee>> GetEmployees(long companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges: false)
                        .Filter(employeeParameters.MinAge, employeeParameters.MaxAge)
                        .Search(employeeParameters.SearchTerm)
                        .Sort(employeeParameters.OrderBy)
                        .OrderBy(e => e.Name)
                        .Skip(employeeParameters.PageNumber - 1 * employeeParameters.PageSize)
                        .Take(employeeParameters.PageSize)
                        .ToListAsync();

            var count = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges: false).CountAsync();
            return new PagedList<Employee>(employees, employeeParameters.PageNumber, employeeParameters.PageSize, count);
        }
    }
}
