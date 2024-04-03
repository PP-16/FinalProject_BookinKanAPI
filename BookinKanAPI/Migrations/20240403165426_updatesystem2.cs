using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookinKanAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatesystem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    SystemSettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameWeb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactFB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactIG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePrompay = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.SystemSettingId);
                });

            migrationBuilder.CreateTable(
                name: "ImageSlides",
                columns: table => new
                {
                    ImageSlideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageSlides = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemSettingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageSlides", x => x.ImageSlideId);
                    table.ForeignKey(
                        name: "FK_ImageSlides_SystemSettings_SystemSettingId",
                        column: x => x.SystemSettingId,
                        principalTable: "SystemSettings",
                        principalColumn: "SystemSettingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$IuQPHoiN9z6yvyi3T7N68OD8/ntAFQCwAq.B9eqeBpJmrvwkKjhOO");

            migrationBuilder.CreateIndex(
                name: "IX_ImageSlides_SystemSettingId",
                table: "ImageSlides",
                column: "SystemSettingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageSlides");

            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.UpdateData(
                table: "Passengers",
                keyColumn: "PassengerId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ffLXWURrTQkj96SxmXuq9.kTzzHkCVyjhtTYbcagstw8Q3eD.1A66");
        }
    }
}
