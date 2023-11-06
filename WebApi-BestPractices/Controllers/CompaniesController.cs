using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using WebApi_BestPractices.ModelBinders;

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

        [HttpGet("{id}", Name = "CompanyById")]
        public IActionResult GetCompany(long id)
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

            return CreatedAtRoute("CompanyById", new { companyToReturn.Id }, companyToReturn);
        }

        [HttpGet("collection/{ids}", Name = "CompanyCollection")]
        public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<long> ids)
        {
            if (ids is null)
            {
                _logger.LogError("Ids are null");
                return BadRequest("Ids are null");
            }

            var companies = _repository.Company.GetByIds(ids, trackChanges: false);

            if (ids.Count() != companies.Count())
            {
                _logger.LogError("Some Ids are not valid in the collection");
                return BadRequest("Some Ids are not valid in the collection");
            }

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return Ok(companiesToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection is null)
            {
                _logger.LogError("Company Collection is null");
                return BadRequest("Company Collection is null");
            }

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var company in companyEntities)
            {
                _repository.Company.CreateCompany(company);
            }

            _repository.Save();
            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            // No Header Location support for the list. Thus, creating comma separated
            var ids = string.Join(", ", companyCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("CompanyCollection", new { ids }, companyCollectionToReturn);
        }
    }
}