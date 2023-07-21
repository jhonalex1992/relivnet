using relivnet.domain.models;

namespace relivnet.infraestructure.api.middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Func<UserInfoModel> _userInfoFactory;
    private UserInfoModel UserInfo => _userInfoFactory();

    public JwtMiddleware(RequestDelegate next, Func<UserInfoModel> userInfoFactory)
    {
        this._next = next;
        this._userInfoFactory = userInfoFactory;
    }
    
    public async Task Invoke(HttpContext context)
    {
        await this._next(context);
    }
}