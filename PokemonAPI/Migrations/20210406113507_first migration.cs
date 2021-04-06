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
                    TypeEffectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OffensePokemonTypeId = table.Column<int>(type: "int", nullable: false),
                    DefensePokemonTypeId = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeEffects", x => x.TypeEffectId);
                    table.ForeignKey(
                        name: "FK_TypeEffects_PokemonTypes_DefensePokemonTypeId",
                        column: x => x.DefensePokemonTypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "PokemonTypeId");
                    table.ForeignKey(
                        name: "FK_TypeEffects_PokemonTypes_OffensePokemonTypeId",
                        column: x => x.OffensePokemonTypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "PokemonTypeId");
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
                columns: new[] { "TypeEffectId", "DefensePokemonTypeId", "OffensePokemonTypeId", "Power" },
                values: new object[,]
                {
                    { 8, 2, 2, 2m },
                    { 99, 16, 15, 0.5m },
                    { 84, 16, 13, 2m },
                    { 68, 16, 10, 0m },
                    { 11, 16, 3, 2m },
                    { 100, 1, 15, 0m },
                    { 98, 10, 15, 2m },
                    { 97, 15, 15, 2m },
                    { 91, 15, 13, 0.5m },
                    { 49, 15, 7, 0.5m },
                    { 21, 15, 3, 0m },
                    { 3, 15, 1, 0m },
                    { 94, 14, 14, 2m },
                    { 76, 14, 12, 2m },
                    { 52, 14, 8, 0.5m },
                    { 38, 14, 6, 0.5m },
                    { 25, 14, 4, 0.5m },
                    { 7, 14, 2, 2m },
                    { 92, 7, 13, 0.5m },
                    { 90, 5, 13, 0.5m },
                    { 89, 2, 13, 0.5m },
                    { 88, 3, 13, 0.5m },
                    { 86, 10, 13, 2m },
                    { 85, 6, 13, 2m },
                    { 69, 13, 11, 2m },
                    { 61, 13, 9, 0.5m },
                    { 101, 15, 16, 2m },
                    { 102, 10, 16, 2m },
                    { 103, 16, 16, 0.5m },
                    { 105, 3, 16, 0.5m },
                    { 116, 2, 18, 0.5m },
                    { 115, 3, 18, 2m },
                    { 114, 14, 18, 2m },
                    { 113, 16, 18, 2m },
                    { 106, 18, 17, 2m },
                    { 104, 18, 16, 0.5m },
                    { 96, 18, 14, 0m },
                    { 87, 18, 13, 0.5m },
                    { 44, 18, 7, 2m },
                    { 17, 18, 3, 0.5m },
                    { 112, 4, 17, 0.5m },
                    { 111, 17, 17, 0.5m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefensePokemonTypeId", "OffensePokemonTypeId", "Power" },
                values: new object[,]
                {
                    { 37, 13, 6, 0.5m },
                    { 110, 2, 17, 0.5m },
                    { 108, 11, 17, 2m },
                    { 107, 12, 17, 2m },
                    { 95, 17, 14, 0.5m },
                    { 93, 17, 13, 0.5m },
                    { 82, 17, 12, 0.5m },
                    { 75, 17, 11, 0.5m },
                    { 67, 17, 10, 0.5m },
                    { 60, 17, 9, 2m },
                    { 43, 17, 6, 0.5m },
                    { 33, 17, 5, 0.5m },
                    { 15, 17, 3, 2m },
                    { 2, 17, 1, 0.5m },
                    { 109, 8, 17, 0.5m },
                    { 117, 7, 18, 0.5m },
                    { 28, 13, 5, 2m },
                    { 4, 13, 2, 2m },
                    { 47, 9, 7, 0.5m },
                    { 34, 9, 6, 2m },
                    { 23, 9, 4, 2m },
                    { 54, 6, 8, 0.5m },
                    { 53, 8, 8, 0.5m },
                    { 51, 4, 8, 2m },
                    { 50, 5, 8, 2m },
                    { 31, 8, 5, 0.5m },
                    { 46, 7, 7, 0.5m },
                    { 45, 6, 7, 2m },
                    { 42, 7, 6, 0.5m },
                    { 19, 7, 3, 0.5m },
                    { 41, 6, 6, 0.5m },
                    { 40, 5, 6, 0.5m },
                    { 39, 2, 6, 0.5m },
                    { 36, 4, 6, 2m },
                    { 30, 6, 5, 2m },
                    { 26, 6, 4, 0.5m },
                    { 5, 6, 2, 2m },
                    { 29, 3, 5, 2m },
                    { 18, 5, 3, 0.5m },
                    { 27, 4, 4, 0.5m },
                    { 22, 2, 4, 2m },
                    { 10, 4, 2, 2m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefensePokemonTypeId", "OffensePokemonTypeId", "Power" },
                values: new object[,]
                {
                    { 13, 1, 3, 2m },
                    { 55, 9, 8, 0m },
                    { 56, 8, 9, 2m },
                    { 57, 2, 9, 2m },
                    { 58, 7, 9, 2m },
                    { 83, 4, 12, 0.5m },
                    { 81, 12, 12, 0.5m },
                    { 80, 2, 12, 0.5m },
                    { 79, 9, 12, 2m },
                    { 78, 6, 12, 2m },
                    { 77, 5, 12, 2m },
                    { 72, 12, 11, 2m },
                    { 12, 12, 3, 2m },
                    { 6, 12, 2, 2m },
                    { 74, 9, 11, 0.5m },
                    { 73, 3, 11, 0.5m },
                    { 71, 5, 11, 2m },
                    { 16, 13, 3, 0m },
                    { 70, 2, 11, 2m },
                    { 48, 11, 7, 0.5m },
                    { 35, 11, 6, 2m },
                    { 32, 11, 5, 0.5m },
                    { 24, 11, 4, 2m },
                    { 14, 11, 3, 2m },
                    { 9, 11, 2, 2m },
                    { 1, 11, 1, 0.5m },
                    { 66, 10, 10, 0.5m },
                    { 65, 7, 10, 2m },
                    { 20, 10, 3, 0.5m },
                    { 63, 5, 9, 0m },
                    { 62, 6, 9, 0.5m },
                    { 59, 11, 9, 2m },
                    { 118, 17, 18, 0.5m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeEffects_DefensePokemonTypeId",
                table: "TypeEffects",
                column: "DefensePokemonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeEffects_OffensePokemonTypeId",
                table: "TypeEffects",
                column: "OffensePokemonTypeId");
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
