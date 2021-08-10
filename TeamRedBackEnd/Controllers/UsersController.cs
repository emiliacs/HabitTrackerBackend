using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using AutoMapper;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.Database.Repositories;
using TeamRedBackEnd.DataTransferObject;


namespace TeamRedBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        readonly Services.PasswordService _passwordService;
        readonly Services.IMailService _mailService;
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;
        private readonly Services.CheckUserDataService _checkService;


        public UsersController(IRepositoryWrapper wrapper, Services.PasswordService passwordService, Services.IMailService mailService, IMapper mapper, Services.CheckUserDataService checkService)
        {
            _passwordService = passwordService;
            _mailService = mailService;
            _repoWrapper = wrapper;
            _mapper = mapper;
            _checkService = checkService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _repoWrapper.UsersRepository.GetAllUsers();
            return Ok(_mapper.Map<List<OtherUserDto>>(users));
        }

        [HttpGet]
        [Route("{userId:int}")]
        public IActionResult GetUserById(int userId)
        {
            if (_checkService.GetUserTokenId(HttpContext.User) != userId)
            {
                var otherUser = _repoWrapper.UsersRepository.GetUserById(userId);
                if (otherUser == null) return NotFound("User with ID: " + userId + " doesn't exist");
                var otherUserDto = _mapper.Map<OtherUserDto>(otherUser);
                return Ok(otherUserDto);
            }
            var user = _repoWrapper.UsersRepository.GetUserById(userId);

            if (user == null) return NotFound("User with ID: " + userId + " doesn't exist");

            var userdto = _mapper.Map<UserDto>(user);

            return Ok(userdto);
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

            var userDto = _mapper.Map<OtherUserDto>(user);

            return Ok(userDto);
        }

        [HttpGet]
        [Route("me")]
        public IActionResult GetUserWithJWT()
        {
            ClaimsPrincipal principal = HttpContext.User;

            int userId = _checkService.GetUserTokenId(principal);

            if (userId == 0) return NotFound();
            var user = _repoWrapper.UsersRepository.GetUserById(userId);

            if (user == null) return NotFound();
            
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddNewUser([FromBody] CreateUserDto usermodel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_repoWrapper.UsersRepository.GetUserByName(usermodel.Name) != null)
            {
                ModelState.AddModelError("name", "User name already in use");
                return Conflict(ModelState);
            }

            if (_repoWrapper.UsersRepository.GetUserByEmail(usermodel.Email) != null)
            {
                ModelState.AddModelError("email", "Email address already in use");
                return Conflict(ModelState);
            }

            var user = _mapper.Map<User>(usermodel);
            user.UpperEmail = user.Email.ToUpper();
            user.UpperName = user.Name.ToUpper();

            _passwordService.CreateSalt(user);
            _passwordService.HashPassword(user);

            user.VerificationCode = Nanoid.Nanoid.Generate();
            DataObjects.MailRequest mail = _mailService.MakeVerificationMail(user);
            _mailService.SendMailAsync(mail);

            _repoWrapper.UsersRepository.AddUser(user);

            if (!_repoWrapper.TryToSave()) return Conflict("User name or email address is already in use");

            return Ok("User " + usermodel.Name + " has been created\n");
        }

        [HttpPatch]
        [Route("verify")]
        [AllowAnonymous]
        public IActionResult VerifyUserEmail()
        {
            string verificationCode = HttpContext.Request.Query["verificationcode"];

            if (String.IsNullOrEmpty(verificationCode)) return NotFound("Input can't be null");

            int userId = _checkService.GetUserTokenId(HttpContext.User);

            User user = _repoWrapper.UsersRepository.GetSingle(u => u.Id == userId && u.VerificationCode == verificationCode);

            if (user == null) return NotFound();

            user.Verified = true;
            _repoWrapper.UsersRepository.Update(user);

            return Ok("Verification success");
        }

        [HttpPatch]
        [Route("{userId:int}")]
        public ActionResult<UserDto> EditUserProfile(int userId, [FromBody] EditUserDto editUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_checkService.GetUserTokenId(HttpContext.User) != userId) return Forbid();

            var existingUserProfile = _repoWrapper.UsersRepository.GetUserById(userId);

            var checkIfNameExists = _repoWrapper.UsersRepository.GetUserByName(editUserDto.Name);
            var checkIfEmailExists = _repoWrapper.UsersRepository.GetUserByEmail(editUserDto.Email);

            if (checkIfNameExists != null && checkIfNameExists.Id != existingUserProfile.Id)
            {
                ModelState.AddModelError("name", "User name already in use");
                return Conflict(ModelState);
            }
            else if (checkIfEmailExists != null && checkIfEmailExists.Id != existingUserProfile.Id)
            {
                ModelState.AddModelError("email", "This email address is already in use");
                return Conflict(ModelState);
            }

            _mapper.Map(editUserDto, existingUserProfile);

            _repoWrapper.UsersRepository.Update(existingUserProfile);
            _repoWrapper.Save();

            var userdto = _mapper.Map<UserDto>(existingUserProfile);

            return Ok(userdto);
        }

        [HttpPatch]
        [Route("{id:int}/changePassword")]
        public IActionResult ChangeUserPassword(int id, [FromBody] EditPasswordDto edit)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_checkService.GetUserTokenId(HttpContext.User) != id) return Forbid();

            var existingUserProfile = _repoWrapper.UsersRepository.GetUserById(id);

            if (existingUserProfile == null) return NotFound("User not found");

            if (!_passwordService.VerifyHash(edit.CurrentPassword, existingUserProfile)) return Conflict("Password incorrect");

            if (edit.NewPassword != edit.ConfrimPassword) return Conflict("Passwords don't match");

            existingUserProfile.Password = edit.NewPassword;

            _passwordService.CreateSalt(existingUserProfile);
            _passwordService.HashPassword(existingUserProfile);

            _repoWrapper.UsersRepository.Update(existingUserProfile);
            _repoWrapper.Save();

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            if (_checkService.GetUserTokenId(HttpContext.User) != id) return Forbid();

            if (!_repoWrapper.UsersRepository.Exists(u => u.Id == id)) return NotFound("No user with id: " + id);
            _repoWrapper.UsersRepository.RemoveUser(id);
            _repoWrapper.Save();
            return Ok("User has been deleted");
        }

    }
}
