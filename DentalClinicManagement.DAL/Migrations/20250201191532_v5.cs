using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinicManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DentistManagements",
                columns: table => new
                {
                    DId = table.Column<int>(type: "int", nullable: false),
                    RId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentistManagements", x => new { x.DId, x.RId });
                    table.ForeignKey(
                        name: "FK_DentistManagements_Dentists_DId",
                        column: x => x.DId,
                        principalTable: "Dentists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DentistManagements_Receptionists_RId",
                        column: x => x.RId,
                        principalTable: "Receptionists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DentistManagements_RId",
                table: "DentistManagements",
                column: "RId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DentistManagements");
        }
    }
}
