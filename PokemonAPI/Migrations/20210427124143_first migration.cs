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
                    { new Guid("1eecc83a-39c1-42e6-a863-1b930fe8fff7"), "Sleeping Pokemon", "Monster", 1, "Snorlax", 143 },
                    { new Guid("d50a0299-86f0-4f36-a57c-2a460870b8a9"), "Head Butt Pokemon", "Monster", 4, "Rampardos", 409 },
                    { new Guid("07cb5925-f9aa-41cc-9311-58880ff220d1"), "Tundra Pokemon", "Monster", 6, "Aurorus", 699 },
                    { new Guid("9e7384bb-3314-46e6-b864-ecd8a87a090f"), "Snow Hat Pokemon", "Fairy,Mineral", 3, "Snorunt", 361 },
                    { new Guid("e847d5d7-fd37-43a4-bd69-bc2e12b19c68"), "Worm Pokemon", "Bug", 8, "Snom", 872 },
                    { new Guid("a653b452-9a39-4e03-bfd7-d44fb88ac9fb"), "Leaf-Wrapped Pokemon", "Bug", 5, "Swadloon", 541 },
                    { new Guid("2b149444-2b7c-48fb-a0cf-3835a8c4efea"), "Curlipede Pokemon", "Bug", 5, "Whirlipede", 544 },
                    { new Guid("cc8da5c0-2439-461a-8624-b281f9847089"), "Stealth Pokemon", "Amorphous,Dragon", 8, "Dragapult", 887 },
                    { new Guid("08df9cd6-e45e-4d86-a3f1-73c2b661737a"), "Screech Pokemon", "Amorphous", 2, "Misdreavus", 200 },
                    { new Guid("b7bda1aa-4716-4a1b-9e6f-f8edff7b4663"), "Magical Pokemon", "Amorphous", 4, "Mismagius", 429 },
                    { new Guid("76b7eead-fde1-43ef-83fb-29a29efde6eb"), "Wily Pokemon", "Field,Grass", 3, "Nuzleaf", 274 },
                    { new Guid("d7d951f8-7d61-4c1d-b4d5-eeecc263594c"), "Skunk Pokemon", "Field", 4, "Skuntank", 435 },
                    { new Guid("4d0c3dc4-50bc-4176-9bd9-8ba9d55646d4"), "Iron Armor Pokemon", "Monster", 3, "Aggron", 306 },
                    { new Guid("1f6dde95-0da5-4571-94e6-a2187109e2e2"), "Thorn Pod Pokemon", "Grass,Mineral", 5, "Ferrothorn", 598 },
                    { new Guid("dc0eb60f-7a18-495c-a168-36ccefbcbb71"), "Balloon Pokemon", "Fairy", 1, "Jigglypuff", 39 },
                    { new Guid("63579e21-0ade-4ae2-b430-3125f907fba8"), "Embrace Pokemon", "Human-Like,Amorphous", 3, "Gardevoir", 282 },
                    { new Guid("b73d87c6-e6fd-4f64-ba3e-2b1da37a51d0"), "Bronze Bell Pokemon", "Mineral", 4, "Bronzong", 437 },
                    { new Guid("a124d796-0abc-444b-8abd-1d60a16d24e2"), "Psi Pokemon", "Human-Like", 1, "Abra", 63 },
                    { new Guid("fa7b7ae4-9dd7-490e-974b-be6ecf97b044"), "Dragon Pokemon", "Water 1,Dragon", 1, "Dragonair", 148 },
                    { new Guid("9a848905-50b9-4420-8cb0-554cd13e2069"), "Automaton Pokemon", "Mineral", 5, "Golett", 622 },
                    { new Guid("c8877bc4-2d18-410c-9e7d-323ae59a16a5"), "Order Pokemon", "Undiscovered", 6, "Zygarde", 718 },
                    { new Guid("c1956f38-94d9-40bc-8420-ad0f75b73dcf"), "Blocking Pokemon", "Field", 8, "Obstagoon", 598 },
                    { new Guid("7fd1e9b6-ed9e-4f8d-808b-75119a428c60"), "Lizard Pokemon", "Monster,Dragon", 1, "Charmander", 4 },
                    { new Guid("bfca5085-3d5a-4055-80a9-4e1bd183972e"), "Sun Pokemon", "Bug", 5, "Volcarona", 637 },
                    { new Guid("6bae96ad-2523-442f-83f9-7b0b9e5c15ec"), "Superpower Pokemon", "Human-Like", 1, "Machoke", 67 },
                    { new Guid("a878374a-c734-49d4-86be-464182d63a57"), "Mud Fish Pokemon", "Monster,Water 1", 3, "Swampert", 260 },
                    { new Guid("6dab1d48-fd04-4f4a-8b3d-a243e76b8a2e"), "Mock Kelp Pokemon", "Bug", 6, "Skrelp", 637 },
                    { new Guid("8e001855-2ace-4494-86fa-c0dcc5060b49"), "Raven Pokemon", "Flying", 8, "Corviknight", 823 },
                    { new Guid("c1d0a18d-a4e8-4f0c-8b27-f80743383ee2"), "Single Horn Pokemon", "Bug", 2, "Heracross", 214 },
                    { new Guid("390a686c-52f1-4c37-97da-3a4447e2a3a9"), "Sickle Grass Pokemon", "Grass", 7, "Fomantis", 753 },
                    { new Guid("c420ca5c-d71b-45e8-992a-7523bc24f2dd"), "Sea Lily Pokemon", "Water 3", 3, "Lileep", 345 },
                    { new Guid("fca2f1a3-ccca-4ec8-9f6a-c065ea790bd0"), "Poison Pin Pokemon", "Undiscovered", 7, "Naganadel", 804 },
                    { new Guid("425a094a-792a-4203-b476-64ada11d276f"), "Bouquet Pokemon", "Fairy,Grass", 4, "Roserade", 407 },
                    { new Guid("1ebfff64-f258-468b-9940-52d10dc18a2d"), "Magnet Area Pokemon", "Mineral", 4, "Magnezone", 462 },
                    { new Guid("6baf9079-d024-4103-8a43-f3219d9438a3"), "Deep Black Pokemon", "Undiscovered", 5, "Zekrom", 644 },
                    { new Guid("789becb6-eb99-4ae6-839a-730e8043365f"), "Balloon Pokemon", "Amorphous", 4, "Drifloon", 425 }
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
                    { new Guid("1eecc83a-39c1-42e6-a863-1b930fe8fff7"), 1 },
                    { new Guid("07cb5925-f9aa-41cc-9311-58880ff220d1"), 11 },
                    { new Guid("d50a0299-86f0-4f36-a57c-2a460870b8a9"), 11 },
                    { new Guid("c420ca5c-d71b-45e8-992a-7523bc24f2dd"), 11 },
                    { new Guid("8e001855-2ace-4494-86fa-c0dcc5060b49"), 17 },
                    { new Guid("63579e21-0ade-4ae2-b430-3125f907fba8"), 10 },
                    { new Guid("b73d87c6-e6fd-4f64-ba3e-2b1da37a51d0"), 10 },
                    { new Guid("a124d796-0abc-444b-8abd-1d60a16d24e2"), 10 },
                    { new Guid("1ebfff64-f258-468b-9940-52d10dc18a2d"), 17 },
                    { new Guid("b73d87c6-e6fd-4f64-ba3e-2b1da37a51d0"), 17 },
                    { new Guid("4d0c3dc4-50bc-4176-9bd9-8ba9d55646d4"), 17 },
                    { new Guid("1f6dde95-0da5-4571-94e6-a2187109e2e2"), 17 },
                    { new Guid("6baf9079-d024-4103-8a43-f3219d9438a3"), 14 },
                    { new Guid("c8877bc4-2d18-410c-9e7d-323ae59a16a5"), 9 },
                    { new Guid("9a848905-50b9-4420-8cb0-554cd13e2069"), 9 },
                    { new Guid("a878374a-c734-49d4-86be-464182d63a57"), 9 },
                    { new Guid("4d0c3dc4-50bc-4176-9bd9-8ba9d55646d4"), 11 },
                    { new Guid("d7d951f8-7d61-4c1d-b4d5-eeecc263594c"), 16 },
                    { new Guid("76b7eead-fde1-43ef-83fb-29a29efde6eb"), 16 },
                    { new Guid("c1956f38-94d9-40bc-8420-ad0f75b73dcf"), 16 },
                    { new Guid("c8877bc4-2d18-410c-9e7d-323ae59a16a5"), 14 },
                    { new Guid("fa7b7ae4-9dd7-490e-974b-be6ecf97b044"), 14 },
                    { new Guid("cc8da5c0-2439-461a-8624-b281f9847089"), 14 },
                    { new Guid("2b149444-2b7c-48fb-a0cf-3835a8c4efea"), 13 },
                    { new Guid("a653b452-9a39-4e03-bfd7-d44fb88ac9fb"), 13 },
                    { new Guid("e847d5d7-fd37-43a4-bd69-bc2e12b19c68"), 13 },
                    { new Guid("c1d0a18d-a4e8-4f0c-8b27-f80743383ee2"), 13 },
                    { new Guid("6baf9079-d024-4103-8a43-f3219d9438a3"), 8 },
                    { new Guid("bfca5085-3d5a-4055-80a9-4e1bd183972e"), 13 },
                    { new Guid("9a848905-50b9-4420-8cb0-554cd13e2069"), 15 },
                    { new Guid("cc8da5c0-2439-461a-8624-b281f9847089"), 15 },
                    { new Guid("08df9cd6-e45e-4d86-a3f1-73c2b661737a"), 15 },
                    { new Guid("b7bda1aa-4716-4a1b-9e6f-f8edff7b4663"), 15 },
                    { new Guid("e847d5d7-fd37-43a4-bd69-bc2e12b19c68"), 12 },
                    { new Guid("9e7384bb-3314-46e6-b864-ecd8a87a090f"), 12 },
                    { new Guid("07cb5925-f9aa-41cc-9311-58880ff220d1"), 12 },
                    { new Guid("789becb6-eb99-4ae6-839a-730e8043365f"), 15 },
                    { new Guid("1ebfff64-f258-468b-9940-52d10dc18a2d"), 8 },
                    { new Guid("fca2f1a3-ccca-4ec8-9f6a-c065ea790bd0"), 14 },
                    { new Guid("63579e21-0ade-4ae2-b430-3125f907fba8"), 18 },
                    { new Guid("a653b452-9a39-4e03-bfd7-d44fb88ac9fb"), 6 },
                    { new Guid("425a094a-792a-4203-b476-64ada11d276f"), 6 }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { new Guid("6dab1d48-fd04-4f4a-8b3d-a243e76b8a2e"), 4 },
                    { new Guid("a878374a-c734-49d4-86be-464182d63a57"), 4 },
                    { new Guid("dc0eb60f-7a18-495c-a168-36ccefbcbb71"), 18 },
                    { new Guid("c1d0a18d-a4e8-4f0c-8b27-f80743383ee2"), 3 },
                    { new Guid("6bae96ad-2523-442f-83f9-7b0b9e5c15ec"), 3 },
                    { new Guid("6dab1d48-fd04-4f4a-8b3d-a243e76b8a2e"), 7 },
                    { new Guid("fca2f1a3-ccca-4ec8-9f6a-c065ea790bd0"), 7 },
                    { new Guid("1f6dde95-0da5-4571-94e6-a2187109e2e2"), 6 },
                    { new Guid("bfca5085-3d5a-4055-80a9-4e1bd183972e"), 2 },
                    { new Guid("425a094a-792a-4203-b476-64ada11d276f"), 7 },
                    { new Guid("2b149444-2b7c-48fb-a0cf-3835a8c4efea"), 7 },
                    { new Guid("d7d951f8-7d61-4c1d-b4d5-eeecc263594c"), 7 },
                    { new Guid("8e001855-2ace-4494-86fa-c0dcc5060b49"), 5 },
                    { new Guid("c420ca5c-d71b-45e8-992a-7523bc24f2dd"), 6 },
                    { new Guid("390a686c-52f1-4c37-97da-3a4447e2a3a9"), 6 },
                    { new Guid("789becb6-eb99-4ae6-839a-730e8043365f"), 5 },
                    { new Guid("7fd1e9b6-ed9e-4f8d-808b-75119a428c60"), 2 },
                    { new Guid("dc0eb60f-7a18-495c-a168-36ccefbcbb71"), 1 },
                    { new Guid("c1956f38-94d9-40bc-8420-ad0f75b73dcf"), 1 },
                    { new Guid("76b7eead-fde1-43ef-83fb-29a29efde6eb"), 6 }
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
