using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net.Http;
using System.Security.Claims;

namespace PolicyAuthJWT.Config.Auth
{
    public static class PolicyAuthorization
    {
        public static void Configure(AuthorizationOptions options)
        {
            options.AddPolicy("HomeRead", policy =>
            {
                policy.Requirements.Add(new PolicyAuthorizationRequirement("HomeRead"));
            });

            options.AddPolicy("HomeWrite", policy =>
            {
                policy.Requirements.Add(new PolicyAuthorizationRequirement("HomeWrite"));
            });
        }
    }

    public class PolicyAuthorizationRequirement : IAuthorizationRequirement
    {
        public string PolicyName;

        public PolicyAuthorizationRequirement(string policyName)
        {
            PolicyName = policyName;
        }
    }

    public class PolicyAuthorizationHandler : AuthorizationHandler<PolicyAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyAuthorizationRequirement requirement)
        {
            try
            {
                int userId = Convert
                    .ToInt32(context.User
                        .FindFirst(ClaimTypes.NameIdentifier)
                        ?.Value);

                var user = Enums.Users.FirstOrDefault(x => x.Id == userId);

                if (user != null && user.Policies.Contains(requirement.PolicyName))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }

            }
            catch
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}