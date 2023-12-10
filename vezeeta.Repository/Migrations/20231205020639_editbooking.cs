using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vezeeta.Repository.Migrations
{
    public partial class editbooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Coupons_CouponId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Bookings",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Bookings",
                newName: "PriceBefore");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "Bookings",
                type: "int",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "CouponId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "FinalPrice",
                table: "Bookings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c9087601-cc2f-4464-8a3c-bd2e1bfd8421", "AQAAAAEAACcQAAAAEGwSBgUHtCHpNqHRTk4feU7S/B3PTcCG/LNbxYnvKQ4TqJKwQi5rAq3hLCJaXGVAKw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Coupons_CouponId",
                table: "Bookings",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Coupons_CouponId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Bookings",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "PriceBefore",
                table: "Bookings",
                newName: "Price");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "CouponId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1829cbff-ef77-44e7-819c-1a9264f3f1d9", "AQAAAAEAACcQAAAAEOA4BdZRgqvMRA+ojuAIsHfjOB68EdladErda6uU0KkzGrKRQIdguUozR6GHIPxfrQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Coupons_CouponId",
                table: "Bookings",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
