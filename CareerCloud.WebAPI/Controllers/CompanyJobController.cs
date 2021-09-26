using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/company/v1")]
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic _logic;
        public CompanyJobController()
        {
            var repo = new EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repo);
        }

        [HttpGet]
        [Route("job")]
        public ActionResult GetAllCompanyJob()
        {
            var companies = _logic.GetAll();
            if (companies == null)
            {
                return NotFound();
            }
            return new OkObjectResult(companies);
        }

        [HttpGet]
        [Route("job/{companyJobId}")]
        public ActionResult GetCompanyJob(Guid id)
        {
            var company = _logic.Get(id);
            if (company == null)
            {
                return NotFound();
            }
            return new OkObjectResult(company);
        }

        [HttpPost]
        [Route("job")]
        public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] companyJobPocos)
        {
            _logic.Add(companyJobPocos);
            return new OkResult();
        }

        [HttpPut]
        [Route("job")]
        public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] companyJobPocos)
        {
            _logic.Update(companyJobPocos);
            return new OkResult();
        }

        [HttpDelete]
        [Route("job")]
        public ActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] companyJobPocos)
        {
            _logic.Delete(companyJobPocos);
            return new OkResult();
        }
    }
}
