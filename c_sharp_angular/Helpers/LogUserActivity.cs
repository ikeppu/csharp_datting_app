using System;
using c_sharp_angular.Extensions;
using c_sharp_angular.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace c_sharp_angular.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            var userId = resultContext.HttpContext.User.GetUserID();
            var username = resultContext.HttpContext.User.GetUsername();

            var repo = resultContext.HttpContext.RequestServices
                .GetRequiredService<IUserRepository>();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(username))
            {
                var user = await repo.GetUserByIdAsync(int.Parse(userId));
                user.LastActive = DateTime.UtcNow;
                await repo.SaveAllAsync();
            }

        }
    }
}

