using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnixerPos.DataAccess.Migrations
{
    public partial class addFistDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "User",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "User",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Token",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "Token",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Store",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "Store",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Shift",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "Shift",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Receipt",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "Receipt",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "ManageCash",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "ManageCash",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "User",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "User",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Token",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "Token",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Store",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "Store",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Shift",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "Shift",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Receipt",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "Receipt",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                table: "ManageCash",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "ManageCash",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GetUtcDate()");
        }
    }
}
