using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi_BestPractices.Mappers
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress, opt => opt.MapFrom<FullAddressResolver>());
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
        }
    }

    public class FullAddressResolver : IValueResolver<Company, CompanyDto, string>
    {
        public string Resolve(Company source, CompanyDto destination, string destMember, ResolutionContext context)
        {
            return $"{source.Address} {source.Country}";
        }
    }
}