using Microsoft.AspNetCore.Mvc;
using MVM.Domain.Models;
using MVM.Domain.Services;

namespace MVM.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Priperties

        private readonly IAuthService _authService;

        #endregion

        #region Contructor

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion

        #region Public Methods

        [HttpPost]
        [Route("Login")]
        public JsonResult Login([FromBody] AuthModel authModel)
        {
            return new JsonResult(_authService.Login(authModel));
        }

        #endregion
    }
}
