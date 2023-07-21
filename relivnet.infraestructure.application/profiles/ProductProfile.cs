using AutoMapper;
using relivnet.domain.entities;
using relivnet.domain.models;
using relivnet.infraestructure.application.models.requests;
using relivnet.infraestructure.application.models.responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relivnet.infraestructure.application.profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<ProductEntity, ProductReponseModel>()
               .ForMember(des => des.Category, opt => opt.MapFrom(x => x.Category))
               .ForMember(des => des.State, opt => opt.MapFrom(x => x.State));

            CreateMap<ProductRequestModel, ProductEntity>();
            CreateMap<ProductEntity, ProductModel>();
        }
    }
}
