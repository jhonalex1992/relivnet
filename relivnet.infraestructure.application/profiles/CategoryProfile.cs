using AutoMapper;
using relivnet.domain.entities;
using relivnet.domain.models;
using relivnet.infraestructure.application.models.requests;
using relivnet.infraestructure.application.models.responses;

namespace relivnet.infraestructure.application.profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<CategoryEntity, CategoryResponseModel>();
            CreateMap<CategoryRequestModel, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryModel>();
        }
    }
}
