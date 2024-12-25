using BookinKanAPI.DTOs;
using BookinKanAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookinKanAPI.ServicesManage.SystemSettingServiceManage
{
    public interface ISystemSettingService
    {
        Task<string> CreateAndUpdateSystem(SystemSettingDTO settingDTO);
        Task<List<SystemSetting>> GetSystem();
        Task<List<SystemSetting>> GetSystemById(int Id);
        Task<object> DeleteImageSlide(int imgId);
        Task<string> DeleteSetting(int Id);
    }
}
