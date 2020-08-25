using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GAPTest.Web.Migrations
{
    public partial class FixingDBModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicyCustomers");

            migrationBuilder.AddColumn<float>(
                name: "CoveringPercentage",
                table: "Policies",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "Policies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoveringPercentage",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Policies");

            migrationBuilder.CreateTable(
                name: "PolicyCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoveringPercentage = table.Column<float>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true),
                    PolicyId = table.Column<int>(nullable: true),
                    State = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyCustomers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyCustomers_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolicyCustomers_CustomerId",
                table: "PolicyCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyCustomers_PolicyId",
                table: "PolicyCustomers",
                column: "PolicyId");
        }
    }
}
