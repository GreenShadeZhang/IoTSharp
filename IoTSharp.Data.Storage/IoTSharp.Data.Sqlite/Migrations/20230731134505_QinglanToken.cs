using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoTSharp.Data.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class QinglanToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QinglanTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccessToken = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Scope = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    TokenType = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpiresIn = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QinglanTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QinglanTokens_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QinglanTokens_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QinglanTokens_CustomerId",
                table: "QinglanTokens",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_QinglanTokens_TenantId",
                table: "QinglanTokens",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QinglanTokens");
        }
    }
}
