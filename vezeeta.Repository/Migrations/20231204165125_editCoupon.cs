using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vezeeta.Repository.Migrations
{
    public partial class editCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deactivate",
                table: "Coupons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "88c6380f-83e9-4df8-9155-3c960d72fb5e", "AQAAAAEAACcQAAAAEGQBXky3H+33/mLqHrKYB6XYsXUqWxfPPea67sd7KlXueCgfqffGItAjrsKGjBXXFA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deactivate",
                table: "Coupons");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "08bc5445-9625-4266-b093-eaf26b05eb14", "AQAAAAEAACcQAAAAEPpScuYGOQWMEW4AzgVd9rpXsvht0nUeH3mJ33+ecQvdi4y34of85Wi/OYAYs5sO3g==" });
        }
    }
}
