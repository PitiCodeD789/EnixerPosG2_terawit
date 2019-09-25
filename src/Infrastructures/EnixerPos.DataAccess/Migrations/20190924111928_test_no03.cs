using Microsoft.EntityFrameworkCore.Migrations;

namespace EnixerPos.DataAccess.Migrations
{
    public partial class test_no03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EWalletAccountNo",
                table: "Store",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EWalletAccountNo",
                table: "Store");
        }
    }
}
