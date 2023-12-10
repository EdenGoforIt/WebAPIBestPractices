using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges)
        {
            return await FindAll(trackChanges).Include(x => x.Employees).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetByIds(IEnumerable<long> ids, bool trackChanges)
        {
            return await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public async Task<Company> GetCompany(long id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(id), trackChanges).Include(x => x.Employees).SingleOrDefaultAsync();
        }
    }
}
