using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vezeeta.Repository.Migrations
{
    public partial class edit2coupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8e033a35-0643-4bfc-bdfe-d8f38d328447", "AQAAAAEAACcQAAAAECT6SaWljaY9TicMNJScyyLp7Mx8fq6SvB66B3dvr7gd2uXnytqVKkroCL9ENQFI9w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "88c6380f-83e9-4df8-9155-3c960d72fb5e", "AQAAAAEAACcQAAAAEGQBXky3H+33/mLqHrKYB6XYsXUqWxfPPea67sd7KlXueCgfqffGItAjrsKGjBXXFA==" });
        }
    }
}
