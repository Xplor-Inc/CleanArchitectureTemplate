using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitectureTemplate.SqlServer.Migrations;

/// <inheritdoc />
public partial class InitialDatabaseSetup : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                AccountActivateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                ActivationEmailSent = table.Column<bool>(type: "bit", nullable: false),
                EmailAddress = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                Gender = table.Column<int>(type: "int", nullable: false),
                ImagePath = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                IsAccountActivated = table.Column<bool>(type: "bit", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                LastLoginDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LastLogoutDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LastName = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                PasswordChangeDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                PasswordHash = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                PasswordSalt = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                Role = table.Column<int>(type: "int", nullable: false),
                SecurityStamp = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                CreatedById = table.Column<long>(type: "bigint", nullable: false),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DeletedById = table.Column<long>(type: "bigint", nullable: true),
                DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_Users_Users_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Users_Users_DeletedById",
                    column: x => x.DeletedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Users_Users_UpdatedById",
                    column: x => x.UpdatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "AccountRecoveries",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                EmailSent = table.Column<bool>(type: "bit", nullable: false),
                PasswordResetAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                PasswordResetSuccessfully = table.Column<bool>(type: "bit", nullable: false),
                ResetLink = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                ResetLinkExpired = table.Column<bool>(type: "bit", nullable: false),
                ResetLinkSentAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                RetryCount = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<long>(type: "bigint", nullable: false),
                CreatedById = table.Column<long>(type: "bigint", nullable: false),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DeletedById = table.Column<long>(type: "bigint", nullable: true),
                DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AccountRecoveries", x => x.Id);
                table.ForeignKey(
                    name: "FK_AccountRecoveries_Users_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_AccountRecoveries_Users_DeletedById",
                    column: x => x.DeletedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_AccountRecoveries_Users_UpdatedById",
                    column: x => x.UpdatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_AccountRecoveries_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "ChangeLogs",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                EntityName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                NewValue = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                OldValue = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                PrimaryKey = table.Column<long>(type: "bigint", nullable: false),
                PropertyName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                CreatedById = table.Column<long>(type: "bigint", nullable: false),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ChangeLogs", x => x.Id);
                table.ForeignKey(
                    name: "FK_ChangeLogs_Users_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Counters",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Browser = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                CreatedById = table.Column<long>(type: "bigint", nullable: true),
                Device = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                IPAddress = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                LastVisit = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                OperatingSystem = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                Page = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                Search = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                ServerName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                VisitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                EnquirySent = table.Column<int>(type: "int", nullable: false),
                Tracking = table.Column<int>(type: "int", nullable: false),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Counters", x => x.Id);
                table.ForeignKey(
                    name: "FK_Counters_Users_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Enquiries",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CreatedById = table.Column<long>(type: "bigint", nullable: true),
                EnquiryType = table.Column<int>(type: "int", nullable: false),
                Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                IsResolved = table.Column<bool>(type: "bit", nullable: false),
                Message = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                Name = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                Resolution = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                VisitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DeletedById = table.Column<long>(type: "bigint", nullable: true),
                DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Enquiries", x => x.Id);
                table.ForeignKey(
                    name: "FK_Enquiries_Users_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Enquiries_Users_DeletedById",
                    column: x => x.DeletedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Enquiries_Users_UpdatedById",
                    column: x => x.UpdatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Notifications",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Message = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                ExpireAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                StartAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                IsPermanent = table.Column<bool>(type: "bit", nullable: false),
                CreatedById = table.Column<long>(type: "bigint", nullable: false),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DeletedById = table.Column<long>(type: "bigint", nullable: true),
                DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Notifications", x => x.Id);
                table.ForeignKey(
                    name: "FK_Notifications_Users_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Notifications_Users_DeletedById",
                    column: x => x.DeletedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Notifications_Users_UpdatedById",
                    column: x => x.UpdatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "RequestLogs",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CreatedById = table.Column<long>(type: "bigint", nullable: true),
                Page = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                Search = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                VisitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RequestLogs", x => x.Id);
                table.ForeignKey(
                    name: "FK_RequestLogs_Users_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "UserLogins",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CreatedById = table.Column<long>(type: "bigint", nullable: true),
                Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                IsLoginSuccess = table.Column<bool>(type: "bit", nullable: false),
                IsValidUser = table.Column<bool>(type: "bit", nullable: false),
                UserId = table.Column<long>(type: "bigint", nullable: true),
                VisitorId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserLogins", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserLogins_Users_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_UserLogins_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AccountRecoveries_CreatedById",
            table: "AccountRecoveries",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_AccountRecoveries_DeletedById",
            table: "AccountRecoveries",
            column: "DeletedById");

        migrationBuilder.CreateIndex(
            name: "IX_AccountRecoveries_UpdatedById",
            table: "AccountRecoveries",
            column: "UpdatedById");

        migrationBuilder.CreateIndex(
            name: "IX_AccountRecoveries_UserId",
            table: "AccountRecoveries",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ChangeLogs_CreatedById",
            table: "ChangeLogs",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_Counters_CreatedById",
            table: "Counters",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_Enquiries_CreatedById",
            table: "Enquiries",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_Enquiries_DeletedById",
            table: "Enquiries",
            column: "DeletedById");

        migrationBuilder.CreateIndex(
            name: "IX_Enquiries_UpdatedById",
            table: "Enquiries",
            column: "UpdatedById");

        migrationBuilder.CreateIndex(
            name: "IX_Notifications_CreatedById",
            table: "Notifications",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_Notifications_DeletedById",
            table: "Notifications",
            column: "DeletedById");

        migrationBuilder.CreateIndex(
            name: "IX_Notifications_UpdatedById",
            table: "Notifications",
            column: "UpdatedById");

        migrationBuilder.CreateIndex(
            name: "IX_RequestLogs_CreatedById",
            table: "RequestLogs",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_UserLogins_CreatedById",
            table: "UserLogins",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_UserLogins_UserId",
            table: "UserLogins",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_CreatedById",
            table: "Users",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_Users_DeletedById",
            table: "Users",
            column: "DeletedById");

        migrationBuilder.CreateIndex(
            name: "IX_Users_EmailAddress_DeletedOn",
            table: "Users",
            columns: new[] { "EmailAddress", "DeletedOn" },
            unique: true,
            filter: "[DeletedOn] IS NULL");

        migrationBuilder.CreateIndex(
            name: "IX_Users_UpdatedById",
            table: "Users",
            column: "UpdatedById");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AccountRecoveries");

        migrationBuilder.DropTable(
            name: "ChangeLogs");

        migrationBuilder.DropTable(
            name: "Counters");

        migrationBuilder.DropTable(
            name: "Enquiries");

        migrationBuilder.DropTable(
            name: "Notifications");

        migrationBuilder.DropTable(
            name: "RequestLogs");

        migrationBuilder.DropTable(
            name: "UserLogins");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
