using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mockingbird.API.Migrations
{
    /// <inheritdoc />
    public partial class IndexUpdateForApiResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Method_ApiResourceId",
                table: "Method");

            migrationBuilder.DropIndex(
                name: "IX_Method_Name_MethodType",
                table: "Method");

            migrationBuilder.CreateIndex(
                name: "IX_Method_ApiResourceId_Name_MethodType",
                table: "Method",
                columns: new[] { "ApiResourceId", "Name", "MethodType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Method_ApiResourceId_Name_MethodType",
                table: "Method");

            migrationBuilder.CreateIndex(
                name: "IX_Method_ApiResourceId",
                table: "Method",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Method_Name_MethodType",
                table: "Method",
                columns: new[] { "Name", "MethodType" },
                unique: true);
        }
    }
}
