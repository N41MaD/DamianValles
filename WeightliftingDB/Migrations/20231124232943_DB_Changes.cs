using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeightLiftingPersistance.Migrations
{
    /// <inheritdoc />
    public partial class DB_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arranque",
                table: "Attempt");

            migrationBuilder.DropColumn(
                name: "Envion",
                table: "Attempt");

            migrationBuilder.RenameColumn(
                name: "TotalPeso",
                table: "Attempt",
                newName: "Value");

            migrationBuilder.CreateTable(
                name: "StartupAttempts",
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
                    table.PrimaryKey("PK_StartupAttempts", x => x.AttemptID);
                    table.ForeignKey(
                        name: "FK_StartupAttempts_Athlete_AthleteID",
                        column: x => x.AthleteID,
                        principalTable: "Athlete",
                        principalColumn: "AthleteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StartupAttempts_AthleteID",
                table: "StartupAttempts",
                column: "AthleteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StartupAttempts");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Attempt",
                newName: "TotalPeso");

            migrationBuilder.AddColumn<int>(
                name: "Arranque",
                table: "Attempt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Envion",
                table: "Attempt",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
