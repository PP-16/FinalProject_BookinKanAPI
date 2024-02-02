using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.DriverServiceManage
{
    public interface IDriverService
    {
        Task<List<Driver>> GetDrivers();
        Task<string> CreateandUpdateDrivers(DriverDTO driverDTO);
        Task DeleteCars(Driver driver);
        Task<Driver> GetByIdAsync(int id);
        Task<List<Driver>> SearchCarsDriver(string Name);
        Task<List<Driver>> searchDriverByCharges(int minCharges, int maxCharges);
        Task<string> ChangeIsuse(int Id, bool isuse);
    }
}
