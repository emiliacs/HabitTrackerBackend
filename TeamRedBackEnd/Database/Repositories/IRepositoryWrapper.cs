using System.Threading.Tasks;

namespace TeamRedBackEnd.Database.Repositories
{
    public interface IRepositoryWrapper
    {
        IUsersRepository UsersRepository { get; }
        IHabitRepository HabitRepository { get; }
        IHabitHistoryRepository HabitHistoryRepository { get; }
        void Save();
        bool TryToSave();
        Task SaveAsync();
    }
}
