using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5school.DAL.Migrations
{
    /// <inheritdoc />
    public partial class deleteClassesAndTeacherFromSubStream : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classes",
                table: "SubStreams");

            migrationBuilder.DropColumn(
                name: "Teacher",
                table: "SubStreams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Classes",
                table: "SubStreams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Teacher",
                table: "SubStreams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
