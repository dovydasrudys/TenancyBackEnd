using Microsoft.EntityFrameworkCore.Migrations;

namespace TenancyPlatform.Migrations
{
    public partial class Add_Owner_to_RealEstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "RealEstates",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_OwnerId",
                table: "RealEstates",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Users_OwnerId",
                table: "RealEstates",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Users_OwnerId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_OwnerId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "RealEstates");
        }
    }
}
