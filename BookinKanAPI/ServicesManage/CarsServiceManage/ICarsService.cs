using BookinKanAPI.DTOs;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.CarsServiceManage
{
    public interface ICarsService
    {
        Task<List<Cars>> GetCars();
        Task<string> CreateandUpdateCars(CarsRequestDTO carsDto);
        Task DeleteCars(Cars Cars);
        Task<Cars> GetByIdAsync(int id);
        Task<List<Cars>> SearchCarsBrand(string CarName);
        Task<(string errorMessage, List<string> imageNames)> UploadImageAsync(IFormFileCollection formFiles);
        Task<object> DeleteImageCars(int imgId);
        Task<List<Cars>>GetCarForRent();
         Task<string> UpdateStatusCars(int ID, StatusCars newStatus);
        Task<string> UpdateClassCar(int ID, int ClassID);
        Task<string> UpdateStatusRentCars(int ID);
        Task<string> ChangeIsuse(int Id, bool isuse);

    }
}
