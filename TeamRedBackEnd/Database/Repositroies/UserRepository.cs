using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Database.Repositroies
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext context;
        public UserRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddUser(User newUser)
        {
            Console.WriteLine(newUser.Id + " is id of new user");
            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public void AddUser(string name, string email, string password)
        {
            User user = new User ()
            {
                Name = name, 
                Email = email, 
                Password = password 
            };
            context.Users.Add(user);
            context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }


        public User GetUser(int id)
        {
            return context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUser(string name)
        {
            if (name.Contains("@")) 
            {
                return context.Users.Where(u => u.Email == name).FirstOrDefault();
            }
            return context.Users.Where(u => u.Name == name).FirstOrDefault();
        }

        public List<User> GetUsersWithIdArray(int[] idArray)
        {
            List<User> userList = new List<User>();
            for (int i = 0; i < idArray.Length; i++)
            {
                
                userList.Add(GetUser(idArray[i]));
            }
            return userList;
        }

        public void RemoveUser(int id)
        {
            context.Users.Remove(GetUser(id));
            context.SaveChanges();
        }

        public void RemoveUser(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public void UpdateUser(int id, User userInfo)
        {
            User user = GetUser(id);

            user.Name = userInfo.Name;
            user.Email = userInfo.Email;
            user.Password = userInfo.Password;
            user.Picture = userInfo.Picture;
            user.PublicProfile = userInfo.PublicProfile;
            user.FriendIds = userInfo.FriendIds;
            user.GroupIds = userInfo.GroupIds;
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
