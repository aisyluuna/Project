using Microsoft.EntityFrameworkCore.Migrations;

namespace QueueForChildren.Migrations
{
    public partial class zags_child : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthCertifcateNumber",
                table: "ZagsChild",
                newName: "Serial");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "ZagsChild",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "ZagsChild");

            migrationBuilder.RenameColumn(
                name: "Serial",
                table: "ZagsChild",
                newName: "BirthCertifcateNumber");
        }
    }
}
