using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/security/v1")]
    public class SecurityLoginsRoleController : ControllerBase
    {
        private readonly SecurityLoginsRoleLogic _logic;
        public SecurityLoginsRoleController()
        {
            var repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            _logic = new SecurityLoginsRoleLogic(repo);

        }

        [HttpGet]
        [Route("loginsRole")]
        public ActionResult GetAllSecurityLoginRole()
        {
            var securities = _logic.GetAll();
            if (securities == null)
            {
                return NotFound();
            }
            return new OkObjectResult(securities);
        }

        [HttpGet]
        [Route("loginsRole/{securityLoginsRoleId}")]
        public ActionResult GetSecurityLoginsRole(Guid id)
        {
            var security = _logic.Get(id);
            if (security == null)
            {
                return NotFound();
            }
            return new OkObjectResult(security);
        }

        [HttpPost]
        [Route("loginsRole")]
        public ActionResult PostSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] securityLoginsRolePocos)
        {
            _logic.Add(securityLoginsRolePocos);
            return new OkResult();
        }

        [HttpPut]
        [Route("loginsRole")]
        public ActionResult PutSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] securityLoginsRolePocos)
        {
            _logic.Update(securityLoginsRolePocos);
            return new OkResult();
        }

        [HttpDelete]
        [Route("loginsRole")]
        public ActionResult DeleteSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] securityLoginsRolePocos)
        {
            _logic.Delete(securityLoginsRolePocos);
            return new OkResult();
        }
    }
}
