using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace TeamRedBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private readonly Database.Repositories.IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;

        public HabitsController(Database.Repositories.IRepositoryWrapper wrapper, IMapper mapper)
        {
            _repoWrapper = wrapper;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHabits()
        {
            var habits = await _repoWrapper.HabitRepository.FindAllAsync();
            if (habits == null) return NotFound("No Habits Found");
            var habitsDto = _mapper.Map<List<DataTransferObject.HabitDto>>(habits); 
            return Ok(habitsDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetHabit(int id) 
        {
            if (!_repoWrapper.HabitRepository.Exists(h => h.Id == id)) return NotFound("no habit with this id found");

            var habit = _repoWrapper.HabitRepository.GetHabit(id);
            var habitDto = _mapper.Map<DataTransferObject.HabitDto>(habit);
            return Ok(habitDto);
        }

        [HttpGet]
        [Route("user/{userId:int}")]
        public async Task<IActionResult> GetAllHabitsOfOwner(int userId)
        {
            var habits = await _repoWrapper.HabitRepository.FindByConditionAsync(h => h.OwnerId == userId);
            if (habits == null) return NotFound("No habits found");
            var habitDtos = _mapper.Map < List<DataTransferObject.HabitDto> >(habits);
            return Ok(habitDtos);
        }

        [HttpPost]
        public IActionResult AddNewHabit([FromBody]DataTransferObject.HabitDto habitModel) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            Database.Models.Habit habit = _mapper.Map<Database.Models.Habit>(habitModel);
            _repoWrapper.HabitRepository.AddHabit(habit);
            _repoWrapper.Save();
            return Ok("New Habit added");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteHabit(int id)
        {
            if (!_repoWrapper.HabitRepository.Exists(h => h.Id == id)) return NotFound("No habit with this id");
            _repoWrapper.HabitRepository.RemoveHabit(id);
            _repoWrapper.Save();
            return Ok();
        }

    }
}
