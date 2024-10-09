using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blood_Link.Migrations
{
    public partial class PersonUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Persons",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "BirthDate",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WalletAddress",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_personId",
                table: "Nurses",
                column: "personId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_personId",
                table: "Doctors",
                column: "personId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Persons_personId",
                table: "Doctors",
                column: "personId",
                principalTable: "Persons",
                principalColumn: "personId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Persons_personId",
                table: "Nurses",
                column: "personId",
                principalTable: "Persons",
                principalColumn: "personId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Persons_personId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Persons_personId",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Nurses_personId",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_personId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ContactNo",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "WalletAddress",
                table: "Persons",
                newName: "Name");
        }
    }
}
