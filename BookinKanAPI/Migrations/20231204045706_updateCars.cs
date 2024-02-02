using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceSeat",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantitySeat",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$CSmhmhXZdzS79NUrnOCtG.BIiOGorXSXYWWEoXY2oamgg8ixuG/8m");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceSeat",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "QuantitySeat",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$LN3.wEuTKIRQNkI/csBwIOE0WoDrceTbTsQOFII169qlNw5pOAB9u");
        }
    }
}
