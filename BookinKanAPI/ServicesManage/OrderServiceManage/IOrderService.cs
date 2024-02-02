using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.OrderServiceManage
{
    public interface IOrderService
    {
        Task<List<OrderRent>> GetOrders();
        //Task<string> CreateAndUpdateOrders(OrderRentDTO rentDTO);

        Task<List<OrderRentItem>> GetOrdersItems();
        //Task<string> CreateAndUpdateOrdersItems(OrderRentItemDTO rentItemDTO);

        Task<string> CreateOrderRent(OrderRentItemDTO request);

        Task<List<OrderRentItem>> GetRentedCarsNow();
    }
}

