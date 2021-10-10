using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicDotNet.Data.Migrations
{
    public partial class AddArtistBio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Artists");
        }
    }
}
