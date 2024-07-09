using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Registartion.Migrations
{
    /// <inheritdoc />
    public partial class initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    OrgId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.OrgId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationUser",
                columns: table => new
                {
                    OrganisationsOrgId = table.Column<string>(type: "text", nullable: false),
                    UsersUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationUser", x => new { x.OrganisationsOrgId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_OrganisationUser_Organisations_OrganisationsOrgId",
                        column: x => x.OrganisationsOrgId,
                        principalTable: "Organisations",
                        principalColumn: "OrgId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUser_UsersUserId",
                table: "OrganisationUser",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganisationUser");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
