using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blood_Link.Migrations
{
    public partial class AddAppointmentRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentRequests",
                columns: table => new
                {
                    AppointmentRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AppointmentSetupId = table.Column<int>(type: "int", nullable: false),
                    isAppointed = table.Column<bool>(type: "bit", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentRequests", x => x.AppointmentRequestId);
                    table.ForeignKey(
                        name: "FK_AppointmentRequests_AppointmentSetups_AppointmentSetupId",
                        column: x => x.AppointmentSetupId,
                        principalTable: "AppointmentSetups",
                        principalColumn: "AppointmentSetupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentRequests_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "clientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentRequests_AppointmentSetupId",
                table: "AppointmentRequests",
                column: "AppointmentSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentRequests_ClientId",
                table: "AppointmentRequests",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentRequests");
        }
    }
}
