using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateisUse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isUse",
                table: "RouteCars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isUse",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isUse",
                table: "Passengers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "OrderRentItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isUse",
                table: "Itineraries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isUse",
                table: "Drivers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isUse",
                table: "ClassCars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isUse",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverId",
                keyValue: 101,
                column: "isUse",
                value: false);

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                columns: new[] { "Password", "isUse" },
                values: new object[] { "$2a$11$T2LblrS1WFA2IYSW7wXLluNiuOZschbjNbJJvpNShgcXCU7UHb8kK", false });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "isUse",
                value: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "isUse",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isUse",
                table: "RouteCars");

            migrationBuilder.DropColumn(
                name: "isUse",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isUse",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "OrderRentItems");

            migrationBuilder.DropColumn(
                name: "isUse",
                table: "Itineraries");

            migrationBuilder.DropColumn(
                name: "isUse",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "isUse",
                table: "ClassCars");

            migrationBuilder.DropColumn(
                name: "isUse",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$TkFBgXWTdw6qpGfOfOloAO65cgtiGmd0xCxSK/WpAiQAQTrABqg3.");
        }
    }
}
