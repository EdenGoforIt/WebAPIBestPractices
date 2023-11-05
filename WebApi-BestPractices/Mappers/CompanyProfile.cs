using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi_BestPractices.Mappers
{
	public class CompanyProfile : Profile
	{
		public CompanyProfile()
		{
			CreateMap<Company, CompanyDto>().ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

			CreateMap<Employee, EmployeeDto>();
			CreateMap<CompanyForCreationDto, Company>();
		}
	}
}