using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vezeeta.Repository.Migrations
{
    public partial class edit2appuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "770c2b2f-963e-4033-9383-d27f3b7fb8de", "AQAAAAEAACcQAAAAEEaL4hVltjunHnesl+9egG5w1ThwF4ukmnWpIz1AJOzEPQqd3/r9LCFA+P8Op7WT1A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2336bc8f-05d9-4e65-ad66-80a3ea21833c", "AQAAAAEAACcQAAAAELupnUfOFLftUsphjiBdfPvsi8LSBbprTuW4EBLcW58evAwGS3zRzxgyv7jBqNU7vw==" });
        }
    }
}
