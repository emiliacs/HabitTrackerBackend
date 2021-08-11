using NUnit.Framework;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.Services;

namespace TeamRedBackEnd.Tests
{
    [TestFixture]
    public class PasswordServiceTests
    {

        User testUser = new User { Name = "TestUser", Email = "testuser@email.com", Password = "TestPassword1!" };

        [Test]
        public void CreateSalt_UsersSaltIsCreated_ReturnsNotNull()
        {
            var passwordService = new PasswordService();
            passwordService.CreateSalt(testUser);
            Assert.IsNotNull(testUser.Salt);
        }

        [Test]
        public void HashPassword_PasswordIsHashedRight_ReturnsAreEqual()
        {
            var passwordService = new PasswordService();
            var hashedPassword = passwordService.HashPassword(testUser);

            Assert.IsNotNull(testUser.BytePassword);
            Assert.AreEqual(testUser.BytePassword, hashedPassword);
        }

        [TestCase("Testuserpassword1.")]
        public void VerifyHash_UserCanLogInWitghRightPassword_ReturnsTrue(string testUserPassword)
        {
            User newTestUser = new User { Name = "Testuser", Email = "testuser@testing.com", Password = "Testuserpassword1." };

            var passwordService = new PasswordService();

            passwordService.CreateSalt(newTestUser);
            passwordService.HashPassword(newTestUser);
            var result = passwordService.VerifyHash(testUserPassword, newTestUser);
            Assert.IsTrue(result);
        }


        [Test]
        public void HashPassword_HashedPasswordsArenotEqual_ReturnsAreNotEqual()
        {
            User userOne = new User { Name = "TestuserOne", Email = "testuserOne@testing.com", Password = "Password" };
            User userTwo = new User { Name = "TestuserTwo", Email = "testuserOne@testing.com", Password = "Password" };
            var passwordService = new PasswordService();

            passwordService.CreateSalt(userOne);
            passwordService.CreateSalt(userTwo);
            var userOnehash = passwordService.HashPassword(userOne);
            var userTwohash = passwordService.HashPassword(userTwo);

            Assert.AreNotEqual(userTwohash, userOnehash);

        }





    }
}