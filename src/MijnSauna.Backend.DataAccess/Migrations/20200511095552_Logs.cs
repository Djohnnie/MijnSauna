using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MijnSauna.Backend.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Logs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOGS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SysId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Component = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    IsError = table.Column<bool>(nullable: false),
                    ExceptionMessage = table.Column<string>(nullable: true),
                    ExceptionType = table.Column<string>(nullable: true),
                    ExceptionStackTrace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGS", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LOGS_SysId",
                table: "LOGS",
                column: "SysId")
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOGS");
        }
    }
}
