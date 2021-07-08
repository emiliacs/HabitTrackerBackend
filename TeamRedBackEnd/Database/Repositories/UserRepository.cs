﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.ViewModels;

namespace TeamRedBackEnd.Database.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DatabaseContext context;
        public UsersRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddUser(Usermodel usermodel)
        {
            User user = new User()
            {
                Name = usermodel.Name,
                Email = usermodel.Email,
                Password = usermodel.Password,
                Picture = usermodel.Picture,
                PublicProfile = usermodel.PublicProfile,
                FriendIds = usermodel.FriendIds,
                GroupIds = usermodel.GroupIds,
                Salt = usermodel.Salt,
                BytePassword = usermodel.BytePassword,
                VerificationCode = usermodel.VerificationCode
            };
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void AddUser(string name, string email, string password)
        {
            User user = new User()
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

        public void EditUser(Usermodel usermodel)
        {
            
            var existingUser = GetUser(usermodel.Id);
            existingUser.Name = usermodel.Name;
            existingUser.Email = usermodel.Email;
            existingUser.Password = usermodel.Password;
            existingUser.Picture = usermodel.Picture;
            existingUser.PublicProfile = usermodel.PublicProfile;
            existingUser.FriendIds = usermodel.FriendIds;
            existingUser.GroupIds = usermodel.GroupIds;
            existingUser.Salt = usermodel.Salt;
            existingUser.BytePassword = usermodel.BytePassword;
            context.SaveChanges();

           

        }

        public void EditUser(User newUser)
        {

            var existingUser = GetUser(newUser.Id);
            existingUser.Name = newUser.Name;
            existingUser.Email = newUser.Email;
            existingUser.Password = newUser.Password;
            existingUser.Picture = newUser.Picture;
            existingUser.PublicProfile = newUser.PublicProfile;
            existingUser.FriendIds = newUser.FriendIds;
            existingUser.GroupIds = newUser.GroupIds;
            existingUser.Salt = newUser.Salt;
            existingUser.BytePassword = newUser.BytePassword;
            existingUser.VerificationCode = newUser.VerificationCode;
            existingUser.Verified = newUser.Verified;
            context.SaveChanges();



        }

        public User GetUserByVerificationCode(string verificationCode)
        {
            return context.Users.Where(u => u.VerificationCode == verificationCode).FirstOrDefault();
        }
    }
}

