using relivnet.domain.models;
using relivnet.infraestructure.application.models.requests;
using relivnet.infraestructure.application.models.responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relivnet.infraestructure.application
{
    public partial interface IApplicationService
    {
        ProductReponseModel CreateProduct(ProductRequestModel productRequestModel);

        ProductReponseModel UpdateProduct(ProductRequestModel productRequestModel);

        int DeleteProduct(int id);

        ProductReponseModel GetProductId(int id);

        PagedCollection<ProductReponseModel> GetAllProducts(int offset, int limit, string? filter);
    }
}
