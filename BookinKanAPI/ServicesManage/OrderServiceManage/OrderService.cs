 using AutoMapper;
using BookinKanAPI.Data;
using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;

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

        public async Task<object> CreateOrderRent(OrderRentItemDTO request)
        {
            // var user = await _dataContext.Passengers.FirstOrDefaultAsync(e => e.Email == GetUser());

            var order = await _dataContext.OrderRents.Include(x => x.OrderRentItems).FirstOrDefaultAsync(x => x.OrderRentId == 1);

            var user = await _dataContext.Passengers.FirstOrDefaultAsync(m => m.Email == request.Email);
            OrderRent orderRent = new OrderRent // สร้าง OrderRent จาก OrderRentDTO ที่เป็นส่วนร่วมของทั้งสองเงื่อนไข
            {
                OrderSatus = 0,
                PaymentDate = DateTime.Now,
                ConfirmReturn = false
            };

            if (user != null)
            {
                orderRent.PassengerId = user.PassengerId; // เพิ่ม PassengerId ในกรณีที่ไม่มีข้อมูล email

            }
            else
            {
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
                orderRent.Passenger = passenger; // เพิ่ม Passenger ในกรณีที่มีข้อมูล email
            }

            // สร้าง OrderRentItems จาก OrderRentItemDTOs
            foreach (var orderRentItemDTO in request.orderRentItems)
            {

                var car = await _dataContext.Cars.FindAsync(orderRentItemDTO.CarsId);

                OrderRentItem orderRentItem = new OrderRentItem
                {
                    Quantity = orderRentItemDTO.Quantity,
                    CarsPrice = orderRentItemDTO.ItemPrice,
                    DateTimePickup = orderRentItemDTO.DateTimePickup,
                    DateTimeReturn = orderRentItemDTO.DateTimeReturn,
                    PlacePickup = orderRentItemDTO.PlacePickup,
                    PlaceReturn = orderRentItemDTO.PlaceReturn,
                    CarsId = orderRentItemDTO.CarsId,
                    DriverId = orderRentItemDTO.DriverId, // Set based on the condition above
                    OrderRent = orderRent,
                    CreateAt = DateTime.Now
                };
                orderRent.OrderRentItems.Add(orderRentItem);

                await _dataContext.OrderRentItems.AddAsync(orderRentItem);
            }

            await _dataContext.OrderRents.AddAsync(orderRent);

            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't save DB";

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            // Serialize the object with options to handle cyclic references
            var json = JsonSerializer.Serialize(orderRent, options);
            return orderRent;
        }

        public async Task<List<OrderRent>> GetOrders()
        {
            return await _dataContext.OrderRents
        .Include(u => u.Passenger)
        .Include(o => o.OrderRentItems).ThenInclude(d => d.Driver)
        .Include(o => o.OrderRentItems).ThenInclude(d => d.Cars).ThenInclude(c=>c.ClassCars)
        .Include(o => o.OrderRentItems).ThenInclude(d => d.Cars).ThenInclude(c => c.ImageCars)
        .OrderByDescending(i => i.OrderRentId)
        .ToListAsync();
        }

        public async Task<List<OrderRent>> GetOrderById(int ID)
        {
            return await _dataContext.OrderRents
          .Include(u => u.Passenger)
          .Include(o => o.OrderRentItems).ThenInclude(d => d.Driver)
          .Include(o => o.OrderRentItems).ThenInclude(d => d.Cars).ThenInclude(c => c.ClassCars)
          .Include(o => o.OrderRentItems).ThenInclude(d => d.Cars).ThenInclude(c => c.ImageCars)
                  .Where(i => i.OrderRentId == ID)
                .ToListAsync();
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
        public async Task<string> UpdateStatusOrders(int ID, Status newStatus)
        {
            var order = await _dataContext.OrderRents.FindAsync(ID);

            if (order != null)
            {
                order.OrderSatus = newStatus;

            }

            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";

            return null;

        }
        public async Task<string> ConfirmReturnStatus(int Id, bool confirm)
        {
            var order = await _dataContext.OrderRents.FindAsync(Id);
            if (order != null)
            {
                order.ConfirmReturn = confirm;
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";
            return null;
        }

        //public Dictionary<DateTime, long> GetTotalPriceByReturnDate()
        //{
        //    Dictionary<DateTime, long> totalPriceByDate = new Dictionary<DateTime, long>();

        //    var orderItems = _dataContext.OrderRentItems; // Assuming _dataContext is the DataContext of your database
        //    if (orderItems != null)
        //    {
        //        foreach (var item in orderItems)
        //        {
        //            DateTime returnDate = item.DateTimeReturn.Date; // Extracting date part only
        //            long total = item.Quantity * item.CarsPrice;

        //            if (totalPriceByDate.ContainsKey(returnDate))
        //            {
        //                totalPriceByDate[returnDate] += total; // Add to existing total if date already exists
        //            }
        //            else
        //            {
        //                totalPriceByDate[returnDate] = total; // Create new entry if date doesn't exist
        //            }
        //        }
        //    }


        //    return totalPriceByDate;
        //}
        public Dictionary<DateTime, long> GetTotalPriceByReturnDate()
        {
            Dictionary<DateTime, long> totalPriceByDate = new Dictionary<DateTime, long>();

            // Assuming _dataContext is properly instantiated and injected
            var orderItems = _dataContext.OrderRentItems;
            if (orderItems != null)
            {
                foreach (var item in orderItems)
                {
                    DateTime returnDate = item.DateTimeReturn.Date; // Extracting date part only
                    long total = item.Quantity * item.CarsPrice;

                    if (totalPriceByDate.ContainsKey(returnDate))
                    {
                        totalPriceByDate[returnDate] += total; // Add to existing total if date already exists
                    }
                    else
                    {
                        totalPriceByDate[returnDate] = total; // Create new entry if date doesn't exist
                    }
                }
            }

            return totalPriceByDate;
        }

        public Dictionary<DateTime, long> GetTotalPriceByReturnDateMount(int month,int year)
        {
            Dictionary<DateTime, long> totalPriceByDate = new Dictionary<DateTime, long>();

            var orderItems = _dataContext.OrderRentItems; // Assuming _dataContext is the DataContext of your database

            foreach (var item in orderItems)
            {
                DateTime returnDate = item.DateTimeReturn.Date; // Extracting date part only

                // Check if the month of the return date matches the specified month
                if (returnDate.Month == month&& returnDate.Year == year)
                {
                    long total = item.Quantity * item.CarsPrice;

                    if (totalPriceByDate.ContainsKey(returnDate))
                    {
                        totalPriceByDate[returnDate] += total; // Add to existing total if date already exists
                    }
                    else
                    {
                        totalPriceByDate[returnDate] = total; // Create new entry if date doesn't exist
                    }
                }
            }

            return totalPriceByDate;
        }
        public Dictionary<DateTime, long> GetTotalPriceByReturnDateYear(int year)
        {
            Dictionary<DateTime, long> totalPriceByDate = new Dictionary<DateTime, long>();

            var orderItems = _dataContext.OrderRentItems; // Assuming _dataContext is the DataContext of your database

            foreach (var item in orderItems)
            {
                DateTime returnDate = item.DateTimeReturn.Date; // Extracting date part only

                // Check if the year of the return date matches the specified year
                if (returnDate.Year == year)
                {
                    // Group by month
                    DateTime keyDate = new DateTime(returnDate.Year, returnDate.Month, 1);

                    long total = item.Quantity * item.CarsPrice;

                    if (totalPriceByDate.ContainsKey(keyDate))
                    {
                        totalPriceByDate[keyDate] += total; // Add to existing total if date already exists
                    }
                    else
                    {
                        totalPriceByDate[keyDate] = total; // Create new entry if date doesn't exist
                    }
                }
            }

            return totalPriceByDate;

        }


        public async Task<List<OrdersPastDue>> GetOrdersPastDues()
        {
            return (await _dataContext.OrdersPastDues.Include(r => r.OrderRent).ThenInclude(r => r.OrderRentItems).ThenInclude(c => c.Cars).ThenInclude(i => i.ImageCars)
            .Include(r => r.OrderRent).ThenInclude(r => r.OrderRentItems).ThenInclude(d => d.Driver).OrderByDescending(i=>i.OrdersPastDueId).ToListAsync());
        }
        public async Task<List<OrdersPastDue>> GetOrderPastDuesById(int orderRentId)
        {
            return await _dataContext.OrdersPastDues
           .Include(r => r.OrderRent).ThenInclude(r=>r.OrderRentItems).ThenInclude(c=>c.Cars).ThenInclude(i=>i.ImageCars)
            .Include(r => r.OrderRent).ThenInclude(r => r.OrderRentItems).ThenInclude(d => d.Driver)
           .Where(opd => opd.OrderRentId == orderRentId)
           .ToListAsync();
        }

        //public async Task<List<OrdersPastDue>> CheckPastDueOrders(List<OrderRent> orderRents)
        //{
        //    List<OrdersPastDue> pastDueOrders = new List<OrdersPastDue>();

        //    foreach (var orderRent in orderRents)
        //    {
        //        if (orderRent.ConfirmReturn == false)
        //        {
        //            // Iterate through OrderRentItems to find overdue items
        //            foreach (var item in orderRent.OrderRentItems)
        //            {
        //                if (item.DateTimeReturn < DateTime.Now)
        //                {
        //                    // Check if OrdersPastDues already contains OrderRentItemId
        //                    bool isExist = await _dataContext.OrdersPastDues.AnyAsync(opd => opd.OrderRentItemId == item.OrderRentItemId);
        //                    if (!isExist)
        //                    {
        //                        // Calculate the number of days overdue
        //                        int numberOfDays = (int)(DateTime.Now - item.DateTimeReturn).TotalDays + 1;

        //                        // Calculate the total price for the overdue item
        //                        int totalPricePastDue = CalculateTotalPricePastDue(item);

        //                        // Create OrdersPastDue object
        //                        OrdersPastDue pastDueOrder = new OrdersPastDue
        //                        {
        //                            RetrunDate = orderRent.PaymentDate,
        //                            NumberOfDays = numberOfDays,
        //                            TotalPricePastDue = totalPricePastDue,
        //                            OrderRentItemId = item.OrderRentItemId,
        //                            orderRentItem = item,
        //                            Paied = false
        //                        };

        //                        pastDueOrders.Add(pastDueOrder);

        //                        // Add pastDueOrder to the context
        //                        await _dataContext.OrdersPastDues.AddAsync(pastDueOrder);
        //                    }
        //                    else
        //                    {
        //                        // Calculate the number of days overdue
        //                        int numberOfDays = (int)(DateTime.Now - item.DateTimeReturn).TotalDays;

        //                        // Calculate the total price for the overdue item
        //                        int totalPricePastDue = CalculateTotalPricePastDue(item);
        //                        var existingOrderPastDue = await _dataContext.OrdersPastDues.FirstOrDefaultAsync(opd => opd.OrderRentItemId == item.OrderRentItemId);
        //                        if (existingOrderPastDue != null && !existingOrderPastDue.Paied)
        //                        {
        //                            // Update existing OrdersPastDue
        //                            existingOrderPastDue.NumberOfDays = numberOfDays;
        //                            existingOrderPastDue.TotalPricePastDue = totalPricePastDue;


        //                            // Update OrdersPastDue in the context
        //                            _dataContext.OrdersPastDues.Update(existingOrderPastDue);
        //                        }
        //                    }


        //                    // Check and update order status if necessary
        //                    var orderStatus = await _dataContext.OrderRents.FindAsync(orderRent.OrderRentId);
        //                    if (orderStatus != null && orderStatus.OrderSatus != Status.PastDue)
        //                    {
        //                        // Update the status immediately
        //                        orderStatus.OrderSatus = Status.PastDue;
        //                    }
        //                }
        //            }
        //        }

        //    }

        //    // Save changes to the database
        //    await _dataContext.SaveChangesAsync();

        //    return pastDueOrders;
        //}

        public async Task<List<OrdersPastDue>> CheckPastDueOrders(List<OrderRent> orderRents)
        {
            List<OrdersPastDue> pastDueOrders = new List<OrdersPastDue>();

            foreach (var orderRent in orderRents)
            {
                if (!orderRent.ConfirmReturn)
                {
                    // Use Include to load only necessary data for OrderRentItems
                    var orderRentWithItems = await _dataContext.OrderRents
                        .Include(o => o.OrderRentItems)
                        .FirstOrDefaultAsync(o => o.OrderRentId == orderRent.OrderRentId);

                    // Group OrderRentItems by OrderRentId
                    var groupedItems = orderRentWithItems.OrderRentItems
                        .GroupBy(item => item.OrderRentId)
                        .Select(group => new
                        {
                            OrderRentId = group.Key,
                            Items = group.ToList()
                        });

                    foreach (var group in groupedItems)
                    {
                        // Use the first item in the group to create OrdersPastDue
                        var firstItem = group.Items.FirstOrDefault();
                        if (firstItem != null)
                        {
                            // Check if OrdersPastDues already contains OrderRentItemId
                            bool isExist = await _dataContext.OrdersPastDues.AnyAsync(opd => opd.OrderRentId == firstItem.OrderRentId);
                            if (!isExist)
                            {
                                //int numberOfDays = (int)(DateTime.Now - firstItem.DateTimeReturn).TotalDays + 1;
                                //int totalPricePastDue = CalculateTotalPricePastDue(firstItem);
                                int totalPricePastDue = CalculateTotalPricePastDue(group.Items);
                                int numberOfDays = (int)(DateTime.Now - group.Items.First().DateTimeReturn).TotalDays;

                                OrdersPastDue pastDueOrder = new OrdersPastDue
                                {
                                    RetrunDate = orderRent.PaymentDate,
                                    NumberOfDays = numberOfDays,
                                    TotalPricePastDue = totalPricePastDue,
                                    OrderRentId = firstItem.OrderRentId,
                                    Paied = false
                                };

                                pastDueOrders.Add(pastDueOrder);
                                await _dataContext.OrdersPastDues.AddAsync(pastDueOrder);
                                // Update order status if necessary
                                if (orderRentWithItems.OrderSatus != Status.PastDue)
                                {
                                    orderRentWithItems.OrderSatus = Status.PastDue;
                                    _dataContext.OrderRents.Update(orderRentWithItems);
                                }
                            }
                            else
                            {
                                int totalPricePastDue = CalculateTotalPricePastDue(group.Items);
                                int numberOfDays = (int)(DateTime.Now - group.Items.First().DateTimeReturn).TotalDays;

                                var existingOrderPastDue = await _dataContext.OrdersPastDues.FirstOrDefaultAsync(opd => opd.OrderRentId == firstItem.OrderRentId);
                                if (existingOrderPastDue != null && !existingOrderPastDue.Paied)
                                {
                                    existingOrderPastDue.NumberOfDays = numberOfDays;
                                    existingOrderPastDue.TotalPricePastDue = totalPricePastDue;

                                    _dataContext.OrdersPastDues.Update(existingOrderPastDue);
                                }
                            }
                        }
                    }


                }
            }

            await _dataContext.SaveChangesAsync();

            return pastDueOrders;
        }

        public async Task<string> ChangePaymentPastDueStatus(int Id)
        {
            var pastDues = await _dataContext.OrdersPastDues.FindAsync(Id);
            if (pastDues != null)
            {
                pastDues.Paied = true;
            }
            var result = await _dataContext.SaveChangesAsync();
            if (result <= 0) return "Can't Update Sattus";
            return null;
        }

        private int CalculateTotalPricePastDue(IEnumerable<OrderRentItem> items)
        {
            int total = 0;

            foreach (var item in items)
            {
                // Calculate total price only for the period that exceeds the due date
                int daysOverdue = (int)(DateTime.Now - item.DateTimeReturn).TotalDays;

                if (daysOverdue > 0)
                {
                    // Multiply the total by the number of days for each overdue item
                    total += daysOverdue * (item.Quantity * item.CarsPrice);

                    // Include driver charges for each day if applicable
                    if (item.DriverId.HasValue)
                    {
                        total += daysOverdue * (int)item.Driver.Charges;
                    }
                }
            }

            return total;
        }

        public async Task<List<OrderRent>> GetOrdersByUser()
        {
            var orders = await _dataContext.OrderRents
                    .Include(u => u.Passenger)
                    .Include(o => o.OrderRentItems).ThenInclude(d => d.Driver)
                    .Include(o => o.OrderRentItems).ThenInclude(d => d.Cars).ThenInclude(c => c.ClassCars)
                    .Include(o => o.OrderRentItems).ThenInclude(d => d.Cars).ThenInclude(c => c.ImageCars)
                    .Where(b => b.Passenger.Email == GetUser())
                    .OrderByDescending(d => d.OrderRentItems.Max(x => x.DateTimeReturn))
                    .ToListAsync();

            if (orders == null || !orders.Any())
            {
                return null;
            }

            return orders;
        }

    }
}
