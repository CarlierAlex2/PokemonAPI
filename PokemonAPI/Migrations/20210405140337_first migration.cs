using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonAPI.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonTypes",
                columns: table => new
                {
                    PokemonTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes", x => x.PokemonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TypeEffectiveness",
                columns: table => new
                {
                    TypeEffectivenessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    TargetTypeId = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false),
                    PokemonTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeEffectiveness", x => x.TypeEffectivenessId);
                    table.ForeignKey(
                        name: "FK_TypeEffectiveness_PokemonTypes_PokemonTypeId",
                        column: x => x.PokemonTypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "PokemonTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PokemonTypes",
                columns: new[] { "PokemonTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Normal" },
                    { 16, "Dark" },
                    { 15, "Ghost" },
                    { 14, "Dragon" },
                    { 13, "Bug" },
                    { 12, "Ice" },
                    { 11, "Rock" },
                    { 10, "Psychic" },
                    { 9, "Ground" },
                    { 8, "Electric" },
                    { 7, "Poison" },
                    { 6, "Grass" },
                    { 5, "Flying" },
                    { 4, "Water" },
                    { 3, "Fighting" },
                    { 2, "Fire" },
                    { 17, "Steel" },
                    { 18, "Fairy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeEffectiveness_PokemonTypeId",
                table: "TypeEffectiveness",
                column: "PokemonTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeEffectiveness");

            migrationBuilder.DropTable(
                name: "PokemonTypes");
        }
    }
}
