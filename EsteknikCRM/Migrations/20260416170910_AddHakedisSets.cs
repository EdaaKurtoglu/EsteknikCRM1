using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsteknikCRM.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddHakedisSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HakedisSets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ServiceTitle = table.Column<string>(type: "text", nullable: false),
                    SapServiceCode = table.Column<string>(type: "text", nullable: false),
                    ServiceResponsible = table.Column<string>(type: "text", nullable: false),
                    GreenCount = table.Column<int>(type: "integer", nullable: false),
                    BlueCount = table.Column<int>(type: "integer", nullable: false),
                    RedCount = table.Column<int>(type: "integer", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false),
                    InvoiceDate = table.Column<string>(type: "text", nullable: false),
                    PreApprovalDate = table.Column<string>(type: "text", nullable: false),
                    PreApprovalApproveDate = table.Column<string>(type: "text", nullable: false),
                    SetDate = table.Column<string>(type: "text", nullable: false),
                    SetApproveDate = table.Column<string>(type: "text", nullable: false),
                    ExportDate = table.Column<string>(type: "text", nullable: false),
                    PayType = table.Column<string>(type: "text", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalQuantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HakedisSets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HakedisSets");
        }
    }
}
