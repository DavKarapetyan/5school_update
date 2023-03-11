using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5school.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addTeacherStringToSubStream : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Teacher",
                table: "SubStreams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teacher",
                table: "SubStreams");
        }
    }
}
