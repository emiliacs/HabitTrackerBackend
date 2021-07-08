using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.Database.Repositories;
using TeamRedBackEnd.ViewModels;

namespace TeamRedBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

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
        [Route("{userName}")]
        [AllowAnonymous]
        public IActionResult GetUserByNameOrEmail(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return NotFound("Input can't be null");
            }
            var user = _userRepo.GetUser(userName);

            return user switch
            {
                null => NotFound("No user found with this name: " + userName),
                _ => Ok(user),
            };
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddNewUser([FromBody] Usermodel usermodel)
        {

            _passwordService.CreateSalt(usermodel);

            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (_userRepo.GetUser(usermodel.Name) != null)
            {
                ModelState.AddModelError("name", "User name already in use");
                return BadRequest(ModelState);
            }

            if (_userRepo.GetUser(usermodel.Email) != null)
            {
                ModelState.AddModelError("email", "Email address already in use");
                return BadRequest(ModelState);
            }

            usermodel.VerificationCode = Nanoid.Nanoid.Generate();

            _passwordService.HashPassword(usermodel);

            _userRepo.AddUser(usermodel);

            MailRequest mail = _mailService.MakeVerificationMail(usermodel);
            _mailService.SendMailAsync(mail);


            return Ok("User " + usermodel.Name + " has been created\n");
        }


        [HttpPatch]
        [Route("{VerificationLinkCode}")]
        public IActionResult VerifyUser([FromBody] string VerificationLinkCode)
        {
            User user = _userRepo.GetUserByVerificationCode(VerificationLinkCode);
            if (user == null) return BadRequest("Invalid verification link");
            user.Verified = true;
            _userRepo.EditUser(user);
            return Ok(user);
        }


        [HttpPatch]
        [Route("{userId:int}")]
        public ActionResult<Usermodel> EditUserProfile(int userId, [FromBody] Usermodel usermodel)
        {
            var existingUserProfile = _userRepo.GetUser(userId);
            var checkIfNameExists = _userRepo.GetUser(usermodel.Name);
            var checkIfEmailExists = _userRepo.GetUser(usermodel.Email);
            _passwordService.CreateSalt(usermodel);

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
            var userToDelete = _userRepo.GetUser(userName);

            switch (userToDelete)
            {
                case null:
                    return NotFound("No user found with this name: " + userName);

                default:
                    _userRepo.RemoveUser(userToDelete);
                    return Ok("User has been deleted");
            }
        }

    }
}
