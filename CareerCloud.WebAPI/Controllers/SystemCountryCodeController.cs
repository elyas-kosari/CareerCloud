using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/system/v1")]
    public class SystemCountryCodeController : ControllerBase
    {
        private readonly SystemCountryCodeLogic _logic;
        public SystemCountryCodeController()
        {
            var repo = new EFGenericRepository<SystemCountryCodePoco>();
            _logic = new SystemCountryCodeLogic(repo);
        }

        [HttpGet]
        [Route("countryCode")]
        public ActionResult GetAllSystemCountryCode()
        {
            var systems = _logic.GetAll();
            if (systems == null)
            {
                return NotFound();
            }
            return new OkObjectResult(systems);
        }

        [HttpGet]
        [Route("countryCode/{systemCountryCode}")]
        public ActionResult GetSystemCountryCode(string code)
        {
            var system = _logic.Get(code);
            if (system == null)
            {
                return NotFound();
            }
            return new OkObjectResult(system);
        }

        [HttpPost]
        [Route("CountryCode")]
        public ActionResult PostSystemCountryCode(
            [FromBody] SystemCountryCodePoco[] systemCountryCodePocos)
        {
            _logic.Add(systemCountryCodePocos);
            return new OkResult();
        }

        [HttpPut]
        [Route("countryCode")]
        public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] systemCountryCodePocos)
        {
            _logic.Update(systemCountryCodePocos);
            return new OkResult();
        }

        [HttpDelete]
        [Route("countryCode")]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] systemCountryCodePocos)
        {
            _logic.Delete(systemCountryCodePocos);
            return new OkResult();
        }
    }
}
