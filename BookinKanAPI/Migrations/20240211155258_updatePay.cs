using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatePay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderRentId",
                table: "PaymentBookings",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$iDZ4NCvTe5wKfsRZcHcvb.3jyUohRjazEg503gC7IM4unuvdEdcDa");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBookings_OrderRentId",
                table: "PaymentBookings",
                column: "OrderRentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBookings_OrderRents_OrderRentId",
                table: "PaymentBookings",
                column: "OrderRentId",
                principalTable: "OrderRents",
                principalColumn: "OrderRentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBookings_OrderRents_OrderRentId",
                table: "PaymentBookings");

            migrationBuilder.DropIndex(
                name: "IX_PaymentBookings_OrderRentId",
                table: "PaymentBookings");

            migrationBuilder.DropColumn(
                name: "OrderRentId",
                table: "PaymentBookings");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$.hLkHwq.eYldL1T9M2mJKOEJ9Fy7tsVqJDoAcCY9HAnPBRTxW28RK");
        }
    }
}
