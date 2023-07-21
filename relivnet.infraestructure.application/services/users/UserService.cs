using Microsoft.AspNetCore.Identity;
using relivnet.domain.entities;
using relivnet.infraestructure.application.models;
using relivnet.infraestructure.util;
using relivnet.infraestructure.util.exceptions;

namespace relivnet.infraestructure.application
{
    public partial class ApplicationService : IApplicationService
    {
        public UserModel CreateUser(UserModel user)
        {
            bool existUser = this._userDomainRepository.AnySync(x => x.Email == user.Email);
            if (existUser)
            {
                throw new CustomException(ExceptionSettings.ALREDY_EXIST);
            }
            UserEntity userEntity = this._mapper.Map<UserEntity>(user);
            userEntity.Password = new PasswordHasher<UserEntity>().HashPassword(userEntity, user.Password);
            this._userDomainRepository.AddSync(userEntity);
            user.Id = userEntity.Id;
            return user;
        }
    }
}

