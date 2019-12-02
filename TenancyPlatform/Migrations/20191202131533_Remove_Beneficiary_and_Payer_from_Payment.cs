using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TenancyPlatform.Migrations
{
    public partial class Remove_Beneficiary_and_Payer_from_Payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_BeneficiaryId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_PayerId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BeneficiaryId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PayerId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BeneficiaryId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PayerId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "RealEstateId",
                table: "Contracts",
                nullable: true);

            migrationBuilder.InsertData(
                table: "RealEstates",
                columns: new[] { "Id", "Area", "BuildYear", "City", "Country", "Floor", "HouseNr", "Rooms", "Street" },
                values: new object[,]
                {
                    { 1, 80.0, 2007, "Kaunas", "Lithuania", 1, "7", 3, "Veiveriu g." },
                    { 2, 45.0, 1999, "Kaunas", "Lithuania", 6, "56A", 1, "Taikos pr." },
                    { 3, 60.0, 2011, "Vilnius", "Lithuania", 2, "3", 2, "Šimulionio g." }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "Antanas", "Antanaitis", "antanas", "landlord", "antanas@gmail.com" },
                    { 2, "Jonas", "Jonaitis", "jonas", "landlord", "jonas@gmail.com" },
                    { 3, "Dovydas", "Dovydaitis", "dovydas", "tenant", "dovydas@gmail.com" },
                    { 4, "Julius", "Julaitis", "julius", "tenant", "julius@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Adverts",
                columns: new[] { "Id", "Description", "LoanPrice", "OwnerId", "RealEstateId" },
                values: new object[,]
                {
                    { 1, "Puikus butas. Arti mokykla.", 300.0, 1, 1 },
                    { 2, "", 220.0, 2, 2 },
                    { 3, "Išnuomuojame ilgalaikei nuomai butą varpų 11-36, šalia prekybos centro, butas labai šiltas ir sauletas, rakinama laiptine, patogus susisiekimas aplink daug mokyklų, su baldais.", 230.0, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "Duration", "LandlordId", "Price", "RealEstateId", "Start", "TenantId" },
                values: new object[] { 1, 12, 1, 300.0, 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "Date", "ReceiverId", "SenderId" },
                values: new object[,]
                {
                    { 1, "Sveiki, mane domina jūsų nuomojamas butas. Ar būtų galimybė apžiūrėti butą ateinančią savaitę?", new DateTime(2019, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 2, "Laba diena. Susitikti galime trečiadienį.", new DateTime(2019, 1, 1, 8, 24, 13, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 3, "Ačiū. Iki", new DateTime(2019, 1, 1, 11, 37, 48, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 4, "Sveiki, mane domina jūsų nuomojamas butas. Ar būtų galimybė apžiūrėti butą ateinančią savaitę?", new DateTime(2019, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Failures",
                columns: new[] { "Id", "ContractId", "Description", "IsFixed", "ReporterId" },
                values: new object[] { 1, 1, "Sugedusi durų spyna", false, 3 });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "ContractId", "PaymentStatus" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "ContractId", "PaymentStatus" },
                values: new object[] { 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Amount", "Description", "PaymentId" },
                values: new object[,]
                {
                    { 1, 15.0, "Internetas už 2019 02 mėn.", 1 },
                    { 2, 50.0, "Šildymas už 2019 02 mėn.", 1 },
                    { 3, 26.0, "Elektra už 2019 02 mėn.", 1 },
                    { 4, 123.15000000000001, "Komunaliniai už 2019 03 mėn.", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_RealEstateId",
                table: "Contracts",
                column: "RealEstateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_RealEstates_RealEstateId",
                table: "Contracts",
                column: "RealEstateId",
                principalTable: "RealEstates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_RealEstates_RealEstateId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_RealEstateId",
                table: "Contracts");

            migrationBuilder.DeleteData(
                table: "Adverts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Adverts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Adverts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Failures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "RealEstateId",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "BeneficiaryId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayerId",
                table: "Payments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BeneficiaryId",
                table: "Payments",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PayerId",
                table: "Payments",
                column: "PayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_BeneficiaryId",
                table: "Payments",
                column: "BeneficiaryId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_PayerId",
                table: "Payments",
                column: "PayerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
