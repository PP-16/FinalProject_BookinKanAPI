using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.CarsServiceManage
{
    public interface IClassCarsService
    {
        Task<List<ClassCars>> GetClasses();
        Task<string> CreateandUpdateClass(ClassCarsDTO classDto);
        Task DeleteClass(ClassCars classCars);
        Task<ClassCars> GetByIdAsync(int id);
        Task<List<ClassCars>> SearchClass(string classCar);
        Task<string> ChangeIsuse(int Id, bool isuse);
    }
}
