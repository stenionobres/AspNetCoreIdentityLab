using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreIdentityLab.Persistence.Migrations.AspNetCoreIdentityLab
{
    public partial class AddOccupationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "AspNetUsers");
        }
    }
}
