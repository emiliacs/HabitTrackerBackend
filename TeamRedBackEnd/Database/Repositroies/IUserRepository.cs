using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;


namespace TeamRedBackEnd.Database.Repositroies
{
    public interface IUserRepository
    {
        public User GetUser(int id);
        public User GetUser(string name);

        public List<User> GetUsersWithIdArray(int[] idArray);

        public List<User> GetAllUsers();

        public void AddUser(User newUser);
        public void AddUser(string name, string email, string password);

        public void RemoveUser(int id);
        public void RemoveUser(User user);

        public void UpdateUser(int id, User userInfo);


    }
}
