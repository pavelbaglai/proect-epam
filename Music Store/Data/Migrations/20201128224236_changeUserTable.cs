using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Store.Data.Migrations
{
    public partial class changeUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Album");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "User");

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Song",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Album",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
