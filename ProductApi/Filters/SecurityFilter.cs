using Microsoft.AspNetCore.Mvc.Filters;
using Product.App.Contract.IServices;

namespace Shop.EndPoint.Api.Filters
{
    public class SecurityFilter : ActionFilterAttribute
    {
        private readonly string role;
        public SecurityFilter(string role)
        {
            this.role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader))
                throw new UnauthorizedAccessException("دسترسی مجاز نیست");

            if (authHeader.Contains("Bearer"))
                authHeader = authHeader.Replace("Bearer ", "");
            else
                throw new UnauthorizedAccessException("دسترسی مجاز نیست");

            var tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();

            var claims = tokenService.ValidateToken(authHeader);

            if (!claims.Any(x => x.Value == role))
                throw new UnauthorizedAccessException("دسترسی مجاز نیست");

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
