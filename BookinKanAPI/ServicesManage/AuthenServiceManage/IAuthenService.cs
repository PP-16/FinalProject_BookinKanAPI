using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.AuthenServiceManage
{
    public interface IAuthenService
    {
        Task<List<Passenger>> GetAllAccounts();

        Task<object> Login(LoginDTO request);

        Task<object> Register(RegisterDTO request);

        Task<string> ChangePassword(string NewPass,string checkNewPass);

        Task<object> ValidateToken(string token);
        Task<string> CheckOldPass(string oldpass);

        Task<object> createRole(string rolename, string rolenameTH);
        Task<object> getRole();
        Task<string> ChangeIsuse(int Id, bool isuse);
        Task<string> ChangeIsuseRole(int Id, bool isuse);
        Task<object> ChangeRole(int PassId, int RoleId);
    }
}
