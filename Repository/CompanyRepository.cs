﻿using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities.Models;

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

        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Company> GetByIds(IEnumerable<long> ids, bool trackChanges)
        {
            return FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
        }

        public Company GetCompany(long id, bool trackChanges)
        {
            return FindByCondition(x => x.Id.Equals(id), trackChanges).SingleOrDefault();
        }
    }
}
