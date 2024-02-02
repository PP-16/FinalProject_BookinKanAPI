using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentBookings",
                columns: table => new
                {
                    PaymentBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentBookings", x => x.PaymentBookingId);
                    table.ForeignKey(
                        name: "FK_PaymentBookings_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId");
                });

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$VG8zSUyzV618cYlghaKZUe1z.Dkn7I10wyTIBARSRLcd39ZWUwIR.");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBookings_BookingId",
                table: "PaymentBookings",
                column: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentBookings");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$JmoKifVocaMMaJipszhCw.fRxTwXTQ.38.3rbwKGgz0Wg4wMzl1CC");
        }
    }
}
