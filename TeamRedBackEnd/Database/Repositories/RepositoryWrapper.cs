
using System.Threading.Tasks;

namespace TeamRedBackEnd.Database.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DatabaseContext _databaseContext;
        
        private IUsersRepository _userRepository;
        private IHabitRepository _habitRepository;

        public RepositoryWrapper(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        

        public IUsersRepository UsersRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UsersRepository(_databaseContext);
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
