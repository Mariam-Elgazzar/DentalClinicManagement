using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinicManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientManagements",
                columns: table => new
                {
                    PId = table.Column<int>(type: "int", nullable: false),
                    RId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientManagements", x => new { x.PId, x.RId });
                    table.ForeignKey(
                        name: "FK_PatientManagements_Patients_PId",
                        column: x => x.PId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientManagements_Receptionists_RId",
                        column: x => x.RId,
                        principalTable: "Receptionists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientManagements_RId",
                table: "PatientManagements",
                column: "RId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientManagements");
        }
    }
}
