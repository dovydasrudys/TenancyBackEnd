using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TenancyPlatform.Migrations
{
    public partial class Add_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "RealEstateId" },
                values: new object[] { "Ieškome nuomininko be vaikų ir gyvūnų.", 3 });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OwnerId", "RealEstateId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "Adverts",
                columns: new[] { "Id", "Description", "LoanPrice", "OwnerId", "RealEstateId" },
                values: new object[] { 4, "Jaukus butas. Mažos šildymo kainos. Geras susisiekimas su miesto centru.", 280.0, 2, 3 });

            migrationBuilder.UpdateData(
                table: "Failures",
                keyColumn: "Id",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(2019, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Failures",
                columns: new[] { "Id", "ContractId", "Description", "IsFixed", "IssueDate", "ReporterId" },
                values: new object[] { 2, 1, "Šaldytuvo kamera nebešaldo", true, new DateTime(2019, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(2019, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "IssueDate",
                value: new DateTime(2019, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "http://www.interjeroprojektas.lt/get.php?i.799:w.749:h.500");

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "http://www.interjeroprojektas.lt/get.php?i.799:w.749:h.500");

            migrationBuilder.UpdateData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "http://www.interjeroprojektas.lt/get.php?i.799:w.749:h.500");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Adverts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Failures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "RealEstateId" },
                values: new object[] { "", 2 });

            migrationBuilder.UpdateData(
                table: "Adverts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OwnerId", "RealEstateId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Failures",
                keyColumn: "Id",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "IssueDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "IssueDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
