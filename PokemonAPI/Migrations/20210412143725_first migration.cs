using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonAPI.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokedexEntry = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Generation = table.Column<int>(type: "int", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EggGroup = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.PokemonId);
                });

            migrationBuilder.CreateTable(
                name: "Typings",
                columns: table => new
                {
                    TypingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typings", x => x.TypingId);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTyping",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    TypingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTyping", x => new { x.PokemonId, x.TypingId });
                    table.ForeignKey(
                        name: "FK_PokemonTyping_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId");
                    table.ForeignKey(
                        name: "FK_PokemonTyping_Typings_TypingId",
                        column: x => x.TypingId,
                        principalTable: "Typings",
                        principalColumn: "TypingId");
                });

            migrationBuilder.CreateTable(
                name: "TypeEffects",
                columns: table => new
                {
                    TypeEffectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OffenseTypingId = table.Column<int>(type: "int", nullable: false),
                    DefenseTypingId = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeEffects", x => x.TypeEffectId);
                    table.ForeignKey(
                        name: "FK_TypeEffects_Typings_DefenseTypingId",
                        column: x => x.DefenseTypingId,
                        principalTable: "Typings",
                        principalColumn: "TypingId");
                    table.ForeignKey(
                        name: "FK_TypeEffects_Typings_OffenseTypingId",
                        column: x => x.OffenseTypingId,
                        principalTable: "Typings",
                        principalColumn: "TypingId");
                });

            migrationBuilder.InsertData(
                table: "Pokemons",
                columns: new[] { "PokemonId", "Classification", "EggGroup", "Generation", "Name", "PokedexEntry" },
                values: new object[,]
                {
                    { 1, "Sleeping Pokemon", "Monster", 1, "Snorlax", 143 },
                    { 21, "Head Butt Pokemon", "Monster", 4, "Rampardos", 409 },
                    { 22, "Tundra Pokemon", "Monster", 6, "Aurorus", 699 },
                    { 23, "Snow Hat Pokemon", "Fairy, Mineral", 3, "Snorunt", 361 },
                    { 24, "Worm Pokemon", "Bug", 8, "Snom", 872 },
                    { 25, "Leaf-Wrapped Pokemon", "Bug", 5, "Swadloon", 541 },
                    { 26, "Curlipede Pokemon", "Bug", 5, "Whirlipede", 544 },
                    { 28, "Stealth Pokemon", "Amorphous, Dragon", 8, "Dragapult", 887 },
                    { 29, "Screech Pokemon", "Amorphous", 2, "Misdreavus", 200 },
                    { 30, "Magical Pokemon", "Amorphous", 4, "Mismagius", 429 },
                    { 31, "Wily Pokemon", "Field, Grass", 3, "Nuzleaf", 306 },
                    { 32, "Skunk Pokemon", "Field", 4, "Skuntank", 435 },
                    { 33, "Iron Armor Pokemon", "Monster", 3, "Aggron", 306 },
                    { 34, "Thorn Pod Pokemon", "Grass, Mineral", 5, "Ferrothorn", 598 },
                    { 35, "Balloon Pokemon", "Fairy", 1, "Jigglypuff", 39 },
                    { 36, "Embrace Pokemon", "Human-Like, Amorphous", 3, "Gardevoir", 282 },
                    { 20, "Bronze Bell Pokemon", "Mineral", 4, "Bronzong", 437 },
                    { 19, "Psi Pokemon", "Human-Like", 1, "Abra", 63 },
                    { 27, "Dragon Pokemon", "Water 1, Dragon", 1, "Dragonair", 148 },
                    { 17, "Automaton Pokemon", "Mineral", 5, "Golett", 622 },
                    { 18, "Order Pokemon", "Undiscovered", 6, "Zygarde", 718 },
                    { 2, "Blocking Pokemon", "Field", 8, "Obstagoon", 598 },
                    { 3, "Lizard Pokemon", "Monster, Dragon", 1, "Charmander", 4 },
                    { 4, "Sun Pokemon", "Bug", 5, "Volcarona", 637 },
                    { 5, "Superpower Pokemon", "Human-Like", 1, "Machoke", 67 },
                    { 7, "Mud Fish Pokemon", "Monster, Water 1", 3, "Swampert", 260 },
                    { 8, "Mock Kelp Pokemon", "Bug", 6, "Skrelp", 637 },
                    { 9, "Raven Pokemon", "Flying", 8, "Corviknight", 823 },
                    { 6, "Single Horn Pokemon", "Bug", 2, "Heracross", 214 },
                    { 11, "Sickle Grass Pokemon", "Grass", 7, "Fomantis", 753 },
                    { 12, "Sea Lily Pokemon", "Water 3", 3, "Lileep", 345 },
                    { 13, "Poison Pin Pokemon", "Undiscovered", 7, "Naganadel", 804 },
                    { 14, "Bouquet Pokemon", "Fairy, Grass", 4, "Roserade", 407 },
                    { 15, "Magnet Area Pokemon", "Mineral", 4, "Magnezone", 462 },
                    { 16, "Deep Black Pokemon", "Undiscovered", 5, "Zekrom", 644 },
                    { 10, "Balloon Pokemon", "Amorphous", 4, "Drifloon", 425 }
                });

            migrationBuilder.InsertData(
                table: "Typings",
                columns: new[] { "TypingId", "Name" },
                values: new object[,]
                {
                    { 16, "Dark" },
                    { 10, "Psychic" },
                    { 15, "Ghost" },
                    { 14, "Dragon" },
                    { 13, "Bug" },
                    { 12, "Ice" }
                });

            migrationBuilder.InsertData(
                table: "Typings",
                columns: new[] { "TypingId", "Name" },
                values: new object[,]
                {
                    { 11, "Rock" },
                    { 9, "Ground" },
                    { 1, "Normal" },
                    { 7, "Poison" },
                    { 6, "Grass" },
                    { 5, "Flying" },
                    { 4, "Water" },
                    { 3, "Fighting" },
                    { 2, "Fire" },
                    { 17, "Steel" },
                    { 8, "Electric" },
                    { 18, "Fairy" }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 22, 11 },
                    { 21, 11 },
                    { 12, 11 },
                    { 9, 17 },
                    { 36, 10 },
                    { 20, 10 },
                    { 19, 10 },
                    { 15, 17 },
                    { 20, 17 },
                    { 33, 17 },
                    { 34, 17 },
                    { 16, 14 },
                    { 18, 9 },
                    { 17, 9 },
                    { 7, 9 },
                    { 33, 11 },
                    { 32, 16 },
                    { 31, 16 },
                    { 2, 16 },
                    { 18, 14 },
                    { 27, 14 },
                    { 28, 14 },
                    { 26, 13 },
                    { 25, 13 },
                    { 24, 13 },
                    { 6, 13 },
                    { 16, 8 },
                    { 4, 13 },
                    { 17, 15 },
                    { 28, 15 },
                    { 29, 15 },
                    { 30, 15 },
                    { 24, 12 },
                    { 23, 12 },
                    { 22, 12 },
                    { 10, 15 },
                    { 15, 8 },
                    { 13, 14 },
                    { 36, 18 },
                    { 25, 6 },
                    { 14, 6 }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { 8, 4 },
                    { 7, 4 },
                    { 35, 18 },
                    { 6, 3 },
                    { 5, 3 },
                    { 8, 7 },
                    { 13, 7 },
                    { 34, 6 },
                    { 4, 2 },
                    { 14, 7 },
                    { 26, 7 },
                    { 32, 7 },
                    { 9, 5 },
                    { 12, 6 },
                    { 11, 6 },
                    { 10, 5 },
                    { 3, 2 },
                    { 35, 1 },
                    { 2, 1 },
                    { 31, 6 }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefenseTypingId", "OffenseTypingId", "Power" },
                values: new object[,]
                {
                    { 49, 15, 7, 0.5m },
                    { 21, 15, 3, 0m },
                    { 3, 15, 1, 0m },
                    { 106, 18, 17, 2m },
                    { 91, 15, 13, 0.5m },
                    { 107, 12, 17, 2m },
                    { 114, 14, 18, 2m },
                    { 115, 3, 18, 2m },
                    { 104, 18, 16, 0.5m },
                    { 116, 2, 18, 0.5m },
                    { 94, 14, 14, 2m },
                    { 76, 14, 12, 2m },
                    { 52, 14, 8, 0.5m },
                    { 38, 14, 6, 0.5m },
                    { 25, 14, 4, 0.5m },
                    { 7, 14, 2, 2m },
                    { 113, 16, 18, 2m },
                    { 97, 15, 15, 2m },
                    { 68, 16, 10, 0m },
                    { 100, 1, 15, 0m },
                    { 109, 8, 17, 0.5m },
                    { 110, 2, 17, 0.5m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefenseTypingId", "OffenseTypingId", "Power" },
                values: new object[,]
                {
                    { 95, 17, 14, 0.5m },
                    { 93, 17, 13, 0.5m },
                    { 82, 17, 12, 0.5m },
                    { 75, 17, 11, 0.5m },
                    { 67, 17, 10, 0.5m },
                    { 60, 17, 9, 2m },
                    { 43, 17, 6, 0.5m },
                    { 33, 17, 5, 0.5m },
                    { 15, 17, 3, 2m },
                    { 111, 17, 17, 0.5m },
                    { 112, 4, 17, 0.5m },
                    { 17, 18, 3, 0.5m },
                    { 44, 18, 7, 2m },
                    { 87, 18, 13, 0.5m },
                    { 105, 3, 16, 0.5m },
                    { 103, 16, 16, 0.5m },
                    { 102, 10, 16, 2m },
                    { 101, 15, 16, 2m },
                    { 99, 16, 15, 0.5m },
                    { 84, 16, 13, 2m },
                    { 108, 11, 17, 2m },
                    { 11, 16, 3, 2m },
                    { 96, 18, 14, 0m },
                    { 98, 10, 15, 2m },
                    { 2, 17, 1, 0.5m },
                    { 79, 9, 12, 2m },
                    { 90, 5, 13, 0.5m },
                    { 62, 6, 9, 0.5m },
                    { 58, 7, 9, 2m },
                    { 57, 2, 9, 2m },
                    { 56, 8, 9, 2m },
                    { 55, 9, 8, 0m },
                    { 47, 9, 7, 0.5m },
                    { 34, 9, 6, 2m },
                    { 23, 9, 4, 2m },
                    { 54, 6, 8, 0.5m },
                    { 53, 8, 8, 0.5m },
                    { 51, 4, 8, 2m },
                    { 50, 5, 8, 2m },
                    { 31, 8, 5, 0.5m },
                    { 46, 7, 7, 0.5m },
                    { 45, 6, 7, 2m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefenseTypingId", "OffenseTypingId", "Power" },
                values: new object[,]
                {
                    { 42, 7, 6, 0.5m },
                    { 19, 7, 3, 0.5m },
                    { 8, 2, 2, 2m },
                    { 13, 1, 3, 2m },
                    { 10, 4, 2, 2m },
                    { 22, 2, 4, 2m },
                    { 27, 4, 4, 0.5m },
                    { 18, 5, 3, 0.5m },
                    { 63, 5, 9, 0m },
                    { 29, 3, 5, 2m },
                    { 26, 6, 4, 0.5m },
                    { 30, 6, 5, 2m },
                    { 36, 4, 6, 2m },
                    { 39, 2, 6, 0.5m },
                    { 40, 5, 6, 0.5m },
                    { 41, 6, 6, 0.5m },
                    { 5, 6, 2, 2m },
                    { 92, 7, 13, 0.5m },
                    { 20, 10, 3, 0.5m },
                    { 66, 10, 10, 0.5m },
                    { 89, 2, 13, 0.5m },
                    { 88, 3, 13, 0.5m },
                    { 86, 10, 13, 2m },
                    { 85, 6, 13, 2m },
                    { 69, 13, 11, 2m },
                    { 61, 13, 9, 0.5m },
                    { 37, 13, 6, 0.5m },
                    { 28, 13, 5, 2m },
                    { 16, 13, 3, 0m },
                    { 4, 13, 2, 2m },
                    { 83, 4, 12, 0.5m },
                    { 81, 12, 12, 0.5m },
                    { 80, 2, 12, 0.5m },
                    { 117, 7, 18, 0.5m },
                    { 78, 6, 12, 2m },
                    { 77, 5, 12, 2m },
                    { 72, 12, 11, 2m },
                    { 1, 11, 1, 0.5m },
                    { 9, 11, 2, 2m },
                    { 14, 11, 3, 2m },
                    { 24, 11, 4, 2m },
                    { 32, 11, 5, 0.5m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefenseTypingId", "OffenseTypingId", "Power" },
                values: new object[,]
                {
                    { 35, 11, 6, 2m },
                    { 65, 7, 10, 2m },
                    { 48, 11, 7, 0.5m },
                    { 70, 2, 11, 2m },
                    { 71, 5, 11, 2m },
                    { 73, 3, 11, 0.5m },
                    { 74, 9, 11, 0.5m },
                    { 6, 12, 2, 2m },
                    { 12, 12, 3, 2m },
                    { 59, 11, 9, 2m },
                    { 118, 17, 18, 0.5m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTyping_TypingId",
                table: "PokemonTyping",
                column: "TypingId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeEffects_DefenseTypingId",
                table: "TypeEffects",
                column: "DefenseTypingId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeEffects_OffenseTypingId",
                table: "TypeEffects",
                column: "OffenseTypingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonTyping");

            migrationBuilder.DropTable(
                name: "TypeEffects");

            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "Typings");
        }
    }
}
