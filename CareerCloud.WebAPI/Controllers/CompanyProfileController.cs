using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/company/v1")]
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic _logic;
        public CompanyProfileController()
        {
            var repo = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(repo);
        }

        [HttpGet]
        [Route("profile")]
        public ActionResult GetAllCompanyProfile()
        {
            var companies = _logic.GetAll();
            if (companies == null)
            {
                return NotFound();
            }
            return new OkObjectResult(companies);
        }

        [HttpGet]
        [Route("profile/{companyProfileId}")]
        public ActionResult GetCompanyProfile(Guid id)
        {
            var company = _logic.Get(id);
            if (company == null)
            {
                return NotFound();
            }
            return new OkObjectResult(company);
        }

        [HttpPost]
        [Route("profile")]
        public ActionResult PostCompanyProfile([FromBody] CompanyProfilePoco[] companyProfilePocos)
        {
            _logic.Add(companyProfilePocos);
            return new OkResult();
        }

        [HttpPut]
        [Route("profile")]
        public ActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] companyProfilePocos)
        {
            _logic.Update(companyProfilePocos);
            return new OkResult();
        }

        [HttpDelete]
        [Route("profile")]
        public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] companyProfilePocos)
        {
            _logic.Delete(companyProfilePocos);
            return new OkResult();
        }
    }
}
