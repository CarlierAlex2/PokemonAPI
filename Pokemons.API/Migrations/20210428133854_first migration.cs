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
                    { new Guid("762d45e1-f559-4338-aa51-e781962fcb2d"), "Sleeping Pokemon", "Monster", 1, "Snorlax", 143 },
                    { new Guid("6a2c07aa-7261-4d3c-9ba0-77cdf2a1d4fe"), "Head Butt Pokemon", "Monster", 4, "Rampardos", 409 },
                    { new Guid("94a79ddc-e37b-48ca-ac34-8c4038880a52"), "Tundra Pokemon", "Monster", 6, "Aurorus", 699 },
                    { new Guid("6c83e537-25ea-49f0-ba14-9b50e9000c7b"), "Snow Hat Pokemon", "Fairy,Mineral", 3, "Snorunt", 361 },
                    { new Guid("897df779-b730-4a4d-b841-2ed62f40c1ff"), "Worm Pokemon", "Bug", 8, "Snom", 872 },
                    { new Guid("21fc93e2-4d82-4fdc-9ebe-6668ae978d91"), "Leaf-Wrapped Pokemon", "Bug", 5, "Swadloon", 541 },
                    { new Guid("ac0145d3-8ee5-4568-b2bb-408d58a5c122"), "Curlipede Pokemon", "Bug", 5, "Whirlipede", 544 },
                    { new Guid("08f79218-253b-4b66-9598-1b59daa2276d"), "Stealth Pokemon", "Amorphous,Dragon", 8, "Dragapult", 887 },
                    { new Guid("221a1deb-a7b7-4cb2-a3cf-99452981dfc8"), "Screech Pokemon", "Amorphous", 2, "Misdreavus", 200 },
                    { new Guid("41b97bfb-0aa2-427b-abbe-a29ead3ca5d6"), "Magical Pokemon", "Amorphous", 4, "Mismagius", 429 },
                    { new Guid("28d4f7de-c007-478c-9966-f40de0e7c1fb"), "Wily Pokemon", "Field,Grass", 3, "Nuzleaf", 274 },
                    { new Guid("88089c96-d087-42dd-81a0-8df45a22f62c"), "Skunk Pokemon", "Field", 4, "Skuntank", 435 },
                    { new Guid("8c9f3551-07e9-45a4-9309-f980531ae37b"), "Iron Armor Pokemon", "Monster", 3, "Aggron", 306 },
                    { new Guid("78d31781-65a8-424e-95ab-7c90368b7d2b"), "Thorn Pod Pokemon", "Grass,Mineral", 5, "Ferrothorn", 598 },
                    { new Guid("a95e9062-e194-4abd-8dfe-83991aacfb54"), "Balloon Pokemon", "Fairy", 1, "Jigglypuff", 39 },
                    { new Guid("d9f537f2-7042-4fd5-a9f4-d6de1b47a4fe"), "Embrace Pokemon", "Human-Like,Amorphous", 3, "Gardevoir", 282 },
                    { new Guid("1000b9b8-0ab4-4c98-8138-9b15a202ac7b"), "Bronze Bell Pokemon", "Mineral", 4, "Bronzong", 437 },
                    { new Guid("84efed31-01e8-4ac4-896e-6abb8d073e09"), "Psi Pokemon", "Human-Like", 1, "Abra", 63 },
                    { new Guid("870ba31b-e824-485f-9708-4077b2fcd469"), "Dragon Pokemon", "Water 1,Dragon", 1, "Dragonair", 148 },
                    { new Guid("85ecf0f7-4aed-49bd-9a84-836b023eab11"), "Automaton Pokemon", "Mineral", 5, "Golett", 622 },
                    { new Guid("2c52fd70-7e8b-4b33-b25d-48cfa0546ed4"), "Order Pokemon", "Undiscovered", 6, "Zygarde", 718 },
                    { new Guid("e86662c3-077f-4f88-abf6-a158ec1cc858"), "Blocking Pokemon", "Field", 8, "Obstagoon", 598 },
                    { new Guid("59682e49-8da3-4fa6-9bc3-4b04094af87a"), "Lizard Pokemon", "Monster,Dragon", 1, "Charmander", 4 },
                    { new Guid("03a715fe-0cda-40ce-9b81-c8256e99b5af"), "Sun Pokemon", "Bug", 5, "Volcarona", 637 },
                    { new Guid("de51da3f-4d12-463d-8b33-fe23d00c9487"), "Superpower Pokemon", "Human-Like", 1, "Machoke", 67 },
                    { new Guid("9cd88407-247f-40ee-9e7d-10056bad9f4e"), "Mud Fish Pokemon", "Monster,Water 1", 3, "Swampert", 260 },
                    { new Guid("2cccd836-5e6f-408b-9198-70f215c12c13"), "Mock Kelp Pokemon", "Bug", 6, "Skrelp", 637 },
                    { new Guid("931653af-88ec-45e1-9f4d-05f8e0401963"), "Raven Pokemon", "Flying", 8, "Corviknight", 823 },
                    { new Guid("812daf11-fa93-41bc-8024-006f216e1ca9"), "Single Horn Pokemon", "Bug", 2, "Heracross", 214 },
                    { new Guid("b641379b-d93e-4873-aeea-1cdce12985bf"), "Sickle Grass Pokemon", "Grass", 7, "Fomantis", 753 },
                    { new Guid("61e046ab-e29b-4f9f-a766-c95f558639c2"), "Sea Lily Pokemon", "Water 3", 3, "Lileep", 345 },
                    { new Guid("31a78165-3172-486e-a878-16a682b952f8"), "Poison Pin Pokemon", "Undiscovered", 7, "Naganadel", 804 },
                    { new Guid("e66152de-35f0-4ba8-8b2f-75d17f68c515"), "Bouquet Pokemon", "Fairy,Grass", 4, "Roserade", 407 },
                    { new Guid("94378bee-6bab-4bf0-91a1-3a650b35f55f"), "Magnet Area Pokemon", "Mineral", 4, "Magnezone", 462 },
                    { new Guid("debc1386-2792-4b0c-8abe-f40ab2b91370"), "Deep Black Pokemon", "Undiscovered", 5, "Zekrom", 644 },
                    { new Guid("0c693784-c08b-44d0-ba30-1fa43784e085"), "Balloon Pokemon", "Amorphous", 4, "Drifloon", 425 }
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
                    { new Guid("762d45e1-f559-4338-aa51-e781962fcb2d"), 1 },
                    { new Guid("94a79ddc-e37b-48ca-ac34-8c4038880a52"), 11 },
                    { new Guid("6a2c07aa-7261-4d3c-9ba0-77cdf2a1d4fe"), 11 },
                    { new Guid("61e046ab-e29b-4f9f-a766-c95f558639c2"), 11 },
                    { new Guid("931653af-88ec-45e1-9f4d-05f8e0401963"), 17 },
                    { new Guid("d9f537f2-7042-4fd5-a9f4-d6de1b47a4fe"), 10 },
                    { new Guid("1000b9b8-0ab4-4c98-8138-9b15a202ac7b"), 10 },
                    { new Guid("84efed31-01e8-4ac4-896e-6abb8d073e09"), 10 },
                    { new Guid("94378bee-6bab-4bf0-91a1-3a650b35f55f"), 17 },
                    { new Guid("1000b9b8-0ab4-4c98-8138-9b15a202ac7b"), 17 },
                    { new Guid("8c9f3551-07e9-45a4-9309-f980531ae37b"), 17 },
                    { new Guid("78d31781-65a8-424e-95ab-7c90368b7d2b"), 17 },
                    { new Guid("debc1386-2792-4b0c-8abe-f40ab2b91370"), 14 },
                    { new Guid("2c52fd70-7e8b-4b33-b25d-48cfa0546ed4"), 9 },
                    { new Guid("85ecf0f7-4aed-49bd-9a84-836b023eab11"), 9 },
                    { new Guid("9cd88407-247f-40ee-9e7d-10056bad9f4e"), 9 },
                    { new Guid("8c9f3551-07e9-45a4-9309-f980531ae37b"), 11 },
                    { new Guid("88089c96-d087-42dd-81a0-8df45a22f62c"), 16 },
                    { new Guid("28d4f7de-c007-478c-9966-f40de0e7c1fb"), 16 },
                    { new Guid("e86662c3-077f-4f88-abf6-a158ec1cc858"), 16 },
                    { new Guid("2c52fd70-7e8b-4b33-b25d-48cfa0546ed4"), 14 },
                    { new Guid("870ba31b-e824-485f-9708-4077b2fcd469"), 14 },
                    { new Guid("08f79218-253b-4b66-9598-1b59daa2276d"), 14 },
                    { new Guid("ac0145d3-8ee5-4568-b2bb-408d58a5c122"), 13 },
                    { new Guid("21fc93e2-4d82-4fdc-9ebe-6668ae978d91"), 13 },
                    { new Guid("897df779-b730-4a4d-b841-2ed62f40c1ff"), 13 },
                    { new Guid("812daf11-fa93-41bc-8024-006f216e1ca9"), 13 },
                    { new Guid("debc1386-2792-4b0c-8abe-f40ab2b91370"), 8 },
                    { new Guid("03a715fe-0cda-40ce-9b81-c8256e99b5af"), 13 },
                    { new Guid("85ecf0f7-4aed-49bd-9a84-836b023eab11"), 15 },
                    { new Guid("08f79218-253b-4b66-9598-1b59daa2276d"), 15 },
                    { new Guid("221a1deb-a7b7-4cb2-a3cf-99452981dfc8"), 15 },
                    { new Guid("41b97bfb-0aa2-427b-abbe-a29ead3ca5d6"), 15 },
                    { new Guid("897df779-b730-4a4d-b841-2ed62f40c1ff"), 12 },
                    { new Guid("6c83e537-25ea-49f0-ba14-9b50e9000c7b"), 12 },
                    { new Guid("94a79ddc-e37b-48ca-ac34-8c4038880a52"), 12 },
                    { new Guid("0c693784-c08b-44d0-ba30-1fa43784e085"), 15 },
                    { new Guid("94378bee-6bab-4bf0-91a1-3a650b35f55f"), 8 },
                    { new Guid("31a78165-3172-486e-a878-16a682b952f8"), 14 },
                    { new Guid("d9f537f2-7042-4fd5-a9f4-d6de1b47a4fe"), 18 },
                    { new Guid("21fc93e2-4d82-4fdc-9ebe-6668ae978d91"), 6 },
                    { new Guid("e66152de-35f0-4ba8-8b2f-75d17f68c515"), 6 }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { new Guid("2cccd836-5e6f-408b-9198-70f215c12c13"), 4 },
                    { new Guid("9cd88407-247f-40ee-9e7d-10056bad9f4e"), 4 },
                    { new Guid("a95e9062-e194-4abd-8dfe-83991aacfb54"), 18 },
                    { new Guid("812daf11-fa93-41bc-8024-006f216e1ca9"), 3 },
                    { new Guid("de51da3f-4d12-463d-8b33-fe23d00c9487"), 3 },
                    { new Guid("2cccd836-5e6f-408b-9198-70f215c12c13"), 7 },
                    { new Guid("31a78165-3172-486e-a878-16a682b952f8"), 7 },
                    { new Guid("78d31781-65a8-424e-95ab-7c90368b7d2b"), 6 },
                    { new Guid("03a715fe-0cda-40ce-9b81-c8256e99b5af"), 2 },
                    { new Guid("e66152de-35f0-4ba8-8b2f-75d17f68c515"), 7 },
                    { new Guid("ac0145d3-8ee5-4568-b2bb-408d58a5c122"), 7 },
                    { new Guid("88089c96-d087-42dd-81a0-8df45a22f62c"), 7 },
                    { new Guid("931653af-88ec-45e1-9f4d-05f8e0401963"), 5 },
                    { new Guid("61e046ab-e29b-4f9f-a766-c95f558639c2"), 6 },
                    { new Guid("b641379b-d93e-4873-aeea-1cdce12985bf"), 6 },
                    { new Guid("0c693784-c08b-44d0-ba30-1fa43784e085"), 5 },
                    { new Guid("59682e49-8da3-4fa6-9bc3-4b04094af87a"), 2 },
                    { new Guid("a95e9062-e194-4abd-8dfe-83991aacfb54"), 1 },
                    { new Guid("e86662c3-077f-4f88-abf6-a158ec1cc858"), 1 },
                    { new Guid("28d4f7de-c007-478c-9966-f40de0e7c1fb"), 6 }
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
