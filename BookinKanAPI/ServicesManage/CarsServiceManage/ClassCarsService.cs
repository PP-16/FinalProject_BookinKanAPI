using AutoMapper;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookinKanAPI.ServicesManage.CarsServiceManage
{
    public class ClassCarsService : IClassCarsService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ClassCarsService(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<string> CreateandUpdateClass(ClassCarsDTO classDto)
        {
            var Class = await _dataContext.ClassCars.AsNoTracking().FirstOrDefaultAsync(i=>i.ClassCarsId == classDto.ClassCarsId);
            var Classcar = _mapper.Map<ClassCars>(classDto);
            if (Class == null)
            {
                var car = await _dataContext.ClassCars.FirstOrDefaultAsync(n => n.ClassName == classDto.ClassName);
                if (car != null) { return "this Class has alredy"; }
                await _dataContext.ClassCars.AddAsync(Classcar);
            }
            else
            {
                _dataContext.ClassCars.Update(Classcar);
            }

            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't create this class";

            return null;       
         }

        public async Task DeleteClass(ClassCars classCars)
        {
             _dataContext.ClassCars.Remove(classCars);
             await _dataContext.SaveChangesAsync();
        }

        public async Task<List<ClassCars>> GetClasses()
        {
            return await _dataContext.ClassCars.OrderByDescending(i=>i.ClassCarsId).ToListAsync();
        }
        public async Task<ClassCars> GetByIdAsync(int id)
        {
            return await _dataContext.ClassCars.AsNoTracking().FirstOrDefaultAsync(p => p.ClassCarsId == id);
        }

        public async Task<List<ClassCars>> SearchClass(string classCar)
        {
            return await _dataContext.ClassCars.Where(n => n.ClassName.Contains(classCar)).ToListAsync();

        }
        //public async Task<string> ChangeIsuse(int Id, bool isuse)
        //{
        //    var checkIsuse = await _dataContext.ClassCars.FindAsync(Id);
        //    if (checkIsuse != null)
        //    {
        //        checkIsuse.isUse = isuse;
        //    }
        //    var result = await _dataContext.SaveChangesAsync();
        //    if (result <= 0) return "Can't Update Sattus";
        //    return null;
        //}
    }
}
