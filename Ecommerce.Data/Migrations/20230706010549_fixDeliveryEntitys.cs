using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    public partial class fixDeliveryEntitys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97adc4e2-de00-4ddd-bdf8-f72cc55738f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e397a659-df5a-455e-850a-26b9b17cbccc");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverPostalCode",
                table: "Deliverys",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderPostalCode",
                table: "Deliverys",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41f2c979-acb5-4a92-af76-3282e495504e", "2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "669a5043-f6d7-4ad1-8fc4-d2064f235e57", "1", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41f2c979-acb5-4a92-af76-3282e495504e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "669a5043-f6d7-4ad1-8fc4-d2064f235e57");

            migrationBuilder.DropColumn(
                name: "ReceiverPostalCode",
                table: "Deliverys");

            migrationBuilder.DropColumn(
                name: "SenderPostalCode",
                table: "Deliverys");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97adc4e2-de00-4ddd-bdf8-f72cc55738f1", "2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e397a659-df5a-455e-850a-26b9b17cbccc", "1", "Admin", "ADMIN" });
        }
    }
}
