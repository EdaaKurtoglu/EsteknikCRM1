using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsteknikCRM.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerOrCenterPay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerOrCenterPay",
                table: "WorkflowTeamOperations",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerOrCenterPay",
                table: "WorkflowTeamOperations");
        }
    }
}
