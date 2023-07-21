using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using relivnet.infraestructure.application;
using relivnet.infraestructure.application.models.requests;

namespace relivnet.infraestructure.api.controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ProductController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create([FromBody] ProductRequestModel productRequest) =>
            this.Ok(this._applicationService.CreateProduct(productRequest));

        [HttpPut]
        [AllowAnonymous]
        public ActionResult Update([FromBody] ProductRequestModel productRequest) =>
            this.Ok(this._applicationService.UpdateProduct(productRequest));

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public ActionResult Delete(int id)
        {
            this._applicationService.DeleteProduct(id);
            return this.Ok();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult GetProductId(int id) =>
            this.Ok(this._applicationService.GetProductId(id));

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllStates(int offset, int limit, string? filter) =>
            this.Ok(this._applicationService.GetAllProducts(offset, limit, filter));
    }
}
