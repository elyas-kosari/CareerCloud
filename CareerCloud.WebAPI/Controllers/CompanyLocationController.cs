using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/company/v1")]
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic _logic;
        public CompanyLocationController()
        {
            var repo = new EFGenericRepository<CompanyLocationPoco>();
            _logic = new CompanyLocationLogic(repo);
        }

        [HttpGet]
        [Route("location")]
        public ActionResult GetAllCompanyLocation()
        {
            var companies = _logic.GetAll();
            if (companies == null)
            {
                return NotFound();
            }
            return new OkObjectResult(companies);
        }

        [HttpGet]
        [Route("location/{companyLocationId}")]
        public ActionResult GetCompanyLocation(Guid id)
        {
            var company = _logic.Get(id);
            if (company == null)
            {
                return NotFound();
            }
            return new OkObjectResult(company);
        }

        [HttpPost]
        [Route("location")]
        public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] companyLocationPocos)
        {
            _logic.Add(companyLocationPocos);
            return new OkResult();
        }

        [HttpPut]
        [Route("location")]
        public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] companyLocationPocos)
        {
            _logic.Update(companyLocationPocos);
            return new OkResult();
        }

        [HttpDelete]
        [Route("location")]
        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] companyLocationPocos)
        {
            _logic.Delete(companyLocationPocos);
            return new OkResult();
        }
    }
}
