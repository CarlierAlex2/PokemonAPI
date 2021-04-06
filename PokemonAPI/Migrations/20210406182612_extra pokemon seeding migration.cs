using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonAPI.Migrations
{
    public partial class extrapokemonseedingmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 1, 17 });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 1,
                columns: new[] { "Classification", "Generation", "Name", "PokedexEntry" },
                values: new object[] { "Sleeping Pokemon", 1, "Snorlax", 143 });

            migrationBuilder.InsertData(
                table: "Pokemons",
                columns: new[] { "PokemonId", "Classification", "EggGroup", "Generation", "Name", "PokedexEntry" },
                values: new object[,]
                {
                    { 35, "Balloon Pokemon", "Fairy", 1, "Jigglypuff", 39 },
                    { 34, "Thorn Pod Pokemon", "Grass, Mineral", 5, "Ferrothorn", 598 },
                    { 33, "Iron Armor Pokemon", "Monster", 3, "Aggron", 306 },
                    { 32, "Skunk Pokemon", "Field", 4, "Skuntank", 435 },
                    { 31, "Wily Pokemon", "Field, Grass", 3, "Nuzleaf", 306 },
                    { 30, "Magical Pokemon", "Amorphous", 4, "Mismagius", 429 },
                    { 21, "Head Butt Pokemon", "Monster", 4, "Rampardos", 409 },
                    { 28, "Stealth Pokemon", "Amorphous, Dragon", 8, "Dragapult", 887 },
                    { 27, "Dragon Pokemon", "Water 1, Dragon", 1, "Dragonair", 148 },
                    { 26, "Curlipede Pokemon", "Bug", 5, "Whirlipede", 544 },
                    { 25, "Leaf-Wrapped Pokemon", "Bug", 5, "Swadloon", 541 },
                    { 24, "Worm Pokemon", "Bug", 8, "Snom", 872 },
                    { 23, "Snow Hat Pokemon", "Fairy, Mineral", 3, "Snorunt", 361 },
                    { 22, "Tundra Pokemon", "Monster", 6, "Aurorus", 699 },
                    { 36, "Embrace Pokemon", "Human-Like, Amorphous", 3, "Gardevoir", 282 },
                    { 29, "Screech Pokemon", "Amorphous", 2, "Misdreavus", 200 },
                    { 20, "Bronze Bell Pokemon", "Mineral", 4, "Bronzong", 437 },
                    { 18, "Order Pokemon", "Undiscovered", 6, "Zygarde", 718 },
                    { 3, "Lizard Pokemon", "Monster, Dragon", 1, "Charmander", 4 },
                    { 4, "Sun Pokemon", "Bug", 5, "Volcarona", 637 },
                    { 5, "Superpower Pokemon", "Human-Like", 1, "Machoke", 67 },
                    { 6, "Single Horn Pokemon", "Bug", 2, "Heracross", 214 },
                    { 7, "Mud Fish Pokemon", "Monster, Water 1", 3, "Swampert", 260 },
                    { 8, "Mock Kelp Pokemon", "Bug", 6, "Skrelp", 637 },
                    { 9, "Raven Pokemon", "Flying", 8, "Corviknight", 823 },
                    { 10, "Balloon Pokemon", "Amorphous", 4, "Drifloon", 425 },
                    { 11, "Sickle Grass Pokemon", "Grass", 7, "Fomantis", 753 },
                    { 12, "Sea Lily Pokemon", "Water 3", 3, "Lileep", 345 },
                    { 13, "Poison Pin Pokemon", "Undiscovered", 7, "Naganadel", 804 },
                    { 14, "Bouquet Pokemon", "Fairy, Grass", 4, "Roserade", 407 },
                    { 15, "Magnet Area Pokemon", "Mineral", 4, "Magnezone", 462 },
                    { 16, "Deep Black Pokemon", "Undiscovered", 5, "Zekrom", 644 },
                    { 17, "Automaton Pokemon", "Mineral", 5, "Golett", 622 },
                    { 19, "Psi Pokemon", "Human-Like", 1, "Abra", 63 },
                    { 2, "Blocking Pokemon", "Field", 8, "Obstagoon", 598 }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { 2, 16 },
                    { 20, 17 },
                    { 20, 10 },
                    { 21, 11 },
                    { 22, 11 },
                    { 22, 12 },
                    { 23, 12 },
                    { 24, 12 },
                    { 24, 13 },
                    { 25, 13 },
                    { 25, 6 },
                    { 26, 13 },
                    { 26, 7 },
                    { 27, 14 },
                    { 28, 14 },
                    { 28, 15 },
                    { 29, 15 },
                    { 30, 15 },
                    { 31, 6 },
                    { 31, 16 },
                    { 32, 7 },
                    { 32, 16 },
                    { 33, 17 },
                    { 33, 11 },
                    { 34, 6 },
                    { 34, 17 },
                    { 35, 1 },
                    { 35, 18 },
                    { 19, 10 },
                    { 36, 10 },
                    { 18, 9 },
                    { 17, 15 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 13 },
                    { 4, 2 },
                    { 5, 3 },
                    { 6, 13 },
                    { 6, 3 },
                    { 7, 4 },
                    { 7, 9 },
                    { 8, 7 }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { 8, 4 },
                    { 9, 5 },
                    { 9, 17 },
                    { 10, 15 },
                    { 10, 5 },
                    { 11, 6 },
                    { 12, 11 },
                    { 12, 6 },
                    { 13, 7 },
                    { 13, 14 },
                    { 14, 6 },
                    { 14, 7 },
                    { 15, 8 },
                    { 15, 17 },
                    { 16, 14 },
                    { 16, 8 },
                    { 17, 9 },
                    { 18, 14 },
                    { 36, 18 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 2, 16 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 4, 13 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 6, 13 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 7, 9 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 9, 17 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 10, 15 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 11, 6 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 12, 6 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 12, 11 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 13, 7 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 13, 14 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 14, 6 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 14, 7 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 15, 8 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 15, 17 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 16, 8 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 16, 14 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 17, 9 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 17, 15 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 18, 9 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 18, 14 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 19, 10 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 20, 10 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 20, 17 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 21, 11 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 22, 11 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 22, 12 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 23, 12 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 24, 12 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 24, 13 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 25, 6 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 25, 13 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 26, 7 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 26, 13 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 27, 14 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 28, 14 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 28, 15 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 29, 15 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 30, 15 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 31, 6 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 31, 16 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 32, 7 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 32, 16 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 33, 11 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 33, 17 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 34, 6 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 34, 17 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 35, 1 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 35, 18 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 36, 10 });

            migrationBuilder.DeleteData(
                table: "PokemonTyping",
                keyColumns: new[] { "PokemonId", "TypingId" },
                keyValues: new object[] { 36, 18 });

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 36);

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[] { 1, 17 });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[] { 1, 11 });

            migrationBuilder.UpdateData(
                table: "Pokemons",
                keyColumn: "PokemonId",
                keyValue: 1,
                columns: new[] { "Classification", "Generation", "Name", "PokedexEntry" },
                values: new object[] { "Iron Armor Pokemon", 3, "Aggron", 306 });
        }
    }
}
