using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarBuddy.Persistence.Migrations
{
    public partial class ChangeTripRequestColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tripRequest",
                table: "tripRequest");

            migrationBuilder.DropIndex(
                name: "IX_tripRequest_tripId",
                table: "tripRequest");

            migrationBuilder.DropColumn(
                name: "id",
                table: "tripRequest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tripRequest",
                table: "tripRequest",
                columns: new[] { "tripId", "passengerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tripRequest",
                table: "tripRequest");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "tripRequest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_tripRequest",
                table: "tripRequest",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_tripRequest_tripId",
                table: "tripRequest",
                column: "tripId");
        }
    }
}
