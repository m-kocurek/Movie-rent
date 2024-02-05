using Microsoft.EntityFrameworkCore.Migrations;

namespace AGH_movie_rent.Migrations
{
    public partial class AddFewPropertiesToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieRental_Movie_MovieId",
                table: "MovieRental");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieRental_User_UserId",
                table: "MovieRental");

            migrationBuilder.DropIndex(
                name: "IX_MovieRental_MovieId",
                table: "MovieRental");

            migrationBuilder.DropIndex(
                name: "IX_MovieRental_UserId",
                table: "MovieRental");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MovieRental",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "MovieRental",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MovieRental",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "MovieRental",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRental_MovieId",
                table: "MovieRental",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRental_UserId",
                table: "MovieRental",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRental_Movie_MovieId",
                table: "MovieRental",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRental_User_UserId",
                table: "MovieRental",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
