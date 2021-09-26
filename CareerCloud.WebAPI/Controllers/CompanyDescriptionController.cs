using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/company/v1")]
    public class CompanyDescriptionController : ControllerBase
    {
        private readonly CompanyDescriptionLogic _logic;
        public CompanyDescriptionController()
        {
            var repo = new EFGenericRepository<CompanyDescriptionPoco>();
            _logic = new CompanyDescriptionLogic(repo);
        }

        [HttpGet]
        [Route("description")]
        public ActionResult GetAllCompanyDescription()
        {
            var companies = _logic.GetAll();
            if (companies == null)
            {
                return NotFound();
            }
            return new OkObjectResult(companies);
        }

        [HttpGet]
        [Route("description/{companyDescriptionId}")]
        public ActionResult GetCompanyDescription(Guid id)
        {
            var company = _logic.Get(id);
            if (company == null)
            {
                return NotFound();
            }
            return new OkObjectResult(company);
        }

        [HttpPost]
        [Route("description")]
        public ActionResult PostCompanyDescription([FromBody] CompanyDescriptionPoco[] companyDescriptionPocos)
        {
            _logic.Add(companyDescriptionPocos);
            return new OkResult();
        }

        [HttpPut]
        [Route("description")]
        public ActionResult PutCompanyDescription([FromBody] CompanyDescriptionPoco[] companyDescriptionPocos)
        {
            _logic.Update(companyDescriptionPocos);
            return new OkResult();
        }

        [HttpDelete]
        [Route("description")]
        public ActionResult DeleteCompanyDescription([FromBody] CompanyDescriptionPoco[] companyDescriptionPocos)
        {
            _logic.Delete(companyDescriptionPocos);
            return new OkResult();
        }
    }
}
