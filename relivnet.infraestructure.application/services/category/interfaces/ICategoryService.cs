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
        CategoryResponseModel CreateCategory(CategoryRequestModel categoryRequestModel);

        CategoryResponseModel UpdateCategory(CategoryRequestModel categoryRequestModel);

        int DeleteCategory(int id);

        CategoryResponseModel GetCategoryId(int id);

        PagedCollection<CategoryResponseModel> GetAllCategory(int offset, int limit, string? filter);
    }
}
