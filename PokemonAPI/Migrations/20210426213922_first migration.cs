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
                    { new Guid("40bb873f-2acf-4b8e-882b-ceedcbcf817f"), "Sleeping Pokemon", "Monster", 1, "Snorlax", 143 },
                    { new Guid("2ab70264-8249-4b00-9337-75d6a5574e9c"), "Head Butt Pokemon", "Monster", 4, "Rampardos", 409 },
                    { new Guid("361b83c6-81ff-4694-9219-6d41ffb3c448"), "Tundra Pokemon", "Monster", 6, "Aurorus", 699 },
                    { new Guid("cdb5e327-47fe-4395-a682-0a3ee83ae1c4"), "Snow Hat Pokemon", "Fairy, Mineral", 3, "Snorunt", 361 },
                    { new Guid("55d6fad3-615a-4476-995c-49463f074504"), "Worm Pokemon", "Bug", 8, "Snom", 872 },
                    { new Guid("32282913-92e1-48ae-b26b-ab76129e0ece"), "Leaf-Wrapped Pokemon", "Bug", 5, "Swadloon", 541 },
                    { new Guid("21d767c3-6fff-428e-a2fc-7721ed66ea29"), "Curlipede Pokemon", "Bug", 5, "Whirlipede", 544 },
                    { new Guid("35f46562-8ab5-4b61-98c5-ce8838da2ac2"), "Stealth Pokemon", "Amorphous, Dragon", 8, "Dragapult", 887 },
                    { new Guid("765bdf9a-6a83-43a6-94f6-f25ca1b5fad1"), "Screech Pokemon", "Amorphous", 2, "Misdreavus", 200 },
                    { new Guid("5e613c51-20a5-4bb2-ac3f-a4f69237b84d"), "Magical Pokemon", "Amorphous", 4, "Mismagius", 429 },
                    { new Guid("e8e4504d-cbfd-424e-b848-f07bfe0d7381"), "Wily Pokemon", "Field, Grass", 3, "Nuzleaf", 274 },
                    { new Guid("a32ca772-283c-4776-979d-4eaebed48578"), "Skunk Pokemon", "Field", 4, "Skuntank", 435 },
                    { new Guid("c2583f2c-aa81-4031-8626-92bbbf286bfe"), "Iron Armor Pokemon", "Monster", 3, "Aggron", 306 },
                    { new Guid("46efd690-62be-4cad-a5c4-13d61ce74eca"), "Thorn Pod Pokemon", "Grass, Mineral", 5, "Ferrothorn", 598 },
                    { new Guid("dc34adfd-5711-45aa-9dd2-b25a2d7ab5a1"), "Balloon Pokemon", "Fairy", 1, "Jigglypuff", 39 },
                    { new Guid("94b70f7a-e102-45f4-aac1-af4ac8f60869"), "Embrace Pokemon", "Human-Like, Amorphous", 3, "Gardevoir", 282 },
                    { new Guid("275e29b5-8a33-448e-ac86-a49cc7955931"), "Bronze Bell Pokemon", "Mineral", 4, "Bronzong", 437 },
                    { new Guid("c1ec31d8-0015-4d6b-ba75-e349ce5d3581"), "Psi Pokemon", "Human-Like", 1, "Abra", 63 },
                    { new Guid("7630549c-7f3b-4b36-aefb-febd1663cbb9"), "Dragon Pokemon", "Water 1, Dragon", 1, "Dragonair", 148 },
                    { new Guid("6b66d586-979e-4a60-9e69-4c6f74974262"), "Automaton Pokemon", "Mineral", 5, "Golett", 622 },
                    { new Guid("4a6a6ac2-2c7c-415d-838b-857437844365"), "Order Pokemon", "Undiscovered", 6, "Zygarde", 718 },
                    { new Guid("5a23d352-5c7d-43d7-914b-5260db5dc43d"), "Blocking Pokemon", "Field", 8, "Obstagoon", 598 },
                    { new Guid("d46240f9-22de-40b3-a7dd-b8a66abea7a3"), "Lizard Pokemon", "Monster, Dragon", 1, "Charmander", 4 },
                    { new Guid("d1938c55-457a-4ad7-8a7f-1384c4b2eb27"), "Sun Pokemon", "Bug", 5, "Volcarona", 637 },
                    { new Guid("a612fd00-184e-4a3f-969b-9d676150fd8b"), "Superpower Pokemon", "Human-Like", 1, "Machoke", 67 },
                    { new Guid("9fe15559-2e02-45e5-97b1-7605d18a50f2"), "Mud Fish Pokemon", "Monster, Water 1", 3, "Swampert", 260 },
                    { new Guid("236a69da-00ca-4066-adaf-e71ecaa6e0ab"), "Mock Kelp Pokemon", "Bug", 6, "Skrelp", 637 },
                    { new Guid("a7b873c7-9d43-4506-b2bd-778b4a53b9ca"), "Raven Pokemon", "Flying", 8, "Corviknight", 823 },
                    { new Guid("2c4738b0-152e-45bb-b54b-9130660fe35e"), "Single Horn Pokemon", "Bug", 2, "Heracross", 214 },
                    { new Guid("19abab28-b860-4d57-b006-bb1f0b17dd12"), "Sickle Grass Pokemon", "Grass", 7, "Fomantis", 753 },
                    { new Guid("beb21f21-3cc3-45bf-967a-3cf512588e1d"), "Sea Lily Pokemon", "Water 3", 3, "Lileep", 345 },
                    { new Guid("710af572-446f-4edd-9228-fef6f8827fd7"), "Poison Pin Pokemon", "Undiscovered", 7, "Naganadel", 804 },
                    { new Guid("23f4fc0b-498b-4b9f-b98f-27061ee99639"), "Bouquet Pokemon", "Fairy, Grass", 4, "Roserade", 407 },
                    { new Guid("0ec5f816-d6f2-4e08-817c-f30d990198b1"), "Magnet Area Pokemon", "Mineral", 4, "Magnezone", 462 },
                    { new Guid("6cd0ca06-95f3-447e-b1a7-e327b873c045"), "Deep Black Pokemon", "Undiscovered", 5, "Zekrom", 644 },
                    { new Guid("e3e8f216-debb-410f-811c-46c9da30ae00"), "Balloon Pokemon", "Amorphous", 4, "Drifloon", 425 }
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
                    { new Guid("40bb873f-2acf-4b8e-882b-ceedcbcf817f"), 1 },
                    { new Guid("361b83c6-81ff-4694-9219-6d41ffb3c448"), 11 },
                    { new Guid("2ab70264-8249-4b00-9337-75d6a5574e9c"), 11 },
                    { new Guid("beb21f21-3cc3-45bf-967a-3cf512588e1d"), 11 },
                    { new Guid("a7b873c7-9d43-4506-b2bd-778b4a53b9ca"), 17 },
                    { new Guid("94b70f7a-e102-45f4-aac1-af4ac8f60869"), 10 },
                    { new Guid("275e29b5-8a33-448e-ac86-a49cc7955931"), 10 },
                    { new Guid("c1ec31d8-0015-4d6b-ba75-e349ce5d3581"), 10 },
                    { new Guid("0ec5f816-d6f2-4e08-817c-f30d990198b1"), 17 },
                    { new Guid("275e29b5-8a33-448e-ac86-a49cc7955931"), 17 },
                    { new Guid("c2583f2c-aa81-4031-8626-92bbbf286bfe"), 17 },
                    { new Guid("46efd690-62be-4cad-a5c4-13d61ce74eca"), 17 },
                    { new Guid("6cd0ca06-95f3-447e-b1a7-e327b873c045"), 14 },
                    { new Guid("4a6a6ac2-2c7c-415d-838b-857437844365"), 9 },
                    { new Guid("6b66d586-979e-4a60-9e69-4c6f74974262"), 9 },
                    { new Guid("9fe15559-2e02-45e5-97b1-7605d18a50f2"), 9 },
                    { new Guid("c2583f2c-aa81-4031-8626-92bbbf286bfe"), 11 },
                    { new Guid("a32ca772-283c-4776-979d-4eaebed48578"), 16 },
                    { new Guid("e8e4504d-cbfd-424e-b848-f07bfe0d7381"), 16 },
                    { new Guid("5a23d352-5c7d-43d7-914b-5260db5dc43d"), 16 },
                    { new Guid("4a6a6ac2-2c7c-415d-838b-857437844365"), 14 },
                    { new Guid("7630549c-7f3b-4b36-aefb-febd1663cbb9"), 14 },
                    { new Guid("35f46562-8ab5-4b61-98c5-ce8838da2ac2"), 14 },
                    { new Guid("21d767c3-6fff-428e-a2fc-7721ed66ea29"), 13 },
                    { new Guid("32282913-92e1-48ae-b26b-ab76129e0ece"), 13 },
                    { new Guid("55d6fad3-615a-4476-995c-49463f074504"), 13 },
                    { new Guid("2c4738b0-152e-45bb-b54b-9130660fe35e"), 13 },
                    { new Guid("6cd0ca06-95f3-447e-b1a7-e327b873c045"), 8 },
                    { new Guid("d1938c55-457a-4ad7-8a7f-1384c4b2eb27"), 13 },
                    { new Guid("6b66d586-979e-4a60-9e69-4c6f74974262"), 15 },
                    { new Guid("35f46562-8ab5-4b61-98c5-ce8838da2ac2"), 15 },
                    { new Guid("765bdf9a-6a83-43a6-94f6-f25ca1b5fad1"), 15 },
                    { new Guid("5e613c51-20a5-4bb2-ac3f-a4f69237b84d"), 15 },
                    { new Guid("55d6fad3-615a-4476-995c-49463f074504"), 12 },
                    { new Guid("cdb5e327-47fe-4395-a682-0a3ee83ae1c4"), 12 },
                    { new Guid("361b83c6-81ff-4694-9219-6d41ffb3c448"), 12 },
                    { new Guid("e3e8f216-debb-410f-811c-46c9da30ae00"), 15 },
                    { new Guid("0ec5f816-d6f2-4e08-817c-f30d990198b1"), 8 },
                    { new Guid("710af572-446f-4edd-9228-fef6f8827fd7"), 14 },
                    { new Guid("94b70f7a-e102-45f4-aac1-af4ac8f60869"), 18 },
                    { new Guid("32282913-92e1-48ae-b26b-ab76129e0ece"), 6 },
                    { new Guid("23f4fc0b-498b-4b9f-b98f-27061ee99639"), 6 }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { new Guid("236a69da-00ca-4066-adaf-e71ecaa6e0ab"), 4 },
                    { new Guid("9fe15559-2e02-45e5-97b1-7605d18a50f2"), 4 },
                    { new Guid("dc34adfd-5711-45aa-9dd2-b25a2d7ab5a1"), 18 },
                    { new Guid("2c4738b0-152e-45bb-b54b-9130660fe35e"), 3 },
                    { new Guid("a612fd00-184e-4a3f-969b-9d676150fd8b"), 3 },
                    { new Guid("236a69da-00ca-4066-adaf-e71ecaa6e0ab"), 7 },
                    { new Guid("710af572-446f-4edd-9228-fef6f8827fd7"), 7 },
                    { new Guid("46efd690-62be-4cad-a5c4-13d61ce74eca"), 6 },
                    { new Guid("d1938c55-457a-4ad7-8a7f-1384c4b2eb27"), 2 },
                    { new Guid("23f4fc0b-498b-4b9f-b98f-27061ee99639"), 7 },
                    { new Guid("21d767c3-6fff-428e-a2fc-7721ed66ea29"), 7 },
                    { new Guid("a32ca772-283c-4776-979d-4eaebed48578"), 7 },
                    { new Guid("a7b873c7-9d43-4506-b2bd-778b4a53b9ca"), 5 },
                    { new Guid("beb21f21-3cc3-45bf-967a-3cf512588e1d"), 6 },
                    { new Guid("19abab28-b860-4d57-b006-bb1f0b17dd12"), 6 },
                    { new Guid("e3e8f216-debb-410f-811c-46c9da30ae00"), 5 },
                    { new Guid("d46240f9-22de-40b3-a7dd-b8a66abea7a3"), 2 },
                    { new Guid("dc34adfd-5711-45aa-9dd2-b25a2d7ab5a1"), 1 },
                    { new Guid("5a23d352-5c7d-43d7-914b-5260db5dc43d"), 1 },
                    { new Guid("e8e4504d-cbfd-424e-b848-f07bfe0d7381"), 6 }
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
