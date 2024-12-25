using AutoMapper;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookinKanAPI.ServicesManage.RouteCarsServiceManage
{
    public class RouteCarsService:IRouteCarsService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public RouteCarsService(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<string> CreateandUpdateRouteCars(RouteCarsDTO routeDto)
        {
            var route = await _dataContext.RouteCars.AsNoTracking().FirstOrDefaultAsync(i => i.RouteCarsId == routeDto.RouteCarsId);
            var mappRoute = _mapper.Map<RouteCars>(routeDto);
            if(route == null)
            {
                var check = await _dataContext.RouteCars.AsNoTracking().FirstOrDefaultAsync(o => o.OriginName == routeDto.OriginName && o.DestinationName == routeDto.DestinationName);
                if (check != null) return "has this role";
                if (routeDto.OriginName == routeDto.DestinationName) return "this route it's same";
                mappRoute.isUse = true;
               await _dataContext.RouteCars.AddAsync(mappRoute);
            }
            else
            {
                _dataContext.RouteCars.Update(mappRoute);
            }
            var result =  await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Save";

            return null!;
        }

        public async Task DeleteRouteCars(RouteCars routeCars)
        {
            _dataContext.RouteCars.Remove(routeCars);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<RouteCars> GetByIdAsync(int id)
        {
            return await _dataContext.RouteCars.FirstOrDefaultAsync(i => i.RouteCarsId == id);
        }
          
        public async Task<List<RouteCars>> GetRouteCars()
        {
            return await _dataContext.RouteCars.OrderByDescending(i => i.RouteCarsId).ToListAsync();
        }

        public async Task<List<RouteCars>> SearchRouteCars(string RouteCars)
        {
           return  await _dataContext.RouteCars.Where(n => n.OriginName.Contains(RouteCars)|| n.DestinationName.Contains(RouteCars)).ToListAsync();
        }

        public async Task<string> ChangeIsuse(int Id, bool isuse)
        {
            var checkIsuse = await _dataContext.RouteCars.FindAsync(Id);
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
