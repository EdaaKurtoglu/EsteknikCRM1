using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsteknikCRM.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ProductCode = table.Column<string>(type: "text", nullable: true),
                    ProductNameTr = table.Column<string>(type: "text", nullable: true),
                    ProductNameEn = table.Column<string>(type: "text", nullable: true),
                    CostCenter = table.Column<string>(type: "text", nullable: true),
                    BCode = table.Column<string>(type: "text", nullable: true),
                    ProductManager = table.Column<string>(type: "text", nullable: true),
                    WarrantyMonth1 = table.Column<int>(type: "integer", nullable: false),
                    WarrantyMonth2 = table.Column<int>(type: "integer", nullable: false),
                    Ewl = table.Column<bool>(type: "boolean", nullable: false),
                    EwlStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EwlEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    TopGroup = table.Column<string>(type: "text", nullable: true),
                    SubGroup = table.Column<string>(type: "text", nullable: true),
                    SpecialGroup = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    CascadeSystem = table.Column<bool>(type: "boolean", nullable: false),
                    PhaseOut = table.Column<bool>(type: "boolean", nullable: false),
                    PhaseOutDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SapPhaseOut = table.Column<bool>(type: "boolean", nullable: false),
                    SapPhaseOutDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NoSerialNumber = table.Column<bool>(type: "boolean", nullable: false),
                    ProductionPlace = table.Column<string>(type: "text", nullable: true),
                    SalesInfo = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
