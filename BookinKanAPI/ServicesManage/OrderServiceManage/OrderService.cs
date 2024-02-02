 using AutoMapper;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookinKanAPI.ServicesManage.OrderServiceManage
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext  ;

        public OrderService(DataContext dataContext,IMapper mapper,IHttpContextAccessor httpContext)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        private string GetUser() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Email);

        //public async Task<string> CreateAndUpdateOrders(OrderRentDTO rentDTO)
        //{
        //    var checkOrder = await _dataContext.OrderRents.AsNoTracking().Include(u=>u.Passenger).FirstOrDefaultAsync(i => i.OrderRentId == rentDTO.OrderRentId);

        //    var mappOrder = _mapper.Map<OrderRent>(rentDTO);

        //    if(checkOrder == null)
        //    {
        //        await _dataContext.OrderRents.AddAsync(mappOrder);
        //    }
        //    else
        //    {
        //        _dataContext.OrderRents.Update(mappOrder);
        //    }
        //    var result = await _dataContext.SaveChangesAsync();
        //    if (result <= 0) return "can't Save DB";
        //    return null;
            
        //}

        //public async Task<string> CreateAndUpdateOrdersItems(OrderRentItemDTO rentItemDTO)
        //{
        //    var checkItem = await _dataContext.OrderRentItems
        //        .Include(c => c.Cars).ThenInclude(c=>c.ClassCars)
        //        .Include(d => d.Driver)
        //        .Include(o => o.OrderRent)
        //        .FirstOrDefaultAsync(i => i.OrderRentItemId == rentItemDTO.OrderRentItemId);
        //    var mappItem = _mapper.Map<OrderRentItem>(rentItemDTO);
            

        //    if(checkItem == null)
        //    {
        //        var cars = await _dataContext.Cars.Include(c => c.ClassCars).FirstOrDefaultAsync(i => i.CarsId == rentItemDTO.CarsId);
        //        var price = cars.ClassCars.Price;
        //        var item = new OrderRentItem()
        //        {
        //            CarsId = rentItemDTO.CarsId,
        //            CarsPrice = price,
        //            Quantity = rentItemDTO.Quantity,
        //            DateTimePickup = rentItemDTO.DateTimePickup,
        //            DateTimeReturn = rentItemDTO.DateTimeReturn,
        //            DriverId = rentItemDTO.DriverId,
        //            OrderRentId = rentItemDTO.OrderRentId,
        //            PlacePickup = rentItemDTO.PlacePickup,
        //            PlaceReturn = rentItemDTO.PlaceReturn,

        //        };
        //        await _dataContext.OrderRentItems.AddAsync(item);
        //    }
        //    else
        //    {
        //        _dataContext.OrderRentItems.Update(mappItem);
        //    }
        //    var result = await _dataContext.SaveChangesAsync();
        //    if (result <= 0) return "Can't save DB";
        //    return null;
        //}

        public async Task<string> CreateOrderRent(OrderRentItemDTO request)
        {
           // var user = await _dataContext.Passengers.FirstOrDefaultAsync(e => e.Email == GetUser());

            var order = await _dataContext.OrderRents.Include(x=>x.OrderRentItems).FirstOrDefaultAsync(x => x.OrderRentId == 1);

            //var mail = await _dataContext.Passengers.FirstOrDefaultAsync(m => m.Email == request.Email);
            //if (mail != null) { return "have this user"; }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Phone);

            Passenger passenger = new Passenger
            {
                PassengerName = request.PassengerName,
                IDCardNumber = request.IDCardNumber,
                Email = request.Email,
                Phone = request.Phone, 
                Password = passwordHash,
                RoleId = 2,
                isUse = true,
                
            };
            await _dataContext.Passengers.AddAsync(passenger);
            // สร้าง OrderRent จาก OrderRentDTO
            OrderRent orderRent = new OrderRent
            {
                OrderSatus = 0,
                PaymentDate = DateTime.Now,
                Passenger = passenger,
            };

           

            
            // สร้าง OrderRentItems จาก OrderRentItemDTOs
            foreach (var orderRentItemDTO in request.orderRentItems)
            {
                
                var car = await _dataContext.Cars.FindAsync(orderRentItemDTO.CarsId);

                if (car != null && car.ClassCars != null && car.ClassCars.ClassName == "M")
                {
                    // Set DriverId only if the car has ClassName 'M'
                    orderRentItemDTO.DriverId = orderRentItemDTO.DriverId;
                }
                else
                {
                    // Set DriverId to null if the car does not have ClassName 'M'
                    orderRentItemDTO.DriverId = 101;
                }

                OrderRentItem orderRentItem = new OrderRentItem
                {
                    Quantity = orderRentItemDTO.Quantity,
                    CarsPrice = orderRentItemDTO.ItemPrice,
                    DateTimePickup = orderRentItemDTO.DateTimePickup,
                    DateTimeReturn = orderRentItemDTO.DateTimeReturn,
                    PlacePickup = orderRentItemDTO.PlacePickup,
                    PlaceReturn = orderRentItemDTO.PlaceReturn,
                    CarsId = orderRentItemDTO.CarsId,
                    DriverId = orderRentItemDTO.DriverId , // Set based on the condition above
                    OrderRent = orderRent,
                    CreateAt = DateTime.Now
                };
                orderRent.OrderRentItems.Add(orderRentItem);
                // บันทึก OrderRent ลงฐานข้อมูล
                await _dataContext.OrderRents.AddAsync(orderRent);
                //order.OrderRentItems.Add(orderRentItem);
                // บันทึก OrderRentItem ลงฐานข้อมูล
                await _dataContext.OrderRentItems.AddAsync(orderRentItem);
            }

            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't save DB";
            return null;
        }

        public async Task<List<OrderRent>> GetOrders()
        {
            return await _dataContext.OrderRents.Include(u=>u.Passenger).OrderByDescending(i => i.OrderRentId).ToListAsync();
        }

        public async Task<List<OrderRentItem>> GetOrdersItems()
        {
            return await _dataContext.OrderRentItems.AsNoTracking()
                .Include(c => c.Cars).ThenInclude(c => c.ClassCars)
                .Include(d => d.Driver)
                .OrderByDescending(i => i.OrderRentItemId)
                .ToListAsync();
        }


        public async Task<List<OrderRentItem>> GetRentedCarsNow()
        {
            DateTime currentDate = DateTime.Now;

            // Query for OrderRentItems with overlapping rental periods
            var rentedCars = await _dataContext.OrderRentItems
                .Include(or => or.Cars)
                .Where(or => or.DateTimePickup <= currentDate && or.DateTimeReturn >= currentDate)
                .ToListAsync();

            return rentedCars;
        }
    }
}
