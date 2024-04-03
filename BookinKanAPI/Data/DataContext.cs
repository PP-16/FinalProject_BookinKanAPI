using BookinKanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookinKanAPI.Data
{
    public class DataContext:DbContext
    {
        private readonly IConfiguration _config;

        public DataContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnect"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("123456");
            modelBuilder.Entity<Role>().HasData(
                new Role() { RoleId = 1, RoleName = "Admin",RoleNameTH = "ผู้ดูแลระบบ" },
                new Role() { RoleId = 2, RoleName = "Passenger",RoleNameTH = "ผู้ใช้" }
                );
            modelBuilder.Entity<Passenger>().HasData(
                new Passenger() { PassengerId = 1, PassengerName = "Admin", Email = "Admin@g.com", Password = passwordHash, IDCardNumber = "123456789", Phone = "0912345678", RoleId = 1 });
            modelBuilder.Entity<Driver>().HasData(
    new Driver() { DriverId = 101,DriverName = "ไม่มีการเลือก",IDCardNumber ="null",Address="null", Charges = 0,Phone = "null",StatusDriver=0 });
        }

        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<ClassCars> ClassCars { get; set; }
        public DbSet<ImageCars> ImageCars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<OrderRent> OrderRents { get; set; }
        public DbSet<OrderRentItem> OrderRentItems { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Itinerary>Itineraries { get; set; }
        public DbSet<RouteCars>RouteCars { get; set; }
        public DbSet<PaymentBooking> PaymentBookings { get; set; }

        public DbSet<News> News { get; set; }
        public DbSet<ImageNews> ImageNews { get; set; }

        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<ImageSlide> ImageSlides { get; set; }
    }
}
