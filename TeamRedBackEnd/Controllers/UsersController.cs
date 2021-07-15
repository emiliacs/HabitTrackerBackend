using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.Database.Repositories;
using TeamRedBackEnd.ViewModels;

namespace TeamRedBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        Services.PasswordService _passwordService;
        Services.IMailService _mailService;
        private IRepositoryWrapper _repoWrapper;


        public UsersController(IRepositoryWrapper wrapper, Services.PasswordService passwordService, Services.IMailService mailService)
        {
            _passwordService = passwordService;
            _mailService = mailService;
            _repoWrapper = wrapper;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_repoWrapper.UsersRepository.GetAllUsers());
        }

        [HttpGet]
        [Route("{userId:int}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _repoWrapper.UsersRepository.GetUserById(userId);

            if (user == null) return NotFound("User with ID: " + userId + " doesn't exist");

            return Ok(user);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult SearchUserByNameOrEmail()
        {
            string name = HttpContext.Request.Query["name"];
            string email = HttpContext.Request.Query["email"];

            if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(email)) return NotFound("Input can't be null");

            User user = new User();

            if (name != null && email != null) user = _repoWrapper.UsersRepository.GetUserByEmailAndName(name, email);

            if (name != null && email == null) user = _repoWrapper.UsersRepository.GetUserByName(name);

            if (name == null && email != null) user = _repoWrapper.UsersRepository.GetUserByEmail(email);

            return Ok(user);
        }

        [HttpGet]
        [Route("me")]
        public IActionResult GetUserWithJWT()
        {
            ClaimsPrincipal principal = HttpContext.User;

            if (principal.Identity.Name == null) { return BadRequest(); }

            if (Int32.TryParse(principal.Identity.Name, out int id))
            {
                var user = _repoWrapper.UsersRepository.GetUserById(id);
                if (user != null)
                {
                    return Ok(_repoWrapper.UsersRepository.GetUserById(id));
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddNewUser([FromBody] Usermodel usermodel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_repoWrapper.UsersRepository.GetUserByName(usermodel.Name) != null)
            {
                ModelState.AddModelError("name", "User name already in use");
                return BadRequest(ModelState);
            }

            if (_repoWrapper.UsersRepository.GetUserByEmail(usermodel.Email) != null)
            {
                ModelState.AddModelError("email", "Email address already in use");
                return BadRequest(ModelState);
            }

            usermodel.VerificationCode = Nanoid.Nanoid.Generate();
            _passwordService.CreateSalt(usermodel);
            _passwordService.HashPassword(usermodel);
            _repoWrapper.UsersRepository.AddUser(usermodel);
            MailRequest mail = _mailService.MakeVerificationMail(usermodel);
            _mailService.SendMailAsync(mail);
            _repoWrapper.Save();

            return Ok("User " + usermodel.Name + " has been created\n");
        }

        [HttpPatch]
        [Route("verify")]
        [AllowAnonymous]
        public IActionResult VerifyUserEmail()
        {
            string verificationCode = HttpContext.Request.Query["verificationcode"];

            if (String.IsNullOrEmpty(verificationCode)) return NotFound("Input can't be null");

            User user = _repoWrapper.UsersRepository.GetUserByVerificationCode(verificationCode);

            if (user == null) return NotFound();

            user.Verified = true;
            _repoWrapper.UsersRepository.EditUser(user);

            return Ok("Verification success");
        }

        [HttpPatch]
        [Route("{userId:int}")]
        public ActionResult<Usermodel> EditUserProfile(int userId, [FromBody] Usermodel usermodel)
        {
            var existingUserProfile = _repoWrapper.UsersRepository.GetUserById(userId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var checkIfNameExists = _repoWrapper.UsersRepository.GetUserByName(usermodel.Name);
            var checkIfEmailExists = _repoWrapper.UsersRepository.GetUserByEmail(usermodel.Email);

            if (checkIfNameExists != null && checkIfNameExists.Id != existingUserProfile.Id)
            {
                ModelState.AddModelError("name", "User name already in use");
                return BadRequest(ModelState);
            }
            else if (checkIfEmailExists != null && checkIfEmailExists.Id != existingUserProfile.Id)
            {
                ModelState.AddModelError("email", "This email address is already in use");
                return BadRequest(ModelState);
            }

            usermodel.Id = existingUserProfile.Id;
            _passwordService.CreateSalt(usermodel);
            _passwordService.HashPassword(usermodel);
            _repoWrapper.UsersRepository.EditUser(usermodel);
            _repoWrapper.Save();

            return Ok(usermodel);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            if (!_repoWrapper.UsersRepository.Exists(u => u.Id == id)) return NotFound("No user with id: " + id);
            _repoWrapper.UsersRepository.RemoveUser(id);
            _repoWrapper.Save();
            return Ok("User has been deleted");
        }

        [HttpDelete]
        [Route("{userName}")]
        public IActionResult DeleteUser(string userName)
        {
            if (!_repoWrapper.UsersRepository.Exists(u => u.Name == userName)) return NotFound("No user with this username " + userName);
            _repoWrapper.UsersRepository.RemoveUserByName(userName);
            _repoWrapper.Save();
            return Ok("User has been deleted");

        }

    }
}
