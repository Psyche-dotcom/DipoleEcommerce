using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    public partial class addDeliveryEntitys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_AspNetUsers_UserId",
                table: "Delivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d322740-bed7-4933-8f34-ad0697968ff1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d9f062-b6f9-4fda-a3b2-0e886c1475e0");

            migrationBuilder.RenameTable(
                name: "Delivery",
                newName: "Deliverys");

            migrationBuilder.RenameIndex(
                name: "IX_Delivery_UserId",
                table: "Deliverys",
                newName: "IX_Deliverys_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deliverys",
                table: "Deliverys",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97adc4e2-de00-4ddd-bdf8-f72cc55738f1", "2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e397a659-df5a-455e-850a-26b9b17cbccc", "1", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Deliverys_AspNetUsers_UserId",
                table: "Deliverys",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliverys_AspNetUsers_UserId",
                table: "Deliverys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deliverys",
                table: "Deliverys");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97adc4e2-de00-4ddd-bdf8-f72cc55738f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e397a659-df5a-455e-850a-26b9b17cbccc");

            migrationBuilder.RenameTable(
                name: "Deliverys",
                newName: "Delivery");

            migrationBuilder.RenameIndex(
                name: "IX_Deliverys_UserId",
                table: "Delivery",
                newName: "IX_Delivery_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d322740-bed7-4933-8f34-ad0697968ff1", "2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f6d9f062-b6f9-4fda-a3b2-0e886c1475e0", "1", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_AspNetUsers_UserId",
                table: "Delivery",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
