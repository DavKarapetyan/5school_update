using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5school.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeTeacherIdFromSubStream : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubStreams_Teachers_TeacherId",
                table: "SubStreams");

            migrationBuilder.DropIndex(
                name: "IX_SubStreams_TeacherId",
                table: "SubStreams");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SubStreams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "SubStreams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubStreams_TeacherId",
                table: "SubStreams",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubStreams_Teachers_TeacherId",
                table: "SubStreams",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
