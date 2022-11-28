using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace moviemanager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uloga",
                table: "Korisnici",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Prezime",
                table: "Korisnici",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Lozinka",
                table: "Korisnici",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "KorisnickoIme",
                table: "Korisnici",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Ime",
                table: "Korisnici",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Korisnici",
                newName: "Uloga");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Korisnici",
                newName: "Prezime");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Korisnici",
                newName: "Lozinka");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Korisnici",
                newName: "KorisnickoIme");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Korisnici",
                newName: "Ime");
        }
    }
}
