﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseProject.Domain.Interfaces;
using CourseProject.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;

namespace CourseProject.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});

            using (IUnitOfWork repo = new EfUnitOfWork())
            {
                var user = await repo.Users.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                if (user.Roles.Any(x => x.RoleId == "1"))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                }

                context.Validated(identity);
            }
//            identity.AddClaim(new Claim("sub", context.UserName));
//            identity.AddClaim(new Claim("role", "user"));
        }
    }
}