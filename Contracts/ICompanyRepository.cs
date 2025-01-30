using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges);
        Task<Company> GetCompany(long id, bool trackChanges);
        void CreateCompany(Company company);
        Task<IEnumerable<Company>> GetByIds(IEnumerable<long> ids, bool trackChanges);
        void DeleteCompany(Company company);
    }
}