using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Store.Data.Migrations
{
    public partial class addGenreTypeTable_changeGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Album_AlbumID",
                table: "Genre");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Artist_ArtistID",
                table: "Genre");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Song_SongID",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_AlbumID",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_ArtistID",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "AlbumID",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "ArtistID",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Genre");

            migrationBuilder.AlterColumn<int>(
                name: "SongID",
                table: "Genre",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenreTypeID",
                table: "Genre",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GenreType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreType", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genre_GenreTypeID",
                table: "Genre",
                column: "GenreTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_GenreType_GenreTypeID",
                table: "Genre",
                column: "GenreTypeID",
                principalTable: "GenreType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Song_SongID",
                table: "Genre",
                column: "SongID",
                principalTable: "Song",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_GenreType_GenreTypeID",
                table: "Genre");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Song_SongID",
                table: "Genre");

            migrationBuilder.DropTable(
                name: "GenreType");

            migrationBuilder.DropIndex(
                name: "IX_Genre_GenreTypeID",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "GenreTypeID",
                table: "Genre");

            migrationBuilder.AlterColumn<int>(
                name: "SongID",
                table: "Genre",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AlbumID",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArtistID",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Genre",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_AlbumID",
                table: "Genre",
                column: "AlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_ArtistID",
                table: "Genre",
                column: "ArtistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Album_AlbumID",
                table: "Genre",
                column: "AlbumID",
                principalTable: "Album",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Artist_ArtistID",
                table: "Genre",
                column: "ArtistID",
                principalTable: "Artist",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Song_SongID",
                table: "Genre",
                column: "SongID",
                principalTable: "Song",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
