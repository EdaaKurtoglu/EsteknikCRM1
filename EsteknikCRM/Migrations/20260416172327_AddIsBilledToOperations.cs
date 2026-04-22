using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsteknikCRM.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIsBilledToOperations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBilled",
                table: "WorkflowTeamOperations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBilled",
                table: "WorkflowTeamOperations");
        }
    }
}
