using BookinKanAPI.Data;
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

        Task<object> createAndUpdateRole(RoleDTO roleDTO);
        Task<object> getRole();
        Task<string> ChangeIsuse(int Id, bool isuse);
        //Task<string> ChangeIsuseRole(int Id, bool isuse);
        Task<object> ChangeRole(int PassId, int RoleId);
        Task<List<Passenger>> GetAdmin();
        Task<object> RegisterAdmin(RegisterDTO request);
        Task DeleteRole(Role role);
        Task<Role> GetRoleByIdAsync(int id);
        Task<object> UploadImageUser(UploadImageDTO uploadImageDTO);


    }
}
