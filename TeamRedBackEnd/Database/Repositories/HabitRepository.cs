using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Database.Repositories
{
    public class HabitRepository : RepositoryBase<Habit>, IHabitRepository
    {
        public HabitRepository(DatabaseContext context) : base(context){}

        public List<Habit> GetAllHabits()
        {
            return FindAll().ToList();
        }

        public List<Habit> GetHabitByUserId(int UserId)
        {
            return FindByCondition(h => h.OwnerId == UserId).ToList();
        }

        public async Task<List<Habit>> GetAllHabitsAsync()
        {
            return await FindAllAsync();
        }

        public async Task<List<Habit>> GetHabitByUserIdAsync(int UserId)
        {
            return await FindByConditionAsync(h => h.OwnerId == UserId);
        }

        public Habit GetHabit(int id)
        {
            return GetSingle(id);
        }

        public void AddHabit(Habit habit)
        {
            Create(habit);
        }

        public void EditHabit(Habit habitUpdate)
        {
            Habit habit = new Habit
            {
                Id = habitUpdate.Id,
                HabitId = habitUpdate.HabitId,
                OwnerId = habitUpdate.OwnerId,
                Name = habitUpdate.Name,
                Description = habitUpdate.Description,
                Reward = habitUpdate.Reward,
                StartDate = habitUpdate.StartDate,
                EndDate = habitUpdate.EndDate,
                TimesTodo = habitUpdate.TimesTodo,
                DayRepeat = habitUpdate.DayRepeat,
                TimeSpan = habitUpdate.TimeSpan,
                Category = habitUpdate.Category,
                ChosenWeekDays = habitUpdate.ChosenWeekDays,
                PublicHabit = habitUpdate.PublicHabit,
                Favorite = habitUpdate.Favorite
            };
            Update(habit);
        }

        public void RemoveHabit(int id)
        {
            Habit habit = GetSingle(id);
            Delete(habit);
        }

        public void RemoveHabit(Habit habit)
        {
            Delete(habit);
        }

        
    }
}
