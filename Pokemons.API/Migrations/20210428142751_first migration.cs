using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemons.API.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTyping", x => new { x.PokemonId, x.TypingId });
                    table.ForeignKey(
                        name: "FK_PokemonTyping_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonTyping_Typings_TypingId",
                        column: x => x.TypingId,
                        principalTable: "Typings",
                        principalColumn: "TypingId",
                        onDelete: ReferentialAction.Cascade);
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
                    { new Guid("c6767aef-2570-4a06-a508-dabb633fa567"), "Sleeping Pokemon", "Monster", 1, "Snorlax", 143 },
                    { new Guid("9503ff9c-c758-4696-bc90-d941779dca99"), "Head Butt Pokemon", "Monster", 4, "Rampardos", 409 },
                    { new Guid("053a5141-9b56-4a85-8f69-04931a7010c2"), "Tundra Pokemon", "Monster", 6, "Aurorus", 699 },
                    { new Guid("a95e64c7-4db4-4dec-86d6-766975d5f2c6"), "Snow Hat Pokemon", "Fairy,Mineral", 3, "Snorunt", 361 },
                    { new Guid("6a776247-1d5e-43da-b3f3-7624db34b5e3"), "Worm Pokemon", "Bug", 8, "Snom", 872 },
                    { new Guid("7697efcc-2fd4-47b9-8ff8-a3b2b8a6fe48"), "Leaf-Wrapped Pokemon", "Bug", 5, "Swadloon", 541 },
                    { new Guid("389fd12d-2de2-4580-bf23-deba613de135"), "Curlipede Pokemon", "Bug", 5, "Whirlipede", 544 },
                    { new Guid("bd11fef1-b32c-466f-9aac-0d904ccb4bc7"), "Stealth Pokemon", "Amorphous,Dragon", 8, "Dragapult", 887 },
                    { new Guid("77031fbf-d613-4638-92f3-590bd8376520"), "Screech Pokemon", "Amorphous", 2, "Misdreavus", 200 },
                    { new Guid("3764f6da-00ed-4a68-aa3d-3131b29929ce"), "Magical Pokemon", "Amorphous", 4, "Mismagius", 429 },
                    { new Guid("571d3ed7-3b77-4f3c-a251-446dc30d02f6"), "Wily Pokemon", "Field,Grass", 3, "Nuzleaf", 274 },
                    { new Guid("ff17e5a8-9300-4668-8b9f-6026d8b1472a"), "Skunk Pokemon", "Field", 4, "Skuntank", 435 },
                    { new Guid("eb644554-6660-4ecc-b08b-d09ea53abcd9"), "Iron Armor Pokemon", "Monster", 3, "Aggron", 306 },
                    { new Guid("2ee76c5d-3179-4930-acd7-44b4f9775523"), "Thorn Pod Pokemon", "Grass,Mineral", 5, "Ferrothorn", 598 },
                    { new Guid("edcdebde-d477-4dbb-b5bf-5bcd1b1b2007"), "Balloon Pokemon", "Fairy", 1, "Jigglypuff", 39 },
                    { new Guid("42b1c658-75c9-4bcf-95a8-5dc3cec4942c"), "Embrace Pokemon", "Human-Like,Amorphous", 3, "Gardevoir", 282 },
                    { new Guid("1e7e232f-4005-4524-8c97-4a870cbecd44"), "Bronze Bell Pokemon", "Mineral", 4, "Bronzong", 437 },
                    { new Guid("c7bb2593-e473-4964-b811-f807ac8c4227"), "Psi Pokemon", "Human-Like", 1, "Abra", 63 },
                    { new Guid("5b78d2c1-3929-4851-bad2-928b4b899579"), "Dragon Pokemon", "Water 1,Dragon", 1, "Dragonair", 148 },
                    { new Guid("7c9e037a-cd44-420c-86b7-6290b1b3d4d2"), "Automaton Pokemon", "Mineral", 5, "Golett", 622 },
                    { new Guid("45ad8ae0-4bd1-46c8-8a1b-13d547b98a2e"), "Order Pokemon", "Undiscovered", 6, "Zygarde", 718 },
                    { new Guid("c2478ba4-6118-40b9-9746-8b4c1a12f0a4"), "Blocking Pokemon", "Field", 8, "Obstagoon", 598 },
                    { new Guid("6ca5f605-d965-4795-80de-80bc09e1bf42"), "Lizard Pokemon", "Monster,Dragon", 1, "Charmander", 4 },
                    { new Guid("0508dba5-c8a6-4f87-8adc-c6550b94dcab"), "Sun Pokemon", "Bug", 5, "Volcarona", 637 },
                    { new Guid("87a61f3c-3bc4-4a29-a1fe-5b3f2e1ecb9a"), "Superpower Pokemon", "Human-Like", 1, "Machoke", 67 },
                    { new Guid("3ea65225-f952-4549-b06a-6b7c4bb1c107"), "Mud Fish Pokemon", "Monster,Water 1", 3, "Swampert", 260 },
                    { new Guid("27a14f98-963f-46e2-9b34-c5fb4faa42ec"), "Mock Kelp Pokemon", "Bug", 6, "Skrelp", 637 },
                    { new Guid("abbe3e06-8cb6-45d3-8da1-51337b45e8d1"), "Raven Pokemon", "Flying", 8, "Corviknight", 823 },
                    { new Guid("94395695-0736-4d79-b25e-d95520b29328"), "Single Horn Pokemon", "Bug", 2, "Heracross", 214 },
                    { new Guid("97b58c44-e60e-45c2-ae5d-8b9af6c85946"), "Sickle Grass Pokemon", "Grass", 7, "Fomantis", 753 },
                    { new Guid("e8e78a90-6f80-486f-9d2e-097de3b8b326"), "Sea Lily Pokemon", "Water 3", 3, "Lileep", 345 },
                    { new Guid("1ecbff5d-0207-43f0-85c9-112c0f3dff44"), "Poison Pin Pokemon", "Undiscovered", 7, "Naganadel", 804 },
                    { new Guid("58a1e17e-8308-4422-ab7b-baa6f2a5ddd4"), "Bouquet Pokemon", "Fairy,Grass", 4, "Roserade", 407 },
                    { new Guid("d2a35562-a5f3-4bbf-bbd4-da6f4bc6c685"), "Magnet Area Pokemon", "Mineral", 4, "Magnezone", 462 },
                    { new Guid("b35a9edb-ba73-4d3a-b228-59ea3ba3cdbd"), "Deep Black Pokemon", "Undiscovered", 5, "Zekrom", 644 },
                    { new Guid("3d9744a6-7ba4-423d-913e-7f8765703ba4"), "Balloon Pokemon", "Amorphous", 4, "Drifloon", 425 }
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
                    { new Guid("c6767aef-2570-4a06-a508-dabb633fa567"), 1 },
                    { new Guid("053a5141-9b56-4a85-8f69-04931a7010c2"), 11 },
                    { new Guid("9503ff9c-c758-4696-bc90-d941779dca99"), 11 },
                    { new Guid("e8e78a90-6f80-486f-9d2e-097de3b8b326"), 11 },
                    { new Guid("abbe3e06-8cb6-45d3-8da1-51337b45e8d1"), 17 },
                    { new Guid("42b1c658-75c9-4bcf-95a8-5dc3cec4942c"), 10 },
                    { new Guid("1e7e232f-4005-4524-8c97-4a870cbecd44"), 10 },
                    { new Guid("c7bb2593-e473-4964-b811-f807ac8c4227"), 10 },
                    { new Guid("d2a35562-a5f3-4bbf-bbd4-da6f4bc6c685"), 17 },
                    { new Guid("1e7e232f-4005-4524-8c97-4a870cbecd44"), 17 },
                    { new Guid("eb644554-6660-4ecc-b08b-d09ea53abcd9"), 17 },
                    { new Guid("2ee76c5d-3179-4930-acd7-44b4f9775523"), 17 },
                    { new Guid("b35a9edb-ba73-4d3a-b228-59ea3ba3cdbd"), 14 },
                    { new Guid("45ad8ae0-4bd1-46c8-8a1b-13d547b98a2e"), 9 },
                    { new Guid("7c9e037a-cd44-420c-86b7-6290b1b3d4d2"), 9 },
                    { new Guid("3ea65225-f952-4549-b06a-6b7c4bb1c107"), 9 },
                    { new Guid("eb644554-6660-4ecc-b08b-d09ea53abcd9"), 11 },
                    { new Guid("ff17e5a8-9300-4668-8b9f-6026d8b1472a"), 16 },
                    { new Guid("571d3ed7-3b77-4f3c-a251-446dc30d02f6"), 16 },
                    { new Guid("c2478ba4-6118-40b9-9746-8b4c1a12f0a4"), 16 },
                    { new Guid("45ad8ae0-4bd1-46c8-8a1b-13d547b98a2e"), 14 },
                    { new Guid("5b78d2c1-3929-4851-bad2-928b4b899579"), 14 },
                    { new Guid("bd11fef1-b32c-466f-9aac-0d904ccb4bc7"), 14 },
                    { new Guid("389fd12d-2de2-4580-bf23-deba613de135"), 13 },
                    { new Guid("7697efcc-2fd4-47b9-8ff8-a3b2b8a6fe48"), 13 },
                    { new Guid("6a776247-1d5e-43da-b3f3-7624db34b5e3"), 13 },
                    { new Guid("94395695-0736-4d79-b25e-d95520b29328"), 13 },
                    { new Guid("b35a9edb-ba73-4d3a-b228-59ea3ba3cdbd"), 8 },
                    { new Guid("0508dba5-c8a6-4f87-8adc-c6550b94dcab"), 13 },
                    { new Guid("7c9e037a-cd44-420c-86b7-6290b1b3d4d2"), 15 },
                    { new Guid("bd11fef1-b32c-466f-9aac-0d904ccb4bc7"), 15 },
                    { new Guid("77031fbf-d613-4638-92f3-590bd8376520"), 15 },
                    { new Guid("3764f6da-00ed-4a68-aa3d-3131b29929ce"), 15 },
                    { new Guid("6a776247-1d5e-43da-b3f3-7624db34b5e3"), 12 },
                    { new Guid("a95e64c7-4db4-4dec-86d6-766975d5f2c6"), 12 },
                    { new Guid("053a5141-9b56-4a85-8f69-04931a7010c2"), 12 },
                    { new Guid("3d9744a6-7ba4-423d-913e-7f8765703ba4"), 15 },
                    { new Guid("d2a35562-a5f3-4bbf-bbd4-da6f4bc6c685"), 8 },
                    { new Guid("1ecbff5d-0207-43f0-85c9-112c0f3dff44"), 14 },
                    { new Guid("42b1c658-75c9-4bcf-95a8-5dc3cec4942c"), 18 },
                    { new Guid("7697efcc-2fd4-47b9-8ff8-a3b2b8a6fe48"), 6 },
                    { new Guid("58a1e17e-8308-4422-ab7b-baa6f2a5ddd4"), 6 }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { new Guid("27a14f98-963f-46e2-9b34-c5fb4faa42ec"), 4 },
                    { new Guid("3ea65225-f952-4549-b06a-6b7c4bb1c107"), 4 },
                    { new Guid("edcdebde-d477-4dbb-b5bf-5bcd1b1b2007"), 18 },
                    { new Guid("94395695-0736-4d79-b25e-d95520b29328"), 3 },
                    { new Guid("87a61f3c-3bc4-4a29-a1fe-5b3f2e1ecb9a"), 3 },
                    { new Guid("27a14f98-963f-46e2-9b34-c5fb4faa42ec"), 7 },
                    { new Guid("1ecbff5d-0207-43f0-85c9-112c0f3dff44"), 7 },
                    { new Guid("2ee76c5d-3179-4930-acd7-44b4f9775523"), 6 },
                    { new Guid("0508dba5-c8a6-4f87-8adc-c6550b94dcab"), 2 },
                    { new Guid("58a1e17e-8308-4422-ab7b-baa6f2a5ddd4"), 7 },
                    { new Guid("389fd12d-2de2-4580-bf23-deba613de135"), 7 },
                    { new Guid("ff17e5a8-9300-4668-8b9f-6026d8b1472a"), 7 },
                    { new Guid("abbe3e06-8cb6-45d3-8da1-51337b45e8d1"), 5 },
                    { new Guid("e8e78a90-6f80-486f-9d2e-097de3b8b326"), 6 },
                    { new Guid("97b58c44-e60e-45c2-ae5d-8b9af6c85946"), 6 },
                    { new Guid("3d9744a6-7ba4-423d-913e-7f8765703ba4"), 5 },
                    { new Guid("6ca5f605-d965-4795-80de-80bc09e1bf42"), 2 },
                    { new Guid("edcdebde-d477-4dbb-b5bf-5bcd1b1b2007"), 1 },
                    { new Guid("c2478ba4-6118-40b9-9746-8b4c1a12f0a4"), 1 },
                    { new Guid("571d3ed7-3b77-4f3c-a251-446dc30d02f6"), 6 }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefenseTypingId", "OffenseTypingId", "Power" },
                values: new object[,]
                {
                    { 49, 15, 7, 0.5m },
                    { 21, 15, 3, 0m },
                    { 3, 15, 1, 0m },
                    { 105, 18, 17, 2m },
                    { 90, 15, 13, 0.5m },
                    { 106, 12, 17, 2m },
                    { 113, 14, 18, 2m },
                    { 114, 3, 18, 2m },
                    { 103, 18, 16, 0.5m },
                    { 115, 2, 18, 0.5m },
                    { 93, 14, 14, 2m },
                    { 75, 14, 12, 2m },
                    { 52, 14, 8, 0.5m },
                    { 38, 14, 6, 0.5m },
                    { 25, 14, 4, 0.5m },
                    { 7, 14, 2, 2m },
                    { 112, 16, 18, 2m },
                    { 96, 15, 15, 2m },
                    { 67, 16, 10, 0m },
                    { 99, 1, 15, 0m },
                    { 108, 8, 17, 0.5m },
                    { 109, 2, 17, 0.5m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefenseTypingId", "OffenseTypingId", "Power" },
                values: new object[,]
                {
                    { 94, 17, 14, 0.5m },
                    { 92, 17, 13, 0.5m },
                    { 81, 17, 12, 0.5m },
                    { 74, 17, 11, 0.5m },
                    { 66, 17, 10, 0.5m },
                    { 60, 17, 9, 2m },
                    { 43, 17, 6, 0.5m },
                    { 33, 17, 5, 0.5m },
                    { 15, 17, 3, 2m },
                    { 110, 17, 17, 0.5m },
                    { 111, 4, 17, 0.5m },
                    { 17, 18, 3, 0.5m },
                    { 44, 18, 7, 2m },
                    { 86, 18, 13, 0.5m },
                    { 104, 3, 16, 0.5m },
                    { 102, 16, 16, 0.5m },
                    { 101, 10, 16, 2m },
                    { 100, 15, 16, 2m },
                    { 98, 16, 15, 0.5m },
                    { 83, 16, 13, 2m },
                    { 107, 11, 17, 2m },
                    { 11, 16, 3, 2m },
                    { 95, 18, 14, 0m },
                    { 97, 10, 15, 2m },
                    { 2, 17, 1, 0.5m },
                    { 78, 9, 12, 2m },
                    { 89, 5, 13, 0.5m },
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
                    { 91, 7, 13, 0.5m },
                    { 20, 10, 3, 0.5m },
                    { 65, 10, 10, 0.5m },
                    { 88, 2, 13, 0.5m },
                    { 87, 3, 13, 0.5m },
                    { 85, 10, 13, 2m },
                    { 84, 6, 13, 2m },
                    { 68, 13, 11, 2m },
                    { 61, 13, 9, 0.5m },
                    { 37, 13, 6, 0.5m },
                    { 28, 13, 5, 2m },
                    { 16, 13, 3, 0m },
                    { 4, 13, 2, 2m },
                    { 82, 4, 12, 0.5m },
                    { 80, 12, 12, 0.5m },
                    { 79, 2, 12, 0.5m },
                    { 116, 7, 18, 0.5m },
                    { 77, 6, 12, 2m },
                    { 76, 5, 12, 2m },
                    { 71, 12, 11, 2m },
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
                    { 64, 7, 10, 2m },
                    { 48, 11, 7, 0.5m },
                    { 69, 2, 11, 2m },
                    { 70, 5, 11, 2m },
                    { 72, 3, 11, 0.5m },
                    { 73, 9, 11, 0.5m },
                    { 6, 12, 2, 2m },
                    { 12, 12, 3, 2m },
                    { 59, 11, 9, 2m },
                    { 117, 17, 18, 0.5m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PokedexEntry_Generation",
                table: "Pokemons",
                columns: new[] { "PokedexEntry", "Generation" },
                unique: true);

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
