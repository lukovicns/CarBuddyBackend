using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarBuddy.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    carId = table.Column<Guid>(type: "uuid", nullable: true),
                    firstName = table.Column<string>(type: "text", nullable: true),
                    lastName = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    age = table.Column<int>(type: "integer", nullable: false),
                    photo = table.Column<string>(type: "text", nullable: true),
                    isActivated = table.Column<bool>(type: "boolean", nullable: false),
                    activationToken = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    driverId = table.Column<Guid>(type: "uuid", nullable: false),
                    brand = table.Column<string>(type: "text", nullable: true),
                    model = table.Column<string>(type: "text", nullable: true),
                    photo = table.Column<string>(type: "text", nullable: true),
                    numberOfSeats = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car", x => x.id);
                    table.ForeignKey(
                        name: "FK_car_user_driverId",
                        column: x => x.driverId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "conversation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    firstParticipantId = table.Column<Guid>(type: "uuid", nullable: false),
                    secondParticipantId = table.Column<Guid>(type: "uuid", nullable: false),
                    firstParticipantReadStatus = table.Column<int>(type: "integer", nullable: false),
                    secondParticipantReadStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conversation", x => x.id);
                    table.ForeignKey(
                        name: "FK_conversation_user_firstParticipantId",
                        column: x => x.firstParticipantId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_conversation_user_secondParticipantId",
                        column: x => x.secondParticipantId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trip",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    driverId = table.Column<Guid>(type: "uuid", nullable: false),
                    fromCity = table.Column<string>(type: "text", nullable: false),
                    toCity = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    startTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    arriveTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    numberOfPassengers = table.Column<int>(type: "integer", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip", x => x.id);
                    table.ForeignKey(
                        name: "FK_trip_user_driverId",
                        column: x => x.driverId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    conversationId = table.Column<Guid>(type: "uuid", nullable: false),
                    authorId = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message", x => x.id);
                    table.ForeignKey(
                        name: "FK_message_conversation_conversationId",
                        column: x => x.conversationId,
                        principalTable: "conversation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_message_user_authorId",
                        column: x => x.authorId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rating",
                columns: table => new
                {
                    tripId = table.Column<Guid>(type: "uuid", nullable: false),
                    driverId = table.Column<Guid>(type: "uuid", nullable: false),
                    passengerId = table.Column<Guid>(type: "uuid", nullable: false),
                    evaluation = table.Column<double>(type: "double precision", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rating", x => new { x.tripId, x.driverId, x.passengerId });
                    table.ForeignKey(
                        name: "FK_rating_trip_tripId",
                        column: x => x.tripId,
                        principalTable: "trip",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rating_user_driverId",
                        column: x => x.driverId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rating_user_passengerId",
                        column: x => x.passengerId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userTrip",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    tripId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTrip", x => new { x.userId, x.tripId });
                    table.ForeignKey(
                        name: "FK_userTrip_trip_tripId",
                        column: x => x.tripId,
                        principalTable: "trip",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userTrip_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_car_driverId",
                table: "car",
                column: "driverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_conversation_firstParticipantId",
                table: "conversation",
                column: "firstParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_conversation_secondParticipantId",
                table: "conversation",
                column: "secondParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_message_authorId",
                table: "message",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_message_conversationId",
                table: "message",
                column: "conversationId");

            migrationBuilder.CreateIndex(
                name: "IX_rating_driverId",
                table: "rating",
                column: "driverId");

            migrationBuilder.CreateIndex(
                name: "IX_rating_passengerId",
                table: "rating",
                column: "passengerId");

            migrationBuilder.CreateIndex(
                name: "IX_trip_driverId",
                table: "trip",
                column: "driverId");

            migrationBuilder.CreateIndex(
                name: "IX_userTrip_tripId",
                table: "userTrip",
                column: "tripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "rating");

            migrationBuilder.DropTable(
                name: "userTrip");

            migrationBuilder.DropTable(
                name: "conversation");

            migrationBuilder.DropTable(
                name: "trip");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
