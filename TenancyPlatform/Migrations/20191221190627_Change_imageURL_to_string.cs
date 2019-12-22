using Microsoft.EntityFrameworkCore.Migrations;

namespace TenancyPlatform.Migrations
{
    public partial class Change_imageURL_to_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "RealEstates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ImageUrl",
                table: "RealEstates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: 0);
        }
    }
}
