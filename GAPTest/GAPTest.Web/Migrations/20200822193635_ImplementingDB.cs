using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GAPTest.Web.Migrations
{
    public partial class ImplementingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoveringType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoveringType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PolicyName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    PolicyStartDate = table.Column<DateTime>(nullable: false),
                    CoveringPeriod = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    CoveringTypeId = table.Column<int>(nullable: true),
                    RiskTypeId = table.Column<int>(nullable: true),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policies_CoveringType_CoveringTypeId",
                        column: x => x.CoveringTypeId,
                        principalTable: "CoveringType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policies_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policies_RiskType_RiskTypeId",
                        column: x => x.RiskTypeId,
                        principalTable: "RiskType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyCustomer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoveringPercentage = table.Column<float>(nullable: false),
                    State = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true),
                    PolicyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyCustomer_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyCustomer_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CoveringTypeId",
                table: "Policies",
                column: "CoveringTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CustomerId",
                table: "Policies",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_RiskTypeId",
                table: "Policies",
                column: "RiskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyCustomer_CustomerId",
                table: "PolicyCustomer",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyCustomer_PolicyId",
                table: "PolicyCustomer",
                column: "PolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicyCustomer");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "CoveringType");

            migrationBuilder.DropTable(
                name: "RiskType");
        }
    }
}
