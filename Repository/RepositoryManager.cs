using System.Threading.Tasks;
using Contracts;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;

        public ICompanyRepository Company => _companyRepository ??= new CompanyRepository(_repositoryContext);

        public IEmployeeRepository Employe => _employeeRepository ??= new EmployeeRepository(_repositoryContext);

        public RepositoryManager(RepositoryContext repository)
        {
            _repositoryContext = repository;
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
