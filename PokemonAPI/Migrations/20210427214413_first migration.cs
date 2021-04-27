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
                    { new Guid("c7505368-c155-43af-897c-9942d8b6c84e"), "Sleeping Pokemon", "Monster", 1, "Snorlax", 143 },
                    { new Guid("e633ed15-7277-4ebb-bc36-8cba6febb60e"), "Head Butt Pokemon", "Monster", 4, "Rampardos", 409 },
                    { new Guid("ccc804d6-e950-4514-8d8a-464e4e55344a"), "Tundra Pokemon", "Monster", 6, "Aurorus", 699 },
                    { new Guid("d93c580f-87f6-42a7-9375-69acb6f07b0f"), "Snow Hat Pokemon", "Fairy,Mineral", 3, "Snorunt", 361 },
                    { new Guid("02435f04-1fcf-42a9-ad64-30c6a801af11"), "Worm Pokemon", "Bug", 8, "Snom", 872 },
                    { new Guid("f3afeaa8-4cb1-4036-9abe-0e04b91abaee"), "Leaf-Wrapped Pokemon", "Bug", 5, "Swadloon", 541 },
                    { new Guid("ee2bef72-134f-4f3d-9722-271d65d0c84f"), "Curlipede Pokemon", "Bug", 5, "Whirlipede", 544 },
                    { new Guid("3bac001d-fac7-43c1-abac-30765d3c7ee6"), "Stealth Pokemon", "Amorphous,Dragon", 8, "Dragapult", 887 },
                    { new Guid("46c249af-78be-4a43-b10c-62d3109ff96d"), "Screech Pokemon", "Amorphous", 2, "Misdreavus", 200 },
                    { new Guid("2d61ff4a-e16f-4c39-acab-779d693727ca"), "Magical Pokemon", "Amorphous", 4, "Mismagius", 429 },
                    { new Guid("fb53d3d6-5a9b-4277-84e2-12be13327a82"), "Wily Pokemon", "Field,Grass", 3, "Nuzleaf", 274 },
                    { new Guid("9525f48c-9a7b-4366-ac90-fd7688def9e3"), "Skunk Pokemon", "Field", 4, "Skuntank", 435 },
                    { new Guid("e44fee70-d043-403e-8b65-231f85e0bb9c"), "Iron Armor Pokemon", "Monster", 3, "Aggron", 306 },
                    { new Guid("3784195d-3a50-45c5-be11-d27b9ff57513"), "Thorn Pod Pokemon", "Grass,Mineral", 5, "Ferrothorn", 598 },
                    { new Guid("0bd2f77e-1766-49d4-ace8-8ad3a25e5c18"), "Balloon Pokemon", "Fairy", 1, "Jigglypuff", 39 },
                    { new Guid("08f7d781-6220-48d2-ab6d-a83f797b0f78"), "Embrace Pokemon", "Human-Like,Amorphous", 3, "Gardevoir", 282 },
                    { new Guid("f99a9643-1f41-426a-882e-93417ab30e10"), "Bronze Bell Pokemon", "Mineral", 4, "Bronzong", 437 },
                    { new Guid("a3cc0906-979a-4fc7-847e-0c3e8b05bf10"), "Psi Pokemon", "Human-Like", 1, "Abra", 63 },
                    { new Guid("ebec1059-32bc-418e-b134-8173e03d843d"), "Dragon Pokemon", "Water 1,Dragon", 1, "Dragonair", 148 },
                    { new Guid("44979f06-8a23-4e02-85cf-001478bc0211"), "Automaton Pokemon", "Mineral", 5, "Golett", 622 },
                    { new Guid("b2dd63bb-ffea-4f5c-99ed-ada8b7a671a0"), "Order Pokemon", "Undiscovered", 6, "Zygarde", 718 },
                    { new Guid("517266be-c192-4d36-a4b8-b04b169b107e"), "Blocking Pokemon", "Field", 8, "Obstagoon", 598 },
                    { new Guid("3b7d17ed-3464-4331-a36b-0167108a479b"), "Lizard Pokemon", "Monster,Dragon", 1, "Charmander", 4 },
                    { new Guid("ce02c441-ff9e-4574-a7cd-7585f904766f"), "Sun Pokemon", "Bug", 5, "Volcarona", 637 },
                    { new Guid("26484ea2-a423-491e-b277-3b8e4082664d"), "Superpower Pokemon", "Human-Like", 1, "Machoke", 67 },
                    { new Guid("b3dc804e-ffa2-4e73-8cfa-bbd296577fe4"), "Mud Fish Pokemon", "Monster,Water 1", 3, "Swampert", 260 },
                    { new Guid("7e9e3a58-cf34-4730-b4c7-ac878ebbc4cd"), "Mock Kelp Pokemon", "Bug", 6, "Skrelp", 637 },
                    { new Guid("2469d601-c117-4af2-b6a5-b1d56caf87da"), "Raven Pokemon", "Flying", 8, "Corviknight", 823 },
                    { new Guid("61a59743-a2e6-409b-a92a-1d20d07f65f4"), "Single Horn Pokemon", "Bug", 2, "Heracross", 214 },
                    { new Guid("ed729a98-69a6-4043-9d91-39e2f90927cb"), "Sickle Grass Pokemon", "Grass", 7, "Fomantis", 753 },
                    { new Guid("db1d616c-44d5-4b5a-b699-58ac8ceffca2"), "Sea Lily Pokemon", "Water 3", 3, "Lileep", 345 },
                    { new Guid("241395df-5ad8-4164-a848-b6615f2b4f59"), "Poison Pin Pokemon", "Undiscovered", 7, "Naganadel", 804 },
                    { new Guid("3e2c1013-b827-4425-ac34-6909323e617e"), "Bouquet Pokemon", "Fairy,Grass", 4, "Roserade", 407 },
                    { new Guid("b25a7d4c-58bd-4bee-ab3f-986c1bb7c040"), "Magnet Area Pokemon", "Mineral", 4, "Magnezone", 462 },
                    { new Guid("ea45c8a0-0b34-41e9-89fb-81980b5a78c4"), "Deep Black Pokemon", "Undiscovered", 5, "Zekrom", 644 },
                    { new Guid("ad9e4b02-e54e-4898-ba0b-a6ef3a36f347"), "Balloon Pokemon", "Amorphous", 4, "Drifloon", 425 }
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
                    { new Guid("c7505368-c155-43af-897c-9942d8b6c84e"), 1 },
                    { new Guid("ccc804d6-e950-4514-8d8a-464e4e55344a"), 11 },
                    { new Guid("e633ed15-7277-4ebb-bc36-8cba6febb60e"), 11 },
                    { new Guid("db1d616c-44d5-4b5a-b699-58ac8ceffca2"), 11 },
                    { new Guid("2469d601-c117-4af2-b6a5-b1d56caf87da"), 17 },
                    { new Guid("08f7d781-6220-48d2-ab6d-a83f797b0f78"), 10 },
                    { new Guid("f99a9643-1f41-426a-882e-93417ab30e10"), 10 },
                    { new Guid("a3cc0906-979a-4fc7-847e-0c3e8b05bf10"), 10 },
                    { new Guid("b25a7d4c-58bd-4bee-ab3f-986c1bb7c040"), 17 },
                    { new Guid("f99a9643-1f41-426a-882e-93417ab30e10"), 17 },
                    { new Guid("e44fee70-d043-403e-8b65-231f85e0bb9c"), 17 },
                    { new Guid("3784195d-3a50-45c5-be11-d27b9ff57513"), 17 },
                    { new Guid("ea45c8a0-0b34-41e9-89fb-81980b5a78c4"), 14 },
                    { new Guid("b2dd63bb-ffea-4f5c-99ed-ada8b7a671a0"), 9 },
                    { new Guid("44979f06-8a23-4e02-85cf-001478bc0211"), 9 },
                    { new Guid("b3dc804e-ffa2-4e73-8cfa-bbd296577fe4"), 9 },
                    { new Guid("e44fee70-d043-403e-8b65-231f85e0bb9c"), 11 },
                    { new Guid("9525f48c-9a7b-4366-ac90-fd7688def9e3"), 16 },
                    { new Guid("fb53d3d6-5a9b-4277-84e2-12be13327a82"), 16 },
                    { new Guid("517266be-c192-4d36-a4b8-b04b169b107e"), 16 },
                    { new Guid("b2dd63bb-ffea-4f5c-99ed-ada8b7a671a0"), 14 },
                    { new Guid("ebec1059-32bc-418e-b134-8173e03d843d"), 14 },
                    { new Guid("3bac001d-fac7-43c1-abac-30765d3c7ee6"), 14 },
                    { new Guid("ee2bef72-134f-4f3d-9722-271d65d0c84f"), 13 },
                    { new Guid("f3afeaa8-4cb1-4036-9abe-0e04b91abaee"), 13 },
                    { new Guid("02435f04-1fcf-42a9-ad64-30c6a801af11"), 13 },
                    { new Guid("61a59743-a2e6-409b-a92a-1d20d07f65f4"), 13 },
                    { new Guid("ea45c8a0-0b34-41e9-89fb-81980b5a78c4"), 8 },
                    { new Guid("ce02c441-ff9e-4574-a7cd-7585f904766f"), 13 },
                    { new Guid("44979f06-8a23-4e02-85cf-001478bc0211"), 15 },
                    { new Guid("3bac001d-fac7-43c1-abac-30765d3c7ee6"), 15 },
                    { new Guid("46c249af-78be-4a43-b10c-62d3109ff96d"), 15 },
                    { new Guid("2d61ff4a-e16f-4c39-acab-779d693727ca"), 15 },
                    { new Guid("02435f04-1fcf-42a9-ad64-30c6a801af11"), 12 },
                    { new Guid("d93c580f-87f6-42a7-9375-69acb6f07b0f"), 12 },
                    { new Guid("ccc804d6-e950-4514-8d8a-464e4e55344a"), 12 },
                    { new Guid("ad9e4b02-e54e-4898-ba0b-a6ef3a36f347"), 15 },
                    { new Guid("b25a7d4c-58bd-4bee-ab3f-986c1bb7c040"), 8 },
                    { new Guid("241395df-5ad8-4164-a848-b6615f2b4f59"), 14 },
                    { new Guid("08f7d781-6220-48d2-ab6d-a83f797b0f78"), 18 },
                    { new Guid("f3afeaa8-4cb1-4036-9abe-0e04b91abaee"), 6 },
                    { new Guid("3e2c1013-b827-4425-ac34-6909323e617e"), 6 }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { new Guid("7e9e3a58-cf34-4730-b4c7-ac878ebbc4cd"), 4 },
                    { new Guid("b3dc804e-ffa2-4e73-8cfa-bbd296577fe4"), 4 },
                    { new Guid("0bd2f77e-1766-49d4-ace8-8ad3a25e5c18"), 18 },
                    { new Guid("61a59743-a2e6-409b-a92a-1d20d07f65f4"), 3 },
                    { new Guid("26484ea2-a423-491e-b277-3b8e4082664d"), 3 },
                    { new Guid("7e9e3a58-cf34-4730-b4c7-ac878ebbc4cd"), 7 },
                    { new Guid("241395df-5ad8-4164-a848-b6615f2b4f59"), 7 },
                    { new Guid("3784195d-3a50-45c5-be11-d27b9ff57513"), 6 },
                    { new Guid("ce02c441-ff9e-4574-a7cd-7585f904766f"), 2 },
                    { new Guid("3e2c1013-b827-4425-ac34-6909323e617e"), 7 },
                    { new Guid("ee2bef72-134f-4f3d-9722-271d65d0c84f"), 7 },
                    { new Guid("9525f48c-9a7b-4366-ac90-fd7688def9e3"), 7 },
                    { new Guid("2469d601-c117-4af2-b6a5-b1d56caf87da"), 5 },
                    { new Guid("db1d616c-44d5-4b5a-b699-58ac8ceffca2"), 6 },
                    { new Guid("ed729a98-69a6-4043-9d91-39e2f90927cb"), 6 },
                    { new Guid("ad9e4b02-e54e-4898-ba0b-a6ef3a36f347"), 5 },
                    { new Guid("3b7d17ed-3464-4331-a36b-0167108a479b"), 2 },
                    { new Guid("0bd2f77e-1766-49d4-ace8-8ad3a25e5c18"), 1 },
                    { new Guid("517266be-c192-4d36-a4b8-b04b169b107e"), 1 },
                    { new Guid("fb53d3d6-5a9b-4277-84e2-12be13327a82"), 6 }
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
