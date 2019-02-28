using AskQuestion.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AskQuestion.Service
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = await manager.FindAsync(context.UserName, context.Password);
            if(user!=null)
            {
                //assdadsadsadsa
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Id", user.Id));
                identity.AddClaim(new Claim("Username", user.UserName));
                identity.AddClaim(new Claim("Email", user.Email));
                identity.AddClaim(new Claim("FirstName", user.FirstName));
                identity.AddClaim(new Claim("LastName", user.LastName));
                identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                var userRoles = manager.GetRoles(user.Id);
                foreach(string roleName in userRoles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleName));

                }
                var additionalData = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "role",Newtonsoft.Json.JsonConvert.SerializeObject(userRoles)
                    }
                });
                var token = new AuthenticationTicket(identity, additionalData);

                context.Validated(token);

            }
            else
            {
                return;
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach(KeyValuePair<string,string> property in context.Properties.Dictionary)
            {
                TimeSpan span = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));

                if (property.Key == ".issued")
                {
                    context.AdditionalResponseParameters.Add(".issued", span.TotalMilliseconds);

                }
                else if (property.Key == ".expires")
                {
                    context.AdditionalResponseParameters.Add(".expires",span.TotalMilliseconds + 3600*1000);
                  

                }
                else
                {
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
                }
            }
            return Task.FromResult<object>(null);
        }

    }
}