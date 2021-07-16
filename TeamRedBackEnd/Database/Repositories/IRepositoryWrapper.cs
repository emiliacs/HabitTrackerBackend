using System.Threading.Tasks;

namespace TeamRedBackEnd.Database.Repositories
{
    public interface IRepositoryWrapper
    {
        
        IUsersRepository UsersRepository { get; }
        IHabitRepository HabitRepository { get; }
        
        void Save();
        Task SaveAsync();
    }
}
