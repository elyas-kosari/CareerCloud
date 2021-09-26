using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/security/v1")]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic _logic;
        public SecurityLoginsLogController()
        {
            var repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            _logic = new SecurityLoginsLogLogic(repo);
        }

        [HttpGet]
        [Route("loginsLog")]
        public ActionResult GetAllSecurityLoginLog()
        {
            var securities = _logic.GetAll();
            if (securities == null)
            {
                return NotFound();
            }
            return new OkObjectResult(securities);
        }

        [HttpGet]
        [Route("loginsLog/{securityLoginsLogId}")]
        public ActionResult GetSecurityLoginLog(Guid id)
        {
            var security = _logic.Get(id);
            if (security == null)
            {
                return NotFound();
            }
            return new OkObjectResult(security);
        }

        [HttpPost]
        [Route("loginsLog")]
        public ActionResult PostSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] securityLoginsLogPocos)
        {
            _logic.Add(securityLoginsLogPocos);
            return new OkResult();
        }

        [HttpPut]
        [Route("loginsLog")]
        public ActionResult PutSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] securityLoginsLogPocos)
        {
            _logic.Update(securityLoginsLogPocos);
            return new OkResult();
        }

        [HttpDelete]
        [Route("loginsLog")]
        public ActionResult DeleteSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] securityLoginsLogPocos)
        {
            _logic.Delete(securityLoginsLogPocos);
            return new OkResult();
        }
    }
}
