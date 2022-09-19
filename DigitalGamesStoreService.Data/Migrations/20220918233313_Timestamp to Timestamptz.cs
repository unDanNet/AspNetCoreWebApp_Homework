using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGamesStoreService.Data.Migrations
{
    public partial class TimestamptoTimestamptz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastRequestedAt",
                table: "UserSession",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredAt",
                table: "UserSession",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserSession",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastRequestedAt",
                table: "UserSession",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredAt",
                table: "UserSession",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserSession",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");
        }
    }
}
