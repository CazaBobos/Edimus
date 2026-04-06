using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    EntityType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Operation = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogChange",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuditLogId = table.Column<long>(type: "bigint", nullable: false),
                    PropertyName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    OldValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    NewValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogChange_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogChange_AuditLogId",
                table: "AuditLogChange",
                column: "AuditLogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogChange");

            migrationBuilder.DropTable(
                name: "AuditLogs");
        }
    }
}
