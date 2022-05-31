using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_BestPractices.Controllers
{
    public class CompanyController : BaseApiController
    {

        [HttpGet]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            return Ok(Task.FromResult(new List<int>() { 1, 2, 3 }).Result);

        }

        //[HttpGet]
        //[Route("{id:int:min(1)}")]
        //[ProducesResponseType(201, Type = typeof(CompanyServiceModel))]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> Get(int id)
        //    => this.OkOrNotFound(await this.companyService.Details(id));


        //[HttpDelete("{id:int:min(1)}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var companyExists = await this.companyService.Exists(id);
        //    if (!companyExists)
        //    {
        //        return NotFound("Company does not exist.");
        //    }

        //    await this.companyService.DeleteEmployees(id);
        //    await this.companyService.Delete(id);
        //    return this.Ok();
        //}

        //[HttpPost]
        //[ValidateModelState]
        //[ProducesResponseType(200, Type = typeof(CompanyRequestModel))]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> Post([FromBody] CompanyRequestModel model)
        //{
        //    var categoryNameExists = await this.companyService.Exists(model.Name);
        //    if (categoryNameExists)
        //    {
        //        this.ModelState.AddModelError(nameof(CompanyRequestModel.Name), "Company name already exists.");
        //        return BadRequest(this.ModelState);
        //    }

        //    await this.companyService.Create(model);

        //    return this.Ok();
        //}

        //[HttpPost]
        //[Route(nameof(Put))]
        //[ValidateModelState]
        //[ProducesResponseType(200, Type = typeof(CompanyRequestModel))]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> Put([FromBody] CompanyRequestModel model)
        //{
        //    var companyExists = await this.companyService.Exists(model.Id);
        //    if (!companyExists)
        //    {
        //        this.ModelState.AddModelError(nameof(CompanyRequestModel.Name), "Company name does not exists");
        //        return BadRequest(this.ModelState);
        //    }

        //    await this.companyService.Update(model);
        //    return this.Ok();
        //}
    }
}