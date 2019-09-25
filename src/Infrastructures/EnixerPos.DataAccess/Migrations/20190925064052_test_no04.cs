using Microsoft.EntityFrameworkCore.Migrations;

namespace EnixerPos.DataAccess.Migrations
{
    public partial class test_no04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "Store",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OTP",
                table: "Store");
        }
    }
}
