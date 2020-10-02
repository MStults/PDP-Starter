using System.Threading.Tasks;
using PDP.Web.API.Dtos.User;

namespace PDP.Web.API.Services.AuthService
{
    public interface IAuthService
    {
        Task<Response<string>> Register(UserRegisterDto request);

        Task<Response<string>> Login(UserLoginDto request);

        Task<bool> UserExist(string username); 
    }
}