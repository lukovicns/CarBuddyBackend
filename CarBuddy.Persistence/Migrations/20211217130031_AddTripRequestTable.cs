using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarBuddy.Persistence.Migrations
{
    public partial class AddTripRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tripRequest",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    passengerId = table.Column<Guid>(type: "uuid", nullable: false),
                    tripId = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tripRequest", x => x.id);
                    table.ForeignKey(
                        name: "FK_tripRequest_trip_tripId",
                        column: x => x.tripId,
                        principalTable: "trip",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tripRequest_user_passengerId",
                        column: x => x.passengerId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tripRequest_passengerId",
                table: "tripRequest",
                column: "passengerId");

            migrationBuilder.CreateIndex(
                name: "IX_tripRequest_tripId",
                table: "tripRequest",
                column: "tripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tripRequest");
        }
    }
}
