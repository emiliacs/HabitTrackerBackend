using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.ViewModels;

namespace TeamRedBackEnd.Database.Repositories
{
    public interface IUsersRepository : IRepositoryBase<User>
    {
        void AddUser(User user);

        void AddUser(Usermodel userModel);

        Task<List<User>> GetAllUsersAsync();

        List<User> GetAllUsers();

        User GetUserById(int id);

        User GetUserByName(string name);

        User GetUserByEmail(string email);

        User GetUserByEmailAndName(string email, string name);

        void RemoveUser(int id);

        void RemoveUserByName(string username);

        void EditUser(Usermodel usermodel);

        void EditUser(User user);

        User GetUserByVerificationCode(string verificationCode);
    }
}
