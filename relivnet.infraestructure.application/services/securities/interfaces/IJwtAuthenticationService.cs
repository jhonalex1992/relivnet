using relivnet.infraestructure.application.models;
using relivnet.infraestructure.application.models.requests;

namespace relivnet.infraestructure.application;

public partial interface IApplicationService
{
    LoginResponseModel Login(LoginRequestModel loginRequestModel);
}