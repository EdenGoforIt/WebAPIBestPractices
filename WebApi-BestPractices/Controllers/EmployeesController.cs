using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_BestPractices.Controllers
{
	[Route("api/companies/{companyId}/employees")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{

		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;

		public EmployeesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetEmployeesForCompany(Guid companyId)
		{
			var company = _repository.Company.GetCompany(companyId, trackChanges: false);

			if (company is null)
			{
				_logger.LogInfo($"Company with id: ${companyId} doesn't exist");

				return NotFound();
			}

			var employees = _repository.Employe.GetEmployees(companyId, trackChanges: false);

			var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

			return Ok(employeesDto);
		}

	}
}