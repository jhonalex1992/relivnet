using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using relivnet.infraestructure.application;
using relivnet.infraestructure.application.models;
using relivnet.infraestructure.application.models.requests;

namespace relivnet.infraestructure.api.controllers
{
    [Route("api/state")]
    [ApiController]
    [Authorize]
    public class StateController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public StateController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create([FromBody] StateRequestModel stateRequest) =>
            this.Ok(this._applicationService.CreateState(stateRequest));

        [HttpPut]
        [AllowAnonymous]
        public ActionResult Update([FromBody] StateRequestModel stateRequest) =>
            this.Ok(this._applicationService.UpdateState(stateRequest));

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public ActionResult Delete(int id)
        {
            this._applicationService.DeleteState(id);
            return this.Ok();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult GetStateId(int id) =>
            this.Ok(this._applicationService.GetStateId(id));

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllStates(int offset, int limit, string? filter) =>
            this.Ok(this._applicationService.GetAllStates(offset, limit, filter));
    }
}
