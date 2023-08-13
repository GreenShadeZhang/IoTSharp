using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoTSharp.Data.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EqtId",
                table: "Device",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "EqtName",
                table: "Device",
                type: "TEXT",
                nullable: true,
                collation: "NOCASE");

            migrationBuilder.AddColumn<string>(
                name: "EqtTypeName",
                table: "Device",
                type: "TEXT",
                nullable: true,
                collation: "NOCASE");

            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "Device",
                type: "TEXT",
                nullable: true,
                collation: "NOCASE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EqtId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "EqtName",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "EqtTypeName",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "Device");
        }
    }
}
