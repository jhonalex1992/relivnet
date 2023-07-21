using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using relivnet.infraestructure.application;
using relivnet.infraestructure.application.models;

namespace relivnet.infraestructure.api.controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public UserController(
            IApplicationService applicationService) 
        {
            this._applicationService = applicationService;
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create([FromBody] UserModel userModel) =>
             this.Ok(this._applicationService.CreateUser(userModel));
        

    }
}