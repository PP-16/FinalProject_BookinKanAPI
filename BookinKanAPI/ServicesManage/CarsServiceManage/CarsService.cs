using AutoMapper;
using Azure.Core;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs;
using BookinKanAPI.Models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;

namespace BookinKanAPI.ServicesManage.CarsServiceManage
{
    public class CarsService : ICarsService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IImageCarsService _imageCarsService;

        public CarsService(DataContext dataContext, IMapper mapper, IImageCarsService imageCarsService)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _imageCarsService = imageCarsService;
        }
        public async Task<string> CreateandUpdateCars(CarsRequestDTO carsDto)
        {
            var cars = await _dataContext.Cars.AsNoTracking().Include(c => c.ClassCars).FirstOrDefaultAsync(i => i.CarsId == carsDto.CarsId);
            var mappCars = _mapper.Map<Cars>(carsDto);

            (string errorMessage, List<string> imageNames) = await UploadImageAsync(carsDto.ImageCars);
            if (!string.IsNullOrEmpty(errorMessage)) return errorMessage;


            if (cars == null)
            {
                var Class = await _dataContext.ClassCars.FirstOrDefaultAsync(c => c.ClassCarsId == carsDto.ClasscarsId);
                if (Class == null) return "Can't find Class";
                var carNum = await _dataContext.Cars.FirstOrDefaultAsync(c => c.CarRegistrationNumber == carsDto.CarRegistrationNumber);
                if (carNum != null) return "have this car";


                await _dataContext.Cars.AddAsync(mappCars);
                await _dataContext.SaveChangesAsync();
                //จัดการไฟล์ในฐานข้อมูล
                if (imageNames.Count > 0)
                {
                    var images = new List<ImageCars>();
                    foreach (var image in imageNames)
                    {
                        images.Add(new ImageCars
                        {
                            CarsId = mappCars.CarsId,
                            Image = image
                        }); 
                    }
                    await _dataContext.ImageCars.AddRangeAsync(images);
                }
            }
            else 
            {
                _dataContext.Cars.Update(mappCars);
                //ตรวจสอบและจัดการกับไฟล์ที่ส่งเข้ามาใหม่
                if (imageNames.Count > 0)
                {
                    var images = new List<ImageCars>();
                    foreach (var image in imageNames)
                    {
                        images.Add(new ImageCars
                        {
                            CarsId = mappCars.CarsId,
                            Image = image
                        });
                    }
                    //ลบไฟล์เดิม
                    var oldImages = await _dataContext.ImageCars.Where(p => p.CarsId == mappCars.CarsId).ToListAsync();
                    if (oldImages != null)
                    {
                        //ลบไฟล์ใน database
                        _dataContext.ImageCars.RemoveRange(oldImages);
                        //ลบไฟล์ในโฟลเดอร์

                        var files = oldImages.Select(p => p.Image).ToList();
                        await _imageCarsService.DeleteFileImages(files);

                    }
                    //ใส่ไฟล์เข้าไปใหม่
                    await _dataContext.ImageCars.AddRangeAsync(images);
                }
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't create this cars";
            return null!;
        }

        public async Task DeleteCars(Cars Cars)
        {
            _dataContext.Cars.Remove(Cars);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Cars> GetByIdAsync(int id)
        {
            return await _dataContext.Cars.Include(i=>i.ImageCars).AsNoTracking().FirstOrDefaultAsync(p => p.CarsId == id);
        }
        public async Task<List<Cars>> GetCarForRent()
        {
            return await _dataContext.Cars.Include(c=>c.ClassCars).Include(i=>i.ImageCars).Where(c => c.CategoryCar == CategoryCars.ForRent).ToListAsync();
        }
        public async Task<List<Cars>> GetCars()
        {
            return await _dataContext.Cars.Include(c => c.ClassCars).Include(i => i.ImageCars).OrderByDescending(i => i.CarsId).ToListAsync();
        }

        public async Task<List<Cars>> SearchCarsBrand(string CarName)
        {
            return await _dataContext.Cars.Include(c => c.ClassCars).Where(n => n.CarBrand.Contains(CarName)).ToListAsync();
        }

        public async Task<string> UpdateClassCar(int ID, int ClassID)
        {
            var classcar = await _dataContext.ClassCars.FirstOrDefaultAsync(c => c.ClassCarsId == ClassID);
            if (classcar == null) return "Can't found this class";

            var car = await _dataContext.Cars.FindAsync(ID);
            if(car != null)
            {
                car.ClassCarsId = ClassID;
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";

            return null;
        }

        public async Task<string> UpdateStatusCars(int ID, StatusCars newStatus)
        {
            var cars = await _dataContext.Cars.FindAsync(ID);
            if (cars != null)
            {
                cars.StatusCar = newStatus;
            }

            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";

            return null;
        }

        public async Task<string> UpdateStatusRentCars(int ID)
        {
            var cars = await _dataContext.Cars.FindAsync(ID);
            var order = await _dataContext.OrderRentItems.FirstOrDefaultAsync(c => c.CarsId == ID);
            if (cars != null)
            {
                DateTime currentDate = DateTime.Now;
                if (order != null && currentDate >= order.DateTimePickup && currentDate <= order.DateTimeReturn)
                {
                    cars.StatusCar = StatusCars.Rented;
                }
                else
                {
                    cars.StatusCar = StatusCars.Empty;
                }
               
            }

            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";

            return null;
        }

        public async Task<(string errorMessage, List<string> imageNames)> UploadImageAsync(IFormFileCollection formFiles)
        {
            var errorMessege = string.Empty;
            var ImgName = new List<string>();

            if (_imageCarsService.IsUpload(formFiles))
            {
                errorMessege = _imageCarsService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessege))
                {
                    ImgName = await _imageCarsService.UploadImages(formFiles);
                }
            }
            return (errorMessege, ImgName);
        }
        public async Task<string> ChangeIsuse(int Id, bool isuse)
        {
            var checkIsuse = await _dataContext.Cars.FindAsync(Id);
            if (checkIsuse != null)
            {
                checkIsuse.isUse = isuse;
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";
            return null;
        }


    }
}
