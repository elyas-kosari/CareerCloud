using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [ApiController]
    [Route("api/careercloud/system/v1")]
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeController()
        {
            var repo = new EFGenericRepository<SystemLanguageCodePoco>();
            _logic = new SystemLanguageCodeLogic(repo);
        }

        [HttpGet]
        [Route("languageCode")]
        public ActionResult GetAllSystemLanguageCode()
        {
            var systems = _logic.GetAll();
            if (systems == null)
            {
                return NotFound();
            }
            return new OkObjectResult(systems);
        }

        [HttpGet]
        [Route("languageCode/{systemLanguageCode}")]
        public ActionResult GetSystemLanguageCode(string code)
        {
            var system = _logic.Get(code);
            if (system == null)
            {
                return NotFound();
            }
            return new OkObjectResult(system);
        }

        [HttpPost]
        [Route("languageCode")]
        public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco[] systemLanguageCodePocos)
        {
            _logic.Add(systemLanguageCodePocos);
            return new OkResult();
        }

        [HttpPut]
        [Route("languageCode")]
        public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] systemLanguageCodePocos)
        {
            _logic.Update(systemLanguageCodePocos);
            return new OkResult();
        }

        [HttpDelete]
        [Route("languageCode")]
        public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] systemLanguageCodePocos)
        {
            _logic.Delete(systemLanguageCodePocos);
            return new OkResult();
        }
    }
}
