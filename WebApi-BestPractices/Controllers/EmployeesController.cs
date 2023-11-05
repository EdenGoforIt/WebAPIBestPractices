using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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

		[HttpGet("{id}", Name = "EmployeeById")]
		public IActionResult GetEmployee(Guid companyId, Guid id)
		{
			var company = _repository.Company.GetCompany(companyId, trackChanges: false);

			if (company is null)
			{
				_logger.LogInfo($"Company with id: ${companyId} doesn't exist");

				return NotFound();
			}

			var employee = _repository.Employe.GetEmployee(companyId, id, trackChanges: false);

			if (employee is null)
			{
				_logger.LogInfo($"Employee with id: ${id} doesn't exist");

				return NotFound();
			}

			var employeeDto = _mapper.Map<EmployeeDto>(employee);

			return Ok(employeeDto);
		}

		[HttpPost]
		public IActionResult CreateEmployee(Guid companyId, [FromBody] EmployeeForCreationDto employeeDto)
		{
			if (employeeDto is null)
			{
				_logger.LogError("EmployeeDto is null");
				return BadRequest("EmployeeDto is null");
			}

			var employee = _mapper.Map<Employee>(employeeDto);

			var company = _repository.Company.GetCompany(companyId, trackChanges: false);

			if (company is null)
			{
				_logger.LogInfo($"Company with id: ${companyId} doesn't exist");

				return NotFound();
			}

			_repository.Employe.CreateEmploye(companyId, employee);
			_repository.Save();

			var employeeToReturn = _mapper.Map<EmployeeDto>(employee);

			return CreatedAtRoute("EmployeeById", new { companyId, employeeToReturn.Id }, employeeToReturn);
		}

	}
}