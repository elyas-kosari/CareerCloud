using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/applicant/v1")]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic _logic;
        public ApplicantJobApplicationController()
        {
            var repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logic = new ApplicantJobApplicationLogic(repo);
        }

        [HttpGet]
        [Route("jobApplication")]
        public ActionResult GetAllApplicantJobApplication()
        {
            var applicants = _logic.GetAll();
            if (applicants == null)
            {
                return NotFound();
            }
            return new OkObjectResult(applicants);
        }

        [HttpGet]
        [Route("jobApplication/{applicantJobApplicationId}")]
        public ActionResult GetApplicantJobApplication(Guid id)
        {
            var applicant = _logic.Get(id);
            if (applicant == null)
            {
                return NotFound();
            }
            return new OkObjectResult(applicant);
        }

        [HttpPost]
        [Route("jobApplication")]
        public ActionResult PostApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] applicantJobApplications)
        {
            _logic.Add(applicantJobApplications);
            return new OkResult();
        }

        [HttpPut]
        [Route("jobApplication")]
        public ActionResult PutApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] applicantJobApplications)
        {
            _logic.Update(applicantJobApplications);
            return new OkResult();
        }

        [HttpDelete]
        [Route("jobApplication")]
        public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] applicantJobApplications)
        {
            _logic.Delete(applicantJobApplications);
            return new OkResult();
        }
    }
}
