using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class testBookingList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeatNumber",
                table: "Bookings",
                newName: "SeatsSerialized");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$kH0PrMNUl9EZSTPLN8ooV.uMXD9/mdTeBeJdFkv2VhlB9MQH8yzZ2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeatsSerialized",
                table: "Bookings",
                newName: "SeatNumber");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$M3jijhXoDQGiKY9x/2Lq0eEBwlnFX5zZObdVBgBh26y91Sw/Tf/Z6");
        }
    }
}
