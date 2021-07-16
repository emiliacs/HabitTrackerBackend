using System.Collections.Generic;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Database.Repositories
{
    public interface IUsersRepository : IRepositoryBase<User>
    {
        void AddUser(User user);


        Task<List<User>> GetAllUsersAsync();

        List<User> GetAllUsers();

        User GetUserById(int id);

        User GetUserByName(string name);

        User GetUserByEmail(string email);

        User GetUserByEmailAndName(string email, string name);

        void RemoveUser(int id);

        void RemoveUserByName(string username);

        void EditUser(User user);

        User GetUserByVerificationCode(string verificationCode);
    }
}
