using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vezeeta.Repository.Migrations
{
    public partial class editAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2336bc8f-05d9-4e65-ad66-80a3ea21833c", "AQAAAAEAACcQAAAAELupnUfOFLftUsphjiBdfPvsi8LSBbprTuW4EBLcW58evAwGS3zRzxgyv7jBqNU7vw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b42d3b87-0b4b-486e-aeb3-f2cebc2c8cde", "AQAAAAEAACcQAAAAELzYe6izgs7zY7kYZC7judeCC0acsgzTN0zaVrbtjX5Z64y3onsfLRbgNx9mJ5KJEw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId");
        }
    }
}
