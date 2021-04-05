using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonAPI.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonTypes",
                columns: table => new
                {
                    PokemonTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes", x => x.PokemonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TypeEffects",
                columns: table => new
                {
                    UserPokemonTypeId = table.Column<int>(type: "int", nullable: false),
                    TargetPokemonTypeId = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeEffects", x => new { x.UserPokemonTypeId, x.TargetPokemonTypeId });
                    table.ForeignKey(
                        name: "FK_TypeEffects_PokemonTypes_TargetPokemonTypeId",
                        column: x => x.TargetPokemonTypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "PokemonTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PokemonTypes",
                columns: new[] { "PokemonTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Normal" },
                    { 16, "Dark" },
                    { 15, "Ghost" },
                    { 14, "Dragon" },
                    { 13, "Bug" },
                    { 12, "Ice" },
                    { 11, "Rock" },
                    { 10, "Psychic" },
                    { 9, "Ground" },
                    { 8, "Electric" },
                    { 7, "Poison" },
                    { 6, "Grass" },
                    { 5, "Flying" },
                    { 4, "Water" },
                    { 3, "Fighting" },
                    { 2, "Fire" },
                    { 17, "Steel" },
                    { 18, "Fairy" }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TargetPokemonTypeId", "UserPokemonTypeId", "Power" },
                values: new object[,]
                {
                    { 1, 3, 2m },
                    { 14, 12, 2m },
                    { 14, 8, 0.5m },
                    { 14, 6, 0.5m },
                    { 14, 4, 0.5m },
                    { 14, 2, 2m },
                    { 13, 11, 2m },
                    { 13, 9, 0.5m },
                    { 13, 6, 0.5m },
                    { 13, 5, 2m },
                    { 13, 3, 0m },
                    { 13, 2, 2m },
                    { 12, 17, 2m },
                    { 12, 12, 0.5m },
                    { 12, 11, 2m },
                    { 12, 3, 2m },
                    { 12, 2, 2m },
                    { 11, 17, 2m },
                    { 11, 9, 2m },
                    { 11, 7, 0.5m },
                    { 11, 6, 2m },
                    { 11, 5, 0.5m },
                    { 11, 4, 2m },
                    { 11, 3, 2m },
                    { 11, 2, 2m },
                    { 11, 1, 0.5m },
                    { 14, 14, 2m },
                    { 14, 18, 2m },
                    { 15, 1, 0m },
                    { 15, 3, 0m },
                    { 18, 14, 0m },
                    { 18, 13, 0.5m },
                    { 18, 7, 2m },
                    { 18, 3, 0.5m },
                    { 17, 18, 0.5m },
                    { 17, 17, 0.5m },
                    { 17, 14, 0.5m },
                    { 17, 13, 0.5m },
                    { 17, 12, 0.5m },
                    { 17, 11, 0.5m },
                    { 17, 10, 0.5m },
                    { 17, 9, 2m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TargetPokemonTypeId", "UserPokemonTypeId", "Power" },
                values: new object[,]
                {
                    { 10, 16, 2m },
                    { 17, 6, 0.5m },
                    { 17, 3, 2m },
                    { 17, 1, 0.5m },
                    { 16, 18, 2m },
                    { 16, 16, 0.5m },
                    { 16, 15, 0.5m },
                    { 16, 13, 2m },
                    { 16, 10, 0m },
                    { 16, 3, 2m },
                    { 15, 16, 2m },
                    { 15, 15, 2m },
                    { 15, 13, 0.5m },
                    { 15, 7, 0.5m },
                    { 17, 5, 0.5m },
                    { 18, 16, 0.5m },
                    { 10, 15, 2m },
                    { 10, 10, 0.5m },
                    { 5, 9, 0m },
                    { 5, 8, 2m },
                    { 5, 6, 0.5m },
                    { 5, 3, 0.5m },
                    { 4, 17, 0.5m },
                    { 4, 12, 0.5m },
                    { 4, 8, 2m },
                    { 4, 6, 2m },
                    { 4, 4, 0.5m },
                    { 4, 2, 2m },
                    { 3, 18, 2m },
                    { 3, 16, 0.5m },
                    { 3, 13, 0.5m },
                    { 3, 11, 0.5m },
                    { 3, 5, 2m },
                    { 2, 18, 0.5m },
                    { 2, 17, 0.5m },
                    { 2, 13, 0.5m },
                    { 2, 12, 0.5m },
                    { 2, 11, 2m },
                    { 2, 9, 2m },
                    { 2, 6, 0.5m },
                    { 2, 4, 2m },
                    { 2, 2, 2m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TargetPokemonTypeId", "UserPokemonTypeId", "Power" },
                values: new object[,]
                {
                    { 1, 15, 0m },
                    { 5, 11, 2m },
                    { 5, 12, 2m },
                    { 5, 13, 0.5m },
                    { 6, 2, 2m },
                    { 10, 3, 0.5m },
                    { 9, 12, 2m },
                    { 9, 11, 0.5m },
                    { 9, 8, 0m },
                    { 9, 7, 0.5m },
                    { 9, 6, 2m },
                    { 9, 4, 2m },
                    { 8, 17, 0.5m },
                    { 8, 9, 2m },
                    { 8, 8, 0.5m },
                    { 8, 5, 0.5m },
                    { 7, 18, 0.5m },
                    { 10, 13, 2m },
                    { 7, 13, 0.5m },
                    { 7, 9, 2m },
                    { 7, 7, 0.5m },
                    { 7, 6, 0.5m },
                    { 7, 3, 0.5m },
                    { 6, 13, 2m },
                    { 6, 12, 2m },
                    { 6, 9, 0.5m },
                    { 6, 8, 0.5m },
                    { 6, 7, 2m },
                    { 6, 6, 0.5m },
                    { 6, 5, 2m },
                    { 6, 4, 0.5m },
                    { 7, 10, 2m },
                    { 18, 17, 2m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeEffects_TargetPokemonTypeId",
                table: "TypeEffects",
                column: "TargetPokemonTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeEffects");

            migrationBuilder.DropTable(
                name: "PokemonTypes");
        }
    }
}
