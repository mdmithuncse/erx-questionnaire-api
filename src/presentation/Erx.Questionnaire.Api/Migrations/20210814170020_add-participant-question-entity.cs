using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Erx.Questionnaire.Api.Migrations
{
    public partial class addparticipantquestionentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParticipantQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    ParticipantId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantQuestions_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantQuestions_ParticipantId",
                table: "ParticipantQuestions",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantQuestions_QuestionId",
                table: "ParticipantQuestions",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantQuestions");
        }
    }
}
