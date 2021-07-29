using System;
using System.Collections.Generic;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Database.Repositories
{
    public interface IHabitHistoryRepository : IRepositoryBase<History>
    {

        List<History> GetAllHistory();
        List<History> GetHistoryByUserId(int id);
        List<History> GetHistoryFromTimeSpan(int id, string startDate, string endDate);
        History GetOneHistoryById(int id);
        List<History> GetSevenDayHistory(int id);
        void AddHabitToHistory(History history);
        void RemoveHistory(int id);
        void EditHistory(History history);

    }
}
