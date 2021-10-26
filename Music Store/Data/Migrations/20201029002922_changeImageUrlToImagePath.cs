using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Store.Data.Migrations
{
    public partial class changeImageUrlToImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Album");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Song",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Artist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Album",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Album");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Song",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Artist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Album",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
