using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassCars",
                columns: table => new
                {
                    ClassCarsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCars", x => x.ClassCarsId);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Charges = table.Column<double>(type: "float", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDriver = table.Column<int>(type: "int", nullable: false),
                    isUse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleNameTH = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "RouteCars",
                columns: table => new
                {
                    RouteCarsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isUse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteCars", x => x.RouteCarsId);
                });

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    SystemSettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameWeb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactFB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactIG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePrompay = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.SystemSettingId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailCar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantitySeat = table.Column<int>(type: "int", nullable: false),
                    PriceSeat = table.Column<int>(type: "int", nullable: false),
                    isUse = table.Column<bool>(type: "bit", nullable: false),
                    CategoryCar = table.Column<int>(type: "int", nullable: false),
                    StatusCar = table.Column<int>(type: "int", nullable: false),
                    ClassCarsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarsId);
                    table.ForeignKey(
                        name: "FK_Cars_ClassCars_ClassCarsId",
                        column: x => x.ClassCarsId,
                        principalTable: "ClassCars",
                        principalColumn: "ClassCarsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageNews",
                columns: table => new
                {
                    ImageNewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageNews", x => x.ImageNewsId);
                    table.ForeignKey(
                        name: "FK_ImageNews_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isUse = table.Column<bool>(type: "bit", nullable: false),
                    ImagePassenger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerId);
                    table.ForeignKey(
                        name: "FK_Passengers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageSlides",
                columns: table => new
                {
                    ImageSlideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageSlides = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemSettingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageSlides", x => x.ImageSlideId);
                    table.ForeignKey(
                        name: "FK_ImageSlides_SystemSettings_SystemSettingId",
                        column: x => x.SystemSettingId,
                        principalTable: "SystemSettings",
                        principalColumn: "SystemSettingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageCars",
                columns: table => new
                {
                    ImageCarsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageCars", x => x.ImageCarsId);
                    table.ForeignKey(
                        name: "FK_ImageCars_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "CarsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itineraries",
                columns: table => new
                {
                    ItineraryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isUse = table.Column<bool>(type: "bit", nullable: false),
                    RouteCarsId = table.Column<int>(type: "int", nullable: false),
                    CarsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itineraries", x => x.ItineraryId);
                    table.ForeignKey(
                        name: "FK_Itineraries_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "CarsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itineraries_RouteCars_RouteCarsId",
                        column: x => x.RouteCarsId,
                        principalTable: "RouteCars",
                        principalColumn: "RouteCarsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderRents",
                columns: table => new
                {
                    OrderRentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderSatus = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    ConfirmReturn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRents", x => x.OrderRentId);
                    table.ForeignKey(
                        name: "FK_OrderRents_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAtBooking = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeatsSerialized = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    ItineraryId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckIn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Itineraries_ItineraryId",
                        column: x => x.ItineraryId,
                        principalTable: "Itineraries",
                        principalColumn: "ItineraryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderRentItems",
                columns: table => new
                {
                    OrderRentItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CarsPrice = table.Column<int>(type: "int", nullable: false),
                    DateTimePickup = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTimeReturn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlacePickup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceReturn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarsId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    OrderRentId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRentItems", x => x.OrderRentItemId);
                    table.ForeignKey(
                        name: "FK_OrderRentItems_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "CarsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRentItems_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DriverId");
                    table.ForeignKey(
                        name: "FK_OrderRentItems_OrderRents_OrderRentId",
                        column: x => x.OrderRentId,
                        principalTable: "OrderRents",
                        principalColumn: "OrderRentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersPastDues",
                columns: table => new
                {
                    OrdersPastDueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetrunDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    TotalPricePastDue = table.Column<int>(type: "int", nullable: false),
                    Paied = table.Column<bool>(type: "bit", nullable: false),
                    OrderRentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersPastDues", x => x.OrdersPastDueId);
                    table.ForeignKey(
                        name: "FK_OrdersPastDues_OrderRents_OrderRentId",
                        column: x => x.OrderRentId,
                        principalTable: "OrderRents",
                        principalColumn: "OrderRentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentBookings",
                columns: table => new
                {
                    PaymentBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PymentSatus = table.Column<int>(type: "int", nullable: false),
                    ImagePayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryPayment = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    OrderRentId = table.Column<int>(type: "int", nullable: true),
                    OrdersPastDueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentBookings", x => x.PaymentBookingId);
                    table.ForeignKey(
                        name: "FK_PaymentBookings_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId");
                    table.ForeignKey(
                        name: "FK_PaymentBookings_OrderRents_OrderRentId",
                        column: x => x.OrderRentId,
                        principalTable: "OrderRents",
                        principalColumn: "OrderRentId");
                    table.ForeignKey(
                        name: "FK_PaymentBookings_OrdersPastDues_OrdersPastDueId",
                        column: x => x.OrdersPastDueId,
                        principalTable: "OrdersPastDues",
                        principalColumn: "OrdersPastDueId");
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "DriverId", "Address", "Charges", "DriverName", "IDCardNumber", "Phone", "StatusDriver", "isUse" },
                values: new object[] { 101, "null", 0.0, "ไม่มีการเลือก", "null", "null", 0, false });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName", "RoleNameTH" },
                values: new object[,]
                {
                    { 1, "Admin", "ผู้ดูแลระบบ" },
                    { 2, "Passenger", "ผู้ใช้" },
                    { 3, "Employee", "พนักงาน" }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "PassengerId", "Email", "IDCardNumber", "ImagePassenger", "PassengerName", "Password", "Phone", "RoleId", "isUse" },
                values: new object[] { 1, "Admin@g.com", "123456789", null, "Admin", "$2a$11$xxQsWMksO0gUUTNRdpIKBOhGqAI4yYdFQz6r420GJv3FUO8bhKsYm", "0912345678", 1, true });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ItineraryId",
                table: "Bookings",
                column: "ItineraryId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PassengerId",
                table: "Bookings",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ClassCarsId",
                table: "Cars",
                column: "ClassCarsId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageCars_CarsId",
                table: "ImageCars",
                column: "CarsId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageNews_NewsId",
                table: "ImageNews",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageSlides_SystemSettingId",
                table: "ImageSlides",
                column: "SystemSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_Itineraries_CarsId",
                table: "Itineraries",
                column: "CarsId");

            migrationBuilder.CreateIndex(
                name: "IX_Itineraries_RouteCarsId",
                table: "Itineraries",
                column: "RouteCarsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRentItems_CarsId",
                table: "OrderRentItems",
                column: "CarsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRentItems_DriverId",
                table: "OrderRentItems",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRentItems_OrderRentId",
                table: "OrderRentItems",
                column: "OrderRentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRents_PassengerId",
                table: "OrderRents",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersPastDues_OrderRentId",
                table: "OrdersPastDues",
                column: "OrderRentId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_RoleId",
                table: "Passengers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBookings_BookingId",
                table: "PaymentBookings",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBookings_OrderRentId",
                table: "PaymentBookings",
                column: "OrderRentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBookings_OrdersPastDueId",
                table: "PaymentBookings",
                column: "OrdersPastDueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageCars");

            migrationBuilder.DropTable(
                name: "ImageNews");

            migrationBuilder.DropTable(
                name: "ImageSlides");

            migrationBuilder.DropTable(
                name: "OrderRentItems");

            migrationBuilder.DropTable(
                name: "PaymentBookings");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "OrdersPastDues");

            migrationBuilder.DropTable(
                name: "Itineraries");

            migrationBuilder.DropTable(
                name: "OrderRents");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "RouteCars");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "ClassCars");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
