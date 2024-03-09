using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquiMarketApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGenderModelAndUpdateAd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Genders_GenderId",
                table: "Ads");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Ads_GenderId",
                table: "Ads");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Ads",
                newName: "Gender");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Ads",
                newName: "GenderId");

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_GenderId",
                table: "Ads",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Genders_GenderId",
                table: "Ads",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
