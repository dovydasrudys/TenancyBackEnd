using Microsoft.EntityFrameworkCore.Migrations;

namespace TenancyPlatform.Migrations
{
    public partial class Add_ImageURL_RealEstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageUrl",
                table: "RealEstates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "RealEstates");
        }
    }
}
