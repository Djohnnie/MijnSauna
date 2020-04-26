using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MijnSauna.Backend.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CONFIGURATION",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SysId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONFIGURATION", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "SESSIONS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SysId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    ActualEnd = table.Column<DateTime>(nullable: true),
                    IsSauna = table.Column<bool>(nullable: false),
                    IsInfrared = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCancelled = table.Column<bool>(nullable: false),
                    TemperatureGoal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SESSIONS", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "SAMPLES",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SysId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Temperature = table.Column<decimal>(nullable: false),
                    IsSaunaPowered = table.Column<bool>(nullable: false),
                    IsInfraredPowered = table.Column<bool>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAMPLES", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_SAMPLES_SESSIONS_SessionId",
                        column: x => x.SessionId,
                        principalTable: "SESSIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONFIGURATION_Name",
                table: "CONFIGURATION",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CONFIGURATION_SysId",
                table: "CONFIGURATION",
                column: "SysId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_SAMPLES_SessionId",
                table: "SAMPLES",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SAMPLES_SysId",
                table: "SAMPLES",
                column: "SysId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_SESSIONS_SysId",
                table: "SESSIONS",
                column: "SysId")
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONFIGURATION");

            migrationBuilder.DropTable(
                name: "SAMPLES");

            migrationBuilder.DropTable(
                name: "SESSIONS");
        }
    }
}