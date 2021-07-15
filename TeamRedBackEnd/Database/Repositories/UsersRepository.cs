using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.ViewModels;

namespace TeamRedBackEnd.Database.Repositories
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(DatabaseContext context) : base(context) { }

        public void AddUser(User user)
        {
            Create(user);
        }

        public void AddUser(Usermodel userModel)
        {
            User user = new User()
            {
                Name = userModel.Name,
                Email = userModel.Email,
                Password = userModel.Password,
                Picture = userModel.Picture,
                PublicProfile = userModel.PublicProfile,
                FriendIds = userModel.FriendIds,
                GroupIds = userModel.GroupIds,
                Salt = userModel.Salt,
                BytePassword = userModel.BytePassword,
                VerificationCode = userModel.VerificationCode
            };
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


        public void EditUser(Usermodel usermodel)
        {
            var existingUser = GetUserById(usermodel.Id);
            existingUser.Name = usermodel.Name;
            existingUser.Email = usermodel.Email;
            existingUser.Password = usermodel.Password;
            existingUser.Picture = usermodel.Picture;
            existingUser.PublicProfile = usermodel.PublicProfile;
            existingUser.FriendIds = usermodel.FriendIds;
            existingUser.GroupIds = usermodel.GroupIds;
            existingUser.Salt = usermodel.Salt;
            existingUser.BytePassword = usermodel.BytePassword;
            existingUser.VerificationCode = usermodel.VerificationCode;
            Update(existingUser);
        }

        public void EditUser(User updateUser)
        {
            User user = new User
            {
                Id = updateUser.Id,
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

