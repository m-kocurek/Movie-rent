using Microsoft.EntityFrameworkCore.Migrations;

namespace AGH_movie_rent.Migrations
{
    public partial class Add2ColToRental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieName",
                table: "MovieRental",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "MovieRental",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieName",
                table: "MovieRental");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "MovieRental");
        }
    }
}
