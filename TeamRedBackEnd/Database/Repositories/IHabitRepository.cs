using System.Collections.Generic;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Database.Repositories
{
    public interface IHabitRepository : IRepositoryBase<Habit>
    {
        
        List<Habit> GetAllHabits();
        List<Habit> GetHabitByUserId(int UserId);
        Task<List<Habit>> GetAllHabitsAsync();
        Task<List<Habit>> GetHabitByUserIdAsync(int UserId);
        Habit GetHabit(int id);

        void AddHabit(Habit habit);
        void EditHabit(Habit habitUpdate);

        void RemoveHabit(int id);
        void RemoveHabit(Habit habit);

         List<Habit> GetById(int userId);
    }
}
