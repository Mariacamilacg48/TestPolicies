using Microsoft.EntityFrameworkCore.Migrations;

namespace GAPTest.Web.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_CoveringType_CoveringTypeId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_RiskType_RiskTypeId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyCustomer_Customers_CustomerId",
                table: "PolicyCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyCustomer_Policies_PolicyId",
                table: "PolicyCustomer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskType",
                table: "RiskType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PolicyCustomer",
                table: "PolicyCustomer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoveringType",
                table: "CoveringType");

            migrationBuilder.RenameTable(
                name: "RiskType",
                newName: "RiskTypes");

            migrationBuilder.RenameTable(
                name: "PolicyCustomer",
                newName: "PolicyCustomers");

            migrationBuilder.RenameTable(
                name: "CoveringType",
                newName: "CoveringTypes");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyCustomer_PolicyId",
                table: "PolicyCustomers",
                newName: "IX_PolicyCustomers_PolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyCustomer_CustomerId",
                table: "PolicyCustomers",
                newName: "IX_PolicyCustomers_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskTypes",
                table: "RiskTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PolicyCustomers",
                table: "PolicyCustomers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoveringTypes",
                table: "CoveringTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_CoveringTypes_CoveringTypeId",
                table: "Policies",
                column: "CoveringTypeId",
                principalTable: "CoveringTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_RiskTypes_RiskTypeId",
                table: "Policies",
                column: "RiskTypeId",
                principalTable: "RiskTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyCustomers_Customers_CustomerId",
                table: "PolicyCustomers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyCustomers_Policies_PolicyId",
                table: "PolicyCustomers",
                column: "PolicyId",
                principalTable: "Policies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_CoveringTypes_CoveringTypeId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_RiskTypes_RiskTypeId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyCustomers_Customers_CustomerId",
                table: "PolicyCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyCustomers_Policies_PolicyId",
                table: "PolicyCustomers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskTypes",
                table: "RiskTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PolicyCustomers",
                table: "PolicyCustomers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoveringTypes",
                table: "CoveringTypes");

            migrationBuilder.RenameTable(
                name: "RiskTypes",
                newName: "RiskType");

            migrationBuilder.RenameTable(
                name: "PolicyCustomers",
                newName: "PolicyCustomer");

            migrationBuilder.RenameTable(
                name: "CoveringTypes",
                newName: "CoveringType");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyCustomers_PolicyId",
                table: "PolicyCustomer",
                newName: "IX_PolicyCustomer_PolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyCustomers_CustomerId",
                table: "PolicyCustomer",
                newName: "IX_PolicyCustomer_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskType",
                table: "RiskType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PolicyCustomer",
                table: "PolicyCustomer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoveringType",
                table: "CoveringType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_CoveringType_CoveringTypeId",
                table: "Policies",
                column: "CoveringTypeId",
                principalTable: "CoveringType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_RiskType_RiskTypeId",
                table: "Policies",
                column: "RiskTypeId",
                principalTable: "RiskType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyCustomer_Customers_CustomerId",
                table: "PolicyCustomer",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyCustomer_Policies_PolicyId",
                table: "PolicyCustomer",
                column: "PolicyId",
                principalTable: "Policies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
