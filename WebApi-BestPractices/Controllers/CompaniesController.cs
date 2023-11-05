using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_BestPractices.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges: false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return Ok(companiesDto);
        }

        [HttpGet("{id}", Name = "GetCompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repository.Company.GetCompany(id, trackChanges: false);

            if (company == null)
            {
                _logger.LogInfo($"Company with Id: {id} was not found");

                return NotFound(); ;
            }

            return Ok(_mapper.Map<CompanyDto>(company));

        }

        [HttpPost]
        public IActionResult CreatecCompany([FromBody] CompanyForCreationDto companyDto)
        {
            if (companyDto == null)
            {
                _logger.LogError("CompanyDto is null");

                return BadRequest("CompanyDto is null");
            }

            var company = _mapper.Map<Company>(companyDto);

            _repository.Company.CreateCompany(company);
            _repository.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(company);

            return CreatedAtRoute("GetCompanyById", new { id = companyToReturn.Id }, companyToReturn);
        }
    }
}