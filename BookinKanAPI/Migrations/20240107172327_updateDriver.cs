using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateDriver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "DriverId",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "DriverId", "Address", "Charges", "DriverName", "IDCardNumber", "Phone", "StatusDriver" },
                values: new object[] { 101, "null", 0.0, "null", "null", "null", 0 });

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$/R6pG68rkT2YoSfQNSX3sOsrL3QKx8Q5Ohhr1kwjSG/gixgURjWR.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "DriverId",
                keyValue: 101);

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "DriverId", "Address", "Charges", "DriverName", "IDCardNumber", "Phone", "StatusDriver" },
                values: new object[] { 1, "null", 0.0, "null", "null", "null", 0 });

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$4R275eNuruSIzLx0Ka4iUeJhY8R8Y95zKm1VtkzozGx/P4qckHIP2");
        }
    }
}
