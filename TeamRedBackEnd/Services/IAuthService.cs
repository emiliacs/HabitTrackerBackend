

namespace TeamRedBackEnd.Services
{
    public interface IAuthService
    {
        public ViewModels.AuthData GetAuthData(string id);
        public ViewModels.AuthData GetAuthData(string id, int lifespan);
        public bool VerifyPassword(string actualPassword, string hashedPassword);
    }
}
