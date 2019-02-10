using AskQuestion.Core.Infrastructure;
using AskQuestion.Data.Model;
using AskQuestion.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace AskQuestion.Service.Controllers
{
    
    public class UserController : ApiController
    {

        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController()
        {
            _context = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(_context);
            _userManager = new UserManager<ApplicationUser>(store);
        }

        [HttpGet]
        [Route("api/user/GetUserClaims")]
        
        public User GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            User model = new User()
            {
                FirstName = identityClaims.FindFirst("FirstName").Value,
                Email = identityClaims.FindFirst("Email").Value,
                LastName = identityClaims.FindFirst("LastName").Value,
                UserName = identityClaims.FindFirst("UserName").Value
            };
            return model;
        }
    
        [HttpPost]
        [Route("api/user/register")]

        public IdentityResult InsertUser(User obj)
        {
           
            var user = new ApplicationUser() { UserName = obj.UserName, Email = obj.Email };
            user.FirstName = obj.FirstName;
            user.LastName = obj.LastName;
            _userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3

            };
            IdentityResult result = _userManager.Create(user, obj.Password);

            return result;

        }

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        [Route("api/user/delete/{id}")]
        public IdentityResult DeleteUser(string id)
        {
         
            var user = _userManager.FindById(id);
            var rolesForUser =  _userManager.GetRoles(id);
            string[] rolesArray=new string[rolesForUser.Count];
           
            for(int i=0;i<rolesForUser.Count();i++)
            {
                rolesArray[i] = rolesForUser.ElementAt(i).ToString();
            }
            var remFromRole = _userManager.RemoveFromRoles(id, rolesArray);
            
            if(remFromRole.Succeeded)
            {
                var results = _userManager.Delete(user);
                if(results.Succeeded)
                {
                    return results;
                }
                else
                {
                    return results;
                }
            }
            else
            {
                return remFromRole;
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("api/user/update/{id}")]
        public IdentityResult UpdateUser(User obj,string id)
        {
            ApplicationUser user = _userManager.FindById(id);
            user.FirstName = obj.FirstName;
            user.LastName = obj.LastName;
            user.Email = obj.Email;
            user.UserName = obj.UserName;
            var result = _userManager.Update(user);
            return result;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/user/list")]

        public IEnumerable<User> ListUser()
        {
            return _context.Users.Select(x => new User() {FirstName=x.FirstName,LastName=x.LastName,Email=x.Email,UserName=x.UserName,Password=x.PasswordHash }).ToList();
        }


    }
}
