using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core.Migrations
{
    /// <inheritdoc />
    public partial class CountryUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "Users",
                type: "varchar(17)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(17)");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Users",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Users",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Users",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Country_CountryId",
                table: "Users",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Country_CountryId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CountryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "Users",
                type: "varchar(17)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(17)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);
        }
    }
}
