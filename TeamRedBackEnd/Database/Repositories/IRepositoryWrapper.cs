using System;
using System.Collections.Generic;
using System.Linq;
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
