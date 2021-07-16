using Konscious.Security.Cryptography;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Services
{
    public class PasswordService
    {

        public byte[] CreateSalt(User user)
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);

            return user.Salt = buffer;
        }

        public byte[] HashPassword(User user)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(user.Password));

            argon2.Salt = user.Salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 1024;
            user.BytePassword = argon2.GetBytes(16);

            string hashToString = Convert.ToBase64String(user.BytePassword);
            user.Password = hashToString;

            return user.BytePassword;
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
