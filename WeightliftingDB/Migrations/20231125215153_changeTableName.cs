using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeightLiftingPersistance.Migrations
{
    /// <inheritdoc />
    public partial class changeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StartupAttempt");

            migrationBuilder.CreateTable(
                name: "StartAttempt",
                columns: table => new
                {
                    AttemptID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteID = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    AttemptNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartAttempt", x => x.AttemptID);
                    table.ForeignKey(
                        name: "FK_StartAttempt_Athlete_AthleteID",
                        column: x => x.AthleteID,
                        principalTable: "Athlete",
                        principalColumn: "AthleteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StartAttempt_AthleteID",
                table: "StartAttempt",
                column: "AthleteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StartAttempt");

            migrationBuilder.CreateTable(
                name: "StartupAttempt",
                columns: table => new
                {
                    AttemptID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteID = table.Column<int>(type: "int", nullable: false),
                    AttemptNumber = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartupAttempt", x => x.AttemptID);
                    table.ForeignKey(
                        name: "FK_StartupAttempt_Athlete_AthleteID",
                        column: x => x.AthleteID,
                        principalTable: "Athlete",
                        principalColumn: "AthleteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StartupAttempt_AthleteID",
                table: "StartupAttempt",
                column: "AthleteID");
        }
    }
}
