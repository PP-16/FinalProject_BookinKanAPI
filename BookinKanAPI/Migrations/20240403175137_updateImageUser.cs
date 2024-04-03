using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateImageUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePassenger",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                columns: new[] { "ImagePassenger", "Password" },
                values: new object[] { null, "$2a$11$MgfUYbXM4jQ0UlwRlQYs5OyA7MvECvizjLFfZBXXQQhF5nMhH9bJm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePassenger",
                table: "Passengers");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$IuQPHoiN9z6yvyi3T7N68OD8/ntAFQCwAq.B9eqeBpJmrvwkKjhOO");
        }
    }
}
