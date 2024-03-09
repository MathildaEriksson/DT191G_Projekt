using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquiMarketApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdTypeandLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdTypeId",
                table: "Ads",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Ads",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdType",
                columns: table => new
                {
                    AdTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdType", x => x.AdTypeId);
                });

            migrationBuilder.CreateTable(
                name: "County",
                columns: table => new
                {
                    CountyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_County", x => x.CountyId);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CountyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_County_CountyId",
                        column: x => x.CountyId,
                        principalTable: "County",
                        principalColumn: "CountyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_AdTypeId",
                table: "Ads",
                column: "AdTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_CityId",
                table: "Ads",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountyId",
                table: "City",
                column: "CountyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AdType_AdTypeId",
                table: "Ads",
                column: "AdTypeId",
                principalTable: "AdType",
                principalColumn: "AdTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_City_CityId",
                table: "Ads",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AdType_AdTypeId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Ads_City_CityId",
                table: "Ads");

            migrationBuilder.DropTable(
                name: "AdType");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "County");

            migrationBuilder.DropIndex(
                name: "IX_Ads_AdTypeId",
                table: "Ads");

            migrationBuilder.DropIndex(
                name: "IX_Ads_CityId",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "AdTypeId",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Ads");
        }
    }
}
