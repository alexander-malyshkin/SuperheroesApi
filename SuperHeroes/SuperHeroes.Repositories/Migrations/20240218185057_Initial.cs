using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroes.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavouriteSuperheroes",
                columns: table => new
                {
                    UserToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SuperheroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavouriteSuperheroes", x => new { x.UserToken, x.SuperheroId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavouriteSuperheroes");
        }
    }
}
