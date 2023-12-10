using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vezeeta.Repository.Migrations
{
    public partial class edit2price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PriceBefore",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "FinalPrice",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bdcb8ca7-0cbc-4412-8561-589d4f9ec7e1", "AQAAAAEAACcQAAAAEEJFyqlvj569jJ3br4be1f5XVqbU7p8yH8lf3ovt8DwN94XxNCmj6bqfBtWO55QoXw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Doctors",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PriceBefore",
                table: "Bookings",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "FinalPrice",
                table: "Bookings",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c9087601-cc2f-4464-8a3c-bd2e1bfd8421", "AQAAAAEAACcQAAAAEGwSBgUHtCHpNqHRTk4feU7S/B3PTcCG/LNbxYnvKQ4TqJKwQi5rAq3hLCJaXGVAKw==" });
        }
    }
}
