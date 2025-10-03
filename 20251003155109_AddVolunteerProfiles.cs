using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftOfTheGivers.Migrations
{
    /// <inheritdoc />
    public partial class AddVolunteerProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_AspNetUsers_DonorId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VolunteerProfiles",
                newName: "VolunteerProfileId");

            migrationBuilder.RenameColumn(
                name: "ItemDescription",
                table: "Donations",
                newName: "DonationType");

            migrationBuilder.AlterColumn<string>(
                name: "Skills",
                table: "VolunteerProfiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Availability",
                table: "VolunteerProfiles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "VolunteerProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "VolunteerProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DonorId",
                table: "Donations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Donations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerProfiles_UserId",
                table: "VolunteerProfiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_AspNetUsers_DonorId",
                table: "Donations",
                column: "DonorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerProfiles_AspNetUsers_UserId",
                table: "VolunteerProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_AspNetUsers_DonorId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerProfiles_AspNetUsers_UserId",
                table: "VolunteerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_VolunteerProfiles_UserId",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "VolunteerProfileId",
                table: "VolunteerProfiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DonationType",
                table: "Donations",
                newName: "ItemDescription");

            migrationBuilder.AlterColumn<string>(
                name: "Skills",
                table: "VolunteerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Availability",
                table: "VolunteerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DonorId",
                table: "Donations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Donations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_AspNetUsers_DonorId",
                table: "Donations",
                column: "DonorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
