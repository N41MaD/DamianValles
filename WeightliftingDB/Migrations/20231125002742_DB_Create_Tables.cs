using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeightLiftingPersistance.Migrations
{
    /// <inheritdoc />
    public partial class DB_Create_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attempt_Athlete_AthleteID",
                table: "Attempt");

            migrationBuilder.DropForeignKey(
                name: "FK_StartupAttempts_Athlete_AthleteID",
                table: "StartupAttempts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StartupAttempts",
                table: "StartupAttempts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attempt",
                table: "Attempt");

            migrationBuilder.RenameTable(
                name: "StartupAttempts",
                newName: "StartupAttempt");

            migrationBuilder.RenameTable(
                name: "Attempt",
                newName: "PushAttempt");

            migrationBuilder.RenameIndex(
                name: "IX_StartupAttempts_AthleteID",
                table: "StartupAttempt",
                newName: "IX_StartupAttempt_AthleteID");

            migrationBuilder.RenameIndex(
                name: "IX_Attempt_AthleteID",
                table: "PushAttempt",
                newName: "IX_PushAttempt_AthleteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StartupAttempt",
                table: "StartupAttempt",
                column: "AttemptID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PushAttempt",
                table: "PushAttempt",
                column: "AttemptID");

            migrationBuilder.AddForeignKey(
                name: "FK_PushAttempt_Athlete_AthleteID",
                table: "PushAttempt",
                column: "AthleteID",
                principalTable: "Athlete",
                principalColumn: "AthleteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StartupAttempt_Athlete_AthleteID",
                table: "StartupAttempt",
                column: "AthleteID",
                principalTable: "Athlete",
                principalColumn: "AthleteID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushAttempt_Athlete_AthleteID",
                table: "PushAttempt");

            migrationBuilder.DropForeignKey(
                name: "FK_StartupAttempt_Athlete_AthleteID",
                table: "StartupAttempt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StartupAttempt",
                table: "StartupAttempt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PushAttempt",
                table: "PushAttempt");

            migrationBuilder.RenameTable(
                name: "StartupAttempt",
                newName: "StartupAttempts");

            migrationBuilder.RenameTable(
                name: "PushAttempt",
                newName: "Attempt");

            migrationBuilder.RenameIndex(
                name: "IX_StartupAttempt_AthleteID",
                table: "StartupAttempts",
                newName: "IX_StartupAttempts_AthleteID");

            migrationBuilder.RenameIndex(
                name: "IX_PushAttempt_AthleteID",
                table: "Attempt",
                newName: "IX_Attempt_AthleteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StartupAttempts",
                table: "StartupAttempts",
                column: "AttemptID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attempt",
                table: "Attempt",
                column: "AttemptID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attempt_Athlete_AthleteID",
                table: "Attempt",
                column: "AthleteID",
                principalTable: "Athlete",
                principalColumn: "AthleteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StartupAttempts_Athlete_AthleteID",
                table: "StartupAttempts",
                column: "AthleteID",
                principalTable: "Athlete",
                principalColumn: "AthleteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
