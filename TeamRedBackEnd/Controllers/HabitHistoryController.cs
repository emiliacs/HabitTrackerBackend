using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _repoWrapper.HabitHistoryRepository.FindAllAsync();
            return Ok(history);
        }


        [HttpGet]
        [Route("{userId:int}")]
        public IActionResult GetSevenDayHistoryHistory(int userId)
        {

            var history = _repoWrapper.HabitHistoryRepository.GetSevenDayHistory(userId);
            return Ok(history);
        }


        [HttpGet]
        [Route("user/{userId:int}")]
        public IActionResult GetAllHistoryOfUser(int userId)
        {
            var habitHistory = _repoWrapper.HabitHistoryRepository.GetHistoryByUserId(userId);
            if (habitHistory == null) return NotFound();
            return Ok(habitHistory);
        }


        [HttpGet]
        [Route("search/{userId:int}")]
        public IActionResult GetHistoryFromTimeSpan(int userId)
        {
            string startDate = HttpContext.Request.Query["startdate"];
            string endDate = HttpContext.Request.Query["enddate"];
            var habitHistory = _repoWrapper.HabitHistoryRepository.GetHistoryFromTimeSpan(userId, startDate, endDate);
            if (habitHistory.Count == 0) return NotFound("No habit history found");
            return Ok(habitHistory);
        }


        [HttpPost]
        public IActionResult AddNewHabitHistory([FromBody] HabitHistoryDto habitHistoryDto)
        {
            History history = _mapper.Map<History>(habitHistoryDto);
            _repoWrapper.HabitHistoryRepository.AddHabitToHistory(history);
            _repoWrapper.Save();
            return Created(new Uri(Request.GetEncodedUrl() + "/" + history.Id), history);
        }

        [HttpPatch]
        public IActionResult EditHabitHIstory(int historyId, [FromBody] HabitHistoryDto editHistorytDto)
        {
            var existingHistory = _repoWrapper.HabitHistoryRepository.GetOneHistoryById(historyId);
            _mapper.Map(editHistorytDto, existingHistory);
            _repoWrapper.HabitHistoryRepository.Update(existingHistory);
            _repoWrapper.Save();
            return Ok(editHistorytDto);

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
        public IActionResult DeletAllHistoryOfOwner(int userId)
        {
            var ownersHistory = _repoWrapper.HabitHistoryRepository.FindByCondition(h => h.OwnerId == userId);
            if (ownersHistory == null) return NotFound(":");
            foreach (var history in ownersHistory) _repoWrapper.HabitHistoryRepository.Delete(history);
            _repoWrapper.Save();
            return Ok();
        }



    }
}
