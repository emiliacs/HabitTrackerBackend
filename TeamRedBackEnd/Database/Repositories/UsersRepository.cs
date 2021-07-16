using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Database.Repositories
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(DatabaseContext context) : base(context) { }

        public void AddUser(User user)
        {
            Create(user);
        }

        public List<User> GetAllUsers()
        {
            return FindAll().ToList();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await FindAllAsync();
        }

        public User GetUserById(int id)
        {
            return GetSingle(id);
        }
        public User GetUserByName(string name)
        {
            return GetSingle(u => u.Name == name);
        }

        public User GetUserByEmail(string email)
        {
            return GetSingle(u => u.Email == email);
        }

        public User GetUserByEmailAndName(string email, string name)
        {
            return GetSingle(u => (u.Email == email) && (u.Name == name));
        }

        public void RemoveUser(int id)
        {
            User user = GetSingle(id);
            Delete(user);
        }

        public void RemoveUserByName(string username)
        {
            User user = GetSingle(u => u.Name == username);
            Delete(user);
        }

        public void EditUser(User updateUser)
        {
            User user = new User
            {
                Name = updateUser.Name,
                Email = updateUser.Email,
                Password = updateUser.Password,
                Picture = updateUser.Picture,
                PublicProfile = updateUser.PublicProfile,
                FriendIds = updateUser.FriendIds,
                GroupIds = updateUser.GroupIds,
                Salt = updateUser.Salt,
                BytePassword = updateUser.BytePassword
            };
            Update(user);
        }

        public User GetUserByVerificationCode(string verificationCode)
        {
            return GetSingle(u => u.VerificationCode == verificationCode);
        }
    }
}

