using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core.Migrations
{
    /// <inheritdoc />
    public partial class NewLoreMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Tier_tierId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "base_pricing",
                table: "Tier");

            migrationBuilder.RenameColumn(
                name: "tierId",
                table: "Subscriptions",
                newName: "TierId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_tierId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_TierId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TierId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Tier_TierId",
                table: "Subscriptions",
                column: "TierId",
                principalTable: "Tier",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Tier_TierId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "TierId",
                table: "Subscriptions",
                newName: "tierId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_TierId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_tierId");

            migrationBuilder.AddColumn<decimal>(
                name: "base_pricing",
                table: "Tier",
                type: "decimal(5,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "tierId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Tier_tierId",
                table: "Subscriptions",
                column: "tierId",
                principalTable: "Tier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
