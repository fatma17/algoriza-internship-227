using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vezeeta.Repository.Migrations
{
    public partial class edit3coupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1829cbff-ef77-44e7-819c-1a9264f3f1d9", "AQAAAAEAACcQAAAAEOA4BdZRgqvMRA+ojuAIsHfjOB68EdladErda6uU0KkzGrKRQIdguUozR6GHIPxfrQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_DiscoundCode",
                table: "Coupons",
                column: "DiscoundCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupons_DiscoundCode",
                table: "Coupons");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8e033a35-0643-4bfc-bdfe-d8f38d328447", "AQAAAAEAACcQAAAAECT6SaWljaY9TicMNJScyyLp7Mx8fq6SvB66B3dvr7gd2uXnytqVKkroCL9ENQFI9w==" });
        }
    }
}
