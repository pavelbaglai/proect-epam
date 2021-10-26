using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Store.Data.Migrations
{
    public partial class changeNamesOfGenreTypeAndGenreTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Genre_SongID",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "GenreTypeID",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "SongID",
                table: "Genre");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Genre",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SongGenre",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongID = table.Column<int>(nullable: false),
                    GenreID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongGenre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SongGenre_Genre_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genre",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongGenre_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongGenre_GenreID",
                table: "SongGenre",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_SongGenre_SongID",
                table: "SongGenre",
                column: "SongID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongGenre");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Genre");

            migrationBuilder.AddColumn<int>(
                name: "GenreTypeID",
                table: "Genre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SongID",
                table: "Genre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GenreType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreType", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genre_GenreTypeID",
                table: "Genre",
                column: "GenreTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_SongID",
                table: "Genre",
                column: "SongID");

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
    }
}
