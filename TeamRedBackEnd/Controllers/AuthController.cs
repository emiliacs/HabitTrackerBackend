using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamRedBackEnd.ViewModels;

namespace TeamRedBackEnd.Controllers
{
    [ApiController]
    [Route("api")]
    
    public class AuthController : ControllerBase
    {
        Services.IAuthService AuthService;
        Database.Repositroies.IUserRepository UserRepository;
        public AuthController(Services.IAuthService AuthService, Database.Repositroies.IUserRepository UserRepository)
        {
            this.AuthService = AuthService;
            this.UserRepository = UserRepository;
        }

        [HttpPost("login")]
        public ActionResult<AuthData> Post([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Database.Models.User user = UserRepository.GetUser(model.Email);
            bool passwordValid = AuthService.VerifyPassword(model.Password, user.Password);

            if (!passwordValid || user == null) 
            {
                return BadRequest(new { msg = "Incorrect email or password" });
            }
            return AuthService.GetAuthData(user.Id.ToString());
        }

        
        [HttpPost("logout")]
        public ActionResult<AuthData> LogoutPost([FromBody] LoginViewModel model) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Database.Models.User user = UserRepository.GetUser(model.Email);
            return AuthService.GetAuthData(user.Id.ToString(), 10);
        }

      
       
    }
}
