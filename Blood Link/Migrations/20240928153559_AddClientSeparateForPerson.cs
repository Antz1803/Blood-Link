using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blood_Link.Migrations
{
    public partial class AddClientSeparateForPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Persons_personId1",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_personId1",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "KapilaNaHospital",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "clientId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "personId1",
                table: "Persons");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    clientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KapilaNaHospital = table.Column<int>(type: "int", nullable: false),
                    personId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.clientId);
                    table.ForeignKey(
                        name: "FK_Clients_Persons_personId",
                        column: x => x.personId,
                        principalTable: "Persons",
                        principalColumn: "personId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_personId",
                table: "Clients",
                column: "personId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "KapilaNaHospital",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "clientId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "personId1",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_personId1",
                table: "Persons",
                column: "personId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Persons_personId1",
                table: "Persons",
                column: "personId1",
                principalTable: "Persons",
                principalColumn: "personId");
        }
    }
}
