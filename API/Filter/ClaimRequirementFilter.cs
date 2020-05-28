using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Utilities.Constant;

namespace API.Filter
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly FunctionCode _function;
        private readonly ActionCode _action;

        public ClaimRequirementFilter(FunctionCode function, ActionCode action)
        {
            _function = function;
            _action = action;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            var permissionsClaim = context.HttpContext.User.Claims.SingleOrDefault(c => c.Type == SystemConstants.UserClaim.Permissions);
            
            if (permissionsClaim != null)
            {
                var permissions = JsonConvert.DeserializeObject<List<string>>(permissionsClaim.Value);
                if (!permissions.Contains(_function + "_" + _action))
                {
                    context.Result = new UnauthorizedObjectResult("401");
                }
            }
            else
            {
                context.Result = new UnauthorizedObjectResult("401");
            }
        }
    }
}
