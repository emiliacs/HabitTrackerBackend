using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamRedBackEnd.ViewModels;
using TeamRedBackEnd.Services;

namespace TeamRedBackEnd.Controllers
{
    [ApiController]
    [Route("api")]

    public class AuthController : ControllerBase
    {
        IAuthService AuthService;
        IMailService MailService;
        PasswordService PasswordService;

        Database.Repositories.IUsersRepository UserRepository;

        public AuthController(IAuthService AuthService, Database.Repositories.IUsersRepository UserRepository, IMailService MailService, PasswordService PasswordService)
        {
            this.AuthService = AuthService;
            this.MailService = MailService;
            this.PasswordService = PasswordService;
            this.UserRepository = UserRepository;
        }

        [HttpPost("login")]
        public ActionResult<AuthData> Post([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Database.Models.User user = UserRepository.GetUserByEmail(model.Email);
            bool passwordValid = PasswordService.VerifyHash(model.Password, user);

            if (!passwordValid || user == null)
            {
                return BadRequest(new { msg = "Incorrect email or password" });
            }
            return Ok(AuthService.GetAuthData(user.Id.ToString()));
            
        }

        [HttpPost("logout")]
        public ActionResult<AuthData> LogoutPost([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Database.Models.User user = UserRepository.GetUserByEmail(model.Email);
            return AuthService.GetAuthData(user.Id.ToString(), 10);
        }


    }
}
