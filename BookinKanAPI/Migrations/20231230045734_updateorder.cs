using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Place",
                table: "OrderRentItems",
                newName: "PlaceReturn");

            migrationBuilder.AddColumn<string>(
                name: "PlacePickup",
                table: "OrderRentItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$M/.sfDVaQBSvYrdA84kWlev5HZ5ZYFZFkfvzq5dz/RccicJbGHaty");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlacePickup",
                table: "OrderRentItems");

            migrationBuilder.RenameColumn(
                name: "PlaceReturn",
                table: "OrderRentItems",
                newName: "Place");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$kH0PrMNUl9EZSTPLN8ooV.uMXD9/mdTeBeJdFkv2VhlB9MQH8yzZ2");
        }
    }
}
