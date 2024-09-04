using Microsoft.AspNetCore.Mvc.Filters;

namespace blueberry.Filters
{
    public class RoleAuthorizeFilter : IAuthorizationFilter
    {
        private readonly string _role;
        public RoleAuthorizeFilter(string role)
        {
            _role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }

    }
}