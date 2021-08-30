using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.DataTransferObject;
using TeamRedBackEnd.DataTransferObjects;

namespace TeamRedBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HabitsController : ControllerBase
    {
        private readonly Database.Repositories.IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;
        private readonly Services.CheckUserDataService _checkService;

        public HabitsController(Database.Repositories.IRepositoryWrapper wrapper, IMapper mapper, Services.CheckUserDataService checkService)
        {
            _repoWrapper = wrapper;
            _mapper = mapper;
            _checkService = checkService;
        }

        [HttpGet]
        public IActionResult GetAllHabits()
        {
            var userId = _checkService.GetUserTokenId(HttpContext.User);
            var habits = _repoWrapper.HabitRepository.GetById(userId);
            if (habits.Count == 0) return NotFound("No Habits Found");
            var habitsDto = _mapper.Map<List<HabitDto>>(habits);
            return Ok(habitsDto);
        }

        [HttpGet]
        [Route("{habitId:int}")]
        public IActionResult GetHabitById(int habitId)
        {
            if (!_repoWrapper.HabitRepository.Exists(h => h.Id == habitId)) return NotFound("No habit with this id found");
            var habit = _repoWrapper.HabitRepository.GetHabit(habitId);

            if (_checkService.GetUserTokenId(HttpContext.User) == habit.OwnerId) return Ok(_mapper.Map<HabitDto>(habit));
            if (habit.PublicHabit == false) return Forbid();
            var habitDto = _mapper.Map<HabitDto>(habit);
            return Ok(habitDto);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAllHabitsByHabitName()
        {
            string habitname = HttpContext.Request.Query["habitname"];
            if (String.IsNullOrEmpty(habitname)) return NotFound("Input shall not be empty");
            var habits = await _repoWrapper.HabitRepository.FindByConditionAsync(h => h.Name == habitname && h.PublicHabit == true);
            if (habits.Count == 0) return NotFound("No habits found");
            var habitDtos = _mapper.Map<List<HabitDto>>(habits);
            return Ok(habitDtos);
        }

        [HttpGet]
        [Route("user/{userId:int}")]
        public async Task<IActionResult> GetAllPrivateHabitsOfOwner(int userId)
        {
            var isOwnerIdValid = _repoWrapper.UsersRepository.Exists(u => u.Id == userId);
            if (!isOwnerIdValid) return NotFound("No habits found");

            int requestUserId = _checkService.GetUserTokenId(HttpContext.User);
            if (requestUserId != userId) return Forbid();

            var habits = await _repoWrapper.HabitRepository.FindByConditionAsync(h => h.OwnerId == userId && h.PublicHabit == false);

            if (habits == null) return NotFound("No habits found");

            var habitDtos = _mapper.Map<List<HabitDto>>(habits);
            return Ok(habitDtos);
        }

        [HttpGet]
        [Route("user/name")]
        public async Task<IActionResult> GetAllHPublicHabitsOfOwner()
        {
            string userName = HttpContext.Request.Query["userName"];
            if (String.IsNullOrEmpty(userName)) return NotFound("Input shall not be empty");

            var user = _repoWrapper.UsersRepository.GetUserByName(userName);

            var habits = await _repoWrapper.HabitRepository.FindByConditionAsync(h => (h.OwnerId == user.Id) && (h.PublicHabit == true));
            if (habits.Count == 0) return NotFound("No Habits Found");
            var habitsDto = _mapper.Map<List<HabitDto>>(habits);
            return Ok(habitsDto);
        }

        [HttpPost]
        public IActionResult AddNewHabit([FromBody] HabitDto habitModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (habitModel.OwnerId != _checkService.GetUserTokenId(HttpContext.User)) return Forbid();
            var doesUserExist = _repoWrapper.UsersRepository.Exists(u => habitModel.OwnerId == u.Id);
            if (!doesUserExist) return NotFound("No user found with this Id");
            Habit habit = _mapper.Map<Habit>(habitModel);
            _repoWrapper.HabitRepository.AddHabit(habit);
            _repoWrapper.Save();
            return Created(new Uri(Request.GetEncodedUrl() + "/" + habit.Id), habitModel);
        }

        [HttpDelete]
        [Route("{habitId:int}")]
        public IActionResult DeleteHabit(int habitId)
        {
            int requestUserId = _checkService.GetUserTokenId(HttpContext.User);
            if (!_repoWrapper.HabitRepository.Exists(h => h.Id == habitId && h.OwnerId == requestUserId)) return NotFound("No habit with this id");
            _repoWrapper.HabitRepository.RemoveHabit(habitId);
            _repoWrapper.Save();
            return Ok();
        }

        [HttpPatch]
        [Route("{habitId:int}")]
        public IActionResult EditHabit(int habitId, [FromBody] EditHabitDto editHabitDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingHabit = _repoWrapper.HabitRepository.GetHabit(habitId);
            int requestUserId = _checkService.GetUserTokenId(HttpContext.User);
            if (existingHabit == null || requestUserId != existingHabit.OwnerId) return NotFound("No habit with this id");


            if (_repoWrapper.UsersRepository.GetUserById(editHabitDto.OwnerId) == null)
            {
                ModelState.AddModelError("ownerId", "Invalid ownerId");
                return NotFound(ModelState);
            }
            _mapper.Map(editHabitDto, existingHabit);
            _repoWrapper.HabitRepository.Update(existingHabit);
            _repoWrapper.Save();

            var habitDto = _mapper.Map<HabitDto>(existingHabit);
            return Ok(habitDto);
        }
    }
}
