using Microsoft.EntityFrameworkCore.Migrations;

namespace CarBuddy.Persistence.Migrations
{
    public partial class ChangeTripRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfSeats",
                table: "tripRequest",
                newName: "numberOfPassengers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numberOfPassengers",
                table: "tripRequest",
                newName: "numberOfSeats");
        }
    }
}
