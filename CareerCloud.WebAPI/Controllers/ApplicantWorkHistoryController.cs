using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/applicant/v1")]
    public class ApplicantWorkHistoryController : ControllerBase
    {
        private readonly ApplicantWorkHistoryLogic _logic;
        public ApplicantWorkHistoryController()
        {
            var repo = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            _logic = new ApplicantWorkHistoryLogic(repo);
        }

        [HttpGet]
        [Route("workHistory")]
        public ActionResult GetAllApplicantWorkHistory()
        {
            var applicants = _logic.GetAll();
            if (applicants == null)
            {
                return NotFound();
            }
            return new OkObjectResult(applicants);
        }

        [HttpGet]
        [Route("workHistory/{applicantWorkHistoryId}")]
        public ActionResult GetApplicantWorkHistory(Guid id)
        {
            var applicant = _logic.Get(id);
            if (applicant == null)
            {
                return NotFound();
            }
            return new OkObjectResult(applicant);
        }

        [HttpPost]
        [Route("workHistory")]
        public ActionResult PostApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] applicantWorkHistoryPocos)
        {
            _logic.Add(applicantWorkHistoryPocos);
            return new OkResult();
        }

        [HttpPut]
        [Route("workHistory")]
        public ActionResult PutApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] applicantWorkHistoryPocos)
        {
            _logic.Update(applicantWorkHistoryPocos);
            return new OkResult();
        }

        [HttpDelete]
        [Route("workHistory")]
        public ActionResult DeleteApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] applicantWorkHistoryPocos)
        {
            _logic.Delete(applicantWorkHistoryPocos);
            return new OkResult();
        }
    }
}
