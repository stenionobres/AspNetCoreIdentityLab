using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreIdentityLab.Persistence.Migrations.AspNetCoreIdentityLab
{
    public partial class CreateUserLoginIp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLoginIp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    When = table.Column<DateTime>(nullable: false),
                    IP = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginIp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLoginIp_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginIp_UserId",
                table: "UserLoginIp",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLoginIp");
        }
    }
}
