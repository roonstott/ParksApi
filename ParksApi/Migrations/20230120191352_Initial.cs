using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParksApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parks",
                columns: table => new
                {
                    ParkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parks", x => x.ParkId);
                    table.ForeignKey(
                        name: "FK_Parks_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "Name" },
                values: new object[] { 1, "Oregon" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "Name" },
                values: new object[] { 2, "Washington" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "Name" },
                values: new object[] { 3, "California" });

            migrationBuilder.InsertData(
                table: "Parks",
                columns: new[] { "ParkId", "Description", "Name", "StateId" },
                values: new object[] { 1, "A high elevation lake in the crater of an exploded volcano", "Crater Lake", 1 });

            migrationBuilder.InsertData(
                table: "Parks",
                columns: new[] { "ParkId", "Description", "Name", "StateId" },
                values: new object[] { 2, "A forested valley high in the Sierra Nevada mountains, enclosed by towering granite cliffs with cascading waterfalls", "Yosemite", 3 });

            migrationBuilder.InsertData(
                table: "Parks",
                columns: new[] { "ParkId", "Description", "Name", "StateId" },
                values: new object[] { 3, "A 14,410 active volcano with extensive (but shrinking) glacier fields. Beloved for alpine meadows carpeted in summer wildflowers", "Mount Rainier", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Parks_StateId",
                table: "Parks",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parks");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
