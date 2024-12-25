using AutoMapper;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookinKanAPI.ServicesManage.DriverServiceManage
{
    public class DriverService : IDriverService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DriverService(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<string> CreateandUpdateDrivers(DriverDTO driverDTO)
        {
            var route = await _dataContext.Drivers.AsNoTracking().FirstOrDefaultAsync(i => i.DriverId == driverDTO.DriverId);
            var mappDriver = _mapper.Map<Driver>(driverDTO);
            if (route == null)
            {
                var check = await _dataContext.Drivers.AsNoTracking().FirstOrDefaultAsync(n=>n.DriverName == driverDTO.DriverName);
                if (check != null) return "has this Driver";
                mappDriver.isUse = true;
                await _dataContext.Drivers.AddAsync(mappDriver);
            }
            else
            {

                _dataContext.Drivers.Update(mappDriver);
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Save";

            return null!;
        }

        public async Task DeleteCars(Driver driver)
        {
            _dataContext.Drivers.Remove(driver);
            await _dataContext.SaveChangesAsync(); 
              
        }

        public async Task<Driver> GetByIdAsync(int id)
        {
            return await _dataContext.Drivers.FirstOrDefaultAsync(i => i.DriverId == id);
        }

        public async Task<List<Driver>> GetDrivers()
        {
            return await _dataContext.Drivers.OrderByDescending(i => i.DriverId).ToListAsync();
        }

        public async Task<List<Driver>> SearchCarsDriver(string Name)
        {
            return await _dataContext.Drivers.Where(n => n.DriverName.Contains(Name)).ToListAsync();
        }

        public async Task<List<Driver>> searchDriverByCharges(int minCharges, int maxCharges)
        {
            return await _dataContext.Drivers .Where(c => c.Charges >= minCharges && c.Charges <= maxCharges).ToListAsync();
        }

        public async Task<string> ChangeIsuse(int Id, bool isuse)
        {
            var checkIsuse = await _dataContext.Drivers.FindAsync(Id);
            if (checkIsuse != null)
            {
                checkIsuse.isUse = isuse;
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";
            return null;
        }

        public async Task<string> UpdateStatusDriver(int ID,int newStatus)
        {
            var driver = await _dataContext.Drivers.FindAsync(ID);
            if (driver != null)
            {
                driver.StatusDriver = newStatus;
            }

            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";

            return null;
        }

    }
}
