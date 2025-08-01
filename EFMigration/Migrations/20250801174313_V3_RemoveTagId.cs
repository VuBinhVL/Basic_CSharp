using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFMigration.Migrations
{
    /// <inheritdoc />
    public partial class V3_RemoveTagId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Tag");

            migrationBuilder.AddColumn<int>(
                name: "TagIdNew",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "TagIdNew");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "TagIdNew",
                table: "Tag");

            migrationBuilder.AddColumn<string>(
                name: "TagId",
                table: "Tag",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "TagId");
        }
    }
}
