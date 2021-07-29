using System;
using System.Collections.Generic;
using System.Linq;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Database.Repositories
{
    public class HabitHistoryRepository : RepositoryBase<History>, IHabitHistoryRepository
    {
        public HabitHistoryRepository(DatabaseContext context) : base(context) { }

        public List<History> GetAllHistory()
        {
            return FindAll().ToList();
        }
        public List<History> GetHistoryByUserId(int id)
        {
            return FindByCondition(h => h.OwnerId == id).ToList();
        }
        public List<History> GetHistoryFromTimeSpan(int id, string startDate, string endDate)
        {
            DateTime startDateTime = Convert.ToDateTime(startDate);
            DateTime endDateTime = Convert.ToDateTime(endDate);
            return FindByCondition(h => (h.OwnerId == id) && (h.HabitHistoryDate >= startDateTime) && (h.HabitHistoryDate <= endDateTime));
        }

        public History GetOneHistoryById(int id)
        {
            return GetSingle(id);
        }
        public List<History> GetSevenDayHistory(int id)
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.Subtract(TimeSpan.FromDays(7));
            return FindByCondition(h => (h.OwnerId == id) && h.HabitHistoryDate >= startDate && h.HabitHistoryDate <= endDate);
        }

        public void AddHabitToHistory(History history)
        {
            var habitResult = FindByCondition(h => h.Id == history.HabitId);
            Create(history);
        }

        public void RemoveHistory(int id)
        {
            History history = GetSingle(id);
            Delete(history);
        }

        public void EditHistory(History historyUpdate)
        {
            History history = new History
            {
                HabitId = historyUpdate.HabitId,
                OwnerId = historyUpdate.OwnerId,
                HabitHistoryDate = historyUpdate.HabitHistoryDate,
                HabitHistoryResult = historyUpdate.HabitHistoryResult,
                HabitHistoryNum = historyUpdate.HabitHistoryNum
            };
            Update(history);
        }
    }
}
