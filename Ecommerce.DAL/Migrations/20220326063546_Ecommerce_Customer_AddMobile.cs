using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.DAL.Migrations
{
    public partial class Ecommerce_Customer_AddMobile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ea654d18-1cf3-4d2f-9bbd-24edcee8043c"));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "dbo",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "UserId", "CanLogin", "CreatedDate", "CustomerId", "Email", "IsDeleted", "Password", "SupplierId", "UserName", "UserType" },
                values: new object[] { new Guid("903e0cfd-b702-4d02-bc46-4916428a2f7f"), true, new DateTime(2022, 3, 26, 6, 35, 46, 483, DateTimeKind.Utc).AddTicks(3414), null, "admin@gmail.com", false, "QWRtaW5AMTIz", null, "Admin", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("903e0cfd-b702-4d02-bc46-4916428a2f7f"));

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "dbo",
                table: "Customers");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "UserId", "CanLogin", "CreatedDate", "CustomerId", "Email", "IsDeleted", "Password", "SupplierId", "UserName", "UserType" },
                values: new object[] { new Guid("ea654d18-1cf3-4d2f-9bbd-24edcee8043c"), true, new DateTime(2022, 3, 8, 19, 23, 38, 127, DateTimeKind.Utc).AddTicks(1394), null, "admin@gmail.com", false, "QWRtaW5AMTIz", null, "Admin", 1 });
        }
    }
}
