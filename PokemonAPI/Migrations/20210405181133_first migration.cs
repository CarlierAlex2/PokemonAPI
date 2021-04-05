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
                    PokemonTypeId = table.Column<int>(type: "int", nullable: false),
                    TargetPokemonTypeId = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeEffects", x => new { x.PokemonTypeId, x.TargetPokemonTypeId });
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
                columns: new[] { "PokemonTypeId", "TargetPokemonTypeId", "Power" },
                values: new object[,]
                {
                    { 3, 1, 2m },
                    { 12, 14, 2m },
                    { 8, 14, 0.5m },
                    { 6, 14, 0.5m },
                    { 4, 14, 0.5m },
                    { 2, 14, 2m },
                    { 11, 13, 2m },
                    { 9, 13, 0.5m },
                    { 6, 13, 0.5m },
                    { 5, 13, 2m },
                    { 3, 13, 0m },
                    { 2, 13, 2m },
                    { 17, 12, 2m },
                    { 12, 12, 0.5m },
                    { 11, 12, 2m },
                    { 3, 12, 2m },
                    { 2, 12, 2m },
                    { 17, 11, 2m },
                    { 9, 11, 2m },
                    { 7, 11, 0.5m },
                    { 6, 11, 2m },
                    { 5, 11, 0.5m },
                    { 4, 11, 2m },
                    { 3, 11, 2m },
                    { 2, 11, 2m },
                    { 1, 11, 0.5m },
                    { 14, 14, 2m },
                    { 18, 14, 2m },
                    { 1, 15, 0m },
                    { 3, 15, 0m },
                    { 14, 18, 0m },
                    { 13, 18, 0.5m },
                    { 7, 18, 2m },
                    { 3, 18, 0.5m },
                    { 18, 17, 0.5m },
                    { 17, 17, 0.5m },
                    { 14, 17, 0.5m },
                    { 13, 17, 0.5m },
                    { 12, 17, 0.5m },
                    { 11, 17, 0.5m },
                    { 10, 17, 0.5m },
                    { 9, 17, 2m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "PokemonTypeId", "TargetPokemonTypeId", "Power" },
                values: new object[,]
                {
                    { 16, 10, 2m },
                    { 6, 17, 0.5m },
                    { 3, 17, 2m },
                    { 1, 17, 0.5m },
                    { 18, 16, 2m },
                    { 16, 16, 0.5m },
                    { 15, 16, 0.5m },
                    { 13, 16, 2m },
                    { 10, 16, 0m },
                    { 3, 16, 2m },
                    { 16, 15, 2m },
                    { 15, 15, 2m },
                    { 13, 15, 0.5m },
                    { 7, 15, 0.5m },
                    { 5, 17, 0.5m },
                    { 16, 18, 0.5m },
                    { 15, 10, 2m },
                    { 10, 10, 0.5m },
                    { 9, 5, 0m },
                    { 8, 5, 2m },
                    { 6, 5, 0.5m },
                    { 3, 5, 0.5m },
                    { 17, 4, 0.5m },
                    { 12, 4, 0.5m },
                    { 8, 4, 2m },
                    { 6, 4, 2m },
                    { 4, 4, 0.5m },
                    { 2, 4, 2m },
                    { 18, 3, 2m },
                    { 16, 3, 0.5m },
                    { 13, 3, 0.5m },
                    { 11, 3, 0.5m },
                    { 5, 3, 2m },
                    { 18, 2, 0.5m },
                    { 17, 2, 0.5m },
                    { 13, 2, 0.5m },
                    { 12, 2, 0.5m },
                    { 11, 2, 2m },
                    { 9, 2, 2m },
                    { 6, 2, 0.5m },
                    { 4, 2, 2m },
                    { 2, 2, 2m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "PokemonTypeId", "TargetPokemonTypeId", "Power" },
                values: new object[,]
                {
                    { 15, 1, 0m },
                    { 11, 5, 2m },
                    { 12, 5, 2m },
                    { 13, 5, 0.5m },
                    { 2, 6, 2m },
                    { 3, 10, 0.5m },
                    { 12, 9, 2m },
                    { 11, 9, 0.5m },
                    { 8, 9, 0m },
                    { 7, 9, 0.5m },
                    { 6, 9, 2m },
                    { 4, 9, 2m },
                    { 17, 8, 0.5m },
                    { 9, 8, 2m },
                    { 8, 8, 0.5m },
                    { 5, 8, 0.5m },
                    { 18, 7, 0.5m },
                    { 13, 10, 2m },
                    { 13, 7, 0.5m },
                    { 9, 7, 2m },
                    { 7, 7, 0.5m },
                    { 6, 7, 0.5m },
                    { 3, 7, 0.5m },
                    { 13, 6, 2m },
                    { 12, 6, 2m },
                    { 9, 6, 0.5m },
                    { 8, 6, 0.5m },
                    { 7, 6, 2m },
                    { 6, 6, 0.5m },
                    { 5, 6, 2m },
                    { 4, 6, 0.5m },
                    { 10, 7, 2m },
                    { 17, 18, 2m }
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
