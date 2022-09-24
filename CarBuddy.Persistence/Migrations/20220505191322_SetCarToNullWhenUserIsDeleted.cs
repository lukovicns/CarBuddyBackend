using Microsoft.EntityFrameworkCore.Migrations;

namespace CarBuddy.Persistence.Migrations
{
    public partial class SetCarToNullWhenUserIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_car_user_driverId",
                table: "car");

            migrationBuilder.AddForeignKey(
                name: "FK_car_user_driverId",
                table: "car",
                column: "driverId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_car_user_driverId",
                table: "car");

            migrationBuilder.AddForeignKey(
                name: "FK_car_user_driverId",
                table: "car",
                column: "driverId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
