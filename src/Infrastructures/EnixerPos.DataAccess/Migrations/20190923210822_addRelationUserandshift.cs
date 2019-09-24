using Microsoft.EntityFrameworkCore.Migrations;

namespace EnixerPos.DataAccess.Migrations
{
    public partial class addRelationUserandshift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "Shift",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shift_UserEntityId",
                table: "Shift",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_User_UserEntityId",
                table: "Shift",
                column: "UserEntityId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_User_UserEntityId",
                table: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_Shift_UserEntityId",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Shift");
        }
    }
}
