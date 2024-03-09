using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquiMarketApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AdType_AdTypeId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Ads_City_CityId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_City_County_CountyId",
                table: "City");

            migrationBuilder.DropPrimaryKey(
                name: "PK_County",
                table: "County");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdType",
                table: "AdType");

            migrationBuilder.RenameTable(
                name: "County",
                newName: "Counties");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameTable(
                name: "AdType",
                newName: "AdTypes");

            migrationBuilder.RenameIndex(
                name: "IX_City_CountyId",
                table: "Cities",
                newName: "IX_Cities_CountyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Counties",
                table: "Counties",
                column: "CountyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdTypes",
                table: "AdTypes",
                column: "AdTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AdTypes_AdTypeId",
                table: "Ads",
                column: "AdTypeId",
                principalTable: "AdTypes",
                principalColumn: "AdTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Cities_CityId",
                table: "Ads",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Counties_CountyId",
                table: "Cities",
                column: "CountyId",
                principalTable: "Counties",
                principalColumn: "CountyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AdTypes_AdTypeId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Cities_CityId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Counties_CountyId",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Counties",
                table: "Counties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdTypes",
                table: "AdTypes");

            migrationBuilder.RenameTable(
                name: "Counties",
                newName: "County");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameTable(
                name: "AdTypes",
                newName: "AdType");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_CountyId",
                table: "City",
                newName: "IX_City_CountyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_County",
                table: "County",
                column: "CountyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdType",
                table: "AdType",
                column: "AdTypeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_City_County_CountyId",
                table: "City",
                column: "CountyId",
                principalTable: "County",
                principalColumn: "CountyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
