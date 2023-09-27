using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberPlace.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class NumberMat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumberMats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OriginMatrixJson = table.Column<string>(type: "TEXT", nullable: true),
                    CompleteMatrixJson = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberMats", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberMats");
        }
    }
}
