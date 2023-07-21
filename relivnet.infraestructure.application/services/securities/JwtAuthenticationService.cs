using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using relivnet.domain.entities;
using relivnet.infraestructure.application.models;
using relivnet.infraestructure.application.models.requests;
using relivnet.infraestructure.util;
using relivnet.infraestructure.util.exceptions;

namespace relivnet.infraestructure.application;

public partial class ApplicationService : IApplicationService
{
    public LoginResponseModel Login(LoginRequestModel loginRequestModel)
    {
        List<string> navigationsProperties = new List<string>();
        navigationsProperties.Add("UsersRoles");
        UserEntity user = this._userDomainRepository.FirstOrDefaultSync(x => x.Email == loginRequestModel.Email, navigationsProperties);
        if (user == null)
            throw new CustomException(ExceptionSettings.NOT_FOUND);
        bool validPassword = loginRequestModel.Password != null && this.ValidateUserPassword(user, loginRequestModel.Password);
        if (!validPassword)
            throw new CustomException(ExceptionSettings.INVALID_PASSWORD);
        List<Claim> userClaims = this.GenerateUserClaims(user);
        string token = this.GenerateUserToken(userClaims);
        return new LoginResponseModel
        {
            Token = token,
        };
    }

    private string GenerateUserToken(List<Claim> userClaims)
    {
        byte[] key = Encoding.ASCII.GetBytes(this._tokenKey);
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(userClaims),
            NotBefore = DateTime.UtcNow.AddMinutes(-5),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private bool ValidateUserPassword(UserEntity user, string password)
    {
        var passwordHasher = new PasswordHasher<UserEntity>();
        PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);
        return passwordVerificationResult == PasswordVerificationResult.Success;
    }

    private List<Claim> GenerateUserClaims(UserEntity user)
    {
        List<RoleEntity> userRoles = this._roleDomainRepository.GetAllWhereSync(x => x.UsersRoles.Any(x => x.UserId == user.Id));
        List<Claim> claims = new List<Claim>()
        {
            new("id", user.Id.ToString()),
            new("username", user.Email),
        };
        claims.AddRange(userRoles.Select(x => new Claim("role", x.Key)));
        return claims;
    }
}