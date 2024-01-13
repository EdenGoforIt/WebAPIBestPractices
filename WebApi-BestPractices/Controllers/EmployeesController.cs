using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi_BestPractices.ActionFilters;

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

		// TODO: implement CompanyExists filter attribute
		[HttpGet]
		public async Task<IActionResult> GetEmployeesForCompany(long companyId, [FromQuery] EmployeeParameters employeeParameters)
		{
			var company = await _repository.Company.GetCompany(companyId, trackChanges: false);

			if (company is null)
			{
				_logger.LogInfo($"Company with id: ${companyId} doesn't exist");

				return NotFound();
			}

			var employees = await _repository.Employe.GetEmployees(companyId, employeeParameters, trackChanges: false);

			Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employees.MetaData));

			var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

			return Ok(employeesDto);
		}

		[HttpGet("{id}", Name = "EmployeeById")]
		public async Task<IActionResult> GetEmployee(long companyId, long id)
		{
			var company = await _repository.Company.GetCompany(companyId, trackChanges: false);

			if (company is null)
			{
				_logger.LogInfo($"Company with id: ${companyId} doesn't exist");

				return NotFound();
			}

			var employee = await _repository.Employe.GetEmployee(companyId, id, trackChanges: false);

			if (employee is null)
			{
				_logger.LogInfo($"Employee with id: ${id} doesn't exist");

				return NotFound();
			}

			var employeeDto = _mapper.Map<EmployeeDto>(employee);

			return Ok(employeeDto);
		}

		[HttpPost]
		public async Task<IActionResult> CreateEmployee(long companyId, [FromBody] EmployeeForCreationDto employeeDto)
		{
			if (employeeDto is null)
			{
				_logger.LogError("EmployeeDto is null");
				return BadRequest("EmployeeDto is null");
			}

			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state");
				return UnprocessableEntity(ModelState);
			}

			var employee = _mapper.Map<Employee>(employeeDto);

			var company = await _repository.Company.GetCompany(companyId, trackChanges: false);

			if (company is null)
			{
				_logger.LogInfo($"Company with id: ${companyId} doesn't exist");

				return NotFound();
			}

			_repository.Employe.CreateEmploye(companyId, employee);
			await _repository.SaveAsync();

			var employeeToReturn = _mapper.Map<EmployeeDto>(employee);

			return CreatedAtRoute("EmployeeById", new { companyId, employeeToReturn.Id }, employeeToReturn);
		}

		[HttpDelete("{id}")]
		[ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
		public async Task<IActionResult> DeleteEmployee(long companyId, long id)
		{
			var employee = HttpContext.Items["employee"] as Employee;

			_repository.Employe.DeleteEmployee(employee);
			await _repository.SaveAsync();

			return NoContent();
		}


		[HttpPut("{id}")]
		[ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
		public async Task<IActionResult> UpdateEmployee(long companyId, long id, [FromBody] EmployeeForUpdateDto dto)
		{
			var employee = HttpContext.Items["employee"] as Employee;

			_mapper.Map(dto, employee);
			await _repository.SaveAsync();

			return NoContent();
		}

		[HttpPatch("{id}")]
		[ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
		public async Task<IActionResult> UpdateEmployeeForCompany(long companyId, long id, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDto)
		{
			var employee = HttpContext.Items["employee"] as Employee;
			var employeeDto = _mapper.Map<EmployeeForUpdateDto>(employee);

			// This does data modeling and Model State Binding
			patchDto.ApplyTo(employeeDto, ModelState);

			// Model validation with attributes
			TryValidateModel(employeeDto);

			if (!ModelState.IsValid)
			{
				_logger.LogError("Invalid model state");
				return UnprocessableEntity(ModelState);
			}

			_mapper.Map(employeeDto, employee);

			await _repository.SaveAsync();

			return NoContent();
		}
	}
}