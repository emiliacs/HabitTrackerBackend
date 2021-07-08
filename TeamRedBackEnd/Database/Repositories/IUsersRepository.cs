using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.ViewModels;

namespace TeamRedBackEnd.Database.Repositories
{
    public interface IUsersRepository
    {
        public User GetUser(int id);
        public User GetUser(string name);
        public User GetUserByVerificationCode(string verificationCode);

        public List<User> GetUsersWithIdArray(int[] idArray);

        public List<User> GetAllUsers();

        public void AddUser(string name, string email, string password);

        public void RemoveUser(int id);
        public void RemoveUser(User user);

        public void EditUser(Usermodel usermodel);
        public void EditUser(User newUser);
 
         void AddUser(Usermodel usermodel);
    }
}
