using Microsoft.AspNetCore.Mvc;
using TeamRedBackEnd.DataTransferObject;
using TeamRedBackEnd.Services;
using TeamRedBackEnd.Database.Repositories;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Controllers
{
    [ApiController]
    [Route("api")]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService AuthService;
        private readonly PasswordService PasswordService;

        readonly IRepositoryWrapper _wrapper;

        public AuthController(IAuthService AuthService, IRepositoryWrapper wrapper, PasswordService PasswordService)
        {
            this.AuthService = AuthService;
            this.PasswordService = PasswordService;
            _wrapper = wrapper;
        }

        [HttpPost("login")]
        public ActionResult Post([FromBody] LoginData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            User user = _wrapper.UsersRepository.GetUserByEmail(model.Email);

            if (user == null) NotFound(new { msg = "No account with this email" });
             
            bool passwordValid = PasswordService.VerifyHash(model.Password, user);

            if (!passwordValid || user == null)
            {
                return Conflict(new { msg = "Incorrect email or password" });
            }
            return Ok(AuthService.GetAuthData(user.Id.ToString()));
            
        }

        [HttpPost("logout")]
        public ActionResult LogoutPost([FromBody] LoginData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            User user = _wrapper.UsersRepository.GetUserByEmail(model.Email);
            if(user == null) return NotFound();
            return Ok(AuthService.GetAuthData(user.Id.ToString(), 10));
        }


    }
}
