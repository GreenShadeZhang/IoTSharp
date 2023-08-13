using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoTSharp.Migrations
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
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "EqtName",
                table: "Device",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EqtTypeName",
                table: "Device",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "Device",
                type: "text",
                nullable: true);
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
