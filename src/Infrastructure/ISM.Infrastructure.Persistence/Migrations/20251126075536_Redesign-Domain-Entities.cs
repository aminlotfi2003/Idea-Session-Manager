using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISM.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RedesignDomainEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdeaEvaluations_Judges_JudgeId",
                schema: "app",
                table: "IdeaEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Judges_Users_UserId",
                schema: "app",
                table: "Judges");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantProfiles_Users_UserId",
                schema: "app",
                table: "ParticipantProfiles");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantProfiles_UserId",
                schema: "app",
                table: "ParticipantProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "app",
                table: "ParticipantProfiles");

            migrationBuilder.DropColumn(
                name: "AllowedParticipantGroups",
                schema: "app",
                table: "InnovationEvents");

            migrationBuilder.DropColumn(
                name: "JudgeIds",
                schema: "app",
                table: "InnovationEvents");

            migrationBuilder.DropColumn(
                name: "EncryptedParticipantReferenceId",
                schema: "app",
                table: "Ideas");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "app",
                table: "Judges",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Judges_UserId",
                schema: "app",
                table: "Judges",
                newName: "IX_Judges_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "StartDateForIdeaSubmission",
                schema: "app",
                table: "InnovationEvents",
                newName: "IdeaSubmissionStart");

            migrationBuilder.RenameColumn(
                name: "EndDateForIdeaSubmission",
                schema: "app",
                table: "InnovationEvents",
                newName: "IdeaSubmissionEnd");

            migrationBuilder.RenameColumn(
                name: "SubmissionDate",
                schema: "app",
                table: "Ideas",
                newName: "SubmittedAt");

            migrationBuilder.RenameColumn(
                name: "ProposedImplementationMethod",
                schema: "app",
                table: "Ideas",
                newName: "ProposedImplementation");

            migrationBuilder.RenameColumn(
                name: "OverallDecision",
                schema: "app",
                table: "Ideas",
                newName: "FinalDecision");

            migrationBuilder.RenameColumn(
                name: "OverallDecision",
                schema: "app",
                table: "IdeaEvaluations",
                newName: "Decision");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                schema: "app",
                table: "ParticipantProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "app",
                table: "ParticipantProfiles",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "app",
                table: "ParticipantProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RegistrationDate",
                schema: "app",
                table: "ParticipantProfiles",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "AllowedParticipantGroup",
                schema: "app",
                table: "InnovationEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "app",
                table: "InnovationEvents",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "app",
                table: "InnovationEvents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                schema: "app",
                table: "InnovationEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                schema: "app",
                table: "InnovationEvents",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                schema: "app",
                table: "InnovationEvents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WeightedScore",
                schema: "app",
                table: "IdeaEvaluations",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventJudges",
                schema: "app",
                columns: table => new
                {
                    InnovationEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JudgeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventJudges", x => new { x.InnovationEventId, x.JudgeId });
                    table.ForeignKey(
                        name: "FK_EventJudges_InnovationEvents_InnovationEventId",
                        column: x => x.InnovationEventId,
                        principalSchema: "app",
                        principalTable: "InnovationEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventJudges_Judges_JudgeId",
                        column: x => x.JudgeId,
                        principalSchema: "app",
                        principalTable: "Judges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdeaParticipantLinks",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdeaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EncryptedParticipantPayload = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RevealedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaParticipantLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeaParticipantLinks_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalSchema: "app",
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdeaParticipantLinks_ParticipantProfiles_ParticipantProfileId",
                        column: x => x.ParticipantProfileId,
                        principalSchema: "app",
                        principalTable: "ParticipantProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantProfiles_ApplicationUserId",
                schema: "app",
                table: "ParticipantProfiles",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EventJudges_JudgeId",
                schema: "app",
                table: "EventJudges",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaParticipantLinks_IdeaId",
                schema: "app",
                table: "IdeaParticipantLinks",
                column: "IdeaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdeaParticipantLinks_ParticipantProfileId",
                schema: "app",
                table: "IdeaParticipantLinks",
                column: "ParticipantProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaEvaluations_Judges_JudgeId",
                schema: "app",
                table: "IdeaEvaluations",
                column: "JudgeId",
                principalSchema: "app",
                principalTable: "Judges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Judges_Users_ApplicationUserId",
                schema: "app",
                table: "Judges",
                column: "ApplicationUserId",
                principalSchema: "identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantProfiles_Users_ApplicationUserId",
                schema: "app",
                table: "ParticipantProfiles",
                column: "ApplicationUserId",
                principalSchema: "identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdeaEvaluations_Judges_JudgeId",
                schema: "app",
                table: "IdeaEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Judges_Users_ApplicationUserId",
                schema: "app",
                table: "Judges");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantProfiles_Users_ApplicationUserId",
                schema: "app",
                table: "ParticipantProfiles");

            migrationBuilder.DropTable(
                name: "EventJudges",
                schema: "app");

            migrationBuilder.DropTable(
                name: "IdeaParticipantLinks",
                schema: "app");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantProfiles_ApplicationUserId",
                schema: "app",
                table: "ParticipantProfiles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "app",
                table: "ParticipantProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "app",
                table: "ParticipantProfiles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "app",
                table: "ParticipantProfiles");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                schema: "app",
                table: "ParticipantProfiles");

            migrationBuilder.DropColumn(
                name: "AllowedParticipantGroup",
                schema: "app",
                table: "InnovationEvents");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "app",
                table: "InnovationEvents");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "app",
                table: "InnovationEvents");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                schema: "app",
                table: "InnovationEvents");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "app",
                table: "InnovationEvents");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "app",
                table: "InnovationEvents");

            migrationBuilder.DropColumn(
                name: "WeightedScore",
                schema: "app",
                table: "IdeaEvaluations");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                schema: "app",
                table: "Judges",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Judges_ApplicationUserId",
                schema: "app",
                table: "Judges",
                newName: "IX_Judges_UserId");

            migrationBuilder.RenameColumn(
                name: "IdeaSubmissionStart",
                schema: "app",
                table: "InnovationEvents",
                newName: "StartDateForIdeaSubmission");

            migrationBuilder.RenameColumn(
                name: "IdeaSubmissionEnd",
                schema: "app",
                table: "InnovationEvents",
                newName: "EndDateForIdeaSubmission");

            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                schema: "app",
                table: "Ideas",
                newName: "SubmissionDate");

            migrationBuilder.RenameColumn(
                name: "ProposedImplementation",
                schema: "app",
                table: "Ideas",
                newName: "ProposedImplementationMethod");

            migrationBuilder.RenameColumn(
                name: "FinalDecision",
                schema: "app",
                table: "Ideas",
                newName: "OverallDecision");

            migrationBuilder.RenameColumn(
                name: "Decision",
                schema: "app",
                table: "IdeaEvaluations",
                newName: "OverallDecision");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "app",
                table: "ParticipantProfiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AllowedParticipantGroups",
                schema: "app",
                table: "InnovationEvents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JudgeIds",
                schema: "app",
                table: "InnovationEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "EncryptedParticipantReferenceId",
                schema: "app",
                table: "Ideas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantProfiles_UserId",
                schema: "app",
                table: "ParticipantProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaEvaluations_Judges_JudgeId",
                schema: "app",
                table: "IdeaEvaluations",
                column: "JudgeId",
                principalSchema: "app",
                principalTable: "Judges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Judges_Users_UserId",
                schema: "app",
                table: "Judges",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantProfiles_Users_UserId",
                schema: "app",
                table: "ParticipantProfiles",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
