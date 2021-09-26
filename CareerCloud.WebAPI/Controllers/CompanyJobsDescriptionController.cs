using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/company/v1")]
    public class CompanyJobsDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic _logic;
        public CompanyJobsDescriptionController()
        {
            var repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            _logic = new CompanyJobDescriptionLogic(repo);
        }

        [HttpGet]
        [Route("jobDescription")]
        public ActionResult GetAllCompanyJobDescription()
        {
            var companies = _logic.GetAll();
            if (companies == null)
            {
                return NotFound();
            }
            return new OkObjectResult(companies);
        }

        [HttpGet]
        [Route("jobDescription/{companyJobDescriptionId}")]
        public ActionResult GetCompanyJobsDescription(Guid id)
        {
            var company = _logic.Get(id);
            if (company == null)
            {
                return NotFound();
            }
            return new OkObjectResult(company);
        }

        [HttpPost]
        [Route("jobDescription")]
        public ActionResult PostCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] companyJobDescriptionPocos)
        {
            _logic.Add(companyJobDescriptionPocos);
            return new OkResult();
        }

        [HttpPut]
        [Route("jobDescription")]
        public ActionResult PutCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] companyJobDescriptionPocos)
        {
            _logic.Update(companyJobDescriptionPocos);
            return new OkResult();
        }

        [HttpDelete]
        [Route("jobDescription")]
        public ActionResult DeleteCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] companyJobDescriptionPocos)
        {
            _logic.Delete(companyJobDescriptionPocos);
            return new OkResult();
        }
    }
}
