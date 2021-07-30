using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.DataTransferObjects;

namespace TeamRedBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HabitHistoryController : ControllerBase
    {
        private readonly Database.Repositories.IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;

        public HabitHistoryController(Database.Repositories.IRepositoryWrapper wrapper, IMapper mapper)
        {
            _repoWrapper = wrapper;
            _mapper = mapper;
        }

        private ObjectResult CheckIfHistoryExists(List<History> habitHistory)
        {
            if (habitHistory.Count == 0) return NotFound("No history found");
            var historyDtos = _mapper.Map<List<HabitHistoryDto>>(habitHistory);
            return Ok(historyDtos);
        }

        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            var habitHistory = await _repoWrapper.HabitHistoryRepository.FindAllAsync();
            return CheckIfHistoryExists(habitHistory);
        }

        [HttpGet]
        [Route("habit/{habitId}")]
        public async Task<IActionResult> GetAllHistoryOfHabit(int habitId)
        {
            var habitHistory = await _repoWrapper.HabitHistoryRepository.FindByConditionAsync(h => h.HabitId == habitId);
            return CheckIfHistoryExists(habitHistory);
        }

        [HttpGet]
        [Route("user/{userId:int}/sevendayhistory")]
        public IActionResult GetSevenDayHistory(int userId)
        {
            var habitHistory = _repoWrapper.HabitHistoryRepository.GetSevenDayHistory(userId);
            return CheckIfHistoryExists(habitHistory);
        }

        [HttpGet]
        [Route("user/{userId:int}/search")]
        public IActionResult GetAllHistoryOfUser(int userId)
        {
            var habitHistory = _repoWrapper.HabitHistoryRepository.GetHistoryByUserId(userId);
            return CheckIfHistoryExists(habitHistory);
        }

        [HttpGet]
        [Route("search/{userId:int}")]
        public IActionResult GetHistoryFromTimeSpan(int userId)
        {
            string startDate = HttpContext.Request.Query["startdate"];
            string endDate = HttpContext.Request.Query["enddate"];
            if (String.IsNullOrEmpty(startDate) && String.IsNullOrEmpty(endDate)) return NotFound("Input can not be empty");
            var habitHistory = _repoWrapper.HabitHistoryRepository.GetHistoryFromTimeSpan(userId, startDate, endDate);
            return CheckIfHistoryExists(habitHistory);
        }

        [HttpPost]
        public IActionResult AddNewHabitHistory([FromBody] HabitHistoryDto habitHistoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var habit = _repoWrapper.HabitRepository.GetHabit(habitHistoryDto.HabitId);

            if (habit == null) return NotFound("Invalid habitId");

            History history = _mapper.Map<History>(habitHistoryDto);

            history.OwnerId = habit.OwnerId;

            if (habit.EndDate < DateTime.Now) history.HabitHistoryResult = true;

            _repoWrapper.HabitHistoryRepository.AddHabitToHistory(history);
            _repoWrapper.Save();
            return Created(new Uri(Request.GetEncodedUrl() + "/" + history.Id), history);
        }

        [HttpPatch]
        [Route("{historyId:int}")]
        public IActionResult EditHabitHIstory(int historyId, [FromBody] EditHistoryDto editHistorytDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingHistory = _repoWrapper.HabitHistoryRepository.GetOneHistoryById(historyId);

            if (existingHistory == null) return NotFound("No history found with this id");

            var habit = _repoWrapper.HabitRepository.GetHabit(editHistorytDto.HabitId);

            if (habit == null) return NotFound("Invalid habitId");

            if (habit.EndDate < DateTime.Now) existingHistory.HabitHistoryResult = true;

            editHistorytDto.OwnerId = habit.OwnerId;
            editHistorytDto.HabitHistoryDate = existingHistory.HabitHistoryDate;
            _mapper.Map(editHistorytDto, existingHistory);
            _repoWrapper.HabitHistoryRepository.Update(existingHistory);
            _repoWrapper.Save();
            var habitDto = _mapper.Map<EditHistoryDto>(existingHistory);
            return Ok(habitDto);
        }

        [HttpDelete]
        [Route("{historyId:int}")]
        public IActionResult DeleteOneHabitHistory(int historyId)
        {
            if (!_repoWrapper.HabitHistoryRepository.Exists(h => h.Id == historyId)) return NotFound("No history found with this id");
            _repoWrapper.HabitHistoryRepository.RemoveHistory(historyId);
            _repoWrapper.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("user/{ownerId:int}")]
        public IActionResult DeleteAllHistoryOfOwner(int ownerId)
        {
            var ownersHistory = _repoWrapper.HabitHistoryRepository.FindByCondition(h => h.OwnerId == ownerId);
            if (ownersHistory == null) return NotFound("No history found");
            foreach (var history in ownersHistory) _repoWrapper.HabitHistoryRepository.Delete(history);
            _repoWrapper.Save();
            return Ok();
        }
    }
}
