using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoTSharp.Data.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddStreet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Street",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProvinceCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ProvinceName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    CityCode = table.Column<int>(type: "INTEGER", nullable: false),
                    CityName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    DistrictCode = table.Column<int>(type: "INTEGER", nullable: false),
                    DistrictName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    NeighName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    AddressDetail = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Manager = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    ManagerPhone = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    ManagerEmail = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    PeopleNum = table.Column<int>(type: "INTEGER", nullable: false),
                    OlderNum = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    CreateUserId = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Street", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Street_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Street_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Street_CustomerId",
                table: "Street",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Street_TenantId",
                table: "Street",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Street");
        }
    }
}
