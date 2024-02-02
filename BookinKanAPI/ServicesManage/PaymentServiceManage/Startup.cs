namespace BookinKanAPI.ServicesManage.PaymentServiceManage
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // กำหนดค่า Configuration ของคุณ
            services.Configure<StripeSettings>(_configuration.GetSection("StripeSettings"));

            // เพิ่มการสร้าง PaymentService หรือ class อื่น ๆ ที่ใช้ StripeSettings เพื่อรับค่า Configuration
            services.AddTransient<IPaymentService, PaymentService>();

      
        }
    }
}
