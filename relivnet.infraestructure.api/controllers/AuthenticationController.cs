using Microsoft.AspNetCore.Mvc;
using relivnet.infraestructure.application;
using relivnet.infraestructure.application.models;
using relivnet.infraestructure.application.models.requests;

namespace relivnet.infraestructure.api.controllers;

[Route("api/authorizations")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public AuthenticationController(
        IApplicationService applicationService)
    {
        this._applicationService = applicationService;
    }

    [HttpPost]
    public ActionResult<LoginResponseModel> GetLogin([FromBody] LoginRequestModel loginRequestModel) =>
        Ok(this._applicationService.Login(loginRequestModel));
}