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
    public class StateProfile : Profile
    {
        public StateProfile() {
            CreateMap<StateEntity, StateResponseModel>();
            CreateMap<StateRequestModel, StateEntity>();
            CreateMap<StateEntity, StateModel>();
        }
    }
}
