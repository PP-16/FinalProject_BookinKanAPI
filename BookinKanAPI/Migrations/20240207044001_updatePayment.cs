using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryPayment",
                table: "PaymentBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImagePayment",
                table: "PaymentBookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$.hLkHwq.eYldL1T9M2mJKOEJ9Fy7tsVqJDoAcCY9HAnPBRTxW28RK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryPayment",
                table: "PaymentBookings");

            migrationBuilder.DropColumn(
                name: "ImagePayment",
                table: "PaymentBookings");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$wzutS.5ZJ/hdBwfAMz2cg.KkcEFz.ZT4c1plGet5qEGFy8cIFN2IS");
        }
    }
}
