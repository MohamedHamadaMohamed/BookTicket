using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTicket.Migrations
{
    /// <inheritdoc />
    public partial class alterMovieTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "views",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "views",
                table: "Movies");
        }
    }
}
