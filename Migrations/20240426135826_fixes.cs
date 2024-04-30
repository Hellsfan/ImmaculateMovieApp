using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImmaculateMovieApp.Migrations
{
    /// <inheritdoc />
    public partial class fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DirectorMovies",
                table: "Director",
                newName: "MoviesToString");

            migrationBuilder.AddColumn<string>(
                name: "Movies",
                table: "Director",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Movies",
                table: "Director");

            migrationBuilder.RenameColumn(
                name: "MoviesToString",
                table: "Director",
                newName: "DirectorMovies");
        }
    }
}
