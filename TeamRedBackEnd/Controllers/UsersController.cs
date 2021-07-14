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
        private IUsersRepository _userRepo;
        Services.PasswordService _passwordService;
        Services.IMailService _mailService;



        public UsersController(IUsersRepository userRepo, Services.PasswordService passwordService, Services.IMailService mailService)
        {
            _userRepo = userRepo;
            _passwordService = passwordService;
            _mailService = mailService;

        }


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userRepo.GetAllUsers());
        }


        [HttpGet]
        [Route("{userId:int}")]

        public IActionResult GetUserById(int userId)
        {

            var user = _userRepo.GetUser(userId);

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

            if (name != null && email != null) user = _userRepo.GetUserByEmailAndName(name, email);

            if (name != null && email == null) user = _userRepo.GetUserByName(name);

            if (name == null && email != null) user = _userRepo.GetUserByEmail(email);

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
                var user = _userRepo.GetUser(id);
                if (user != null)
                {
                    return Ok(_userRepo.GetUser(id));
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

            if (_userRepo.GetUserByName(usermodel.Name) != null)
            {
                ModelState.AddModelError("name", "User name already in use");
                return BadRequest(ModelState);
            }

            if (_userRepo.GetUserByEmail(usermodel.Email) != null)
            {
                ModelState.AddModelError("email", "Email address already in use");
                return BadRequest(ModelState);
            }

            usermodel.VerificationCode = Nanoid.Nanoid.Generate();

            _passwordService.CreateSalt(usermodel);

            _passwordService.HashPassword(usermodel);

            _userRepo.AddUser(usermodel);

            MailRequest mail = _mailService.MakeVerificationMail(usermodel);
            _mailService.SendMailAsync(mail);


            return Ok("User " + usermodel.Name + " has been created\n");
        }


        [HttpPatch]
        [Route("verify")]
        [AllowAnonymous]
        public IActionResult VerifyUserEmail()
        {
            string verificationCode = HttpContext.Request.Query["verificationcode"];

            if (String.IsNullOrEmpty(verificationCode)) return NotFound("Input can't be null");

            User user = _userRepo.GetUserByVerificationCode(verificationCode);

            if (user == null) return NotFound();

            user.Verified = true;
            _userRepo.EditUser(user);

            return Ok("Verification success");
        }


        [HttpPatch]
        [Route("{userId:int}")]
        public ActionResult<Usermodel> EditUserProfile(int userId, [FromBody] Usermodel usermodel)
        {
            var existingUserProfile = _userRepo.GetUser(userId);
            var checkIfNameExists = _userRepo.GetUserByName(usermodel.Name);
            var checkIfEmailExists = _userRepo.GetUserByEmail(usermodel.Email);


            if (!ModelState.IsValid) return BadRequest(ModelState);


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
            _userRepo.EditUser(usermodel);

            return Ok(usermodel);
        }


        [HttpDelete]
        [Route("{userId:int}")]
        public IActionResult DeleteUser(int userId)
        {
            var userToDelete = _userRepo.GetUser(userId);
            ObjectResult objectresult = NotFound("User with ID: " + userId + " doesn't exist");

            if (userToDelete != null)
            {
                objectresult = Ok("User has been deleted");
                _userRepo.RemoveUser(userToDelete);

            }

            return objectresult;

        }


        [HttpDelete]
        [Route("{userName}")]
        public IActionResult DeleteUser(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName))
            {
                return NotFound("Input can't be null");
            }
            var userToDelete = _userRepo.GetUserByName(userName);

            if (userToDelete == null)
            {
                return NotFound("No user found with this name: " + userName);
            }

            return Ok("User has been deleted");

        }

    }
}
