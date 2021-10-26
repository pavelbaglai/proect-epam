using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Store.Data.Migrations
{
    public partial class changeAlbum_Cart_CartItem_Song : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Song_SongID",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartItem");

            migrationBuilder.AlterColumn<int>(
                name: "SongID",
                table: "CartItem",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AlbumID",
                table: "CartItem",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "CartItem",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_AlbumID",
                table: "CartItem",
                column: "AlbumID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Album_AlbumID",
                table: "CartItem",
                column: "AlbumID",
                principalTable: "Album",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Song_SongID",
                table: "CartItem",
                column: "SongID",
                principalTable: "Song",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Album_AlbumID",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Song_SongID",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_AlbumID",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "AlbumID",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartItem");

            migrationBuilder.AlterColumn<int>(
                name: "SongID",
                table: "CartItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TotalPrice",
                table: "CartItem",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Song_SongID",
                table: "CartItem",
                column: "SongID",
                principalTable: "Song",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
