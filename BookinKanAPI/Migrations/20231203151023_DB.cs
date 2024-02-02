using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class DB : Migration
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
                    StatusDriver = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    DestinationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteCars", x => x.RouteCarsId);
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
                    RouteId = table.Column<int>(type: "int", nullable: false),
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
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    OrderSatus = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false)
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
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    ItineraryId = table.Column<int>(type: "int", nullable: false)
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
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarsId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    OrderRentId = table.Column<int>(type: "int", nullable: false)
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
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRentItems_OrderRents_OrderRentId",
                        column: x => x.OrderRentId,
                        principalTable: "OrderRents",
                        principalColumn: "OrderRentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Passenger" }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "PassengerId", "Email", "IDCardNumber", "PassengerName", "Password", "Phone", "RoleId" },
                values: new object[] { 1, "Admin@g.com", "123456789", "Admin", "$2a$11$SLaCMj/wvxcb4Hyu5vuCFumWKY3LuMJC9TQon/l6dFJW5lbjwSNGO", "0912345678", 1 });

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
                name: "IX_Passengers_RoleId",
                table: "Passengers",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "ImageCars");

            migrationBuilder.DropTable(
                name: "OrderRentItems");

            migrationBuilder.DropTable(
                name: "Itineraries");

            migrationBuilder.DropTable(
                name: "Drivers");

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
