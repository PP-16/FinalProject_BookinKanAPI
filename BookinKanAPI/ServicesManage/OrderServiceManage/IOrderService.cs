using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;

namespace BookinKanAPI.ServicesManage.OrderServiceManage
{
    public interface IOrderService
    {
        Task<List<OrderRent>> GetOrders();
        Task<List<OrderRent>> GetOrderById(int ID);
        //Task<string> CreateAndUpdateOrders(OrderRentDTO rentDTO);

        Task<List<OrderRentItem>> GetOrdersItems();

        //Task<string> CreateAndUpdateOrdersItems(OrderRentItemDTO rentItemDTO);

        Task<object> CreateOrderRent(OrderRentItemDTO request);

        Task<List<OrderRentItem>> GetRentedCarsNow();
        Task<string> UpdateStatusOrders(int ID, Status newStatus);
        Task<string> ConfirmReturnStatus(int Id, bool confirm);
        Dictionary<DateTime, long> GetTotalPriceByReturnDate();
        Dictionary<DateTime, long> GetTotalPriceByReturnDateMount(int month, int year);
        Dictionary<DateTime, long> GetTotalPriceByReturnDateYear(int year);

        Task<List<OrdersPastDue>> GetOrdersPastDues();
        Task<List<OrdersPastDue>> GetOrderPastDuesById(int orderRentId);
        Task<List<OrdersPastDue>> CheckPastDueOrders(List<OrderRent> orderRents);
        Task<string> ChangePaymentPastDueStatus(int Id);

        Task<List<OrderRent>> GetOrdersByUser();
    }
}

