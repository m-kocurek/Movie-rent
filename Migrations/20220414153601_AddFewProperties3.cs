using Microsoft.EntityFrameworkCore.Migrations;

namespace AGH_movie_rent.Migrations
{
    public partial class AddFewProperties3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MovieRental",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "MovieRental");
        }
    }
}
