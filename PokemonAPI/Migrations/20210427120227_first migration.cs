using System;
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
                    { new Guid("649ad67a-2377-459f-b35d-9691220e80c9"), "Sleeping Pokemon", "Monster", 1, "Snorlax", 143 },
                    { new Guid("0758d2fe-2087-48a5-9cb3-13c3f73a1828"), "Head Butt Pokemon", "Monster", 4, "Rampardos", 409 },
                    { new Guid("db445524-9ccc-4c8e-babb-cbd018f5e5e0"), "Tundra Pokemon", "Monster", 6, "Aurorus", 699 },
                    { new Guid("94642d82-3e4a-4259-b3c0-98f57eedf6db"), "Snow Hat Pokemon", "Fairy,Mineral", 3, "Snorunt", 361 },
                    { new Guid("ca2233c7-8cfa-42a2-bfe6-2b2e2722e48a"), "Worm Pokemon", "Bug", 8, "Snom", 872 },
                    { new Guid("5503edfc-580f-4393-a7f9-fe67c28cbfbd"), "Leaf-Wrapped Pokemon", "Bug", 5, "Swadloon", 541 },
                    { new Guid("9683df98-f59b-40d2-b956-006bbc61c504"), "Curlipede Pokemon", "Bug", 5, "Whirlipede", 544 },
                    { new Guid("1ced3104-906b-4c5e-a94f-246f4132cc3a"), "Stealth Pokemon", "Amorphous,Dragon", 8, "Dragapult", 887 },
                    { new Guid("466b53bc-8f31-4df6-90d8-78386b3bc4b1"), "Screech Pokemon", "Amorphous", 2, "Misdreavus", 200 },
                    { new Guid("572217ed-96ea-4270-afd9-26c59d15f0e6"), "Magical Pokemon", "Amorphous", 4, "Mismagius", 429 },
                    { new Guid("175cdbbd-d5f0-4a03-a682-08479dd4a6dc"), "Wily Pokemon", "Field,Grass", 3, "Nuzleaf", 274 },
                    { new Guid("4a867501-e7f7-4bab-917e-64ee590751c2"), "Skunk Pokemon", "Field", 4, "Skuntank", 435 },
                    { new Guid("ac6b1671-6d1c-462c-bafc-e9e0d302d8da"), "Iron Armor Pokemon", "Monster", 3, "Aggron", 306 },
                    { new Guid("ee4b550f-e9ec-4c8f-b2e5-a8ac05e43f43"), "Thorn Pod Pokemon", "Grass,Mineral", 5, "Ferrothorn", 598 },
                    { new Guid("5a76c039-5f29-4030-bdb0-314706b3703f"), "Balloon Pokemon", "Fairy", 1, "Jigglypuff", 39 },
                    { new Guid("75fbf568-c6e5-432c-9183-781bb4c26e0a"), "Embrace Pokemon", "Human-Like,Amorphous", 3, "Gardevoir", 282 },
                    { new Guid("bdb7928f-32ae-48fa-9d04-943bb5ea0c99"), "Bronze Bell Pokemon", "Mineral", 4, "Bronzong", 437 },
                    { new Guid("7d2263a0-ac74-4c7f-8869-4c2be1a90103"), "Psi Pokemon", "Human-Like", 1, "Abra", 63 },
                    { new Guid("20d04832-f713-4ed1-a5ec-be6cc87f3da5"), "Dragon Pokemon", "Water 1,Dragon", 1, "Dragonair", 148 },
                    { new Guid("62deee54-4877-4bd9-a9d7-361720083ea7"), "Automaton Pokemon", "Mineral", 5, "Golett", 622 },
                    { new Guid("c83fdbc7-4199-4e01-9e58-cf841066a284"), "Order Pokemon", "Undiscovered", 6, "Zygarde", 718 },
                    { new Guid("5438857a-df47-4e24-b589-05f49a6cdd87"), "Blocking Pokemon", "Field", 8, "Obstagoon", 598 },
                    { new Guid("1e9cf5ae-d024-4f46-af46-53eaa8dfee8b"), "Lizard Pokemon", "Monster,Dragon", 1, "Charmander", 4 },
                    { new Guid("bc8d38fe-7a5c-41c8-92b0-1b44fcb83f56"), "Sun Pokemon", "Bug", 5, "Volcarona", 637 },
                    { new Guid("e98d5234-8b9f-4786-9763-614156c57d38"), "Superpower Pokemon", "Human-Like", 1, "Machoke", 67 },
                    { new Guid("32771f3c-d8a6-49f1-8448-31731935334c"), "Mud Fish Pokemon", "Monster,Water 1", 3, "Swampert", 260 },
                    { new Guid("54a20e4c-f8df-44f2-98c3-e0c6766f15a5"), "Mock Kelp Pokemon", "Bug", 6, "Skrelp", 637 },
                    { new Guid("deb4b3b0-268d-4987-974e-931a3ab4a424"), "Raven Pokemon", "Flying", 8, "Corviknight", 823 },
                    { new Guid("2a24b2df-519d-4204-8e85-38cdc673683e"), "Single Horn Pokemon", "Bug", 2, "Heracross", 214 },
                    { new Guid("ce5225c3-70e0-4221-87f6-433f0eac2eb9"), "Sickle Grass Pokemon", "Grass", 7, "Fomantis", 753 },
                    { new Guid("b2c85197-7685-4767-ac06-766570ea57c6"), "Sea Lily Pokemon", "Water 3", 3, "Lileep", 345 },
                    { new Guid("752d1a46-f3d9-4651-a7b6-43df7c99f0c4"), "Poison Pin Pokemon", "Undiscovered", 7, "Naganadel", 804 },
                    { new Guid("1ff13e76-e3a9-48ff-9680-0337985fba94"), "Bouquet Pokemon", "Fairy,Grass", 4, "Roserade", 407 },
                    { new Guid("56fb7561-ee65-4680-8b3c-24b7d4cc797b"), "Magnet Area Pokemon", "Mineral", 4, "Magnezone", 462 },
                    { new Guid("04c13937-3269-43da-8b25-81769bf14a73"), "Deep Black Pokemon", "Undiscovered", 5, "Zekrom", 644 },
                    { new Guid("d05afc4c-f993-487d-b808-91195780efc2"), "Balloon Pokemon", "Amorphous", 4, "Drifloon", 425 }
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
                    { new Guid("649ad67a-2377-459f-b35d-9691220e80c9"), 1 },
                    { new Guid("db445524-9ccc-4c8e-babb-cbd018f5e5e0"), 11 },
                    { new Guid("0758d2fe-2087-48a5-9cb3-13c3f73a1828"), 11 },
                    { new Guid("b2c85197-7685-4767-ac06-766570ea57c6"), 11 },
                    { new Guid("deb4b3b0-268d-4987-974e-931a3ab4a424"), 17 },
                    { new Guid("75fbf568-c6e5-432c-9183-781bb4c26e0a"), 10 },
                    { new Guid("bdb7928f-32ae-48fa-9d04-943bb5ea0c99"), 10 },
                    { new Guid("7d2263a0-ac74-4c7f-8869-4c2be1a90103"), 10 },
                    { new Guid("56fb7561-ee65-4680-8b3c-24b7d4cc797b"), 17 },
                    { new Guid("bdb7928f-32ae-48fa-9d04-943bb5ea0c99"), 17 },
                    { new Guid("ac6b1671-6d1c-462c-bafc-e9e0d302d8da"), 17 },
                    { new Guid("ee4b550f-e9ec-4c8f-b2e5-a8ac05e43f43"), 17 },
                    { new Guid("04c13937-3269-43da-8b25-81769bf14a73"), 14 },
                    { new Guid("c83fdbc7-4199-4e01-9e58-cf841066a284"), 9 },
                    { new Guid("62deee54-4877-4bd9-a9d7-361720083ea7"), 9 },
                    { new Guid("32771f3c-d8a6-49f1-8448-31731935334c"), 9 },
                    { new Guid("ac6b1671-6d1c-462c-bafc-e9e0d302d8da"), 11 },
                    { new Guid("4a867501-e7f7-4bab-917e-64ee590751c2"), 16 },
                    { new Guid("175cdbbd-d5f0-4a03-a682-08479dd4a6dc"), 16 },
                    { new Guid("5438857a-df47-4e24-b589-05f49a6cdd87"), 16 },
                    { new Guid("c83fdbc7-4199-4e01-9e58-cf841066a284"), 14 },
                    { new Guid("20d04832-f713-4ed1-a5ec-be6cc87f3da5"), 14 },
                    { new Guid("1ced3104-906b-4c5e-a94f-246f4132cc3a"), 14 },
                    { new Guid("9683df98-f59b-40d2-b956-006bbc61c504"), 13 },
                    { new Guid("5503edfc-580f-4393-a7f9-fe67c28cbfbd"), 13 },
                    { new Guid("ca2233c7-8cfa-42a2-bfe6-2b2e2722e48a"), 13 },
                    { new Guid("2a24b2df-519d-4204-8e85-38cdc673683e"), 13 },
                    { new Guid("04c13937-3269-43da-8b25-81769bf14a73"), 8 },
                    { new Guid("bc8d38fe-7a5c-41c8-92b0-1b44fcb83f56"), 13 },
                    { new Guid("62deee54-4877-4bd9-a9d7-361720083ea7"), 15 },
                    { new Guid("1ced3104-906b-4c5e-a94f-246f4132cc3a"), 15 },
                    { new Guid("466b53bc-8f31-4df6-90d8-78386b3bc4b1"), 15 },
                    { new Guid("572217ed-96ea-4270-afd9-26c59d15f0e6"), 15 },
                    { new Guid("ca2233c7-8cfa-42a2-bfe6-2b2e2722e48a"), 12 },
                    { new Guid("94642d82-3e4a-4259-b3c0-98f57eedf6db"), 12 },
                    { new Guid("db445524-9ccc-4c8e-babb-cbd018f5e5e0"), 12 },
                    { new Guid("d05afc4c-f993-487d-b808-91195780efc2"), 15 },
                    { new Guid("56fb7561-ee65-4680-8b3c-24b7d4cc797b"), 8 },
                    { new Guid("752d1a46-f3d9-4651-a7b6-43df7c99f0c4"), 14 },
                    { new Guid("75fbf568-c6e5-432c-9183-781bb4c26e0a"), 18 },
                    { new Guid("5503edfc-580f-4393-a7f9-fe67c28cbfbd"), 6 },
                    { new Guid("1ff13e76-e3a9-48ff-9680-0337985fba94"), 6 }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { new Guid("54a20e4c-f8df-44f2-98c3-e0c6766f15a5"), 4 },
                    { new Guid("32771f3c-d8a6-49f1-8448-31731935334c"), 4 },
                    { new Guid("5a76c039-5f29-4030-bdb0-314706b3703f"), 18 },
                    { new Guid("2a24b2df-519d-4204-8e85-38cdc673683e"), 3 },
                    { new Guid("e98d5234-8b9f-4786-9763-614156c57d38"), 3 },
                    { new Guid("54a20e4c-f8df-44f2-98c3-e0c6766f15a5"), 7 },
                    { new Guid("752d1a46-f3d9-4651-a7b6-43df7c99f0c4"), 7 },
                    { new Guid("ee4b550f-e9ec-4c8f-b2e5-a8ac05e43f43"), 6 },
                    { new Guid("bc8d38fe-7a5c-41c8-92b0-1b44fcb83f56"), 2 },
                    { new Guid("1ff13e76-e3a9-48ff-9680-0337985fba94"), 7 },
                    { new Guid("9683df98-f59b-40d2-b956-006bbc61c504"), 7 },
                    { new Guid("4a867501-e7f7-4bab-917e-64ee590751c2"), 7 },
                    { new Guid("deb4b3b0-268d-4987-974e-931a3ab4a424"), 5 },
                    { new Guid("b2c85197-7685-4767-ac06-766570ea57c6"), 6 },
                    { new Guid("ce5225c3-70e0-4221-87f6-433f0eac2eb9"), 6 },
                    { new Guid("d05afc4c-f993-487d-b808-91195780efc2"), 5 },
                    { new Guid("1e9cf5ae-d024-4f46-af46-53eaa8dfee8b"), 2 },
                    { new Guid("5a76c039-5f29-4030-bdb0-314706b3703f"), 1 },
                    { new Guid("5438857a-df47-4e24-b589-05f49a6cdd87"), 1 },
                    { new Guid("175cdbbd-d5f0-4a03-a682-08479dd4a6dc"), 6 }
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
