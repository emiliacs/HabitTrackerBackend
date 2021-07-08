using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamRedBackEnd.Database.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DatabaseContext _databaseContext;
        
        private IUserRepository _userRepository;
        private IHabitRepository _habitRepository;

        public RepositoryWrapper(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_databaseContext);
                }
                return _userRepository;
            }
        }
       
        public IHabitRepository HabitRepository
        {
            get
            {
                if (_habitRepository == null)
                {
                    _habitRepository = new HabitRepository(_databaseContext);
                }
                return _habitRepository;
            }
        }
        public void Save()
        {
            _databaseContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}
