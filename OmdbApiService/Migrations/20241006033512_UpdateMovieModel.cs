using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmdbApiService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMovieModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Awards",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Plot",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Rated",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Released",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Runtime",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Writer",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actors",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Awards",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Plot",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Rated",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Released",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Runtime",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Writer",
                table: "Movies");
        }
    }
}
