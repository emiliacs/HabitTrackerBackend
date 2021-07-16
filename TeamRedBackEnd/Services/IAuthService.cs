using TeamRedBackEnd.DataTransferObject;
namespace TeamRedBackEnd.Services
{
    public interface IAuthService
    {
        public AuthData GetAuthData(string id);
        public AuthData GetAuthData(string id, int lifespan);
    }
}
