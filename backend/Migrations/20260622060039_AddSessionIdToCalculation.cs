using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CalculatorAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionIdToCalculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "operations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    symbol = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "calculations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    operand_a = table.Column<double>(type: "double precision", nullable: false),
                    @operator = table.Column<string>(name: "operator", type: "text", nullable: false),
                    operand_b = table.Column<double>(type: "double precision", nullable: false),
                    result = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    operation_id = table.Column<int>(type: "integer", nullable: false),
                    session_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calculations", x => x.id);
                    table.ForeignKey(
                        name: "FK_calculations_operations_operation_id",
                        column: x => x.operation_id,
                        principalTable: "operations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_calculations_operation_id",
                table: "calculations",
                column: "operation_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calculations");

            migrationBuilder.DropTable(
                name: "operations");
        }
    }
}
