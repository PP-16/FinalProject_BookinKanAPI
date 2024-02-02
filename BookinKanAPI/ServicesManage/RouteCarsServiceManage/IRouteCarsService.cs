using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.RouteCarsServiceManage
{
    public interface IRouteCarsService
    {
        Task<List<RouteCars>> GetRouteCars();
        Task<string> CreateandUpdateRouteCars(RouteCarsDTO routeDto);
        Task DeleteRouteCars(RouteCars routeCars);
        Task<RouteCars> GetByIdAsync(int id);
        Task<List<RouteCars>> SearchRouteCars(string RouteCars);
        Task<string> ChangeIsuse(int Id, bool isuse);
    }
}
