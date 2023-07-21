using relivnet.infraestructure.application.models;

namespace relivnet.infraestructure.application
{ 
    public partial interface IApplicationService
    {
        UserModel CreateUser(UserModel userEntityBase);
    }
}
