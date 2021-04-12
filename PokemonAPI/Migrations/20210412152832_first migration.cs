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
                    { new Guid("7b7d665b-5d95-4d55-8f7f-7be362fc97f9"), "Sleeping Pokemon", "Monster", 1, "Snorlax", 143 },
                    { new Guid("d7561fe2-1672-43e9-83b2-a90444dc3863"), "Head Butt Pokemon", "Monster", 4, "Rampardos", 409 },
                    { new Guid("5fbad038-49b1-4e71-a5af-1c45ddd872fc"), "Tundra Pokemon", "Monster", 6, "Aurorus", 699 },
                    { new Guid("b0c07be1-6964-4bd8-b043-adad4a31f58e"), "Snow Hat Pokemon", "Fairy, Mineral", 3, "Snorunt", 361 },
                    { new Guid("ca0c64b9-0347-4edb-b378-6054f3adeb49"), "Worm Pokemon", "Bug", 8, "Snom", 872 },
                    { new Guid("769e9e1f-b2a6-4bac-9f24-94541cca9484"), "Leaf-Wrapped Pokemon", "Bug", 5, "Swadloon", 541 },
                    { new Guid("b022d558-d506-4c45-9239-8db6c8f92be0"), "Curlipede Pokemon", "Bug", 5, "Whirlipede", 544 },
                    { new Guid("7f06468c-c199-42ec-a31f-798ca1e1f0e7"), "Stealth Pokemon", "Amorphous, Dragon", 8, "Dragapult", 887 },
                    { new Guid("1e6bc260-2aa9-4206-aa20-345897ccc32e"), "Screech Pokemon", "Amorphous", 2, "Misdreavus", 200 },
                    { new Guid("67daba33-f80b-439f-b64f-67b8379a1dd2"), "Magical Pokemon", "Amorphous", 4, "Mismagius", 429 },
                    { new Guid("3ce01a8a-5a84-4632-b903-ebcb8fca5eab"), "Wily Pokemon", "Field, Grass", 3, "Nuzleaf", 306 },
                    { new Guid("bcf17486-bb68-425a-921e-834bc17f1e72"), "Skunk Pokemon", "Field", 4, "Skuntank", 435 },
                    { new Guid("9ae91a19-ba47-4506-ab97-fe20718b9bea"), "Iron Armor Pokemon", "Monster", 3, "Aggron", 306 },
                    { new Guid("2f20628d-8f52-4026-bdb6-9d8eff83abef"), "Thorn Pod Pokemon", "Grass, Mineral", 5, "Ferrothorn", 598 },
                    { new Guid("974b6559-e1d7-4150-8854-28d5496c4746"), "Balloon Pokemon", "Fairy", 1, "Jigglypuff", 39 },
                    { new Guid("ed1c79ea-e6e6-4e09-ae08-5c90aff1a61d"), "Embrace Pokemon", "Human-Like, Amorphous", 3, "Gardevoir", 282 },
                    { new Guid("bd56fee9-ac65-4e7e-a43e-cd3313a929c7"), "Bronze Bell Pokemon", "Mineral", 4, "Bronzong", 437 },
                    { new Guid("fcadfdb2-c9e5-4df4-9a06-1c4830292ac0"), "Psi Pokemon", "Human-Like", 1, "Abra", 63 },
                    { new Guid("15ff308f-a137-471a-b082-9e23e1059c98"), "Dragon Pokemon", "Water 1, Dragon", 1, "Dragonair", 148 },
                    { new Guid("7f73f100-8202-4b3b-96f0-e542252b97b9"), "Automaton Pokemon", "Mineral", 5, "Golett", 622 },
                    { new Guid("be7d9df2-a407-41ce-9f34-35ba6170705a"), "Order Pokemon", "Undiscovered", 6, "Zygarde", 718 },
                    { new Guid("3c5524e4-ce5c-4086-bcd0-1de86d595086"), "Blocking Pokemon", "Field", 8, "Obstagoon", 598 },
                    { new Guid("ec0f9285-22ab-4853-9e21-a0b0b796bfbd"), "Lizard Pokemon", "Monster, Dragon", 1, "Charmander", 4 },
                    { new Guid("3d899972-db96-44d9-8592-8d3fcfc9e74e"), "Sun Pokemon", "Bug", 5, "Volcarona", 637 },
                    { new Guid("5feafb41-ad8c-489f-a202-f484ce3cb1b0"), "Superpower Pokemon", "Human-Like", 1, "Machoke", 67 },
                    { new Guid("1120373b-1ed7-45e6-82a3-77d37327b230"), "Mud Fish Pokemon", "Monster, Water 1", 3, "Swampert", 260 },
                    { new Guid("89599747-2623-4a60-a9ed-dce8e5259bb6"), "Mock Kelp Pokemon", "Bug", 6, "Skrelp", 637 },
                    { new Guid("93c2eace-ccec-4afe-8f52-ca9aaf80239e"), "Raven Pokemon", "Flying", 8, "Corviknight", 823 },
                    { new Guid("320aab3a-b006-4d4e-80f4-7f3e0f2dbbb8"), "Single Horn Pokemon", "Bug", 2, "Heracross", 214 },
                    { new Guid("bdfa3d07-9159-45a4-a9cb-0838ad6ac5b6"), "Sickle Grass Pokemon", "Grass", 7, "Fomantis", 753 },
                    { new Guid("da796b4d-0e8c-482b-9636-bccdb5f6c713"), "Sea Lily Pokemon", "Water 3", 3, "Lileep", 345 },
                    { new Guid("62868e50-e229-48e2-b321-0c0f52d4d00d"), "Poison Pin Pokemon", "Undiscovered", 7, "Naganadel", 804 },
                    { new Guid("f560d6ea-6946-487d-ab28-94d5046e32ca"), "Bouquet Pokemon", "Fairy, Grass", 4, "Roserade", 407 },
                    { new Guid("eef550ed-a823-42bd-badf-fdafc7f839a6"), "Magnet Area Pokemon", "Mineral", 4, "Magnezone", 462 },
                    { new Guid("d724d735-a33f-4c5a-9430-0a31e6770519"), "Deep Black Pokemon", "Undiscovered", 5, "Zekrom", 644 },
                    { new Guid("fb1d7ad6-5b62-429c-8e05-6a26f2ddded1"), "Balloon Pokemon", "Amorphous", 4, "Drifloon", 425 }
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
                    { new Guid("7b7d665b-5d95-4d55-8f7f-7be362fc97f9"), 1 },
                    { new Guid("5fbad038-49b1-4e71-a5af-1c45ddd872fc"), 11 },
                    { new Guid("d7561fe2-1672-43e9-83b2-a90444dc3863"), 11 },
                    { new Guid("da796b4d-0e8c-482b-9636-bccdb5f6c713"), 11 },
                    { new Guid("93c2eace-ccec-4afe-8f52-ca9aaf80239e"), 17 },
                    { new Guid("ed1c79ea-e6e6-4e09-ae08-5c90aff1a61d"), 10 },
                    { new Guid("bd56fee9-ac65-4e7e-a43e-cd3313a929c7"), 10 },
                    { new Guid("fcadfdb2-c9e5-4df4-9a06-1c4830292ac0"), 10 },
                    { new Guid("eef550ed-a823-42bd-badf-fdafc7f839a6"), 17 },
                    { new Guid("bd56fee9-ac65-4e7e-a43e-cd3313a929c7"), 17 },
                    { new Guid("9ae91a19-ba47-4506-ab97-fe20718b9bea"), 17 },
                    { new Guid("2f20628d-8f52-4026-bdb6-9d8eff83abef"), 17 },
                    { new Guid("d724d735-a33f-4c5a-9430-0a31e6770519"), 14 },
                    { new Guid("be7d9df2-a407-41ce-9f34-35ba6170705a"), 9 },
                    { new Guid("7f73f100-8202-4b3b-96f0-e542252b97b9"), 9 },
                    { new Guid("1120373b-1ed7-45e6-82a3-77d37327b230"), 9 },
                    { new Guid("9ae91a19-ba47-4506-ab97-fe20718b9bea"), 11 },
                    { new Guid("bcf17486-bb68-425a-921e-834bc17f1e72"), 16 },
                    { new Guid("3ce01a8a-5a84-4632-b903-ebcb8fca5eab"), 16 },
                    { new Guid("3c5524e4-ce5c-4086-bcd0-1de86d595086"), 16 },
                    { new Guid("be7d9df2-a407-41ce-9f34-35ba6170705a"), 14 },
                    { new Guid("15ff308f-a137-471a-b082-9e23e1059c98"), 14 },
                    { new Guid("7f06468c-c199-42ec-a31f-798ca1e1f0e7"), 14 },
                    { new Guid("b022d558-d506-4c45-9239-8db6c8f92be0"), 13 },
                    { new Guid("769e9e1f-b2a6-4bac-9f24-94541cca9484"), 13 },
                    { new Guid("ca0c64b9-0347-4edb-b378-6054f3adeb49"), 13 },
                    { new Guid("320aab3a-b006-4d4e-80f4-7f3e0f2dbbb8"), 13 },
                    { new Guid("d724d735-a33f-4c5a-9430-0a31e6770519"), 8 },
                    { new Guid("3d899972-db96-44d9-8592-8d3fcfc9e74e"), 13 },
                    { new Guid("7f73f100-8202-4b3b-96f0-e542252b97b9"), 15 },
                    { new Guid("7f06468c-c199-42ec-a31f-798ca1e1f0e7"), 15 },
                    { new Guid("1e6bc260-2aa9-4206-aa20-345897ccc32e"), 15 },
                    { new Guid("67daba33-f80b-439f-b64f-67b8379a1dd2"), 15 },
                    { new Guid("ca0c64b9-0347-4edb-b378-6054f3adeb49"), 12 },
                    { new Guid("b0c07be1-6964-4bd8-b043-adad4a31f58e"), 12 },
                    { new Guid("5fbad038-49b1-4e71-a5af-1c45ddd872fc"), 12 },
                    { new Guid("fb1d7ad6-5b62-429c-8e05-6a26f2ddded1"), 15 },
                    { new Guid("eef550ed-a823-42bd-badf-fdafc7f839a6"), 8 },
                    { new Guid("62868e50-e229-48e2-b321-0c0f52d4d00d"), 14 },
                    { new Guid("ed1c79ea-e6e6-4e09-ae08-5c90aff1a61d"), 18 },
                    { new Guid("769e9e1f-b2a6-4bac-9f24-94541cca9484"), 6 },
                    { new Guid("f560d6ea-6946-487d-ab28-94d5046e32ca"), 6 }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { new Guid("89599747-2623-4a60-a9ed-dce8e5259bb6"), 4 },
                    { new Guid("1120373b-1ed7-45e6-82a3-77d37327b230"), 4 },
                    { new Guid("974b6559-e1d7-4150-8854-28d5496c4746"), 18 },
                    { new Guid("320aab3a-b006-4d4e-80f4-7f3e0f2dbbb8"), 3 },
                    { new Guid("5feafb41-ad8c-489f-a202-f484ce3cb1b0"), 3 },
                    { new Guid("89599747-2623-4a60-a9ed-dce8e5259bb6"), 7 },
                    { new Guid("62868e50-e229-48e2-b321-0c0f52d4d00d"), 7 },
                    { new Guid("2f20628d-8f52-4026-bdb6-9d8eff83abef"), 6 },
                    { new Guid("3d899972-db96-44d9-8592-8d3fcfc9e74e"), 2 },
                    { new Guid("f560d6ea-6946-487d-ab28-94d5046e32ca"), 7 },
                    { new Guid("b022d558-d506-4c45-9239-8db6c8f92be0"), 7 },
                    { new Guid("bcf17486-bb68-425a-921e-834bc17f1e72"), 7 },
                    { new Guid("93c2eace-ccec-4afe-8f52-ca9aaf80239e"), 5 },
                    { new Guid("da796b4d-0e8c-482b-9636-bccdb5f6c713"), 6 },
                    { new Guid("bdfa3d07-9159-45a4-a9cb-0838ad6ac5b6"), 6 },
                    { new Guid("fb1d7ad6-5b62-429c-8e05-6a26f2ddded1"), 5 },
                    { new Guid("ec0f9285-22ab-4853-9e21-a0b0b796bfbd"), 2 },
                    { new Guid("974b6559-e1d7-4150-8854-28d5496c4746"), 1 },
                    { new Guid("3c5524e4-ce5c-4086-bcd0-1de86d595086"), 1 },
                    { new Guid("3ce01a8a-5a84-4632-b903-ebcb8fca5eab"), 6 }
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
