using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.Database.Repositroies;
using TeamRedBackEnd.ViewModels;

namespace TeamRedBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepo;
        Services.PasswordService _passwordService;
        Services.IMailService _mailService;

        public UserController(IUserRepository userRepo, Services.PasswordService passwordService, Services.IMailService mailService)
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
        [Route("get/{id:int}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepo.GetUser(id);
            if (user == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = "User with ID: " + id + " doesn't exist"
                };

                return NotFound(response.ReasonPhrase);
            }
            return Ok(user);
        }


        [HttpGet]
        [Route("get/{name}")]
        public IActionResult GetUserByNameOrEmail(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return NotFound("Input can't be null");
            }

            else
            {
                try
                {
                    var user = _userRepo.GetUser(name);
                    return Ok(user);
                }
                catch
                {
                    return NotFound("No user found with this name: " + name);
                }
            }
        }


        [HttpPost]
        [Route("create")]
        public IActionResult AddNewUser([FromBody] Usermodel usermodel)
        {

            _passwordService.CreateSalt(usermodel);

            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (_userRepo.GetUser(usermodel.Name) != null)
            {
                ModelState.AddModelError("name", "User name already in use");
                return BadRequest(ModelState);
            }

            else if (_userRepo.GetUser(usermodel.Email) != null)
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


        [HttpGet]
        [Route("{VerificationLinkCode}")]
        public IActionResult VerifyUser(string VerificationLinkCode)
        {
            User user = _userRepo.GetUserByVerificationCode(VerificationLinkCode);
            if (user == null) return BadRequest("Invalid verification link");
            user.Verified = true;
            _userRepo.EditUser(user);
            
            return Ok(user);
        }


        [HttpPatch]
        [Route("edit/{id:int}")]
        public ActionResult<Usermodel> EditUserProfile(int id, [FromBody] Usermodel usermodel)
        {
            var existingUserProfile = _userRepo.GetUser(id);

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


            return Ok("User has been edited");
        }


        [HttpDelete]
        [Route("delete/{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var userToDelete = _userRepo.GetUser(id);
                _userRepo.RemoveUser(userToDelete);
                return Ok("User has been deleted");
            }
            catch
            {
                return NotFound("User with ID: " + id + " doesn't exist");
            }
        }


        [HttpDelete]
        [Route("delete/{name}")]
        public IActionResult DeleteUser(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return NotFound("Input can't be null");
            }

            else
            {
                try
                {
                    var userToDelete = _userRepo.GetUser(name);
                    _userRepo.RemoveUser(userToDelete);
                    return Ok("User has been deleted");
                }
                catch
                {
                    return NotFound("No user found with this name: " + name);
                }
            }
        }

    }
}
