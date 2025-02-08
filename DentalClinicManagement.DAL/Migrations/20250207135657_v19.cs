using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinicManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class v19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // حذف العمود القديم
            migrationBuilder.DropColumn(name: "DId", table: "Sessions");

            // إعادة إنشاء العمود مع IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "DId",
                table: "Sessions",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1"); // ✅ يجعل Id يبدأ من 1 ويتزايد تلقائيًا
           
            migrationBuilder.DropColumn(name: "RId", table: "Sessions");

            // إعادة إنشاء العمود مع IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "RId",
                table: "Sessions",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1"); // ✅ يجعل Id يبدأ من 1 ويتزايد تلقائيًا
                 
            migrationBuilder.DropColumn(name: "PId", table: "Sessions");

            // إعادة إنشاء العمود مع IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "PId",
                table: "Sessions",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1"); // ✅ يجعل Id يبدأ من 1 ويتزايد تلقائيًا
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Id", table: "Sessions");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Sessions",
                nullable: false);
        }

    }
}
