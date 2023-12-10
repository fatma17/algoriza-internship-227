using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vezeeta.Repository.Migrations
{
    public partial class editTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Time",
                table: "Times",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "501d66ea-5dcc-4a07-8354-74a59b3b7d20", "AQAAAAEAACcQAAAAEIenvsXPg+i/giHCpHHX1iqawlQSspMEwjBP3beK1SrKdHQQkXkibAtaltFXZpPJxg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Times",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "770c2b2f-963e-4033-9383-d27f3b7fb8de", "AQAAAAEAACcQAAAAEEaL4hVltjunHnesl+9egG5w1ThwF4ukmnWpIz1AJOzEPQqd3/r9LCFA+P8Op7WT1A==" });
        }
    }
}
