using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using relivnet.infraestructure.application;
using relivnet.infraestructure.application.models.requests;

namespace relivnet.infraestructure.api.controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public CategoryController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create([FromBody] CategoryRequestModel categoryRequest) =>
            this.Ok(this._applicationService.CreateCategory(categoryRequest));

        [HttpPut]
        [AllowAnonymous]
        public ActionResult Update([FromBody] CategoryRequestModel categoryRequest) =>
            this.Ok(this._applicationService.UpdateCategory(categoryRequest));

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public ActionResult Delete(int id)
        {
            this._applicationService.DeleteCategory(id);
            return this.Ok();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult GetCategoryId(int id) =>
            this.Ok(this._applicationService.GetCategoryId(id));

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllCategories(int offset, int limit, string? filter) =>
            this.Ok(this._applicationService.GetAllCategory(offset, limit, filter));
    }
}
