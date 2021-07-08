using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamRedBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private Database.Repositories.IRepositoryWrapper _repoWrapper;

        public HabitsController(Database.Repositories.IRepositoryWrapper wrapper)
        {
            _repoWrapper = wrapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHabits()
        {
            var habits = await _repoWrapper.HabitRepository.FindAllAsync();
            return Ok(habits);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetHabit(int id) 
        {
            if (!_repoWrapper.HabitRepository.Exists(h => h.Id == id)) return BadRequest("no habit with this id found");
            return Ok(_repoWrapper.HabitRepository.GetHabit(id));
        }

        [HttpGet]
        [Route("user/{userId:int}")]
        public async Task<IActionResult> GetAllHabitsOfOwner(int userId)
        {
            var habits = await _repoWrapper.HabitRepository.FindByConditionAsync(h => h.OwnerId == userId);
            return Ok(habits);
        }

        [HttpPost]
        public IActionResult AddNewHabit([FromBody]ViewModels.HabitModel habitModel) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Database.Models.Habit habit = new Database.Models.Habit() {
                HabitId = habitModel.HabitId,
                OwnerId = habitModel.OwnerId,
                Name = habitModel.Name
            };
            _repoWrapper.HabitRepository.AddHabit(habit);
            _repoWrapper.Save();
            return Ok("New Habit added");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteHabit(int id)
        {
            if (!_repoWrapper.HabitRepository.Exists(h => h.Id == id)) return BadRequest("No habit with this id");
            _repoWrapper.HabitRepository.RemoveHabit(id);
            _repoWrapper.Save();
            return Ok();
        }

    }
}
