using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Services
{
    public class PasswordService
    {

        public byte[] CreateSalt(ViewModels.Usermodel usermodel)
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);

            return usermodel.Salt = buffer;
        }

        public byte[] HashPassword(ViewModels.Usermodel usermodel)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(usermodel.Password));

            argon2.Salt = usermodel.Salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 1024;
            usermodel.BytePassword = argon2.GetBytes(16);

            string hashToString = Convert.ToBase64String(usermodel.BytePassword);
            usermodel.Password = hashToString;

            return usermodel.BytePassword;
        }


        public bool VerifyHash(string password, User user)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = user.Salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 1024;
            var newhash = argon2.GetBytes(16);

            return user.BytePassword.SequenceEqual(newhash);
        }

    }
}
