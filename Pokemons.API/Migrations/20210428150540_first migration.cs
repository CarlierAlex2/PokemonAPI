﻿using System;
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
                    EggGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hp = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    SpAtk = table.Column<int>(type: "int", nullable: false),
                    SpDef = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false)
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
                columns: new[] { "PokemonId", "Attack", "Classification", "Defense", "EggGroup", "Generation", "Hp", "Name", "PokedexEntry", "SpAtk", "SpDef", "Speed" },
                values: new object[,]
                {
                    { new Guid("e02cd172-022c-40d8-952d-3487b718c0e2"), 110, "Sleeping Pokemon", 65, "Monster", 1, 160, "Snorlax", 143, 65, 110, 30 },
                    { new Guid("a72ac717-a8bb-47e4-afd8-d5a3e2766881"), 77, "Tundra Pokemon", 72, "Monster", 6, 123, "Aurorus", 699, 99, 92, 58 },
                    { new Guid("04f8c30a-daf5-431a-8819-11b926e807c1"), 165, "Head Butt Pokemon", 60, "Monster", 4, 97, "Rampardos", 409, 65, 50, 58 },
                    { new Guid("d233f762-7b17-4374-83a6-c08c957de765"), 20, "Psi Pokemon", 15, "Human-Like", 1, 25, "Abra", 63, 105, 55, 90 },
                    { new Guid("38d734cb-008f-4b79-9185-6c076dc176e2"), 100, "Order Pokemon", 121, "Undiscovered", 6, 216, "Zygarde", 718, 91, 95, 85 },
                    { new Guid("98346c71-606f-4ce0-b53b-867e467cdfaf"), 74, "Automaton Pokemon", 50, "Mineral", 5, 59, "Golett", 622, 35, 50, 35 },
                    { new Guid("1c9805f8-4eef-46b8-b3f2-681a686b912a"), 150, "Deep Black Pokemon", 120, "Undiscovered", 5, 100, "Zekrom", 644, 120, 100, 90 },
                    { new Guid("0a60a384-55c5-4639-8d93-1bcd036533b9"), 70, "Magnet Area Pokemon", 115, "Mineral", 4, 70, "Magnezone", 462, 130, 90, 60 },
                    { new Guid("72015dbc-2ec8-431d-a343-38396f6c401c"), 70, "Bouquet Pokemon", 55, "Fairy,Grass", 4, 60, "Roserade", 407, 125, 105, 90 },
                    { new Guid("7489545c-9f12-4872-912f-b9e7174ed68c"), 73, "Poison Pin Pokemon", 73, "Undiscovered", 7, 73, "Naganadel", 804, 127, 73, 121 },
                    { new Guid("90aa96ad-5ad4-45c5-8047-caedbc9d1547"), 41, "Sea Lily Pokemon", 77, "Water 3", 3, 66, "Lileep", 345, 61, 87, 23 },
                    { new Guid("24c2d5fd-1fe8-4b1c-8cc6-5dac519e7f41"), 89, "Bronze Bell Pokemon", 116, "Mineral", 4, 67, "Bronzong", 437, 79, 116, 33 },
                    { new Guid("239aa97e-7e2a-41e8-9f6c-00a8a33458d3"), 50, "Balloon Pokemon", 34, "Amorphous", 4, 90, "Drifloon", 425, 60, 44, 70 },
                    { new Guid("0f3cf491-a4dd-4558-a9d9-256bdc4168ab"), 55, "Sickle Grass Pokemon", 35, "Grass", 7, 40, "Fomantis", 753, 50, 35, 35 },
                    { new Guid("8f66db89-d341-41f3-99f4-3f0c8d4312f6"), 90, "Blocking Pokemon", 101, "Field", 8, 93, "Obstagoon", 598, 60, 81, 95 },
                    { new Guid("491ee503-62ca-42fb-bc45-9556edbb4c4b"), 60, "Sun Pokemon", 65, "Bug", 5, 85, "Volcarona", 637, 135, 105, 100 },
                    { new Guid("02c1e3a9-d092-4ae8-9868-64fdb9583fd4"), 100, "Superpower Pokemon", 70, "Human-Like", 1, 80, "Machoke", 67, 50, 60, 45 },
                    { new Guid("51132596-3e9f-42bc-b919-3eb584e40fb7"), 52, "Lizard Pokemon", 43, "Monster,Dragon", 1, 39, "Charmander", 4, 60, 50, 65 },
                    { new Guid("ff118a41-b604-4cfe-8b9a-c0c10ba5e558"), 110, "Mud Fish Pokemon", 90, "Monster,Water 1", 3, 100, "Swampert", 260, 85, 90, 60 },
                    { new Guid("f9fe42b8-d15f-40f9-ab49-3de84c64f2ae"), 60, "Mock Kelp Pokemon", 60, "Bug", 6, 50, "Skrelp", 637, 60, 60, 30 },
                    { new Guid("5d5df3b1-f0dd-4b66-8a19-c53f89313dd3"), 87, "Raven Pokemon", 105, "Flying", 8, 98, "Corviknight", 823, 53, 85, 67 },
                    { new Guid("a3e37508-fb8a-4787-a4a6-8f910b3f0489"), 125, "Single Horn Pokemon", 75, "Bug", 2, 80, "Heracross", 214, 40, 95, 85 }
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
                    { 12, "Ice" },
                    { 11, "Rock" },
                    { 9, "Ground" },
                    { 4, "Water" },
                    { 7, "Poison" },
                    { 6, "Grass" },
                    { 5, "Flying" },
                    { 3, "Fighting" },
                    { 2, "Fire" },
                    { 1, "Normal" },
                    { 17, "Steel" },
                    { 8, "Electric" },
                    { 18, "Fairy" }
                });

            migrationBuilder.InsertData(
                table: "PokemonTyping",
                columns: new[] { "PokemonId", "TypingId" },
                values: new object[,]
                {
                    { new Guid("e02cd172-022c-40d8-952d-3487b718c0e2"), 1 },
                    { new Guid("04f8c30a-daf5-431a-8819-11b926e807c1"), 11 },
                    { new Guid("72015dbc-2ec8-431d-a343-38396f6c401c"), 7 },
                    { new Guid("a72ac717-a8bb-47e4-afd8-d5a3e2766881"), 12 },
                    { new Guid("239aa97e-7e2a-41e8-9f6c-00a8a33458d3"), 15 },
                    { new Guid("98346c71-606f-4ce0-b53b-867e467cdfaf"), 15 },
                    { new Guid("0a60a384-55c5-4639-8d93-1bcd036533b9"), 8 },
                    { new Guid("1c9805f8-4eef-46b8-b3f2-681a686b912a"), 8 },
                    { new Guid("ff118a41-b604-4cfe-8b9a-c0c10ba5e558"), 9 },
                    { new Guid("98346c71-606f-4ce0-b53b-867e467cdfaf"), 9 },
                    { new Guid("38d734cb-008f-4b79-9185-6c076dc176e2"), 9 },
                    { new Guid("8f66db89-d341-41f3-99f4-3f0c8d4312f6"), 16 },
                    { new Guid("24c2d5fd-1fe8-4b1c-8cc6-5dac519e7f41"), 17 },
                    { new Guid("0a60a384-55c5-4639-8d93-1bcd036533b9"), 17 },
                    { new Guid("5d5df3b1-f0dd-4b66-8a19-c53f89313dd3"), 17 },
                    { new Guid("a72ac717-a8bb-47e4-afd8-d5a3e2766881"), 11 },
                    { new Guid("d233f762-7b17-4374-83a6-c08c957de765"), 10 },
                    { new Guid("24c2d5fd-1fe8-4b1c-8cc6-5dac519e7f41"), 10 },
                    { new Guid("f9fe42b8-d15f-40f9-ab49-3de84c64f2ae"), 7 },
                    { new Guid("38d734cb-008f-4b79-9185-6c076dc176e2"), 14 },
                    { new Guid("7489545c-9f12-4872-912f-b9e7174ed68c"), 7 },
                    { new Guid("72015dbc-2ec8-431d-a343-38396f6c401c"), 6 },
                    { new Guid("8f66db89-d341-41f3-99f4-3f0c8d4312f6"), 1 },
                    { new Guid("51132596-3e9f-42bc-b919-3eb584e40fb7"), 2 },
                    { new Guid("491ee503-62ca-42fb-bc45-9556edbb4c4b"), 2 },
                    { new Guid("02c1e3a9-d092-4ae8-9868-64fdb9583fd4"), 3 },
                    { new Guid("a3e37508-fb8a-4787-a4a6-8f910b3f0489"), 3 },
                    { new Guid("ff118a41-b604-4cfe-8b9a-c0c10ba5e558"), 4 },
                    { new Guid("f9fe42b8-d15f-40f9-ab49-3de84c64f2ae"), 4 },
                    { new Guid("1c9805f8-4eef-46b8-b3f2-681a686b912a"), 14 },
                    { new Guid("a3e37508-fb8a-4787-a4a6-8f910b3f0489"), 13 },
                    { new Guid("5d5df3b1-f0dd-4b66-8a19-c53f89313dd3"), 5 },
                    { new Guid("90aa96ad-5ad4-45c5-8047-caedbc9d1547"), 11 },
                    { new Guid("491ee503-62ca-42fb-bc45-9556edbb4c4b"), 13 },
                    { new Guid("7489545c-9f12-4872-912f-b9e7174ed68c"), 14 },
                    { new Guid("0f3cf491-a4dd-4558-a9d9-256bdc4168ab"), 6 },
                    { new Guid("90aa96ad-5ad4-45c5-8047-caedbc9d1547"), 6 },
                    { new Guid("239aa97e-7e2a-41e8-9f6c-00a8a33458d3"), 5 }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefenseTypingId", "OffenseTypingId", "Power" },
                values: new object[,]
                {
                    { 7, 14, 2, 2m },
                    { 25, 14, 4, 0.5m },
                    { 38, 14, 6, 0.5m },
                    { 67, 16, 10, 0m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefenseTypingId", "OffenseTypingId", "Power" },
                values: new object[,]
                {
                    { 11, 16, 3, 2m },
                    { 87, 3, 13, 0.5m },
                    { 52, 14, 8, 0.5m },
                    { 75, 14, 12, 2m },
                    { 97, 10, 15, 2m },
                    { 99, 1, 15, 0m },
                    { 89, 5, 13, 0.5m },
                    { 96, 15, 15, 2m },
                    { 90, 15, 13, 0.5m },
                    { 21, 15, 3, 0m },
                    { 3, 15, 1, 0m },
                    { 91, 7, 13, 0.5m },
                    { 93, 14, 14, 2m },
                    { 88, 2, 13, 0.5m },
                    { 49, 15, 7, 0.5m },
                    { 15, 17, 3, 2m },
                    { 98, 16, 15, 0.5m },
                    { 115, 2, 18, 0.5m },
                    { 114, 3, 18, 2m },
                    { 113, 14, 18, 2m },
                    { 112, 16, 18, 2m },
                    { 105, 18, 17, 2m },
                    { 103, 18, 16, 0.5m },
                    { 95, 18, 14, 0m },
                    { 86, 18, 13, 0.5m },
                    { 44, 18, 7, 2m },
                    { 17, 18, 3, 0.5m },
                    { 111, 4, 17, 0.5m },
                    { 110, 17, 17, 0.5m },
                    { 109, 2, 17, 0.5m },
                    { 108, 8, 17, 0.5m },
                    { 83, 16, 13, 2m },
                    { 107, 11, 17, 2m },
                    { 94, 17, 14, 0.5m },
                    { 92, 17, 13, 0.5m },
                    { 81, 17, 12, 0.5m },
                    { 74, 17, 11, 0.5m },
                    { 66, 17, 10, 0.5m },
                    { 60, 17, 9, 2m },
                    { 43, 17, 6, 0.5m },
                    { 33, 17, 5, 0.5m },
                    { 85, 10, 13, 2m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefenseTypingId", "OffenseTypingId", "Power" },
                values: new object[,]
                {
                    { 2, 17, 1, 0.5m },
                    { 104, 3, 16, 0.5m },
                    { 102, 16, 16, 0.5m },
                    { 101, 10, 16, 2m },
                    { 100, 15, 16, 2m },
                    { 106, 12, 17, 2m },
                    { 84, 6, 13, 2m },
                    { 76, 5, 12, 2m },
                    { 61, 13, 9, 0.5m },
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
                    { 45, 6, 7, 2m },
                    { 42, 7, 6, 0.5m },
                    { 57, 2, 9, 2m },
                    { 19, 7, 3, 0.5m },
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
                    { 10, 4, 2, 2m },
                    { 13, 1, 3, 2m },
                    { 8, 2, 2, 2m },
                    { 41, 6, 6, 0.5m },
                    { 68, 13, 11, 2m },
                    { 58, 7, 9, 2m },
                    { 63, 5, 9, 0m },
                    { 37, 13, 6, 0.5m }
                });

            migrationBuilder.InsertData(
                table: "TypeEffects",
                columns: new[] { "TypeEffectId", "DefenseTypingId", "OffenseTypingId", "Power" },
                values: new object[,]
                {
                    { 28, 13, 5, 2m },
                    { 16, 13, 3, 0m },
                    { 4, 13, 2, 2m },
                    { 82, 4, 12, 0.5m },
                    { 80, 12, 12, 0.5m },
                    { 79, 2, 12, 0.5m },
                    { 78, 9, 12, 2m },
                    { 77, 6, 12, 2m },
                    { 116, 7, 18, 0.5m },
                    { 71, 12, 11, 2m },
                    { 12, 12, 3, 2m },
                    { 6, 12, 2, 2m },
                    { 62, 6, 9, 0.5m },
                    { 73, 9, 11, 0.5m },
                    { 70, 5, 11, 2m },
                    { 69, 2, 11, 2m },
                    { 59, 11, 9, 2m },
                    { 48, 11, 7, 0.5m },
                    { 35, 11, 6, 2m },
                    { 32, 11, 5, 0.5m },
                    { 24, 11, 4, 2m },
                    { 14, 11, 3, 2m },
                    { 9, 11, 2, 2m },
                    { 1, 11, 1, 0.5m },
                    { 65, 10, 10, 0.5m },
                    { 64, 7, 10, 2m },
                    { 20, 10, 3, 0.5m },
                    { 72, 3, 11, 0.5m },
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
