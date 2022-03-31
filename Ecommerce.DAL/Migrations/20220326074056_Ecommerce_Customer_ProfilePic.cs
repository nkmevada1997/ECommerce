using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.DAL.Migrations
{
    public partial class Ecommerce_Customer_ProfilePic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("903e0cfd-b702-4d02-bc46-4916428a2f7f"));

            migrationBuilder.AddColumn<string>(
                name: "AvatarImage",
                schema: "dbo",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "UserId", "CanLogin", "CreatedDate", "CustomerId", "Email", "IsDeleted", "Password", "SupplierId", "UserName", "UserType" },
                values: new object[] { new Guid("02148f9c-280e-4104-aad2-ec48d5c5fc1b"), true, new DateTime(2022, 3, 26, 7, 40, 56, 672, DateTimeKind.Utc).AddTicks(9299), null, "admin@gmail.com", false, "QWRtaW5AMTIz", null, "Admin", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("02148f9c-280e-4104-aad2-ec48d5c5fc1b"));

            migrationBuilder.DropColumn(
                name: "AvatarImage",
                schema: "dbo",
                table: "Customers");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "UserId", "CanLogin", "CreatedDate", "CustomerId", "Email", "IsDeleted", "Password", "SupplierId", "UserName", "UserType" },
                values: new object[] { new Guid("903e0cfd-b702-4d02-bc46-4916428a2f7f"), true, new DateTime(2022, 3, 26, 6, 35, 46, 483, DateTimeKind.Utc).AddTicks(3414), null, "admin@gmail.com", false, "QWRtaW5AMTIz", null, "Admin", 1 });
        }
    }
}
