using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeightLiftingPersistance.Migrations
{
    /// <inheritdoc />
    public partial class FirstMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Athlete",
                columns: table => new
                {
                    AthleteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athlete", x => x.AthleteID);
                });

            migrationBuilder.CreateTable(
                name: "Attempt",
                columns: table => new
                {
                    AttemptID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteID = table.Column<int>(type: "int", nullable: false),
                    Arranque = table.Column<int>(type: "int", nullable: false),
                    Envion = table.Column<int>(type: "int", nullable: false),
                    TotalPeso = table.Column<int>(type: "int", nullable: false),
                    AttemptNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempt", x => x.AttemptID);
                    table.ForeignKey(
                        name: "FK_Attempt_Athlete_AthleteID",
                        column: x => x.AthleteID,
                        principalTable: "Athlete",
                        principalColumn: "AthleteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attempt_AthleteID",
                table: "Attempt",
                column: "AthleteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attempt");

            migrationBuilder.DropTable(
                name: "Athlete");
        }
    }
}
