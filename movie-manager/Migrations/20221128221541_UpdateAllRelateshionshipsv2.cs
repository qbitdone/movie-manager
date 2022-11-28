using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace moviemanager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllRelateshionshipsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Directors_DirectorId1",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectorId1",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DirectorId1",
                table: "Movies");

            migrationBuilder.AlterColumn<Guid>(
                name: "DirectorId",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                table: "Movies",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "DirectorId",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "DirectorId1",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId1",
                table: "Movies",
                column: "DirectorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Directors_DirectorId1",
                table: "Movies",
                column: "DirectorId1",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
