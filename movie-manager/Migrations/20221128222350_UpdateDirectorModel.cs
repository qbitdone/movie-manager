using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace moviemanager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDirectorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Directors_DirectorId",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Directors_DirectorId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "Directors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DirectorId",
                table: "Directors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Directors_DirectorId",
                table: "Directors",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Directors_DirectorId",
                table: "Directors",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id");
        }
    }
}
