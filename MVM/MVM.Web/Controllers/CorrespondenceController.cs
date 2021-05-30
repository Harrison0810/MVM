using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVM.Domain.Helpers;
using MVM.Domain.Models;
using MVM.Domain.Services;

namespace MVM.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class CorrespondenceController : ControllerBase
    {
        #region Properties

        private readonly ICorrespondenceService _correspondenceService;

        #endregion

        #region Constructor

        public CorrespondenceController(ICorrespondenceService correspondenceService)
        {
            _correspondenceService = correspondenceService;
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("GetCorresponces")]
        public JsonResult GetCorresponces()
        {
            return new JsonResult(_correspondenceService.GetCorresponces());
        }

        [HttpGet]
        [Route("GetCorresponce/{Id}")]
        public JsonResult GetCorresponces(int Id)
        {
            return new JsonResult(_correspondenceService.GetCorresponce(Id));
        }

        [HttpPost]
        [Route("AddCorrespondence")]
        public JsonResult AddCorrespondence([FromBody] CorrespondencesModel model)
        {
            model.UserId = Helper.GetUserIdToken(HttpContext);
            return new JsonResult(_correspondenceService.AddCorrespondence(model));
        }

        [HttpPost]
        [Route("EditCorrespondence")]
        public JsonResult EditCorrespondence([FromBody] CorrespondencesModel model)
        {
            return new JsonResult(_correspondenceService.EditCorrespondence(model));
        }

        [HttpPost]
        [Route("DeleteCorrespondence")]
        public JsonResult DeleteCorrespondence([FromBody] CorrespondencesModel model)
        {
            return new JsonResult(_correspondenceService.DeleteCorrespondence(model));
        }

        [HttpGet]
        [Route("GetCorrespondenceTypes")]
        public JsonResult GetCorrespondenceTypes()
        {
            return new JsonResult(_correspondenceService.GetCorrespondenceTypes());
        }

        #endregion
    }
}
