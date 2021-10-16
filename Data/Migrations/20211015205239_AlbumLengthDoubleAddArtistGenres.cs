using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicDotNet.Data.Migrations
{
    public partial class AlbumLengthDoubleAddArtistGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genres",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Length",
                table: "Albums",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Artists");

            migrationBuilder.AlterColumn<int>(
                name: "Length",
                table: "Albums",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
